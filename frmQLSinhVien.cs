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
    public partial class frmQLSinhVien : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmQLSinhVien()
        {
            InitializeComponent();
        }

        private void frmQLSinhVien_Load(object sender, EventArgs e)
        {
            cbbKhoa.DataSource = db.Khoas;
            cbbKhoa.DisplayMember = "TenKhoa";
            cbbKhoa.ValueMember = "MaKhoa";
            fillSinhVienKhoa();
            if (frmMain.IsMod != 1)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void fillSinhVienKhoa()
        {
            dgvSinhVien.DataSource = BUS.SinhVienBUS.GetDSSinhVienbyMaKhoa(cbbKhoa.SelectedValue.ToString(), db)
            .Select(p => new { p.MaSV,
                               p.TenSV,
                               p.MaKhoa,
                               p.NgaySinh,
                               p.DiaChi,
                               DTB = (from d in db.KetQuas
                                      where d.MaSV == p.MaSV
                                      select d.Diem
                                      ).Average()
                              });
            
        }

        private void cbbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillSinhVienKhoa();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "") { MessageBox.Show("Cần có tối thiểu mã số sinh viên để thêm sinh viên mới."); return; }
            SinhVien temp = BUS.SinhVienBUS.GetSinhVienbyMaSV(txtMaSV.Text, db);
            if (temp != null) { MessageBox.Show("Mã số sinh viên đã tồn tại."); return; }
            try
            {
                BUS.SinhVienBUS.ThemSinhVien(txtMaSV.Text, txtTenSV.Text, dtpNgaySinh.Value.Date, txtDiaChi.Text, cbbKhoa.SelectedValue.ToString(), db);
                fillSinhVienKhoa();
                MessageBox.Show("Thêm thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SinhVien sv = BUS.SinhVienBUS.GetSinhVienbyMaSV(txtMaSV.Text, db);
            if (sv == null) { MessageBox.Show("Lỗi."); return; }
            try
            {
                BUS.SinhVienBUS.XoaSinhVien(sv, db);
                fillSinhVienKhoa();
                MessageBox.Show("Xóa thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var likequery = string.Format("%{0}%", txtMaSV.Text);
            var ds = db.SinhViens.Where(p => SqlMethods.Like(p.MaSV, likequery));
            dgvSinhVien.DataSource = ds.Select(p => new
            {
                p.MaSV,
                p.TenSV,
                p.MaKhoa,
                p.NgaySinh,
                p.DiaChi,
                DTB = (from d in db.KetQuas
                       where d.MaSV == p.MaSV
                       select d.Diem
                                      ).Average()
            });
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SinhVien sv = BUS.SinhVienBUS.GetSinhVienbyMaSV(txtMaSV.Text, db);
            if (sv == null)
            { MessageBox.Show("Mã số sinh viên không tồn tại."); return; }
            try
            {
                BUS.SinhVienBUS.SuaSinhVien(sv, txtTenSV.Text, txtDiaChi.Text, dtpNgaySinh.Value.Date, db);
                fillSinhVienKhoa();
                MessageBox.Show("Sửa thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //data binding
        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow = e.RowIndex;
            if (numrow >= 0)
            {
                txtMaSV.Text = dgvSinhVien.Rows[numrow].Cells[0].Value.ToString();
                txtTenSV.Text = dgvSinhVien.Rows[numrow].Cells[1].Value.ToString();
                if (dgvSinhVien.Rows[numrow].Cells[5].Value == null) { txtDTB.Text = "0"; }
                else { txtDTB.Text = dgvSinhVien.Rows[numrow].Cells[5].Value.ToString(); }
                txtDiaChi.Text = dgvSinhVien.Rows[numrow].Cells[4].Value.ToString();
                dtpNgaySinh.Value = (DateTime)dgvSinhVien.Rows[numrow].Cells[3].Value;
            }
        }
        //reloading
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillSinhVienKhoa();
        }
        //reporting
        private void btnReport_Click(object sender, EventArgs e)
        {
            frmReportSinhVien f = new frmReportSinhVien();
            f.Show();
        }
    }
}
