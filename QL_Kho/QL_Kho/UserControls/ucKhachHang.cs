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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;

namespace QL_Kho.UserControls
{
    public partial class ucKhachHang : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucKhachHang _instance;
        public static ucKhachHang Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucKhachHang();
                return _instance;

            }
        }
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucKhachHang()
        {
            InitializeComponent();
            loadform();
            SetControl("Reset");
        }
        #region Public functions
        private void SetControl(string state)
        {
            switch (state)
            {
                case "Reset":
                    txtID.Enabled = false;
                    txtHoTen.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtMaKhachHang.Enabled = false;
                    txtNguoiNhan.Enabled = false;
                    txtEmail.Enabled = false;
                    txtSDT.Enabled = false;
                    rtbGhiChu.Enabled = false;
                    dtpThoiHan.Enabled = false;
                    txtTrangThai.Enabled = false;
                    

                    btnThem1.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnGhi.Enabled = false;
                    btnHuy.Enabled = false;
                    btnTimKiem.Enabled = true;
                    btnKiem.Enabled = false;

                    break;
                case "Insert":
                    
                    try
                    {
                        if (txtHoTen.Text.Trim() == "")
                        {
                            MessageBox.Show("Nhập vào tên khách hàng", "Thông báo");
                            txtHoTen.Focus();
                        }
                        else if (txtNguoiNhan.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa cung cấp thời hạn", "Thông báo");
                            txtNguoiNhan.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if (dtpThoiHan.Value == DateTime.Now)
                        {
                            MessageBox.Show("Bạn chưa cung cấp thời hạn", "Thông báo");
                            dtpThoiHan.Focus();
                        }                    
                        else {
                            string sqlInsert_tk = "INSERT_KHACHHANG";
                            SqlCommand cmd = new SqlCommand(sqlInsert_tk, conn);
                            cmd.Parameters.AddWithValue("@TEN_KHACHHANG", txtHoTen.Text.Trim());
                            cmd.Parameters.AddWithValue("@MA_KHACHHANG", txtMaKhachHang.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGUOI_NHAN", txtNguoiNhan.Text.Trim());
                            cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                            cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
                            cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@THOI_HAN", dtpThoiHan.Value);
                            cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                            cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
                            //cmd.Parameters.Add("@ID_OUT", SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.ExecuteNonQuery();
                           
                            //cmd.Parameters.Clear();
                            //cmd.CommandType = CommandType.StoredProcedure;

                            var result = cmd.ExecuteNonQuery();
                            if (result > 0)
                            {
                                loadform();
                                MessageBox.Show("Thêm thành công!", "Thông báo");
                                clear();
                                txtHoTen.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm!", "Thông báo");
                            }
                        }
                    }
                    catch (Exception ex)
                     {

                        MessageBox.Show(ex.Message, "Thông báo");
                    }
                    
                    break;
                case "Update":
                    try
                    {
                        if (txtHoTen.Text.Trim() == "")
                        {
                            MessageBox.Show("Nhập vào tên khách hàng", "Thông báo");
                            txtHoTen.Focus();
                        }
                        else if (dtpThoiHan.Value == DateTime.Now)
                        {
                            MessageBox.Show("Bạn chưa cung cấp thời hạn", "Thông báo");
                            dtpThoiHan.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if (txtMaKhachHang.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập mã khách hàng", "THông báo");
                            txtMaKhachHang.Focus();
                        }
                        else
                        {
                            string sqlInsert_tk = "UPDATE_KHACHHANG";
                            SqlCommand cmd = new SqlCommand(sqlInsert_tk, conn);
                            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGUOI_NHAN", txtNguoiNhan.Text.Trim());
                            cmd.Parameters.AddWithValue("@MA_KHACHHANG", txtMaKhachHang.Text.Trim());
                            cmd.Parameters.AddWithValue("@TEN_KHACHHANG", txtHoTen.Text.Trim());
                            cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                            cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
                            cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@THOI_HAN", dtpThoiHan.Value);
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
                                MessageBox.Show("SỬA thành công!", "Thông báo");
                                
                            }
                            else
                            {
                                MessageBox.Show("Không thể sửa!", "Thông báo");
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Thông báo");
                    }
                    break;
                case "Delete":
                    try
                    {
                        if (txtID.Text.Trim() == "")
                        {
                            MessageBox.Show("Nhập vào ID ", "Thông báo");
                            txtID.Focus();
                        }
                        else
                        {
                            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                string sqlDelete = "DELETE_KHACHHANG";
                                SqlCommand cmd = new SqlCommand(sqlDelete, conn);
                                cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                                cmd.Parameters.AddWithValue("@TEN_KHACHHANG", txtHoTen.Text.Trim());
                                cmd.Parameters.AddWithValue("@MA_KHACHHANG", txtMaKhachHang.Text.Trim());
                                cmd.Parameters.AddWithValue("@NGUOI_NHAN", txtNguoiNhan.Text.Trim());
                                cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                                cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
                                cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
                                cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                                cmd.Parameters.AddWithValue("@THOI_HAN ", dtpThoiHan.Value);
                                cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                                cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                                cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                                cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                                cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
                                cmd.CommandType = CommandType.StoredProcedure;
                                var result = cmd.ExecuteNonQuery();
                                if (result > 0)
                                {
                                    clear();
                                    loadform();
                                    SetControl("Reset");
                                    MessageBox.Show("Xóa thành công.", "Thông báo");
                                }
                            }
                            else
                            {
                                clear();
                                SetControl("Reset");
                                bindingdata();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Thông báo");
                    }
                    break;
                
                default:
                    break;
            }
        }
        public void loadform()
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT * FROM QL_KHACH_HANG WHERE TRANG_THAI=1";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvKhachHang.DataSource = ds.Tables[0];
            }
            else
            {
                grvKhachHang.DataSource = null;

            }
            banghi();
            bindingdata();
        }

        //load du lieu len bang
        private void bindingdata()
        {
            txtID.DataBindings.Clear();
            txtHoTen.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtMaKhachHang.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtNguoiNhan.DataBindings.Clear();
            rtbGhiChu.DataBindings.Clear();
            dtpThoiHan.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();

            txtID.DataBindings.Add(new Binding("Text", ds.Tables[0], "ID", false, DataSourceUpdateMode.Never));
            txtNguoiNhan.DataBindings.Add(new Binding("Text", ds.Tables[0], "NGUOI_NHAN", false, DataSourceUpdateMode.Never));
            txtMaKhachHang.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_KHACHHANG", false, DataSourceUpdateMode.Never));
            txtEmail.DataBindings.Add(new Binding("Text", ds.Tables[0], "EMAIL", false, DataSourceUpdateMode.Never));
            txtHoTen.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_KHACHHANG", false, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", ds.Tables[0], "DIA_CHI", false, DataSourceUpdateMode.Never));
            txtSDT.DataBindings.Add(new Binding("Text", ds.Tables[0], "SO_DIENTHOAI", false, DataSourceUpdateMode.Never));
            rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
            dtpThoiHan.DataBindings.Add(new Binding("Text", ds.Tables[0], "THOI_HAN", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));
        }
        public void banghi()
        {
            lblTongSo.Text = "Tổng số: " + ds.Tables[0].Rows.Count.ToString() + " bản ghi";
        }       
        public void clear()
        {
            txtID.Text = "";
            txtHoTen.Text = "";
            txtMaKhachHang.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            rtbGhiChu.Text = "";
            txtNguoiNhan.Text = "";
            dtpThoiHan.Value = DateTime.Now;
            txtTrangThai.Text = "";
        }
        #endregion

        #region Even
        private void btnThem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtID.Enabled = false;
            txtHoTen.Enabled = true;
            txtNguoiNhan.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            txtSDT.Enabled = true;
            rtbGhiChu.Enabled = true;
            dtpThoiHan.Enabled = true;
            txtTrangThai.Enabled = true;
            txtMaKhachHang.Enabled = true;

            btnThem1.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            clear();
            state = "Insert";
        }
        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtID.Enabled = false;
            txtHoTen.Enabled = true;
            txtNguoiNhan.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            txtSDT.Enabled = true;
            rtbGhiChu.Enabled = true;
            dtpThoiHan.Enabled = true;
            txtTrangThai.Enabled = true;
            txtMaKhachHang.Enabled = true;

            btnThem1.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            state = "Update";
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtID.Enabled = true;
            txtHoTen.Enabled = true;
            txtNguoiNhan.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            txtSDT.Enabled = true;
            rtbGhiChu.Enabled = true;
            dtpThoiHan.Enabled = true;
            txtTrangThai.Enabled = true;
            txtMaKhachHang.Enabled = true;

            btnThem1.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = true;
            btnKiem.Enabled = false;

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
            state = "Reset";
            SetControl(state);
        }
        private void btnTimKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            txtHoTen.Enabled = true;
            txtID.Enabled = true;
            txtDiaChi.Enabled = false;
            txtNguoiNhan.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            rtbGhiChu.Enabled = false;
            dtpThoiHan.Enabled = false;
            txtTrangThai.Enabled = false;
            txtMaKhachHang.Enabled = false;

            btnThem1.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = false;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = true;

            clear();
        }
        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string ID = txtID.Text;
            string KhachHang = txtHoTen.Text;
            string DiaChi = txtDiaChi.Text;
            string Email = txtEmail.Text;
            string NguoiNhan = txtNguoiNhan.Text;
            string SDT = txtSDT.Text;
            string TrangThai = txtTrangThai.Text;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_KHACH_HANG " + "WHERE  ID LIKE N'%" + ID + "%' and " +
                                                                        "TEN_KHACHHANG LIKE N'%" + KhachHang + "%'", conn);


            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            //buoc 5: do du lieu tu tb vao dataGridView'
            grvKhachHang.DataSource = tb;
            grvKhachHang.Refresh();
        }
        #endregion

    }
}
