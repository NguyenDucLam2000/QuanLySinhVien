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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        static public bool IsLogin;
        static public string LoggedInUsername;
        static public int IsMod;

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            IsLogin = false;
            IsMod = 0;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsLogin) { MessageBox.Show("Bạn đă đăng nhập."); return; }
            frmLogin f = new frmLogin();
            f.MdiParent = this;
            f.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in this.MdiChildren) { childForm.Close(); }
            IsLogin = false;
            IsMod = 0;
            MessageBox.Show("Đã đăng xuất.");
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsLogin) { MessageBox.Show("Bạn chưa đăng nhập."); return; }
            frmQLKhoa f = new frmQLKhoa();
            f.MdiParent = this;
            f.Show();
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsLogin) { MessageBox.Show("Bạn chưa đăng nhập."); return; }
            frmQLSinhVien f = new frmQLSinhVien();
            f.MdiParent = this;
            f.Show();
        }

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIntro f = new frmIntro();
            f.Show();
        }

        private void họcPhầnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsLogin) { MessageBox.Show("Bạn chưa đăng nhập."); return; }
            frmQLMonHoc f = new frmQLMonHoc();
            f.MdiParent = this;
            f.Show();
        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsLogin) { MessageBox.Show("Bạn chưa đăng nhập."); return; }
            frmQLDiem f = new frmQLDiem();
            f.MdiParent = this;
            f.Show();
        }

        private void statusStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusStripToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                statusStrip1.Visible = false;
            }
            else
            {
                statusStrip1.Visible = true;
            }
        }

        private void toolStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStripToolStripMenuItem.CheckState == CheckState.Unchecked)
            {
                toolStrip1.Visible = false;
            }
            else
            {
                toolStrip1.Visible = true;
            }
        }

        private void diagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDiagram f = new frmDiagram();
            f.Show();
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsLogin) { MessageBox.Show("Bạn chưa đăng nhập."); return; }
            if (IsMod != 1) { MessageBox.Show("Bạn không có khả năng phân quyền."); return; }
            frmModerator f = new frmModerator();
            f.MdiParent = this;
            f.Show();
        }
    }
}
