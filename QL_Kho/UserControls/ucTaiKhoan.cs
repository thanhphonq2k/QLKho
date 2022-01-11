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
    public partial class ucTaiKhoan : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucTaiKhoan _instance;
        public static ucTaiKhoan Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucTaiKhoan();
                return _instance;

            }
        }
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucTaiKhoan()
        {
            InitializeComponent();
            loadform();
            loadcbo_ID_nguoi();
            SetControl("Reset");
        }
        #region public functions
        public void loadform()
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "GET_TAIKHOAN";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvTaiKhoan.DataSource = ds.Tables[0];
            }
            else
            {
                grvTaiKhoan.DataSource = null;

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
            cboID_NDung.DataBindings.Clear();
            txtTenTaiKhoan.DataBindings.Clear();
            txtMatKhau.DataBindings.Clear();
            dtpNgayHieuLuc.DataBindings.Clear();
            rtbGhiChu.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();


            txtID.DataBindings.Add(new Binding("Text", ds.Tables[0], "ID", false, DataSourceUpdateMode.Never));
            cboID_NDung.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_NDUNG", false, DataSourceUpdateMode.Never));
            txtTenTaiKhoan.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_TKHOAN", false, DataSourceUpdateMode.Never));
            txtMatKhau.DataBindings.Add(new Binding("Text", ds.Tables[0], "MAT_KHAU", false, DataSourceUpdateMode.Never));
            rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
            dtpNgayHieuLuc.DataBindings.Add(new Binding("Text", ds.Tables[0], "NGAY_HLUC", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));

        }
        private void clear()
        {
            txtID.Text = "";
            cboID_NDung.Text = "";
            txtTenTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            dtpNgayHieuLuc.Value = DateTime.Now;
            rtbGhiChu.Text = "";
            txtTrangThai.Text = "";
        }
        public void loadcbo_ID_nguoi()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_NDUNG", conn);
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
            r["ID"] = "";
            r["TEN_NDUNG"] = "---chọn người dùng----";
            tb.Rows.InsertAt(r, 0);
            cboID_NDung.DataSource = tb;
            cboID_NDung.DisplayMember = "TEN_NDUNG";
            cboID_NDung.ValueMember = "ID";
            
        }
        private void SetControl(string state)
        {
            switch (state)
            {
                case "Reset":
                    txtID.Enabled = false;
                    cboID_NDung.Enabled = false;
                    txtTenTaiKhoan.Enabled = false;
                    dtpNgayHieuLuc.Enabled = false;
                    txtMatKhau.Enabled = false;
                    rtbGhiChu.Enabled = false;
                    txtTrangThai.Enabled = false;

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
                        if (txtTenTaiKhoan.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập tên tài khoản", "Thông báo");
                            txtTenTaiKhoan.Focus();
                        }
                        else if (dtpNgayHieuLuc.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập ngày hiệu lực", "Thông báo");
                            dtpNgayHieuLuc.Focus();
                        }
                        else if (txtMatKhau.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập mật khẩu", "Thông báo");
                            txtMatKhau.Focus();
                        }
                        else if (cboID_NDung.SelectedValue == "--Chọn người dùng--")
                        {
                            MessageBox.Show("Chưa chọn người dùng", "Thông báo");
                            cboID_NDung.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if ( txtTenTaiKhoan.Text.Trim() != "" || txtMatKhau.Text != "")
                        {
                            string sqlInsert = "INSERT_TAIKHOAN";
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            cmd.Parameters.AddWithValue("@ID_NDUNG", cboID_NDung.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@TEN_TKHOAN", txtTenTaiKhoan.Text.Trim());
                            cmd.Parameters.AddWithValue("@MAT_KHAU", txtMatKhau.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGAY_HLUC", dtpNgayHieuLuc.Value);
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
                        if (txtTenTaiKhoan.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập tên tài khoản", "Thông báo");
                            txtTenTaiKhoan.Focus();
                        }
                        else if (dtpNgayHieuLuc.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập ngày hiệu lực", "Thông báo");
                            dtpNgayHieuLuc.Focus();
                        }
                        else if (txtMatKhau.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập mật khẩu", "Thông báo");
                            txtMatKhau.Focus();
                        }
                        else if (cboID_NDung.SelectedValue == "--Chọn người dùng--")
                        {
                            MessageBox.Show("Chưa chọn người dùng", "Thông báo");
                            cboID_NDung.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if ( txtTenTaiKhoan.Text.Trim() != "" || txtMatKhau.Text != "")
                        {
                            string sqlInsert = "UPDATE_TAIKHOAN";
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                            cmd.Parameters.AddWithValue("@ID_NDUNG", cboID_NDung.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@TEN_TKHOAN", txtTenTaiKhoan.Text.Trim());
                            cmd.Parameters.AddWithValue("@MAT_KHAU", txtMatKhau.Text.Trim());
                            cmd.Parameters.AddWithValue("@NGAY_HLUC", dtpNgayHieuLuc.Value);
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
                                clear();
                                txtTenTaiKhoan.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Sửa thất bại", "Thông báo");
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Hãy đảm bảo là bạn nhập đầy đủ thồng tin!", "Thông báo");
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
                                string sqlInsert = "DELETE_TAIKHOAN";
                                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                                cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                                cmd.Parameters.AddWithValue("@ID_NDUNG", cboID_NDung.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@TEN_TKHOAN", txtTenTaiKhoan.Text.Trim());
                                cmd.Parameters.AddWithValue("@MAT_KHAU", txtMatKhau.Text.Trim());
                                cmd.Parameters.AddWithValue("@NGAY_HLUC", dtpNgayHieuLuc.Value);
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
                                    MessageBox.Show("Xóa thành công", "Thông báo");
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
                        MessageBox.Show(ex.Message, "Thông báo");
                    }
                    break;
            }
        }
        #endregion

        #region Even
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtID.Enabled = true;
            cboID_NDung.Enabled = true;
            txtTenTaiKhoan.Enabled = true;
            dtpNgayHieuLuc.Enabled = true;
            txtMatKhau.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            clear();
            loadcbo_ID_nguoi();
            state = "Insert";
        }
        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtID.Enabled = false;
            cboID_NDung.Enabled = true;
            txtTenTaiKhoan.Enabled = true;
            dtpNgayHieuLuc.Enabled = true;
            txtMatKhau.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            //loadcbo_ID_nguoi();
            state = "Update";
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtID.Enabled = true;
            cboID_NDung.Enabled = true;
            txtTenTaiKhoan.Enabled = true;
            dtpNgayHieuLuc.Enabled = true;
            txtMatKhau.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;

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
            cboID_NDung.Enabled = false;
            txtTenTaiKhoan.Enabled = true;
            rtbGhiChu.Enabled = false;
            txtTrangThai.Enabled = false;
            txtMatKhau.Enabled = false;
            dtpNgayHieuLuc.Enabled = false;

            btnThem.Enabled = false;
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
            string TenTaiKhoan = txtTenTaiKhoan.Text;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_TAIKHOAN " + "WHERE TEN_TKHOAN LIKE N'%" + TenTaiKhoan + "%'", conn);

            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            //buoc 5: do du lieu tu tb vao dataGridView
            grvTaiKhoan.DataSource = tb;
            grvTaiKhoan.Refresh();
        }
        #endregion
    }
}
