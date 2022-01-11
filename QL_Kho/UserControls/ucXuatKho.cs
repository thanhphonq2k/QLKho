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
    public partial class ucXuatKho : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucXuatKho _instance;
        public static ucXuatKho Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucXuatKho();
                return _instance;

            }
        }

        public bool FA { get; private set; }
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucXuatKho()
        {
            InitializeComponent();
            loadform();
            SetControl("Reset");
            loadcbo_HTTT();
            loadcbo_KhachHang();
            loadcbo_MaHH();
        }
        #region public function
        private void clear()
        {
            txtID.Text = "";
            //cboMaKhachHang.Text = "";
            cboMaHH.Text = "";
            //cboMaThanhToan.Text = "";
            txtSoLuong.Text = "";
            //dtpNgayXuatKho.Value = DateTime.Now;
            rtbGhiChu.Text = "";
            //txtTrangThai.Text = "";
        }
        public void loadform()
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "GET_XUATKHO";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvNhapKho.DataSource = ds.Tables[0];
            }
            else
            {
                grvNhapKho.DataSource = null;

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
            txtID.DataBindings.Clear();
            cboMaKhachHang.DataBindings.Clear();
            cboMaHH.DataBindings.Clear();
            cboMaThanhToan.DataBindings.Clear();
            txtSoLuong.DataBindings.Clear();
            dtpNgayXuatKho.DataBindings.Clear();
            rtbGhiChu.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();
            txtMaHoaDon.DataBindings.Clear();

            txtID.DataBindings.Add(new Binding("Text", ds.Tables[0], "ID", false, DataSourceUpdateMode.Never));
            cboMaKhachHang.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_KHACHHANG", false, DataSourceUpdateMode.Never));
            cboMaThanhToan.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_THANHTOAN", false, DataSourceUpdateMode.Never));
            cboMaHH.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_HANGHOA", false, DataSourceUpdateMode.Never));
            txtSoLuong.DataBindings.Add(new Binding("Text", ds.Tables[0], "SO_LUONG", false, DataSourceUpdateMode.Never));
            rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
            dtpNgayXuatKho.DataBindings.Add(new Binding("Text", ds.Tables[0], "NGAY_XUAT_KHO", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));
            txtMaHoaDon.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_HOADON", false, DataSourceUpdateMode.Never));
        }
        public void loadcbo_MaHH()
        {
            //conn = new SqlConnection(ConnectionString);
            //if (conn.State == ConnectionState.Closed)
            //{
            //    conn.Open();
            //}
            //string query = "SELECT N'--Chọn mã hàng hóa--' AS MA_HANGHOA,''AS	MA_HANGHOA UNION ALL SELECT A.MA_HANGHOA,A.TEN_HANGHOA FROM    dbo.QL_HANGHOA A";
            //SqlCommand cmd = new SqlCommand(query, conn);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //ds = new DataSet();
            //da.Fill(ds);
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    cboMaHH.DataSource = ds.Tables[0];
            //    cboMaHH.DisplayMember = "MA_HANGHOA";
            //    cboMaHH.ValueMember = "MA_HANGHOA";
            //}
            //else
            //{
            //    cboMaHH.DataSource = null;
            //}
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_HANGHOA", conn);
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
            r["MA_HANGHOA"] = "";
            r["TEN_HANGHOA"] = "---chọn hàng hóa----";
            tb.Rows.InsertAt(r, 0);
            cboMaHH.DataSource = tb;
            cboMaHH.DisplayMember = "TEN_HANGHOA";
            cboMaHH.ValueMember = "MA_HANGHOA";
            cboMaHH.Refresh();
        }
        public void loadcbo_KhachHang()
        {
            
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_KHACH_HANG", conn);
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
            r["MA_KHACHHANG"] = "";
            r["TEN_KHACHHANG"] = "---chọn khách hàng----";
            tb.Rows.InsertAt(r, 0);
            cboMaKhachHang.DataSource = tb;
            cboMaKhachHang.DisplayMember = "TEN_KHACHHANG";
            cboMaKhachHang.ValueMember = "MA_KHACHHANG";
            cboMaKhachHang.Refresh();
        }
        public void loadcbo_HTTT()
        {         
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from HT_THANHTOAN", conn);
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
            r["MA_THANHTOAN"] = "";
            r["TEN_THANHTOAN"] = "---chọn hình thức thanh toán----";
            tb.Rows.InsertAt(r, 0);
            cboMaThanhToan.DataSource = tb;
            cboMaThanhToan.DisplayMember = "TEN_THANHTOAN";
            cboMaThanhToan.ValueMember = "MA_THANHTOAN";
            cboMaThanhToan.Refresh();
        }
        private void SetControl(string state)
        {
            switch (state)
            {
                case "Reset":
                    txtID.Enabled = false;
                    cboMaKhachHang.Enabled = false;
                    cboMaHH.Enabled = false;
                    cboMaThanhToan.Enabled = false;
                    dtpNgayXuatKho.Enabled = false;
                    txtSoLuong.Enabled = false;
                    rtbGhiChu.Enabled = false;
                    txtTrangThai.Enabled = false;
                    txtMaHoaDon.Enabled = false;

                    btnThem.Enabled = true;
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
                        if (cboMaHH.SelectedValue == "--Chọn mã hàng hóa--")
                        {
                            MessageBox.Show("Chưa chọn mã hàng hóa", "Thông báo");
                            cboMaHH.Focus();
                        }
                        else if (txtMaHoaDon.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập mã hóa đơn", "Thông báo");
                            txtMaHoaDon.Focus();
                        }
                        else if (dtpNgayXuatKho.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa chọn ngày xuất kho", "Thông báo");
                            dtpNgayXuatKho.Focus();
                        }
                        else if (txtSoLuong.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập số lượng", "Thông báo");
                            txtSoLuong.Focus();
                        }
                        else if (cboMaKhachHang.SelectedValue == "--Chọn mã khach hang--")
                        {
                            MessageBox.Show("Chưa chọn mã khách hàng", "Thông báo");
                            cboMaKhachHang.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        
                        else if (txtID.Text.Trim() != "" || cboMaKhachHang.Text.Trim() != "" || cboMaHH.Text.Trim() != "" || txtSoLuong.Text != "")
                        {
                            string sqlInsert = "INSERT_TT_XUATKHO";
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            cmd.Parameters.AddWithValue("@MA_HANGHOA", cboMaHH.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_THANHTOAN", cboMaThanhToan.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_KHACHHANG", cboMaKhachHang.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_HOADON", txtMaHoaDon.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGAY_XUAT_KHO", dtpNgayXuatKho.Value);
                            cmd.Parameters.AddWithValue("@SO_LUONG", txtSoLuong.Text.Trim());
                            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                            cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                            cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                            cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);

                            //string Error = "";
                            //var fResult = CheckHangHoa(txtSoLuong.Text.Trim(), ref Error);
                            //if (fResult == false)
                            //{
                            //    MessageBox.Show("Trong kho không đủ " + txtSoLuong.Text.Trim() + " trong kho");
                            //    return;
                            //}
                            //bool cExits = false;
                            //cExits = CheckHangHoa(txtSoLuong.Text.Trim());
                            //if (cExits == true)
                            //{
                            //    MessageBox.Show("Trong kho không đủ " + txtSoLuong.Text.Trim() + " trong kho");
                            //    return;
                            //}
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
                            loadform();
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
                        if (cboMaHH.SelectedValue == "--Chọn mã hàng hóa--")
                        {
                            MessageBox.Show("Chưa chọn mã hàng hóa", "Thông báo");
                            cboMaHH.Focus();
                        }
                        else if (txtMaHoaDon.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập mã hóa đơn", "Thông báo");
                            txtMaHoaDon.Focus();
                        }
                        else if (dtpNgayXuatKho.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập ngày xuất kho", "Thông báo");
                            dtpNgayXuatKho.Focus();
                        }
                        else if (txtSoLuong.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập số lượng", "Thông báo");
                            txtSoLuong.Focus();
                        }
                        else if (cboMaKhachHang.SelectedValue == "--Chọn mã khach hang--")
                        {
                            MessageBox.Show("Chưa chọn mã khách hàng", "Thông báo");
                            cboMaKhachHang.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if (txtID.Text.Trim() != "" || cboMaKhachHang.Text.Trim() != "" || cboMaHH.Text.Trim() != "" || txtSoLuong.Text != "")
                        {
                            string sqlInsert = "UPDATE_TT_XUATKHO";
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                            cmd.Parameters.AddWithValue("@MA_HANGHOA", cboMaHH.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_THANHTOAN", cboMaThanhToan.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_KHACHHANG", cboMaKhachHang.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@MA_HOADON", txtMaHoaDon.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGAY_XUAT_KHO", dtpNgayXuatKho.Value);
                            cmd.Parameters.AddWithValue("@SO_LUONG", txtSoLuong.Text.Trim());
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
                            if (txtID.Text.Trim() != "")
                            {
                                string sqlDelete = "DELETE_TT_XUATKHO";
                                SqlCommand cmd = new SqlCommand(sqlDelete, conn);
                                cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                                cmd.Parameters.AddWithValue("@MA_HANGHOA", cboMaHH.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@MA_THANHTOAN", cboMaThanhToan.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@MA_KHACHHANG", cboMaKhachHang.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@MA_HOADON", txtMaHoaDon.Text.Trim());
                                cmd.Parameters.AddWithValue("@NGAY_XUAT_KHO", dtpNgayXuatKho.Value);
                                cmd.Parameters.AddWithValue("@SO_LUONG", txtSoLuong.Text.Trim());
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
                                    clear();
                                    loadform();
                                    SetControl("Reset");
                                    MessageBox.Show("Xóa thành công.", "Thông báo");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Không thể xóa!", "Thông báo");
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "thông báo");
                    }
                    break;
            }
        }
        public bool CheckHangHoa(string SoLuong)
        {
            try
            {
                conn = new SqlConnection(ConnectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string query = "CHECK_EXITS_SOLUONG";
                SqlCommand cmd = new SqlCommand(query, conn);
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@SO_LUONG", SoLuong);
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
            txtID.Enabled = false;
            cboMaKhachHang.Enabled = true;
            cboMaHH.Enabled = true;
            cboMaThanhToan.Enabled = true;
            dtpNgayXuatKho.Enabled = true;
            txtSoLuong.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;
            txtMaHoaDon.Enabled = true;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            clear();           
            //loadcbo_HTTT();
            loadcbo_MaHH();
            //loadcbo_KhachHang();
            state = "Insert";
        }
        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtID.Enabled = true;
            cboMaKhachHang.Enabled = true;
            cboMaHH.Enabled = true;
            cboMaThanhToan.Enabled = true;
            dtpNgayXuatKho.Enabled = true;
            txtSoLuong.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;
            txtMaHoaDon.Enabled = true;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            //loadcbo_HTTT();
            //loadcbo_MaHH();
            //loadcbo_KhachHang();
            state = "Update";
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtID.Enabled = true;
            cboMaKhachHang.Enabled = true;
            cboMaHH.Enabled = true;
            cboMaThanhToan.Enabled = true;
            dtpNgayXuatKho.Enabled = true;
            txtSoLuong.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;
            txtMaHoaDon.Enabled = true;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
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
            ucHangHoa frm = new ucHangHoa();
            frm.Show();
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
            txtID.Enabled = false;
            cboMaKhachHang.Enabled = false;
            cboMaHH.Enabled = false;
            cboMaThanhToan.Enabled = false;
            dtpNgayXuatKho.Enabled = true;
            txtSoLuong.Enabled = false;
            rtbGhiChu.Enabled = false;
            txtTrangThai.Enabled = false;
            txtMaHoaDon.Enabled = true;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = false;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = true;

            //loadcbo_HTTT();
            //loadcbo_MaHH();
            //loadcbo_KhachHang();
            clear();
        }
        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string MaKhachHang = cboMaKhachHang.Text;
            string MaThanhToan = cboMaThanhToan.Text;
            string MaHangHoa = cboMaHH.Text;
            string SoLuong = txtSoLuong.Text;
            string TrangThai = txtTrangThai.Text;
            string MaHD = txtMaHoaDon.Text;
            DateTime NgayXuatKho = Convert.ToDateTime(dtpNgayXuatKho.Text);          
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_TT_XUATKHO " + "WHERE  MA_HOADON LIKE N'%" + MaHD + "%' and " +
                                                                                    "(NGAY_XUAT_KHO between '" + NgayXuatKho + "' AND getdate())", conn);

            //"TEN_HANGHOA LIKE N'%" + MaHangHoa + "%' AND " +
            //                                                            /*"MA_THANHTOAN LIKE N'%" + MaThanhToan + "%' AND " +
            //                                                            "TRANG_THAI LIKE N'%" + TrangThai + "%' AND " +
            //                                                            "SO_LUONG LIKE N'%" + SoLuong + "%' AND " +*/
            //   
            //tim kiem xuat kho tu ngay nao den nay
            //SqlCommand cmd = new SqlCommand("TIMKIEMXUATKHO", conn);
            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            grvNhapKho.DataSource = tb;
            grvNhapKho.Refresh();
        }
        //private void cboMaKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    rtbGhiChu.Text = cboMaKhachHang.SelectedValue.ToString();
        //}
        private void btnTroVe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("UPDATE  QL_TT_XUATKHO SET TRANG_THAI = '0'", conn);
            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            grvNhapKho.DataSource = tb;
            grvNhapKho.Refresh();
        }
        private void btnBaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmHoaDon hd = new frmHoaDon();
            hd.Show();
        }
        #endregion
    }
}
