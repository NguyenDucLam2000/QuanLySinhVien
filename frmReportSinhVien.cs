using Microsoft.Reporting.WinForms;
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
    public partial class frmReportSinhVien : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmReportSinhVien()
        {
            InitializeComponent();
        }

        private void frmReportSinhVien_Load(object sender, EventArgs e)
        {
            cbbKhoa.DataSource = db.Khoas.OrderBy(p => p.MaKhoa);
            cbbKhoa.DisplayMember = "TenKhoa";
            cbbKhoa.ValueMember = "MaKhoa";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string makhoa = cbbKhoa.SelectedValue.ToString();
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSetSinhVien", db.SinhViens.Where(p => p.MaKhoa == makhoa))
                );
            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("txtKhoa", cbbKhoa.Text.ToString());
            param[1] = new ReportParameter("txtName", txtPrinter.Text.ToString());
            param[2] = new ReportParameter("txtDate", DateTime.Now.ToString());
            reportViewer1.LocalReport.SetParameters(param);

            this.reportViewer1.RefreshReport();
        }
    }
}
