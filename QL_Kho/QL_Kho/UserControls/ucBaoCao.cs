using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;

namespace QL_Kho.UserControls
{
    public partial class ucBaoCao : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucBaoCao _instance;
        public static ucBaoCao Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucBaoCao();
                return _instance;

            }
        }
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucBaoCao()
        {
            InitializeComponent();
            
        }
        //public void loadcbo_id()
        //{
        //    SqlConnection conn = new SqlConnection(ConnectionString);
        //    conn.Open();
        //    //buoc 2: tao doi tuong Command
        //    SqlCommand cmd = new SqlCommand("Select * from HOA_DON", conn);
        //    cmd.ExecuteNonQuery();
        //    //buoc 3: do du lieu vào DataAdater từ cmd
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //buoc 4: do du lieu tu dataAdap
        //    DataTable tb = new DataTable();
        //    da.Fill(tb);
        //    cmd.Dispose();
        //    conn.Close();

        //    //buoc 5: do du lieu vao cbo
        //    //them dong lop
        //    DataRow r;
        //    r = tb.NewRow();
        //    //r["MA_LOP"] = "";
        //    //r["TEN_LOP"] = "---chọn lớp----";
        //    tb.Rows.InsertAt(r, 0);
        //    cboMaHD.DataSource = tb;
        //    cboMaHD.DisplayMember = "MA_HOADON";
        //    cboMaHD.ValueMember = "MA_HOADON";

        //}
        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            BaoCao rpt = new BaoCao();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("HOA_DON_1", conn);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            rpt.SetDataSource(tb);
            crystalReportViewer1.ReportSource = rpt;
            cmd.Dispose();
            conn.Close();
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
           
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnIn_Click_1(object sender, EventArgs e)
        {
            BaoCao rpt = new BaoCao();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from HOA_DON WHERE  MA_HOADON='" + txtHD.Text + "'", conn);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            rpt.SetDataSource(tb);
            crystalReportViewer1.ReportSource = rpt;
            cmd.Dispose();
            conn.Close();
        }
    }
}
