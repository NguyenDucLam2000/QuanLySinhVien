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
    public partial class frmQLMonHoc : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmQLMonHoc()
        {
            InitializeComponent();
        }

        private void frmQLMonHoc_Load(object sender, EventArgs e)
        {
            cbbKhoa.DataSource = db.Khoas;
            cbbKhoa.DisplayMember = "TenKhoa";
            cbbKhoa.ValueMember = "MaKhoa";
            fillHocPhanKhoa();
            if (frmMain.IsMod != 1)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void fillHocPhanKhoa()
        {
            dgvHocPhan.DataSource = BUS.MonHocBUS.GetDSMonHocbyMaKhoa(cbbKhoa.SelectedValue.ToString(), db)
                .Select(p => new
            {
                p.MaMH, p.TenMH, p.MaKhoa, p.SoTinChi, p.GV
            });
        }

        private void cbbKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillHocPhanKhoa();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaMonHoc.Text == "") { MessageBox.Show("Cần có tối thiểu mã học phần để thêm học phần mới."); return; }
            MonHoc temp = BUS.MonHocBUS.GetMonHocbyMaMH(txtMaMonHoc.Text, db);
            if (temp != null) { MessageBox.Show("Mã học phần đã tồn tại."); return; }
            try
            {
                BUS.MonHocBUS.ThemMonHoc(txtMaMonHoc.Text, txtTenMonHoc.Text, cbbKhoa.SelectedValue.ToString(), (int)nudSoTinChi.Value, txtTenGV.Text, db);
                fillHocPhanKhoa();
                MessageBox.Show("Thêm thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MonHoc mh = BUS.MonHocBUS.GetMonHocbyMaMH(txtMaMonHoc.Text, db);
            if (mh == null) { MessageBox.Show("Lỗi."); return; }
            try
            {
                BUS.MonHocBUS.XoaMonHoc(mh, db);
                fillHocPhanKhoa();
                MessageBox.Show("Xóa thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MonHoc mh = BUS.MonHocBUS.GetMonHocbyMaMH(txtMaMonHoc.Text, db);
            if (mh == null) { MessageBox.Show("Mã học phần không tồn tại."); return; }
            try
            {
                BUS.MonHocBUS.SuaMonHoc(mh, txtTenMonHoc.Text, txtTenGV.Text, (int)nudSoTinChi.Value, db);
                fillHocPhanKhoa();
                MessageBox.Show("Sửa thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var likequery = string.Format("%{0}%", txtMaMonHoc.Text);
            var ds = db.MonHocs.Where(p => SqlMethods.Like(p.MaMH, likequery));
            dgvHocPhan.DataSource = ds.Select(p => new
            {
                p.MaMH,
                p.TenMH,
                p.GV,
                p.SoTinChi,
                p.MaKhoa
            });
        }
        //data binding
        private void dgvHocPhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow = e.RowIndex;
            if (numrow >= 0)
            {
                txtMaMonHoc.Text = dgvHocPhan.Rows[numrow].Cells[0].Value.ToString();
                txtTenMonHoc.Text = dgvHocPhan.Rows[numrow].Cells[1].Value.ToString();
                txtTenGV.Text = dgvHocPhan.Rows[numrow].Cells[4].Value.ToString();
                nudSoTinChi.Value = (int)dgvHocPhan.Rows[numrow].Cells[3].Value;
            }
        }
        //reporting
        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportMonHoc f = new frmReportMonHoc();
            f.Show();
        }
        //reloading
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillHocPhanKhoa();
        }
    }
}
