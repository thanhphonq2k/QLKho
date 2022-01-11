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

    public partial class ucNhaCungCap : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucNhaCungCap _instance;
        public static ucNhaCungCap Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucNhaCungCap();
                return _instance;

            }
        }
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        public ucNhaCungCap()
        {
            InitializeComponent();
            loadform();
            SetControl("Reset");
        }
        #region Public function
        private void SetControl(string state)
        {
            switch (state)
            {
                case "Reset":
                    txtMaNhaCC.Enabled = false;
                    txtNhaCungCap.Enabled = false;
                    txtDiaChi.Enabled = false;
                    txtEmail.Enabled = false;
                    txtSDT.Enabled = false;
                    rtbGhiChu.Enabled = false;
                    dtpThoiHan.Enabled = false;
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
                        if (txtNhaCungCap.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo");
                            txtNhaCungCap.Focus();
                        }
                        else if (txtDiaChi.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập địa chỉ", "Thông báo");
                            txtDiaChi.Focus();
                        }
                        else if (dtpThoiHan.Value == DateTime.Now)
                        {
                            MessageBox.Show("Bạn chưa cung cấp thời hạn", "Thông báo");
                            dtpThoiHan.Focus();
                        }
                        else if (txtEmail.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập Email", "THông báo");
                            txtEmail.Focus();
                        }
                        else if (txtSDT.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập số điện thoại", "Thông báo");
                            txtSDT.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else
                        {
                            string sqlInsert_tk = "INSERT_NHA_CUNGCAP";
                            SqlCommand cmd = new SqlCommand(sqlInsert_tk, conn);
                            cmd.Parameters.AddWithValue("@MA_NHA_CUNGCAP", txtMaNhaCC.Text.Trim());
                            cmd.Parameters.AddWithValue("@TEN_NHA_CUNGCAP", txtNhaCungCap.Text.Trim());
                            cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                            cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
                            cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());                          
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
                                txtNhaCungCap.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Không thể thêm!", "Thông báo");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Thông báo");
                    }
                    break;
                case "Update":
                    try
                    {
                        if (txtMaNhaCC.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập MÃ nhà cung cấp", "Thông báo");
                            txtMaNhaCC.Focus();
                        }
                        if (txtNhaCungCap.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo");
                            txtNhaCungCap.Focus();
                        }
                        else if (txtDiaChi.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập địa chỉ", "Thông báo");
                            txtDiaChi.Focus();
                        }
                        else if (dtpThoiHan.Value == DateTime.Now)
                        {
                            MessageBox.Show("Bạn chưa cung cấp thời hạn", "Thông báo");
                            dtpThoiHan.Focus();
                        }
                        else if (txtEmail.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập Email", "THông báo");
                            txtEmail.Focus();
                        }
                        else if (txtSDT.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập số điện thoại", "Thông báo");
                            txtSDT.Focus();
                        }
                        else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                        {
                            MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                            txtTrangThai.Focus();
                        }
                        else
                        {
                            string sqlUpdate_tk = "UPDATE_NHA_CUNGCAP";
                            SqlCommand cmd = new SqlCommand(sqlUpdate_tk, conn);
                            cmd.Parameters.AddWithValue("@MA_NHA_CUNGCAP", txtMaNhaCC.Text.Trim());
                            cmd.Parameters.AddWithValue("@TEN_NHA_CUNGCAP", txtNhaCungCap.Text.Trim());
                            cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                            cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
                            cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
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
                                MessageBox.Show("Sửa thành công!", "Thông báo");
                                clear();
                                txtNhaCungCap.Focus();
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
                        if (txtMaNhaCC.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa chọn thông tin xóa", "Thông báo");
                            txtMaNhaCC.Focus();
                        }
                        if (XtraMessageBox.Show("Bạn có chắc chắc muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (txtMaNhaCC.Text.Trim() != "")
                            {
                                string sqlDelete = "DELETE_DM_NHA_CUNGCAP";
                                SqlCommand cmd = new SqlCommand(sqlDelete, conn);
                                cmd.Parameters.AddWithValue("@MA_NHA_CUNGCAP", txtMaNhaCC.Text.Trim());
                                cmd.Parameters.AddWithValue("@TEN_NHA_CUNGCAP", txtNhaCungCap.Text.Trim());
                                cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                                cmd.Parameters.AddWithValue("@SO_DIENTHOAI", txtSDT.Text.Trim());
                                cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text.Trim());
                                cmd.Parameters.AddWithValue("@GHI_CHU", rtbGhiChu.Text.Trim());
                                cmd.Parameters.AddWithValue("@THOI_HAN", dtpThoiHan.Value);
                                cmd.Parameters.AddWithValue("@TRANG_THAI", txtTrangThai.Text.Trim());
                                cmd.Parameters.AddWithValue("@NGUOI_TAO", "Linh");
                                cmd.Parameters.AddWithValue("@NGAY_TAO", DateTime.Now);
                                cmd.Parameters.AddWithValue("@NGUOI_SUA", "Linh");
                                cmd.Parameters.AddWithValue("@NGAY_SUA", DateTime.Now);
                                cmd.CommandType = CommandType.StoredProcedure;

                                var result = cmd.ExecuteNonQuery();
                                
                                MessageBox.Show("Xóa thành công!", "Thông báo");
                                
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
                default:
                    break;
            }
        }
        private void clear()
        {
            txtMaNhaCC.Text = "";
            txtNhaCungCap.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            rtbGhiChu.Text = "";
            txtTrangThai.Text = "";
            dtpThoiHan.Value = DateTime.Now;
        }
        public void loadform()
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT * FROM DM_NHA_CUNGCAP WHERE TRANG_THAI=1 ";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvNhaCungCap.DataSource = ds.Tables[0];
            }
            else
            {
                grvNhaCungCap.DataSource = null;

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
            txtNhaCungCap.DataBindings.Clear();
            txtMaNhaCC.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            dtpThoiHan.DataBindings.Clear();
            rtbGhiChu.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();


            txtMaNhaCC.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_NHA_CUNGCAP", false, DataSourceUpdateMode.Never));
            txtNhaCungCap.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_NHA_CUNGCAP", false, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", ds.Tables[0], "DIA_CHI", false, DataSourceUpdateMode.Never));
            txtEmail.DataBindings.Add(new Binding("Text", ds.Tables[0], "EMAIL", false, DataSourceUpdateMode.Never));
            txtSDT.DataBindings.Add(new Binding("Text", ds.Tables[0], "SO_DIENTHOAI", false, DataSourceUpdateMode.Never));
            rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
            dtpThoiHan.DataBindings.Add(new Binding("Text", ds.Tables[0], "THOI_HAN", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));

        }
        #endregion

        #region Even       
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaNhaCC.Enabled = true;
            txtNhaCungCap.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            txtSDT.Enabled = true;
            rtbGhiChu.Enabled = true;
            dtpThoiHan.Enabled = true;
            txtTrangThai.Enabled = true;


            btnThem.Enabled = false;
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
            txtMaNhaCC.Enabled = false;
            txtNhaCungCap.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            txtSDT.Enabled = true;
            rtbGhiChu.Enabled = true;
            dtpThoiHan.Enabled = true;
            txtTrangThai.Enabled = true;


            btnThem.Enabled = false;
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
            txtMaNhaCC.Enabled = true;
            txtNhaCungCap.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;
            txtSDT.Enabled = true;
            rtbGhiChu.Enabled = true;
            dtpThoiHan.Enabled = true;
            txtTrangThai.Enabled = true;


            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnGhi.Enabled = true;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = true;
            btnKiem.Enabled = false;

            SetControl("Delete");
            SetControl("Reset");
            clear();
            //txtMaNhaCC.Text = "";
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
        private void btnTimKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaNhaCC.Enabled = true;
            txtNhaCungCap.Enabled = true;
            txtDiaChi.Enabled = false;
            txtEmail.Enabled = false;
            txtSDT.Enabled = false;
            rtbGhiChu.Enabled = false;
            dtpThoiHan.Enabled = false;
            txtTrangThai.Enabled = true;


            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnGhi.Enabled = false;
            btnHuy.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = true;
            
            clear();
            
        }
        private void btnHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            clear();
            loadform();
            state = "Reset";
            SetControl(state);
        }
        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string MaNhaCC = txtMaNhaCC.Text;
            string NhaCungCap = txtNhaCungCap.Text;
            string DiaChi = txtDiaChi.Text;
            string Email = txtEmail.Text;
            string SDT = txtSDT.Text;
            string TrangThai = txtTrangThai.Text;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from DM_NHA_CUNGCAP " + "WHERE MA_NHA_CUNGCAP LIKE N'%" + MaNhaCC + "%' and " +
                                                                        "TEN_NHA_CUNGCAP LIKE N'%" + NhaCungCap + "%' AND " +
                                                                        "DIA_CHI LIKE N'%" + DiaChi + "%'AND " +
                                                                        "TRANG_THAI LIKE N'%" + TrangThai + "%' AND " +
                                                                        "SO_DIENTHOAI LIKE N'%" + SDT + "%'", conn);
                                                                    

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
            grvNhaCungCap.DataSource = tb;
            grvNhaCungCap.Refresh();
        }
        #endregion
    }
}
