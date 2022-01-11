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
using System.IO;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;

namespace QL_Kho.UserControls
{
    public partial class ucNguoiDung : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucNguoiDung _instance;
        public static ucNguoiDung Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucNguoiDung();
                return _instance;
            }
        }
        #region Variables
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        public static string strError = "";
        public static string status = "";
        string UrlFileUpload = "";
        byte[] arrImage;
        private object imageParameter;
        public static string State = "-1";
        SqlConnection conn;
        public static DataSet ds;
        private string FileName;
        private string UrlFileUpLoad;
        #endregion
        public ucNguoiDung()
        {
            InitializeComponent();
            
            getData();
            BindingData();
            loadcbo_MaCV();
            setControl("Reset");
            
        }
        #region public function
        //public void banghi()
        //{
        //    lblTongSo.Text = "Tổng số: " + ds.Tables[0].Rows.Count.ToString() + " bản ghi";
        //}
        //public void bindingdata()
        //{
        //    txtTenNguoiDung.DataBindings.Clear();
        //    cboMaChucVu.DataBindings.Clear();
        //    txtID.DataBindings.Clear();
        //    dtpNgaySinh.DataBindings.Clear();
        //    txtSoCMT.DataBindings.Clear();
        //    txtDiaChi.DataBindings.Clear();
        //    txtSDT.DataBindings.Clear();
        //    txtEmail.DataBindings.Clear();
        //    txtDuongDanAnh.DataBindings.Clear();
        //    txtTrangThai.DataBindings.Clear();
        //    rtbGhiChu.DataBindings.Clear();
        //    picNguoiDung.DataBindings.Clear();

        //    txtTenNguoiDung.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_NDUNG", false, DataSourceUpdateMode.Never));
        //    cboMaChucVu.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_CHUCVU", false, DataSourceUpdateMode.Never));
        //    txtID.DataBindings.Add(new Binding("Text", ds.Tables[0], "ID", false, DataSourceUpdateMode.Never));
        //    dtpNgaySinh.DataBindings.Add(new Binding("Text", ds.Tables[0], "NGAY_SINH", false, DataSourceUpdateMode.Never));
        //    txtSoCMT.DataBindings.Add(new Binding("Text", ds.Tables[0], "SO_CMT", false, DataSourceUpdateMode.Never));
        //    txtEmail.DataBindings.Add(new Binding("Text", ds.Tables[0], "EMAIL", false, DataSourceUpdateMode.Never));
        //    txtDiaChi.DataBindings.Add(new Binding("Text", ds.Tables[0], "DIA_CHI", false, DataSourceUpdateMode.Never));
        //    txtSDT.DataBindings.Add(new Binding("Text", ds.Tables[0], "SO_DIENTHOAI", false, DataSourceUpdateMode.Never));
        //    txtDuongDanAnh.DataBindings.Add(new Binding("Text", ds.Tables[0], "DUONG_DAN_ANH", false, DataSourceUpdateMode.Never));
        //    txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));
        //    rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
        //    picNguoiDung.DataBindings.Add(new Binding("ImageLocation", ds.Tables[0], "DUONG_DAN_ANH", false, DataSourceUpdateMode.Never));
        //}
        //public void SetControl(string Status)
        //{
        //    switch (Status)
        //    {
        //        case "Reset":
        //            lblError.Text = "";

        //            btnThem.Enabled = true;
        //            btnSua.Enabled = true;
        //            btnXoa.Enabled = true;
        //            btnGhi.Enabled = false;
        //            btnHuy.Enabled = false;
        //            btnTimKiem.Enabled = true;
        //            btnKiem.Enabled = false;

        //            txtTenNguoiDung.Enabled = false;
        //            cboMaChucVu.Enabled = false;
        //            rdoNam.Enabled = false;
        //            rdoNu.Enabled = false;
        //            dtpNgaySinh.Enabled = false;
        //            txtSoCMT.Enabled = false;
        //            txtID.Enabled = false;
        //            txtDiaChi.Enabled = false;
        //            txtSDT.Enabled = false;
        //            txtEmail.Enabled = false;
        //            dtpThoiHan.Enabled = false;
        //            txtDuongDanAnh.Enabled = false;
        //            rtbGhiChu.Enabled = false;
        //            txtTrangThai.Enabled = false;

        //            txtTenNguoiDung.Focus();
        //            break;

        //        case "Insert":
        //            lblError.Text = "";

        //            btnThem.Enabled = false;
        //            btnSua.Enabled = false;
        //            btnXoa.Enabled = false;
        //            btnGhi.Enabled = true;
        //            btnHuy.Enabled = true;
        //            btnTimKiem.Enabled = false;
        //            btnKiem.Enabled = false;

        //            txtTenNguoiDung.Enabled = true;
        //            cboMaChucVu.Enabled = true;
        //            rdoNam.Enabled = true;
        //            rdoNu.Enabled = true;
        //            dtpNgaySinh.Enabled = true;
        //            txtSoCMT.Enabled = true;
        //            txtID.Enabled = true;
        //            txtDiaChi.Enabled = true;
        //            txtSDT.Enabled = true;
        //            txtEmail.Enabled = true;
        //            dtpThoiHan.Enabled = true;
        //            txtDuongDanAnh.Enabled = true;
        //            rtbGhiChu.Enabled = true;
        //            txtTrangThai.Enabled = true;

        //            loadcbo_MaChucVu();
        //            txtTenNguoiDung.Focus();
        //            break;

        //        case "Update":
        //            lblError.Text = "";

        //            btnThem.Enabled = false;
        //            btnSua.Enabled = false;
        //            btnXoa.Enabled = false;
        //            btnGhi.Enabled = true;
        //            btnHuy.Enabled = true;
        //            btnTimKiem.Enabled = false;
        //            btnKiem.Enabled = false;

        //            txtTenNguoiDung.Enabled = true;
        //            cboMaChucVu.Enabled = true;
        //            rdoNam.Enabled = true;
        //            rdoNu.Enabled = true;
        //            dtpNgaySinh.Enabled = true;
        //            txtSoCMT.Enabled = true;
        //            txtID.Enabled = true;
        //            txtDiaChi.Enabled = true;
        //            txtSDT.Enabled = true;
        //            txtEmail.Enabled = true;
        //            dtpThoiHan.Enabled = true;
        //            txtDuongDanAnh.Enabled = true;
        //            rtbGhiChu.Enabled = true;
        //            txtTrangThai.Enabled = true;

        //            loadcbo_MaChucVu();
        //            txtTenNguoiDung.Focus();

        //            break;
        //        case "Delete":
        //            lblError.Text = "";

        //            btnThem.Enabled = true;
        //            btnSua.Enabled = true;
        //            btnXoa.Enabled = true;
        //            btnGhi.Enabled = false;
        //            btnHuy.Enabled = false;
        //            btnTimKiem.Enabled = true;
        //            btnKiem.Enabled = false;

        //            txtTenNguoiDung.Enabled = true;
        //            cboMaChucVu.Enabled = true;
        //            rdoNam.Enabled = true;
        //            rdoNu.Enabled = true;
        //            dtpNgaySinh.Enabled = true;
        //            txtSoCMT.Enabled = true;
        //            txtID.Enabled = true;
        //            txtDiaChi.Enabled = true;
        //            txtSDT.Enabled = true;
        //            txtEmail.Enabled = true;
        //            dtpThoiHan.Enabled = true;
        //            txtDuongDanAnh.Enabled = true;
        //            rtbGhiChu.Enabled = true;
        //            txtTrangThai.Enabled = true;

        //            txtTenNguoiDung.Focus();

        //            break;

        //        default:
        //            break;
        //    }
        //}
        //public void LoadGrd()
        //{
        //    conn = new SqlConnection(ConnectionString);
        //    if (conn.State == ConnectionState.Closed)
        //    {
        //        conn.Open();
        //    }
        //    string query = "GET_NDUNG ";
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    ds = new DataSet();
        //    da.Fill(ds);
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        grvNguoiDung.DataSource = ds.Tables[0];
        //    }
        //    else
        //    {
        //        grvNguoiDung.DataSource = null;
        //        lblTongSo.Text = "Tổng số: 0 bản ghi";
        //    }
        //    banghi();
        //    bindingdata();
        //}
        //public int Gtinh()
        //{
        //    if (rdoNam.Checked == true)
        //    {
        //        return 1;
        //    }
        //    else return 0;
        //}
        //public void Clear()
        //{
        //    txtTenNguoiDung.Text = "";
        //    cboMaChucVu.Text = "";
        //    rdoNam.Checked = false;
        //    rdoNu.Checked = false;
        //    txtSoCMT.Text = "";
        //    txtTrangThai.Text = "";
        //    txtID.Text = "";
        //    txtDiaChi.Text = "";
        //    txtSDT.Text = "";
        //    txtEmail.Text = "";
        //    dtpNgaySinh.Value = DateTime.Now;
        //    dtpThoiHan.Value = DateTime.Now;
        //    txtDuongDanAnh.Text = "";
        //    rtbGhiChu.Text = "";

        //    picNguoiDung.Image = null;
        //}
        //public void loadcbo_MaChucVu()
        //{
        //    SqlConnection conn = new SqlConnection(ConnectionString);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand("Select * from DM_CHUCVU", conn);
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
        //    r["MA_CHUCVU"] = "";
        //    r["TEN_CHUCVU"] = "---chọn chức vụ----";
        //    tb.Rows.InsertAt(r, 0);
        //    cboMaChucVu.DataSource = tb;
        //    cboMaChucVu.DisplayMember = "TEN_CHUCVU";
        //    cboMaChucVu.ValueMember = "MA_CHUCVU";
        //    cboMaChucVu.Refresh();
        //}
        //public void InsertData()
        //{
        //    try
        //    {
        //        if (txtTenNguoiDung.Text.Trim() == "")
        //        {
        //            lblError.Text = "Nhập vào tên người dùng";
        //            txtTenNguoiDung.Focus();
        //        }
        //        else if (txtID.Text.Trim() == "")
        //        {
        //            lblError.Text = "Nhập vào ID";
        //            txtID.Focus();
        //        }
        //        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
        //        {
        //            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
        //            txtTrangThai.Focus();
        //        }
        //        else
        //        {
        //            if (txtDuongDanAnh.Text != null && txtDuongDanAnh.Text.Trim() != "")
        //            {
        //                var arr = txtDuongDanAnh.Text.Split('\\');
        //                var FileName = arr[arr.Length - 1];
        //                FileName = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "_" + FileName;
        //                System.IO.File.Copy(txtDuongDanAnh.Text.Trim(), UrlFileUpload + FileName);

        //                Image img = Image.FromFile(txtDuongDanAnh.Text.Trim());

        //                using (MemoryStream ms = new MemoryStream())
        //                {
        //                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                    arrImage = ms.ToArray();
        //                }
        //            }
        //            string sqlInsert = "INSERT_QL_NDUNG";
        //            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
        //            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
        //            cmd.Parameters.AddWithValue("@MA_CHUCVU", cboMaChucVu.SelectedValue.ToString());
        //            cmd.Parameters.AddWithValue("@TEN_NDUNG", txtTenNguoiDung.Text.Trim());
        //            cmd.Parameters.AddWithValue("@NGAY_SINH", dtpNgaySinh.Value);
        //            cmd.Parameters.AddWithValue("@GIOI_TINH", Gtinh());

        //            cmd.Parameters.AddWithValue("@DUONG_DAN_ANH", UrlFileUpload + FileName);
        //            SqlParameter imageParameter = new SqlParameter("@ANH", SqlDbType.Image);
        //            imageParameter.Value = DBNull.Value;
        //            cmd.Parameters.Add(imageParameter);
        //            cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
        //            cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
        //            cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
        //            cmd.Parameters.AddWithValue("@SO_CMT", txtSoCMT.Text.Trim());
        //            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
        //            cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
        //            cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
        //            cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
        //            cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
        //            cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            var result = cmd.ExecuteNonQuery();
        //            if (result > 0)
        //            {
        //                LoadGrd();
        //                lblError.Text = "Thêm thành công!";
        //                Clear();
        //            }
        //            else
        //            {
        //                lblError.Text = "Không thể thêm!";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo");
        //    }
        //}

        //public void UpdateData()
        //{
        //    try
        //    {
        //        if (txtTenNguoiDung.Text.Trim() == "")
        //        {
        //            lblError.Text = "Nhập vào tên người dùng";
        //            txtTenNguoiDung.Focus();
        //        }
        //        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
        //        {
        //            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
        //            txtTrangThai.Focus();
        //        }
        //        else
        //        {

        //            string sqlUpdate = "UPDATE_QL_NDUNG";
        //            SqlCommand cmd = new SqlCommand(sqlUpdate, conn);
        //            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
        //            cmd.Parameters.AddWithValue("@MA_CHUCVU", cboMaChucVu.SelectedValue.ToString());
        //            cmd.Parameters.AddWithValue("@TEN_NDUNG", txtTenNguoiDung.Text.Trim());
        //            cmd.Parameters.AddWithValue("@NGAY_SINH", dtpNgaySinh.Value);
        //            cmd.Parameters.AddWithValue("@GIOI_TINH", Gtinh());
        //            cmd.Parameters.AddWithValue("@DUONG_DAN_ANH", txtDuongDanAnh.Text.Trim());
        //            //cmd.Parameters.AddWithValue("@DUONG_DAN_ANH", UrlFileUpload + FileName);
        //            cmd.Parameters.AddWithValue("@ANH", arrImage);
        //            cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
        //            cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
        //            cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
        //            cmd.Parameters.AddWithValue("@SO_CMT", txtSoCMT.Text.Trim());
        //            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
        //            cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
        //            cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
        //            cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
        //            cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
        //            cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);

        //            cmd.CommandType = CommandType.StoredProcedure;
        //            var result = cmd.ExecuteNonQuery();
        //            if (result > 0)
        //            {
        //                LoadGrd();
        //                lblError.Text = "Sửa thành công!";
        //            }
        //            else
        //            {
        //                lblError.Text = "Không thể Sửa!";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo");
        //    }
        //}
        //public void DeleteData()
        //{
        //    try
        //    {
        //        if (txtTenNguoiDung.Text.Trim() == "")
        //        {
        //            lblError.Text = "Nhập vào tên người dùng";
        //            txtTenNguoiDung.Focus();
        //        }
        //        else
        //        {
        //            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //            {
        //                string sqlDelete = "DELETE_QL_NDUNG";
        //                SqlCommand cmd = new SqlCommand(sqlDelete, conn);
        //                cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
        //                cmd.Parameters.AddWithValue("@MA_CHUCVU", cboMaChucVu.SelectedValue.ToString());
        //                cmd.Parameters.AddWithValue("@TEN_NDUNG", txtTenNguoiDung.Text.Trim());
        //                cmd.Parameters.AddWithValue("@NGAY_SINH", dtpNgaySinh.Value);
        //                cmd.Parameters.AddWithValue("@GIOI_TINH", Gtinh());

        //                cmd.Parameters.AddWithValue("@DUONG_DAN_ANH", UrlFileUpload + FileName);
        //                cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
        //                cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
        //                cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
        //                cmd.Parameters.AddWithValue("@SO_CMT", txtSoCMT.Text.Trim());
        //                cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
        //                cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
        //                cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
        //                cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
        //                cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
        //                cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                var result = cmd.ExecuteNonQuery();
        //                if (result > 0)
        //                {
        //                    Clear();
        //                    LoadGrd();
        //                    SetControl("Reset");
        //                    lblError.Text = "Đã xóa";
        //                }
        //            }
        //            else
        //            {
        //                Clear();
        //                SetControl("Reset");
        //                bindingdata();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Thông báo");
        //    }
        //}
        #endregion
        public void setControl(string State)
        {
            switch (State)
            {
                case "Reset":

                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnTimKiem.Enabled = true;
                    btnGhi.Enabled = false;
                    btnHuy.Enabled = false;
                    btnKiem.Enabled = false;
                    btnDuongDanANh.Enabled = false;

                    txtID.Enabled = false;
                    txtTenNguoiDung.Enabled = false;
                    cboMaChucVu.Enabled = false;
                    dtpNgaySinh.Enabled = false;
                    rdoNam.Enabled = false;
                    rdoNu.Enabled = false;
                    txtSoCMT.Enabled = false;
                    txtTrangThai.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtSDT.Enabled = false;
                    txtEmail.Enabled = false;
                    dtpThoiHan.Enabled = false;
                    txtDuongDanAnh.Enabled = false;
                    rtbGhiChu.Enabled = false;
                    picNguoiDung.Enabled = false;

                    txtID.Focus();
                    lblError.Text = "";
                    lblStatus.Text = "";
                    break;
                case "Insert":

                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnTimKiem.Enabled = false;
                    btnGhi.Enabled = true;
                    btnHuy.Enabled = true;
                    btnKiem.Enabled = false;
                    btnDuongDanANh.Enabled = true;

                    txtID.Enabled = true;
                    txtTenNguoiDung.Enabled = true;
                    cboMaChucVu.Enabled = true;
                    dtpNgaySinh.Enabled = true;
                    rdoNam.Enabled = true;
                    rdoNu.Enabled = true;
                    txtSoCMT.Enabled = true;
                    txtTrangThai.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtSDT.Enabled = true;
                    txtEmail.Enabled = true;
                    dtpThoiHan.Enabled = true;
                    txtDuongDanAnh.Enabled = true;
                    rtbGhiChu.Enabled = true;
                    picNguoiDung.Enabled = true;
                    txtID.Focus();

                    txtID.Text = "";
                    txtTenNguoiDung.Text = "";
                    cboMaChucVu.Text = "";
                    txtSoCMT.Text = "";
                    txtTrangThai.Text = "";
                    txtDiaChi.Text = "";
                    txtSDT.Text = "";
                    txtEmail.Text = "";
                    txtDuongDanAnh.Text = "";
                    rtbGhiChu.Text = "";
                    //picNguoiDung.Image=null;

                    break;
                case "Update":

                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnTimKiem.Enabled = false;
                    btnGhi.Enabled = true;
                    btnHuy.Enabled = true;
                    btnKiem.Enabled = false;
                    btnDuongDanANh.Enabled = true;

                    txtID.Enabled = true;
                    txtTenNguoiDung.Enabled = true;
                    cboMaChucVu.Enabled = true;
                    dtpNgaySinh.Enabled = true;
                    rdoNam.Enabled = true;
                    rdoNu.Enabled = true;
                    txtSoCMT.Enabled = true;
                    txtTrangThai.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtSDT.Enabled = true;
                    txtEmail.Enabled = true;
                    dtpThoiHan.Enabled = true;
                    txtDuongDanAnh.Enabled = true;
                    rtbGhiChu.Enabled = true;
                    picNguoiDung.Enabled = true;
                    txtID.Focus();
                    break;
                case "Delete":
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnTimKiem.Enabled = true;
                    btnGhi.Enabled = false;
                    btnHuy.Enabled = false;
                    btnKiem.Enabled = false;
                    btnDuongDanANh.Enabled = false;

                    txtID.Enabled = false;
                    txtTenNguoiDung.Enabled = false;
                    cboMaChucVu.Enabled = false;
                    dtpNgaySinh.Enabled = false;
                    rdoNam.Enabled = false;
                    rdoNu.Enabled = false;
                    txtSoCMT.Enabled = false;
                    txtTrangThai.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtSDT.Enabled = false;
                    txtEmail.Enabled = false;
                    dtpThoiHan.Enabled = false;
                    txtDuongDanAnh.Enabled = false;
                    rtbGhiChu.Enabled = false;
                    picNguoiDung.Enabled = false;
                    txtID.Focus();
                    lblError.Text = "";
                    lblStatus.Text = "";
                    break;
                case "Search":
                    break;
                default:
                    break;
            }
        }
        public void loadcbo_MaCV()
        {

            try
            {
                conn = new SqlConnection(ConnectionString);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string query = "SELECT * FROM DM_CHUCVU";
                SqlCommand cmd = new SqlCommand(query, conn);
                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    cboMaChucVu.ValueMember = "MA_CHUCVU";
                    cboMaChucVu.DisplayMember = "MA_CHUCVU";
                    cboMaChucVu.DataSource = ds.Tables[0];
                }
                else
                {
                    cboMaChucVu.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getData()
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "GET_NDUNG";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvNguoiDung.DataSource = ds.Tables[0];
                lblTongSo.Text = "Tổng số : " + (ds.Tables[0].Rows.Count) + " bản ghi";
                txtID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                cboMaChucVu.Text = ds.Tables[0].Rows[0]["TEN_CHUCVU"].ToString();
                txtTenNguoiDung.Text = ds.Tables[0].Rows[0]["TEN_NDUNG"].ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["NGAY_SINH"]);
                txtDuongDanAnh.Text = ds.Tables[0].Rows[0]["DUONG_DAN_ANH"].ToString();
                txtSDT.Text = ds.Tables[0].Rows[0]["SO_DIENTHOAI"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                txtDiaChi.Text = ds.Tables[0].Rows[0]["DIA_CHI"].ToString();
                txtSoCMT.Text = ds.Tables[0].Rows[0]["SO_CMT"].ToString();
                rtbGhiChu.Text = ds.Tables[0].Rows[0]["GHI_CHU"].ToString();
                txtTrangThai.Text = ds.Tables[0].Rows[0]["TRANG_THAI"].ToString();

            }
            else
            {
                grvNguoiDung.DataSource = ds.Tables[0];
                lblTongSo.Text = "Tổng số : 0 bản ghi";
            }
        }
        private void btnDuongDanANh_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtDuongDanAnh.Text = dlg.FileName;
                picNguoiDung.ImageLocation = txtDuongDanAnh.Text.Trim();
                picNguoiDung.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {              
                State = "Insert";
                setControl(State);
                BindingData();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                State = "Update";
                setControl(State);
                BindingData();
           }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var bResult = DeleteData(ref strError);
            if (bResult == true)
            {
                
                lblStatus.Text = "Xóa dữ liệu thành công";
                State = "Delete";
                setControl(State);
                getData();
            }
            else
            {
                lblError.Text = "Xóa dữ liệu không thành công";
            }
        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                byte[] arrImage;
                var arr = txtDuongDanAnh.Text.Split('\\');
                var fileName = arr[arr.Length - 1];
                fileName = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + "_" + fileName;
                File.Copy(txtDuongDanAnh.Text.Trim(), UrlFileUpLoad + fileName);
                Image img = Image.FromFile(UrlFileUpLoad + fileName);

                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    arrImage = ms.ToArray();
                }
                if (State == "Insert")
                {
                    conn = new SqlConnection(ConnectionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "INSERT_QL_NDUNG";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                    cmd.Parameters.AddWithValue("@MA_CHUCVU", cboMaChucVu.Text.Trim());
                    cmd.Parameters.AddWithValue("@TEN_NDUNG", txtTenNguoiDung.Text.Trim());
                    cmd.Parameters.AddWithValue("@DUONG_DAN_ANH", txtDuongDanAnh.Text.Trim());
                    cmd.Parameters.AddWithValue("@NGAY_SINH", dtpNgaySinh.Text.Trim());
                    string GioiTinh = "-1";
                    if (rdoNam.Checked == true)
                    {
                        GioiTinh = "1";
                    }
                    else
                        GioiTinh = "0";
                    cmd.Parameters.AddWithValue("@GIOI_TINH", GioiTinh);
                    cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
                    cmd.Parameters.AddWithValue("@ANH", arrImage);
                    cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@SO_CMT", txtSoCMT.Text.Trim());
                    cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                    cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                    cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                    cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                    cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm Thành Công", "Thông Báo");
                        setControl("Reset");
                        BindingData();
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Thêm Thất Bại", "Thông Báo");
                    }
                }
                if (State == "Update")
                {
                    conn = new SqlConnection(ConnectionString);
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "UPDATE_QL_NDUNG";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                    cmd.Parameters.AddWithValue("@MA_CHUCVU", cboMaChucVu.Text.Trim());
                    cmd.Parameters.AddWithValue("@TEN_NDUNG", txtTenNguoiDung.Text.Trim());
                    cmd.Parameters.AddWithValue("@DUONG_DAN_ANH", txtDuongDanAnh.Text.Trim());
                    cmd.Parameters.AddWithValue("@NGAY_SINH", dtpNgaySinh.Text.Trim());
                    string GioiTinh = "-1";
                    if (rdoNam.Checked == true)
                    {
                        GioiTinh = "1";
                    }
                    else
                        GioiTinh = "0";
                    cmd.Parameters.AddWithValue("@GIOI_TINH", GioiTinh);
                    cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
                    cmd.Parameters.AddWithValue("@ANH", arrImage);
                    cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@SO_CMT", txtSoCMT.Text.Trim());
                    cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                    cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                    cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                    cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                    cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Sửa Thành Công", "Thông Báo");
                        setControl("Reset");
                        BindingData();
                        getData();
                    }
                    else
                    {
                        MessageBox.Show("Sửa Thất Bại", "Thông Báo");
                    }
                }
                
            }
            catch (Exception EX)
            {

                lblError.Text = EX.Message;
            }
        }
        private bool DeleteData(ref string strError)
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string sqlDelete = "DELETE_QL_NDUNG";
            SqlCommand cmd = new SqlCommand(sqlDelete, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
            cmd.Parameters.AddWithValue("@MA_CHUCVU", cboMaChucVu.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@TEN_NDUNG", txtTenNguoiDung.Text.Trim());
            cmd.Parameters.AddWithValue("@NGAY_SINH", dtpNgaySinh.Value);
            string GioiTinh = "-1";
            if (rdoNam.Checked == true)
            {
                GioiTinh = "1";
            }
            else
                GioiTinh = "0";
            cmd.Parameters.AddWithValue("@GIOI_TINH", GioiTinh);
            cmd.Parameters.AddWithValue("@DUONG_DAN_ANH", txtDuongDanAnh.Text.Trim());
            cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
            cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
            cmd.Parameters.AddWithValue("@SO_CMT", txtSoCMT.Text.Trim());
            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
            cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
            cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
            cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
            cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
            cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
            cmd.ExecuteNonQuery();
            return true;
        }
        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            State = "Reset";
            setControl(State);
            getData();
        }

        private void btnTimKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnTimKiem.Enabled = false;
            btnGhi.Enabled = false;
            btnHuy.Enabled = true;
            btnKiem.Enabled = true;
            btnDuongDanANh.Enabled = false;

            txtID.Enabled = false;
            txtTenNguoiDung.Enabled = true;
            cboMaChucVu.Enabled = false;
            dtpNgaySinh.Enabled = false;
            rdoNam.Enabled = false;
            rdoNu.Enabled = false;
            txtSoCMT.Enabled = false;
            txtTrangThai.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
            txtEmail.Enabled = false;
            dtpThoiHan.Enabled = false;
            txtDuongDanAnh.Enabled = false;
            rtbGhiChu.Enabled = false;
            picNguoiDung.Enabled = false;
            txtID.Focus();
            lblError.Text = "";
            lblStatus.Text = "";
            txtTenNguoiDung.Text = "";
            BindingData();
        }

        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string TenND = txtTenNguoiDung.Text;
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_NDUNG " + "WHERE  TEN_NDUNG LIKE N'%" + TenND + "%'", conn);

            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            BindingData();
            grvNguoiDung.DataSource = tb;
            grvNguoiDung.Refresh();
        }

        private void grvNguoiDung_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                lblError.Text = "";
                lblStatus.Text = "";

                GridHitInfo info = gridView1.CalcHitInfo(e.Location);
                if (info.InRowCell)
                {
                    int row = info.RowHandle;
                    GridColumn colum = info.Column;

                    txtID.Text = gridView1.GetRowCellValue(row, "ID").ToString();
                    cboMaChucVu.Text = gridView1.GetRowCellValue(row, "MA_CHUCVU").ToString();
                    txtTenNguoiDung.Text = gridView1.GetRowCellValue(row, "TEN_NDUNG").ToString();
                    txtDuongDanAnh.Text = gridView1.GetRowCellValue(row, "DUONG_DAN_ANH").ToString();
                    txtSDT.Text = gridView1.GetRowCellValue(row, "SO_DIENTHOAI").ToString();
                    txtEmail.Text = gridView1.GetRowCellValue(row, "EMAIL").ToString();
                    dtpNgaySinh.Text = gridView1.GetRowCellValue(row, "NGAY_SINH").ToString();
                    string gioitinh = gridView1.GetRowCellValue(row, "GIOI_TINH").ToString();
                    if (gioitinh == "1")
                    {
                        rdoNam.Checked = true;
                    }
                    else
                    {
                        rdoNu.Checked = true;
                    }

                    txtDiaChi.Text = gridView1.GetRowCellValue(row, "DIA_CHI").ToString();
                    picNguoiDung.Text = gridView1.GetRowCellValue(row, "ANH").ToString();
                    txtSoCMT.Text = gridView1.GetRowCellValue(row, "SO_CMT").ToString();
                    rtbGhiChu.Text = gridView1.GetRowCellValue(row, "GHI_CHU").ToString();
                    txtTrangThai.Text = gridView1.GetRowCellValue(row, "TRANG_THAI").ToString();

                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        public void BindingData()
        {           
            picNguoiDung.DataBindings.Clear();          
            picNguoiDung.DataBindings.Add(new Binding("ImageLocation", ds.Tables[0], "DUONG_DAN_ANH", false, DataSourceUpdateMode.Never));
        }
    }
}
