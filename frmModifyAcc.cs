using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UIP_Project
{
    public partial class frmModifyAcc : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmModifyAcc()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "") { MessageBox.Show("vui lòng nhập tên tài khoản."); return; }
            NguoiDung temp = BUS.NguoiDungBUS.GetNguoiDung(txtUsername.Text, db);
            if (temp != null) { MessageBox.Show("Người dùng đã tồn tại."); return; }
            if (txtPassword.Text != txtRePassword.Text) { MessageBox.Show("Mật khẩu không trùng khớp."); return; }
            try
            {
                BUS.NguoiDungBUS.ThemNguoiDung(txtUsername.Text, txtPassword.Text, db);
                MessageBox.Show("Thêm tài khoản thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            NguoiDung user = BUS.NguoiDungBUS.GetNguoiDung(txtUsername.Text, db);
            if (user == null) { MessageBox.Show("Tài khoản không tồn tại."); return; }
            if (user.MatKhau != BUS.NguoiDungBUS.GetMD5(txtPassword.Text)) { MessageBox.Show("Mật khẩu không trùng khớp."); return; }
            try
            {
                BUS.NguoiDungBUS.SuaNguoiDung(user, txtRePassword.Text, db);
                MessageBox.Show("Sửa mật khẩu thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            NguoiDung user = db.NguoiDungs.SingleOrDefault(p => p.TenDangNhap == txtUsername.Text);
            if (user == null) { MessageBox.Show("Tài khoản không tồn tại."); return; }
            if (txtPassword.Text != txtRePassword.Text || user.MatKhau != BUS.NguoiDungBUS.GetMD5(txtPassword.Text))
            {
                MessageBox.Show("Mật khẩu không trùng khớp."); return;
            }
            try
            {
                BUS.NguoiDungBUS.XoaNguoiDung(user, db);
                MessageBox.Show("Xóa tài khoản thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
