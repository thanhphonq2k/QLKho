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
    public partial class ucHangHoa : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucHangHoa _instance;
        public static ucHangHoa Instance
        {
            get
            {
                if (_instance == null) 
                    _instance = new ucHangHoa();
                    return _instance;                 
            }
        }
        #region Variables
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        public static string strError = "";
        public static string Status = "";
        string state = "";
        SqlConnection conn;
        DataSet ds;
        #endregion
        public ucHangHoa()
        {
            InitializeComponent();
            loadform();
            SetControl("Reset");
            loadcbo_MaDV();
            loadcbo_MaNhaCC();
        }
        #region public functions
        public void loadform()
        {
            //tạo kết nói với database
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "GET_HANGHOA";
            //SELECT * FROM QL_HANGHOA WHERE TRANG_THAI='1'
            //tạo đối tượng Command
            SqlCommand cmd = new SqlCommand(query, conn);
            //do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            //đổ dữ liệu từ dataadter
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvHangHoa.DataSource = ds.Tables[0];
            }
            else
            {
                grvHangHoa.DataSource = null;

            }
            banghi();
            bindingdata();
        }
        public void banghi()
        {
            lblTongSo.Text = "Tổng số: " + ds.Tables[0].Rows.Count.ToString() + " bản ghi";
        }
        public void bindingdata()
        {
            //load dữ liệu từ gridview lên ô text
            txtMaHH.DataBindings.Clear();
            txtTenHH.DataBindings.Clear();
            txtSoLuong.DataBindings.Clear();
            txtGiaRa.DataBindings.Clear();
            txtGiaVao.DataBindings.Clear();
            rtbGhiChu.DataBindings.Clear();
            txtQR.DataBindings.Clear();
            txtBar.DataBindings.Clear();
            cboMaDonVi.DataBindings.Clear();
            cboMaNhaCC.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();
            

            //txtID.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_CHUCVU", false, DataSourceUpdateMode.Never));
            txtMaHH.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_HANGHOA", false, DataSourceUpdateMode.Never));
            txtTenHH.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_HANGHOA", false, DataSourceUpdateMode.Never));
            txtSoLuong.DataBindings.Add(new Binding("Text", ds.Tables[0], "SO_LUONG", false, DataSourceUpdateMode.Never));
            txtGiaRa.DataBindings.Add(new Binding("Text", ds.Tables[0], "GIA_RA", false, DataSourceUpdateMode.Never));
            txtGiaVao.DataBindings.Add(new Binding("Text", ds.Tables[0], "GIA_VAO", false, DataSourceUpdateMode.Never));
            rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
            txtQR.DataBindings.Add(new Binding("Text", ds.Tables[0], "QRCode", false, DataSourceUpdateMode.Never));
            txtBar.DataBindings.Add(new Binding("Text", ds.Tables[0], "BarCode", false, DataSourceUpdateMode.Never));
            cboMaDonVi.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_DONVI", false, DataSourceUpdateMode.Never));
            cboMaNhaCC.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_NHA_CUNGCAP", false, DataSourceUpdateMode.Never));

            //cboMaDonVi.DataBindings.Add(new Binding("ValueMember", ds.Tables[0], "MA_DONVI", false, DataSourceUpdateMode.Never));
            //cboMaDonVi.DataBindings.Add(new Binding("DisplayMember", ds.Tables[0], "MA_DONVI", false, DataSourceUpdateMode.Never));
            //cboMaNhaCC.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_NHA_CUNGCAP", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));
        }
        private void clear()
        {
            txtMaHH.Text = "";
            txtTenHH.Text = "";
            txtSoLuong.Text = "";
            txtGiaRa.Text = "";
            txtGiaVao.Text = "";
            rtbGhiChu.Text = "";
            txtQR.Text = "";
            txtBar.Text = "";
            rtbGhiChu.Text = "";
            cboMaNhaCC.Text = "";
            cboMaDonVi.Text = "";
            txtTrangThai.Text = "";
            
        }
        public void loadcbo_MaNhaCC()
        {

            try
            {
                conn = new SqlConnection(ConnectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string query = "SELECT * FROM DM_NHA_CUNGCAP";
                SqlCommand cmd = new SqlCommand(query, conn);
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    cboMaNhaCC.ValueMember = "MA_NHA_CUNGCAP";
                    cboMaNhaCC.DisplayMember = "TEN_NHA_CUNGCAP";
                    cboMaNhaCC.DataSource = ds.Tables[0];
                }
                else
                {
                    cboMaNhaCC.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void loadcbo_MaDV()
        {
            
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from DM_DONVI", conn);
            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();

            //buoc 5: do du lieu vao cbo
            //them dong lop
            DataRow r;
            r = tb.NewRow();
            r["MA_DONVI"] = "";
            r["TEN_DONVI"] = "---chọn đơn vị----";
            tb.Rows.InsertAt(r, 0);
            cboMaDonVi.DataSource = tb;
            cboMaDonVi.DisplayMember = "TEN_DONVI";
            cboMaDonVi.ValueMember = "MA_DONVI";            
            cboMaDonVi.Refresh();
        }
        public void SetControl(string state)
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

                    txtMaHH.Enabled = false;
                    txtTenHH.Enabled = false;
                    txtSoLuong.Enabled = false;
                    txtGiaRa.Enabled = false;
                    txtGiaVao.Enabled = false;
                    cboMaDonVi.Enabled = false;
                    cboMaNhaCC.Enabled = false;
                    txtQR.Enabled = false;
                    txtBar.Enabled = false;
                    rtbGhiChu.Enabled = false;
                    txtTrangThai.Enabled = false;
                    
                    break;
                case "Insert":
                    try
                    {
                        if (txtMaHH.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập mã hàng hóa", "Thông báo");
                            txtMaHH.Focus();
                        }
                        else if(txtTrangThai.Text.Trim() !="1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if (cboMaDonVi.SelectedValue == "--Chọn--")
                        {
                            MessageBox.Show("Chưa chọn mã loại đơn vị", "Thông báo");
                            cboMaDonVi.Focus();
                        }
                        else if (System.Text.RegularExpressions.Regex.IsMatch(txtSoLuong.Text, "[^0-9]"))
                        {
                            MessageBox.Show("Số lượng chỉ sử dụng số!!");
                            //txtSoLuong.Text = txtSoLuong.Text.Remove(txtSoLuong.Text.Length - 1);
                        }
                        else if (txtTenHH.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập tên hàng hóa", "Thông báo");
                            txtTenHH.Focus();
                        }                      
                        else if (cboMaNhaCC.SelectedValue == "--Chọn mã nhà cung cấp--")
                        {
                            MessageBox.Show("Chưa chọn mã nhà cung cấp", "Thông báo");
                            cboMaNhaCC.Focus();
                        }
                        else if (System.Text.RegularExpressions.Regex.IsMatch(txtGiaVao.Text, "[^0-9]"))
                        {
                            MessageBox.Show("Giá chỉ sử dụng số!!");
                            txtGiaVao.Text = txtGiaVao.Text.Remove(txtGiaVao.Text.Length - 1);
                        }
                        else if (System.Text.RegularExpressions.Regex.IsMatch(txtGiaRa.Text, "[^0-9]"))
                        {
                            MessageBox.Show("Giá chỉ sử dụng số!!");
                            txtGiaRa.Text = txtGiaRa.Text.Remove(txtGiaRa.Text.Length - 1);
                        }
                        else if  (txtMaHH.Text.Trim() != "" || cboMaDonVi.Text.Trim() != "" || cboMaNhaCC.Text.Trim() != "" || txtTenHH.Text != "")
                        {
                            string sqlInsert = "INSERT_HANGHOA";
                            //tạo đối tượng
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            //truy vấn dữ liệu từ người dùng
                            cmd.Parameters.AddWithValue("@MA_HANGHOA", txtMaHH.Text.Trim());
                            cmd.Parameters.AddWithValue("@TEN_HANGHOA", txtTenHH.Text.Trim());
                            cmd.Parameters.AddWithValue("@SO_LUONG", txtSoLuong.Text.Trim());
                            cmd.Parameters.AddWithValue("@GIA_VAO", txtGiaRa.Text.Trim());
                            cmd.Parameters.AddWithValue("@GIA_RA", txtGiaVao.Text.Trim());
                            cmd.Parameters.AddWithValue("@MA_DONVI", cboMaDonVi.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_NHA_CUNGCAP", cboMaNhaCC.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@QRCode", txtQR.Text.Trim());
                            cmd.Parameters.AddWithValue("@BarCode", txtBar.Text.Trim());
                            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                            cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                            cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
                            //kiểm tra hàm check
                            string Error = "";
                            var fResult = CheckHangHoa(txtTenHH.Text.Trim(), ref Error);
                            if (fResult == false)
                            {
                                if (Error != "")
                                {
                                    MessageBox.Show(Error);
                                }
                                else
                                {
                                    MessageBox.Show("Đã tồn tại hàng hóa " + txtTenHH.Text.Trim() + " trong dữ liệu");                                    
                                }
                                return;
                            }
                            //thực thi câu lệnh
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
                case "Update":
                    try
                    {
                        if (txtMaHH.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập tên đơn vị", "Thông báo");
                            txtMaHH.Focus();
                            return;
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if (cboMaDonVi.SelectedValue == "--Chọn mã đơn vị--")
                        {
                            MessageBox.Show("Chưa chọn mã loại đơn vị", "Thông báo");
                            cboMaDonVi.Focus();
                            return;
                        }

                        else if (txtTenHH.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                            txtTenHH.Focus();
                            return;
                        }
                        else if (txtBar.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                            txtBar.Focus();
                            return;
                        }
                        else if (txtQR.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                            txtQR.Focus();
                            return;
                        }
                        else if (cboMaNhaCC.SelectedValue == "--Chọn mã nhà cung cấp--")
                        {
                            MessageBox.Show("Chưa chọn mã nhà cung cấp", "Thông báo");
                            cboMaNhaCC.Focus();
                            return;
                        }
                        
                        //else if (System.Text.RegularExpressions.Regex.IsMatch(txtGiaVao.Text, "[^0-9]"))
                        //{
                        //    MessageBox.Show("Giá chỉ sử dụng số!!");
                        //    txtGiaVao.Text = txtGiaVao.Text.Remove(txtGiaVao.Text.Length - 1);
                        //}
                        ////else if (System.Text.RegularExpressions.Regex.IsMatch(txtGiaRa.Text, "[^0-9]"))
                        ////{
                        ////    MessageBox.Show("Giá chỉ sử dụng số!!");
                        ////    txtGiaRa.Text = txtGiaRa.Text.Remove(txtGiaRa.Text.Length - 1);
                        ////}
                        else if (txtMaHH.Text.Trim() != "" || cboMaDonVi.Text.Trim() != "" || cboMaNhaCC.Text.Trim() != "" || txtTenHH.Text != "")
                        {
                            string sqlInsert = "UPDATE_HANGHOA";
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MA_HANGHOA", txtMaHH.Text.Trim());
                            cmd.Parameters.AddWithValue("@TEN_HANGHOA", txtTenHH.Text.Trim());
                            cmd.Parameters.AddWithValue("@SO_LUONG", txtSoLuong.Text.Trim());
                            cmd.Parameters.AddWithValue("@GIA_RA", txtGiaRa.Text.Trim());
                            cmd.Parameters.AddWithValue("@GIA_VAO", txtGiaVao.Text.Trim());
                            cmd.Parameters.AddWithValue("@MA_DONVI", cboMaDonVi.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_NHA_CUNGCAP", cboMaNhaCC.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@QRCode", txtQR.Text.Trim());
                            cmd.Parameters.AddWithValue("@BarCode", txtBar.Text.Trim());
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
                                MessageBox.Show("Sửa thành công", "Thông báo");
                            }
                            else
                            {
                                MessageBox.Show("Sửa thất bại", "Thông báo");
                            }
                            loadform();
                            clear();
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
                        if (XtraMessageBox.Show("Bạn có chắc chắc muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (txtMaHH.Text.Trim() != "")
                            {
                                string sqlInsert = "DELETE_HANGHOA";
                                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                
                                cmd.Parameters.AddWithValue("@MA_HANGHOA", txtMaHH.Text.Trim());
                                cmd.Parameters.AddWithValue("@TEN_HANGHOA", txtTenHH.Text.Trim());
                                cmd.Parameters.AddWithValue("@SO_LUONG", txtSoLuong.Text.Trim());
                                cmd.Parameters.AddWithValue("@GIA_RA", txtGiaRa.Text.Trim());
                                cmd.Parameters.AddWithValue("@GIA_VAO", txtGiaVao.Text.Trim());
                                cmd.Parameters.AddWithValue("@MA_DONVI", cboMaDonVi.Text.ToString());
                                cmd.Parameters.AddWithValue("@MA_NHA_CUNGCAP", cboMaNhaCC.Text.ToString());
                                cmd.Parameters.AddWithValue("@QRCode", txtQR.Text.Trim());
                                cmd.Parameters.AddWithValue("@BarCode", txtBar.Text.Trim());
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
            }
           
        }

        public bool CheckHangHoa(string HangHoa, ref string strError)
        {
            try
            {
                conn = new SqlConnection(ConnectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string query = "SELECT * FROM QL_HANGHOA WHERE TEN_HANGHOA = '" + HangHoa + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo");
                return true;
            }
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

            txtMaHH.Enabled = true;
            txtTenHH.Enabled = true;
            txtSoLuong.Enabled = true;
            txtGiaRa.Enabled = true;
            txtGiaVao.Enabled = true;
            cboMaDonVi.Enabled = true;
            cboMaNhaCC.Enabled = true;
            txtQR.Enabled = true;
            txtBar.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;           

            clear();
            loadcbo_MaDV();
            loadcbo_MaNhaCC();
            cboMaNhaCC.Text = "----CHỌN NHÀ CUNG CẤP---";
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

            txtMaHH.Enabled = true;
            txtTenHH.Enabled = true;
            txtSoLuong.Enabled = true;
            txtGiaRa.Enabled = true;
            txtGiaVao.Enabled = true;
            cboMaDonVi.Enabled = true;
            cboMaNhaCC.Enabled = true;
            txtQR.Enabled = true;
            txtBar.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;
            
            //loadcbo_MaDV();
            //loadcbo_MaNhaCC();
            state = "Update";
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            txtMaHH.Enabled = true;
            txtTenHH.Enabled = true;
            txtSoLuong.Enabled = true;
            txtGiaRa.Enabled = true;
            txtGiaVao.Enabled = true;
            cboMaDonVi.Enabled = true;
            cboMaNhaCC.Enabled = true;
            txtQR.Enabled = true;
            txtBar.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;
            

            //loadcbo_MaDV();
            //loadcbo_MaNhaCC();
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

            txtMaHH.Enabled = false;
            txtTenHH.Enabled = true;
            txtSoLuong.Enabled = false;
            cboMaDonVi.Enabled = false;
            cboMaNhaCC.Enabled = false;
            txtGiaRa.Enabled = false;
            txtGiaVao.Enabled = false;
            txtQR.Enabled = false;
            txtBar.Enabled = false;
            rtbGhiChu.Enabled = false;
            txtTrangThai.Enabled = true;
            

            loadcbo_MaDV();
            loadcbo_MaNhaCC();
            clear();
        }
        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string MaHangHoa = txtMaHH.Text;
            string TenHangHoa = txtTenHH.Text;
            string MaDonVi = cboMaDonVi.Text;
            string TrangThai = txtTrangThai.Text;
            string MaNhaCC = cboMaNhaCC.Text;
            string TenNhaCungCap = cboMaNhaCC.Text;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_HANGHOA " + "WHERE  MA_HANGHOA LIKE N'%" + MaHangHoa + "%' and " +
                                                                        "TEN_HANGHOA LIKE N'%" + TenHangHoa + "%' AND " +
                                                                        "TRANG_THAI LIKE N'%" + TrangThai + "%'", conn);


            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            grvHangHoa.DataSource = tb;
            grvHangHoa.Refresh();
        }
        #endregion
    }
}
