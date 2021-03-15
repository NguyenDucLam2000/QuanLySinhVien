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
    public partial class frmQLDiem : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmQLDiem()
        {
            InitializeComponent();
        }

        private void frmQLDiem_Load(object sender, EventArgs e)
        {
            fillKetQua();
            if (frmMain.IsMod != 1)
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnRemove.Enabled = false;
            }
        }

        private void fillKetQua()
        {
            var ds = from kq in db.KetQuas
                     from mh in db.MonHocs
                     from sv in db.SinhViens
                     where kq.MaMH == mh.MaMH && kq.MaSV == sv.MaSV
                     select new {
                                  sv.MaKhoa,
                                  kq.MaMH,
                                  sv.TenSV,
                                  mh.TenMH,
                                  kq.MaSV,
                                  kq.Diem
                     } ;
            dgvKetQua.DataSource = ds;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text == "" || txtMaMH.Text == "") { MessageBox.Show("Vui lòng nhập đầy đủ thông tin để thêm mới."); return; }
            try
            {
                KetQua temp = db.KetQuas.SingleOrDefault(p => p.MaSV == txtMaSV.Text && p.MaMH == txtMaMH.Text);
                if (temp != null) { MessageBox.Show("Kết quả đã tồn tại."); return; }
                db.ExecuteCommand("INSERT INTO KetQua VALUES ({0}, {1}, {2})", txtMaSV.Text, txtMaMH.Text, (float)nudDiem.Value);
                fillKetQua();
                MessageBox.Show("Thêm thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            KetQua kq = db.KetQuas.SingleOrDefault(p => p.MaSV == txtMaSV.Text && p.MaMH == txtMaMH.Text);
            if (kq == null) {MessageBox.Show("Lỗi."); return;}
            try
            {
                db.ExecuteCommand("DELETE FROM KetQua WHERE MaSV = {0} AND MaMH = {1}", txtMaSV.Text, txtMaMH.Text);
                fillKetQua();
                MessageBox.Show("Xóa thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            KetQua kq = db.KetQuas.SingleOrDefault(p => p.MaSV == txtMaSV.Text && p.MaMH == txtMaMH.Text);
            if (kq == null) { MessageBox.Show("Điểm không tồn tại."); return; }
            try
            {
                db.ExecuteCommand("UPDATE KetQua SET Diem = {0} WHERE MaSV = {1} AND MaMH = {2}", (float)nudDiem.Value, txtMaSV.Text, txtMaMH.Text);
                fillKetQua();
                MessageBox.Show("Sửa thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillKetQua();
        }

        private void btnSearchMaSV_Click(object sender, EventArgs e)
        {
            var ds = from kq in db.KetQuas
                     from mh in db.MonHocs
                     from sv in db.SinhViens
                     where kq.MaSV == txtMaSV.Text && kq.MaMH == mh.MaMH && kq.MaSV == sv.MaSV
                     select new
                     {
                         sv.MaKhoa,
                         kq.MaMH,
                         sv.TenSV,
                         mh.TenMH,
                         kq.MaSV,
                         kq.Diem
                     } ;
            dgvKetQua.DataSource = ds;
                     
        }

        private void btnSearchMaMH_Click(object sender, EventArgs e)
        {
            var ds = from kq in db.KetQuas
                     from mh in db.MonHocs
                     from sv in db.SinhViens
                     where kq.MaMH == txtMaMH.Text && kq.MaMH == mh.MaMH && kq.MaSV == sv.MaSV
                     select new
                     {
                         sv.MaKhoa,
                         kq.MaMH,
                         sv.TenSV,
                         mh.TenMH,
                         kq.MaSV,
                         kq.Diem
                     };
            dgvKetQua.DataSource = ds;
        }

        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numrow = e.RowIndex;
            if (numrow >= 0)
            {
                txtMaSV.Text = dgvKetQua.Rows[numrow].Cells[4].Value.ToString();
                txtMaMH.Text = dgvKetQua.Rows[numrow].Cells[1].Value.ToString();
                txtTenSV.Text = dgvKetQua.Rows[numrow].Cells[2].Value.ToString();
                txtTenMH.Text = dgvKetQua.Rows[numrow].Cells[3].Value.ToString();
                string temp = dgvKetQua.Rows[numrow].Cells[5].Value.ToString();
                nudDiem.Value = decimal.Parse(temp);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            fillKetQua();
        }
    }
}
