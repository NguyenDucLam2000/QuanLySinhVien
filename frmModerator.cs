using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIP_Project
{
    public partial class frmModerator : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmModerator()
        {
            InitializeComponent();
        }

        private void frmModerator_Load(object sender, EventArgs e)
        {
            fillUser();
        }

        private void fillUser()
        {
            dgvUser.DataSource = db.NguoiDungs.OrderBy(p => p.TenDangNhap)
                .Select(p => new {
                    p.TenDangNhap,
                    p.Moderator
                });
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillUser();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var likequery = string.Format("%{0}%", txtUsername.Text);
            var ds = db.NguoiDungs.Where(p => SqlMethods.Like(p.TenDangNhap, likequery));
            dgvUser.DataSource = ds.Select(p => new
            {
                p.TenDangNhap,
                p.Moderator,
            });
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow = e.RowIndex;
            if (numrow >= 0)
            {
                txtUsername.Text = dgvUser.Rows[numrow].Cells[0].Value.ToString();
                if (dgvUser.Rows[numrow].Cells[1].Value.ToString() == "1") { rbMod.Checked = true; }
                else { rbMod.Checked = false; }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            NguoiDung temp = BUS.NguoiDungBUS.GetNguoiDung(txtUsername.Text, db);
            if (temp == null) { MessageBox.Show("Lỗi."); return; }
            else
            {
                temp.MatKhau = BUS.NguoiDungBUS.GetMD5("123456");
                db.SubmitChanges();
                MessageBox.Show("Đã khôi phục mật khẩu.");
            }
        }

        private void btnGrantMod_Click(object sender, EventArgs e)
        {
            NguoiDung temp = BUS.NguoiDungBUS.GetNguoiDung(txtUsername.Text, db);
            if (temp == null) { MessageBox.Show("Lỗi."); return; }
            else
            {
                if (frmMain.LoggedInUsername == temp.TenDangNhap || temp.Moderator == 1)
                {
                    return;
                }
                else
                {
                    temp.Moderator = 1;
                    db.SubmitChanges();
                    fillUser();
                    MessageBox.Show("Giao quyền thành công.");
                }
            }
        }

        private void btnRemoveMod_Click(object sender, EventArgs e)
        {
            NguoiDung temp = BUS.NguoiDungBUS.GetNguoiDung(txtUsername.Text, db);
            if (temp == null) { MessageBox.Show("Lỗi."); return; }
            else
            {
                if (frmMain.LoggedInUsername == temp.TenDangNhap)
                {
                    MessageBox.Show("Không thể hủy quyền của bản thân.");
                    return;
                }
                else
                {
                    temp.Moderator = 0;
                    db.SubmitChanges();
                    fillUser();
                    MessageBox.Show("Hủy quyền thành công.");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            NguoiDung user = db.NguoiDungs.SingleOrDefault(p => p.TenDangNhap == txtUsername.Text);
            if (user == null) { MessageBox.Show("Tài khoản không tồn tại."); return; }
            if (user.Moderator == 1) { MessageBox.Show("Không thể xóa Moderator!"); return; }
            try
            {
                BUS.NguoiDungBUS.XoaNguoiDung(user, db);
                fillUser();
                MessageBox.Show("Xóa tài khoản thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
