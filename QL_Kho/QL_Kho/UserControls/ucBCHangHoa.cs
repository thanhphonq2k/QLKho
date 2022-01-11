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
    public partial class ucBCHangHoa : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucBCHangHoa _instance;
        public static ucBCHangHoa Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucBCHangHoa();
                return _instance;

            }
        }
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucBCHangHoa()
        {
            InitializeComponent();
            
        }

        private void ucBCHangHoa_Load(object sender, EventArgs e)
        {
            CRHangHoa rpt = new CRHangHoa();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_HANGHOA", conn);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            rpt.SetDataSource(tb);
            CRVHH.ReportSource = rpt;
            cmd.Dispose();
            conn.Close();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            CRHangHoa rpt = new CRHangHoa();
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_HANGHOA WHERE  MA_HANGHOA='"+txtMaHH.Text+"'", conn);
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            rpt.SetDataSource(tb);
            CRVHH.ReportSource = rpt;
            cmd.Dispose();
            conn.Close();
        }
    }
}
