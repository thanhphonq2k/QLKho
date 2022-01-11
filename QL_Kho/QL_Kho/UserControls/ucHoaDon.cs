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
    public partial class ucHoaDon : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucHoaDon _instance;
        public static ucHoaDon Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucHoaDon();
                return _instance;

            }
        }
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucHoaDon()
        {
            InitializeComponent();
            loadform();
            SetControl("Reset");
        }

        private void ucHoaDon_Load(object sender, EventArgs e)
        {

        }
        
        //public void loadcbo_MaHH()
        //{
        //    SqlConnection conn = new SqlConnection(ConnectionString);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("Select * from QL_HANGHOA", conn);
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
        //    r["MA_HANGHOA"] = "";
        //    r["TEN_HANGHOA"] = "---chọn hàng hóa----";
        //    tb.Rows.InsertAt(r, 0);
        //    cboMaHH.DataSource = tb;
        //    cboMaHH.DisplayMember = "TEN_HANGHOA";
        //    cboMaHH.ValueMember = "MA_HANGHOA";
        //    cboMaHH.Refresh();
        //}
        public void loadform()
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT * FROM QL_HOADON";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvHoaDon.DataSource = ds.Tables[0];
            }
            else
            {
                grvHoaDon.DataSource = null;

            }
            banghi();
            bindingdata();
        }
        public void banghi()
        {
            lblTongSo.Text = "Tổng số bản ghi: " + ds.Tables[0].Rows.Count.ToString() + " bản ghi";
        }
        public void bindingdata()
        {
            txtMaHoaDon.DataBindings.Clear();


            txtMaHoaDon.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_HOADON", false, DataSourceUpdateMode.Never));

        }
        private void SetControl(string state)
        {
            switch (state)
            {
                case "Reset":
                    btnTimKiem.Enabled = true;
                    btnKiem.Enabled = false;

                    txtMaHoaDon.Enabled = false;
                    break;
                case "Insert":
                    try
                    {
                        if (txtMaHoaDon.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập mã hóa đơn", "Thông báo");
                            txtMaHoaDon.Focus();
                        }
                        else if (txtMaHoaDon.Text.Trim() != "" )
                        {
                            string sqlInsert = "INSERT_HOADON";
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            cmd.Parameters.AddWithValue("@MA_HOADON", txtMaHoaDon.Text.Trim());
                            cmd.CommandType = CommandType.StoredProcedure;
                            var result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                loadform();
                                MessageBox.Show("Thêm thành công", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Thêm thất bại", "Thông báo");
                            }
                            //loadform();
                            clear();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo");
                    }
                    break;
            }
        }
        private void btnTimKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = true;

            txtMaHoaDon.Enabled = true;
            clear();
        }
        private void clear()
        {
            txtMaHoaDon.Text = "";
            
        }
        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string MaHoaDon = txtMaHoaDon.Text;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_HOADON " + "WHERE  MA_HOADON LIKE N'%" + MaHoaDon + "%' ", conn);

            //tim kiem xuat kho tu ngay nao den nay
            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            grvHoaDon.DataSource = tb;
            grvHoaDon.Refresh();
        }

        private void btnHuy1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadform();
            SetControl("Reset");
        }

        private void barThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            txtMaHoaDon.Enabled = true;

            clear();
            state = "Insert";
        }

        private void barGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (state == "Insert")
            {
                SetControl(state);
            }
        }
    }

}
