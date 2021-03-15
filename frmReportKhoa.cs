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
    public partial class frmReportKhoa : Form
    {
        dbQuanLyDataContext db = new dbQuanLyDataContext();
        public frmReportKhoa()
        {
            InitializeComponent();
        }

        private void frmReportKhoa_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSetKhoa", db.Khoas.Select(p => p))
                );
            //this.KhoaTableAdapter.Fill(this.QuanLySinhVienDataSet.Khoa);
            ReportParameter[] param = new ReportParameter[2];
            param[0] = new ReportParameter("txtName", txtPrinter.Text.ToString());
            param[1] = new ReportParameter("txtDate", DateTime.Now.ToString());
            reportViewer1.LocalReport.SetParameters(param);
            this.reportViewer1.RefreshReport();
        }
    }
}
