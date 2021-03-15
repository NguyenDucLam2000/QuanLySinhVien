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
    public partial class frmQLKhoa : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmQLKhoa()
        {
            InitializeComponent();
        }

        private void frmQLKhoa_Load(object sender, EventArgs e)
        {
            fillKhoa();
            if (frmMain.IsMod != 1)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void fillKhoa()
        {
            dgvKhoa.DataSource = BUS.KhoaBUS.GetDSKhoa(db);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaKhoa.Text == "") { MessageBox.Show("Cần có tối thiểu mã khoa để thêm khoa mới."); return; }
            Khoa temp = BUS.KhoaBUS.GetKhoabyMaKhoa(txtMaKhoa.Text, db);
            if (temp != null) { MessageBox.Show("Mã khoa đã tồn tại."); return; }

            try
            {
                BUS.KhoaBUS.ThemKhoa(txtMaKhoa.Text, txtTenKhoa.Text, db);
                fillKhoa();
                MessageBox.Show("Thêm thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Khoa kh = BUS.KhoaBUS.GetKhoabyMaKhoa(txtMaKhoa.Text, db);
            if (kh == null) { MessageBox.Show("Lỗi."); return; }
            try
            {
                BUS.KhoaBUS.XoaKhoa(kh, db);
                fillKhoa();
                MessageBox.Show("Xóa thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var likequery = string.Format("%{0}%", txtMaKhoa.Text);
            var ds = db.Khoas.Where(p => SqlMethods.Like(p.MaKhoa, likequery));
            dgvKhoa.DataSource = ds.Select(p => new
            {
                p.MaKhoa,
                p.TenKhoa
            });

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Khoa kh = BUS.KhoaBUS.GetKhoabyMaKhoa(txtMaKhoa.Text, db);
            if (kh == null) { MessageBox.Show("Khoa không tồn tại."); return; }

            try
            {
                BUS.KhoaBUS.SuaKhoa(kh, txtTenKhoa.Text, db);
                fillKhoa();
                MessageBox.Show("Sửa thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //data binding
        private void dgvKhoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow = e.RowIndex;
            if (numrow >= 0)
            {
                txtMaKhoa.Text = dgvKhoa.Rows[numrow].Cells[0].Value.ToString();
                txtTenKhoa.Text = dgvKhoa.Rows[numrow].Cells[1].Value.ToString();
            }
        }
        //reporting
        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportKhoa f = new frmReportKhoa();
            f.Show();
        }
        //reloading
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillKhoa();
        }
    }
}
