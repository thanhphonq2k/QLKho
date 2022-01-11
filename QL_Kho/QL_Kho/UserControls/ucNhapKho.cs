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
    public partial class ucNhapKho : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucNhapKho _instance;
        public static ucNhapKho Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucNhapKho();
                return _instance;

            }
        }
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucNhapKho()
        {
            InitializeComponent();
            loadform();
            loadcbo_ID_nguoi();
            loadcbo_MaHH();
            SetControl("Reset");
        }


        private void clear()
        {
            txtID.Text = "";
            cboID_NDung.Text = "";
            cboMaHH.Text = "";
            txtSoLuong.Text = "";
            dtpNgayNhapKho.Value = DateTime.Now;
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
            string query = "GET_NHAPKHO ";
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
        private void SetControl(string state)
        {
            switch(state)
            {
                case "Reset":
                    txtID.Enabled = false;
                    cboID_NDung.Enabled = false;
                    cboMaHH.Enabled = false;
                    dtpNgayNhapKho.Enabled = false;
                    txtSoLuong.Enabled = false;
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
                         if (cboMaHH.SelectedValue == "--Chọn mã hàng hóa--")
                        {
                            MessageBox.Show("Chưa chọn mã hàng hóa", "Thông báo");
                            cboMaHH.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if (dtpNgayNhapKho.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                            dtpNgayNhapKho.Focus();
                        }
                        else if (txtSoLuong.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                            txtSoLuong.Focus();
                        }
                        else if (cboID_NDung.SelectedValue == "--Chọn mã nhà cung cấp--")
                        {
                            MessageBox.Show("Chưa chọn mã nhà cung cấp", "Thông báo");
                            cboID_NDung.Focus();
                        }
                        else if (txtID.Text.Trim() != "" || cboID_NDung.Text.Trim() != "" || cboMaHH.Text.Trim() != "" || txtSoLuong.Text != "")
                        {
                            string sqlInsert = "INSERT_TT_NHAPKHO";
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            cmd.Parameters.AddWithValue("@MA_HANGHOA", cboMaHH.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@ID_NDUNG", cboID_NDung.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@NGAY_NHAP_KHO", dtpNgayNhapKho.Value);
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
                        else if (dtpNgayNhapKho.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                            dtpNgayNhapKho.Focus();
                        }
                        else if (txtSoLuong.Text.Trim() == "")
                        {
                            MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                            txtSoLuong.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else if (cboID_NDung.SelectedValue == "--Chọn mã nhà cung cấp--")
                        {
                            MessageBox.Show("Chưa chọn mã nhà cung cấp", "Thông báo");
                            cboID_NDung.Focus();
                        }
                        else if (txtID.Text.Trim() != "" || cboID_NDung.Text.Trim() != "" || cboMaHH.Text.Trim() != "" || txtSoLuong.Text != "")
                        {
                            string sqlInsert = "UPDATE_TT_NHAPKHO";
                            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                            cmd.Parameters.AddWithValue("@MA_HANGHOA", cboMaHH.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@ID_NDUNG", cboID_NDung.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@NGAY_NHAP_KHO", dtpNgayNhapKho.Value);
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
                                string sqlDelete = "DELETE_TT_NHAPKHO";
                                SqlCommand cmd = new SqlCommand(sqlDelete, conn);
                                cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                                cmd.Parameters.AddWithValue("@MA_HANGHOA", cboMaHH.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@ID_NDUNG", cboID_NDung.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@NGAY_NHAP_KHO", dtpNgayNhapKho.Value);
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
                        MessageBox.Show(ex.Message, "Thông báo");
                    }
                    break;
            }
        }
        public void banghi()
        {
            lblTongSo.Text = "Tổng số bản ghi: " + ds.Tables[0].Rows.Count.ToString() + " bản ghi";
        }
        public void bindingdata()
        {
            txtID.DataBindings.Clear();
            cboID_NDung.DataBindings.Clear();
            cboMaHH.DataBindings.Clear();
            txtSoLuong.DataBindings.Clear();
            dtpNgayNhapKho.DataBindings.Clear();
            rtbGhiChu.DataBindings.Clear();           
            txtTrangThai.DataBindings.Clear();


            txtID.DataBindings.Add(new Binding("Text", ds.Tables[0], "ID", false, DataSourceUpdateMode.Never));
            cboID_NDung.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_NDUNG", false, DataSourceUpdateMode.Never));
            cboMaHH.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_HANGHOA", false, DataSourceUpdateMode.Never));
            txtSoLuong.DataBindings.Add(new Binding("Text", ds.Tables[0], "SO_LUONG", false, DataSourceUpdateMode.Never));
            rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
            dtpNgayNhapKho.DataBindings.Add(new Binding("Text", ds.Tables[0], "NGAY_NHAP_KHO", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));

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
            r["TEN_NDUNG"] = "---chọn ----";
            tb.Rows.InsertAt(r, 0);
            cboID_NDung.DataSource = tb;
            cboID_NDung.DisplayMember = "TEN_NDUNG";
            cboID_NDung.ValueMember = "ID";
            cboID_NDung.Refresh();
        }
        public void loadcbo_MaHH()
        {
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

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            txtID.Enabled = false;
            cboID_NDung.Enabled = true;
            cboMaHH.Enabled = true;
            dtpNgayNhapKho.Enabled = true;
            txtSoLuong.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;


            clear();
            loadcbo_ID_nguoi();
            loadcbo_MaHH();
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

            txtID.Enabled = false;
            cboID_NDung.Enabled = true;
            cboMaHH.Enabled = true;
            dtpNgayNhapKho.Enabled = true;
            txtSoLuong.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;

            state = "Update";
            loadform();
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

            txtID.Enabled = true;
            cboID_NDung.Enabled = true;
            cboMaHH.Enabled = true;
            dtpNgayNhapKho.Enabled = true;
            txtSoLuong.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;

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

            txtID.Enabled = false;
            cboID_NDung.Enabled = false;
            cboMaHH.Enabled = false;
            dtpNgayNhapKho.Enabled = true;
            txtSoLuong.Enabled = false;
            rtbGhiChu.Enabled = false;
            txtTrangThai.Enabled = true;

            clear();
            
        }

        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            string ID_NDung = cboID_NDung.Text;
            string MaHangHoa = cboMaHH.Text;
            string TrangThai = txtTrangThai.Text;
            DateTime NgayNhap = Convert.ToDateTime(dtpNgayNhapKho.Text);
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from QL_TT_NHAPKHO " + "WHERE  ID_NDUNG LIKE N'%" + ID_NDung + "%' and " +
                                                                        /*"MA_HANGHOA LIKE N'%" + MaHangHoa + "%' AND " +*/
                                                                        "TRANG_THAI LIKE N'%" + TrangThai + "%'", conn);
            /*"SO_DIENTHOAI LIKE N'%" + SDT + "%' AND " +*/


            cmd.ExecuteNonQuery();
            //buoc 3: do du lieu vào DataAdater từ cmd
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //buoc 4: do du lieu tu dataAdap
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            conn.Close();
            //buoc 5: do du lieu tu tb vao dataGridView'
            //"(NGAY_SUA between '" + NgaySua + "' AND getdate()) AND " +
            //    "EMAIL LIKE N'%" + Email + "%'
            grvNhapKho.DataSource = tb;
            grvNhapKho.Refresh();
            
        }
    }
}
