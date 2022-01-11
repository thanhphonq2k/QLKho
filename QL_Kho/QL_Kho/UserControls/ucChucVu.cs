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
    public partial class ucChucVu : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucChucVu _instance;
        public static ucChucVu Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucChucVu();
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
        public ucChucVu()
        {
            InitializeComponent();            
            loadform();
            loadcbo_quyen();
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
            string query = "SELECT * FROM DM_CHUCVU WHERE TRANG_THAI='1'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvChucVu.DataSource = ds.Tables[0];
            }
            else
            {
                grvChucVu.DataSource = null;

            }

            banghi();
            bindingdata();
        }
        public void loadcbo_quyen()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            #region
            //conn = new SqlConnection(ConnectionString);
            //if (conn.State == ConnectionState.Closed)
            //{
            //    conn.Open();
            //}
            //string query = "SELECT  ID,''AS	ID UNION ALL SELECT A.ID,A.TEN_QUYEN FROM  dbo.QL_QUYEN A";
            //SqlCommand cmd = new SqlCommand(query, conn);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //ds = new DataSet();
            //da.Fill(ds);
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    cboID_quyen.DataSource = ds.Tables[0];
            //    cboID_quyen.DisplayMember = "ID";
            //    cboID_quyen.ValueMember = "ID";
            //}
            //else
            //{
            //    cboID_quyen.DataSource = null;
            //}
            #endregion
            SqlCommand cmd = new SqlCommand("Select * from QL_QUYEN", conn);
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
            r["MA_QUYEN"] = "";
            r["TEN_QUYEN"] = "---chọn quyền----";
            tb.Rows.InsertAt(r, 0);
            cboMaQuyen.DataSource = tb;
            cboMaQuyen.DisplayMember = "TEN_QUYEN";
            cboMaQuyen.ValueMember = "MA_QUYEN";
        }
        public void banghi()
        {
            lblTongSo.Text = "Tổng số: " + ds.Tables[0].Rows.Count.ToString() + " bản ghi";
        }
        public void clear()
        {
            txtMaChucVu.Text = "";
            txtTenChucVu.Text = "";
            rtbGhiChu.Text = "";
            rtbMoTa.Text = "";          
            txtTrangThai.Text = "";
            cboMaQuyen.Text = "";
        }
        public void SetControl(string state)
        {
            switch (state)
            {
                case "Reset":
                    {
                        btnThem.Enabled = true;
                        btnSua.Enabled = true;
                        btnXoa.Enabled = true;
                        btnHuy.Enabled = false;
                        btnGhi.Enabled = false;
                        btnTimKiem.Enabled = true;
                        btnKiem.Enabled = false;

                        txtTenChucVu.Enabled = false;
                        txtMaChucVu.Enabled = false;
                        rtbGhiChu.Enabled = false;
                        rtbMoTa.Enabled = false;
                        txtTrangThai.Enabled = false;
                        cboMaQuyen.Enabled = false;
                    }
                    break;

                case "Insert":
                    {
                        try
                        {                          
                             if (txtTenChucVu.Text.Trim() == "")
                            {
                                MessageBox.Show("Chưa nhập tên chức vụ", "Thông báo");
                                txtTenChucVu.Focus();
                            }
                            else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                            {
                                MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                                txtTrangThai.Focus();
                            }
                            else if (txtTenChucVu.Text.Trim() != "" || txtTenChucVu.Text != "")
                            {
                                string sqlInsert = "INSERT_DM_CHUCVU";
                                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                                cmd.Parameters.AddWithValue("@MA_CHUCVU", txtMaChucVu.Text.Trim());
                                cmd.Parameters.AddWithValue("@MA_QUYEN", cboMaQuyen.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@TEN_CHUCVU", txtTenChucVu.Text.Trim());
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
                                    //load_insert_table();
                                    loadform();
                                    //clear();
                                    //txtTenChucVu.Text = "";
                                    //txtTenChucVu.Focus();
                                    MessageBox.Show("Thêm thành công", "Thông báo");
                                }
                                else
                                {
                                    MessageBox.Show("Thêm thất bại", "Thông báo");
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
                    }
                    break;
                case "Update":
                    {
                        try
                        {
                            
                             if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                            {
                                MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                                txtTrangThai.Focus();
                            }
                            else if (txtTenChucVu.Text.Trim() == "")
                            {
                                MessageBox.Show("Chưa nhập tên chức vụ", "Thông báo");
                                txtTenChucVu.Focus();
                                return;
                            }
                            if (txtTenChucVu.Text.Trim() != "" || txtTenChucVu.Text != "")
                            {
                                string sqlInsert = "UPDATE_DM_CHUCVU";
                                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                                cmd.Parameters.AddWithValue("@MA_CHUCVU", txtMaChucVu.Text.Trim());
                                cmd.Parameters.AddWithValue("@MA_QUYEN", cboMaQuyen.SelectedValue.ToString());
                                cmd.Parameters.AddWithValue("@TEN_CHUCVU", txtTenChucVu.Text.Trim());
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
                                    txtTenChucVu.Focus();
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
                    }
                    break;
                case "Delete":
                    {
                        try
                        {
                            
                            if (XtraMessageBox.Show("Bạn có chắc chắc muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {


                                if (txtMaChucVu.Text.Trim() != "")
                                {
                                    string sqlDelete = "DELETE_DM_CHUCVU";
                                    SqlCommand cmd = new SqlCommand(sqlDelete, conn);
                                    cmd.Parameters.AddWithValue("@MA_CHUCVU", txtMaChucVu.Text.Trim());
                                    cmd.Parameters.AddWithValue("@MA_QUYEN", cboMaQuyen.SelectedValue.ToString());
                                    cmd.Parameters.AddWithValue("@TEN_CHUCVU", txtTenChucVu.Text.Trim());
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
                    }
                    break;
            }
                
        }
        public void bindingdata()
        {
            txtTenChucVu.DataBindings.Clear();
            txtMaChucVu.DataBindings.Clear();
            rtbGhiChu.DataBindings.Clear();
            rtbMoTa.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();
            cboMaQuyen.DataBindings.Clear();

            txtMaChucVu.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_CHUCVU", false, DataSourceUpdateMode.Never));
            txtTenChucVu.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_CHUCVU", false, DataSourceUpdateMode.Never));
            rtbMoTa.DataBindings.Add(new Binding("Text", ds.Tables[0], "MO_TA", false, DataSourceUpdateMode.Never));
            rtbGhiChu.DataBindings.Add(new Binding("Text", ds.Tables[0], "GHI_CHU", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));
            cboMaQuyen.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_QUYEN", false, DataSourceUpdateMode.Never));

        }
        #endregion

        #region Even
        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            txtMaChucVu.Enabled = true;
            txtTenChucVu.Enabled = true;
            rtbMoTa.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;
            cboMaQuyen.Enabled = true;

            clear();
            loadcbo_quyen();
            state = "Insert";
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

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;
            btnTimKiem.Enabled = true;
            btnKiem.Enabled = false;

            txtMaChucVu.Enabled = true;
            txtTenChucVu.Enabled = true;
            rtbGhiChu.Enabled = true;
            rtbMoTa.Enabled = true;
            txtTrangThai.Enabled = true;
            cboMaQuyen.Enabled = true;

            SetControl("Delete");
            SetControl("Reset");
            loadcbo_quyen();
            clear();
            loadform();
        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;
            btnTimKiem.Enabled = false;
            btnKiem.Enabled = false;

            txtMaChucVu.Enabled = true;
            txtTenChucVu.Enabled = true;
            rtbMoTa.Enabled = true;
            rtbGhiChu.Enabled = true;
            txtTrangThai.Enabled = true;
            cboMaQuyen.Enabled = true;

            
            state = "Update";
        }

        private void btnTimKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            txtMaChucVu.Enabled = true;
            txtTenChucVu.Enabled = true;
            rtbMoTa.Enabled = false;
            rtbGhiChu.Enabled = false;
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

        private void btnKiem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string MaChucVu = txtMaChucVu.Text;
            string ChucVu = txtTenChucVu.Text;
            string TrangThai = txtTrangThai.Text;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from DM_CHUCVU " + "WHERE MA_CHUCVU LIKE N'%" + MaChucVu + "%' and " +
                                                                        "TEN_CHUCVU LIKE N'%" + ChucVu + "%' AND " +
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
            grvChucVu.DataSource = tb;
            grvChucVu.Refresh();
        }
        #endregion
    }
}
