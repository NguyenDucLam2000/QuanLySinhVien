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
    public partial class frmLogin : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Thoát form?", "Thoát", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes) this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            NguoiDung temp = BUS.NguoiDungBUS.GetNguoiDung(txtUsername.Text, db);
            if (temp == null) { MessageBox.Show("Tên đăng nhập không tồn tại."); return; }
            else if (temp.MatKhau != BUS.NguoiDungBUS.GetMD5(txtPassword.Text)) { MessageBox.Show("Sai mật khẩu."); return; }
            
            frmMain.IsLogin = true;
            if (temp.Moderator == 1) { frmMain.IsMod = 1; }
            frmMain.LoggedInUsername = txtUsername.Text;
            MessageBox.Show("Đăng nhập thành công.");
            this.Close();
            
        }

        private void btnModifyAcc_Click(object sender, EventArgs e)
        {
            frmModifyAcc f = new frmModifyAcc();
            f.ShowDialog();
        }
    }
}
