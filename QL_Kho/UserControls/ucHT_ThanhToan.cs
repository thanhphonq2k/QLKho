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
    public partial class ucHT_ThanhToan : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucHT_ThanhToan _instance;
        public static ucHT_ThanhToan Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucHT_ThanhToan();
                return _instance;

            }
        }

        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucHT_ThanhToan()
        {
            InitializeComponent();
            loadform();
            SetControl("Reset");
        }

        #region public function
        private void SetControl(string state)
        {
            switch (state)
            {
                case "Reset":
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnGhi.Enabled = false;
                    btnHuy.Enabled = false;
                    btnTimKiem.Enabled = true;
                    btnKiem.Enabled = false;

                    
                    txtMaThanhToan.Enabled = false;
                    txtHinhThucTT.Enabled = false;
                    txtTrangThai.Enabled = false;
                    rtbMoTa.Enabled = false;
                    rtbGhiChu.Enabled = false;
                    txtTrangThai.Enabled = false;
                    
                    break;
                case "Insert":
                    try
                    {
                        if (txtHinhThucTT.Text.Trim() == "")
                        {
                            MessageBox.Show("Nhập vào tên hình thức thanh toán", "Thông báo");
                            txtHinhThucTT.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else
                        {
                            string sqlInsert_tk = "INSERT_HT_THANHTOAN";
                            SqlCommand cmd = new SqlCommand(sqlInsert_tk, conn);
                            cmd.Parameters.AddWithValue("@MA_THANHTOAN", txtMaThanhToan.Text.Trim());
                            cmd.Parameters.AddWithValue("@TEN_THANHTOAN", txtHinhThucTT.Text.Trim());
                            cmd.Parameters.AddWithValue("@MO_TA", rtbMoTa.Text.Trim());
                            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                            cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                            cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
                            cmd.CommandType = CommandType.StoredProcedure;

                            var result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                loadform();
                                MessageBox.Show("Thêm thành công!", "Thông báo");
                                clear();
                                txtMaThanhToan.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm!", "Thông báo");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thong bao");
                        
                    }
                    break;
                case "Update":
                    try
                    {
                        if (txtHinhThucTT.Text.Trim() == "")
                        {
                            MessageBox.Show("Nhập vào tên hình thức thanh toán", "Thông báo");
                            txtHinhThucTT.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else
                        {
                            string sqlInsert_tk = "UPDATE_HT_THANHTOAN";
                            SqlCommand cmd = new SqlCommand(sqlInsert_tk, conn);
                            cmd.Parameters.AddWithValue("@MA_THANHTOAN", txtMaThanhToan.Text.Trim());
                            cmd.Parameters.AddWithValue("@TEN_THANHTOAN", txtHinhThucTT.Text.Trim());
                            cmd.Parameters.AddWithValue("@MO_TA", rtbMoTa.Text.Trim());
                            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                            cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                            cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
                            cmd.CommandType = CommandType.StoredProcedure;
                            var result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                loadform();
                                MessageBox.Show("Sửa thành công!", "Thông báo");
                                clear();
                                txtMaThanhToan.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Không thể sửa!", "Thông báo");
                            }
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    break;
                case "Delete":
                    try
                    {
                        if (txtHinhThucTT.Text.Trim() == "")
                        {
                            MessageBox.Show("Nhập vào tên hình thức thanh toán", "Thông báo");
                            txtHinhThucTT.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập trạng thái", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        if (XtraMessageBox.Show("Bạn có chắc chắc muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {


                            if (txtMaThanhToan.Text.Trim() != "")
                            {
                                string sqlDelete = "DELETE_HT_THANHTOAN";
                                SqlCommand cmd = new SqlCommand(sqlDelete, conn);
                                cmd.Parameters.AddWithValue("@MA_THANHTOAN", txtMaThanhToan.Text.Trim());
                                cmd.Parameters.AddWithValue("@TEN_THANHTOAN", txtHinhThucTT.Text.Trim());
                                cmd.Parameters.AddWithValue("@MO_TA", rtbMoTa.Text.Trim());
                                cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                                cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                                cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                                cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                                cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                                cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);

                                cmd.CommandType = CommandType.StoredProcedure;

                                var result = cmd.ExecuteNonQuery();
                                MessageBox.Show("Xóa thành công", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Xóa thất bại", "Thông báo");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Thông báo");
                    }
                    break;
                case "Search":
                    break;
            }
        }
        private void clear()
        {
            txtMaThanhToan.Text= "";
            txtHinhThucTT.Text = "";
            rtbMoTa.Text = "";
            rtbGhiChu.Text = "";
            txtTrangThai.Text = "";
            
            
        }
        public void loadform()
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT * FROM HT_THANHTOAN WHERE TRANG_THAI='1'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvThanhToan.DataSource = ds.Tables[0];
            }
            else
            {
                grvThanhToan.DataSource = null;

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
            txtMaThanhToan.DataBindings.Clear();
            txtHinhThucTT.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();
            rtbMoTa.DataBindings.Clear();
            rtbGhiChu.DataBindings.Clear();           


            txtMaThanhToan.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_THANHTOAN", false, DataSourceUpdateMode.Never));
            txtHinhThucTT.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_THANHTOAN", false, DataSourceUpdateMode.Never));
            rtbMoTa.DataBindings.Add(new Binding("Text", ds.Tables[0], "MO_TA", false, DataSourceUpdateMode.Never));
            rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));
           
        }
        #endregion

        #region Even
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            txtMaThanhToan.Enabled = true;
            txtHinhThucTT.Enabled = true;
            txtTrangThai.Enabled = true;
            rtbMoTa.Enabled = true;
            rtbGhiChu.Enabled = true;
            

            clear();
            //loadcbo();
            state = "Insert";
        }
        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            txtMaThanhToan.Enabled = false;
            txtHinhThucTT.Enabled = true;
            txtTrangThai.Enabled = true;
            rtbMoTa.Enabled = true;
            rtbGhiChu.Enabled = true;
            

            //loadcbo();
            state = "Update";
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = true;
            btnKiem.Enabled = false;

            txtMaThanhToan.Enabled = false;
            txtHinhThucTT.Enabled = true;
            txtTrangThai.Enabled = true;
            rtbMoTa.Enabled = true;
            rtbGhiChu.Enabled = true;
            

            SetControl("Delete");
            SetControl("Reset");
            clear();

            loadform();
            
        }
        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (state == "Insert")
            {
                SetControl(state);
            }


            else if (state == "Update")
            {
                SetControl(state);

            }
        }
        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            clear();
            loadform();
            SetControl("Reset");
            state = "";
        }
        private void btnTimKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = false;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = true;

            txtMaThanhToan.Enabled = true;
            txtHinhThucTT.Enabled = true;
            txtTrangThai.Enabled = true;
            rtbMoTa.Enabled = false;
            rtbGhiChu.Enabled = false;
            

            clear();
        }
        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string MaThanhToan = txtMaThanhToan.Text;
            string HinhThucTT = txtHinhThucTT.Text;
            string TrangThai = txtTrangThai.Text;
            
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from HT_THANHTOAN " + "WHERE MA_THANHTOAN LIKE N'%" + MaThanhToan + "%' AND " +
                                                                        "TEN_THANHTOAN LIKE N'%" + HinhThucTT+ "%' AND " +
                                                                        "TRANG_THAI LIKE N'%" + TrangThai + "%'", conn);

            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            //buoc 5: do du lieu tu tb vao dataGridView
            grvThanhToan.DataSource = tb;
            grvThanhToan.Refresh();
        }
        #endregion
    }
}