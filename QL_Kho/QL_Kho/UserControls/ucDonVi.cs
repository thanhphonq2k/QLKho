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
    public partial class ucDonVi : DevExpress.XtraEditors.XtraUserControl
    {
        #region Variables
        string state = "";
        SqlConnection conn;
        public static DataSet ds;
        public static string ConnectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=QL_KHO;User ID=sa;Password= 123";
        #endregion
        private static ucDonVi _instance;
        public static ucDonVi Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucDonVi();
                return _instance;
            }
        }
        public ucDonVi()
        {
            InitializeComponent();
            loadform();
            SetControl("Reset");
        }
        #region Public Function
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

                        txtTenDonVi.Enabled = false;
                        txtMaDonVi.Enabled = false;
                        txtDiaChi.Enabled = false;
                        rtbMoTa.Enabled = false;
                        txtTrangThai.Enabled = false;
                    }
                    break;
                case "Insert":
                    {
                        try
                        {
                            if (txtTenDonVi.Text.Trim() == "")
                            {
                                MessageBox.Show("Chưa nhập tên đơn vị", "Thông báo");
                                txtTenDonVi.Focus();
                            }
                            if (txtMaDonVi.Text.Trim() == "")
                            {
                                MessageBox.Show("Chưa chọn mã loại đơn vị", "Thông báo");
                                txtMaDonVi.Focus();
                            }
                            if (txtDiaChi.Text.Trim() == "")
                            {
                                MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                                txtDiaChi.Focus();
                            }
                            else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                            {
                                MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                                txtTrangThai.Focus();
                            }
                            else if (txtTenDonVi.Text.Trim() != "" || txtDiaChi.Text.Trim() != "")
                            {

                                string sqlInsert = "INSERT_DM_DONVI";
                                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                                cmd.Parameters.AddWithValue("@MA_DONVI", txtMaDonVi.Text.Trim());
                                cmd.Parameters.AddWithValue("@TEN_DONVI", txtTenDonVi.Text.Trim());
                                cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                                cmd.Parameters.AddWithValue("@MO_TA", rtbMoTa.Text.Trim());
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
                                    clear();
                                    //txtTenDonVi.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("Thêm thất bại", "Thông báo");
                                }
                                loadform();
                                clear();

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
                            if (txtTenDonVi.Text.Trim() == "")
                            {
                                MessageBox.Show("Chưa nhập tên đơn vị", "Thông báo");
                                txtTenDonVi.Focus();
                                return;
                            }
                            else if (txtDiaChi.Text.Trim() == "")
                            {
                                MessageBox.Show("Chưa nhập địa chỉ", "Thông báo");
                                txtDiaChi.Focus();
                                return;
                            }
                            else if (txtTrangThai.Text.Trim() != "1" && txtTrangThai.Text.Trim() != "0")
                            {
                                MessageBox.Show("Trạng thái chỉ nhập 1 hoặc 0.", "Thông báo");
                                txtTrangThai.Focus();
                            }
                            else if (txtTenDonVi.Text.Trim() != "" || txtDiaChi.Text.Trim() != "")
                            {

                                string sqlInsert = "UPDATE_DM_DONVI";
                                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@TEN_DONVI", txtTenDonVi.Text.Trim());
                                cmd.Parameters.AddWithValue("@MA_DONVI", txtMaDonVi.Text.Trim());
                                cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                                cmd.Parameters.AddWithValue("@MO_TA", rtbMoTa.Text.Trim());
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
                            if (txtTrangThai.Text.Trim() == "")
                            {
                                MessageBox.Show("Chưa chọn thông tin cần xóa", "Thông báo");
                                txtTrangThai.Focus();
                                return;
                            }
                            else if (XtraMessageBox.Show("Bạn có chắc chắc muốn xóa", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                if (txtMaDonVi.Text.Trim() != "")
                                {
                                    string sqlDelete = "DELETE_DM_DONVI";
                                    SqlCommand cmd = new SqlCommand(sqlDelete, conn);
                                    cmd.Parameters.AddWithValue("@MA_DONVI", txtMaDonVi.Text.Trim());
                                    cmd.Parameters.AddWithValue("@TEN_DONVI", txtTenDonVi.Text.Trim());
                                    cmd.Parameters.AddWithValue("@DIA_CHI", txtDiaChi.Text.Trim());
                                    cmd.Parameters.AddWithValue("@MO_TA", rtbMoTa.Text.Trim());
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
                        clear();
                        loadform();
                    }
                    break;
            }
        }
        //load du lieu len table
        public void loadform()
        {
            conn = new SqlConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT * FROM dbo.DM_DONVI WHERE TRANG_THAI='1'";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                grvDonVi.DataSource = ds.Tables[0];
            }
            else
            {
                grvDonVi.DataSource = null;

            }
            banghi();
            bindingdata();


        }
        public void banghi()
        {
            lblTongSo.Text = "Tổng số: " + ds.Tables[0].Rows.Count.ToString() + " bản ghi";
        }
        //bấm chuột hiện thị lên các ô text
        public void bindingdata()
        {
            txtMaDonVi.DataBindings.Clear();
            txtTenDonVi.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            rtbMoTa.DataBindings.Clear();
            txtTrangThai.DataBindings.Clear();


            txtMaDonVi.DataBindings.Add(new Binding("Text", ds.Tables[0], "MA_DONVI", false, DataSourceUpdateMode.Never));
            txtTenDonVi.DataBindings.Add(new Binding("Text", ds.Tables[0], "TEN_DONVI", false, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", ds.Tables[0], "DIA_CHI", false, DataSourceUpdateMode.Never));
            rtbMoTa.DataBindings.Add(new Binding("Text", ds.Tables[0], "MO_TA", false, DataSourceUpdateMode.Never));
            txtTrangThai.DataBindings.Add(new Binding("Text", ds.Tables[0], "TRANG_THAI", false, DataSourceUpdateMode.Never));
        }
        public void clear()
        {
            txtMaDonVi.Text = "";
            txtTenDonVi.Text = "";
            txtDiaChi.Text = "";
            rtbMoTa.Text = "";
            txtTrangThai.Text = "";


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

            txtMaDonVi.Enabled = true;
            txtTenDonVi.Enabled = true;
            txtDiaChi.Enabled = true;
            rtbMoTa.Enabled = true;
            txtTrangThai.Enabled = true;
            

            clear();
            //Load_cbo();
            state = "Insert";
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

            txtMaDonVi.Enabled = false;
            txtTenDonVi.Enabled = true;
            txtDiaChi.Enabled = true;
            rtbMoTa.Enabled = true;
            txtTrangThai.Enabled = true;
            

            //Load_cbo();
            state = "Update";
        }
        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnHuy.Enabled = true;
            btnGhi.Enabled = true;


            txtMaDonVi.Enabled = true;
            txtTenDonVi.Enabled = true;
            txtDiaChi.Enabled = true;
            rtbMoTa.Enabled = true;
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
            txtMaDonVi.Enabled = true;
            txtTenDonVi.Enabled = true;
            txtDiaChi.Enabled = true;
            rtbMoTa.Enabled = false;
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
            string MaDonVi = txtMaDonVi.Text;
            string DonVi = txtTenDonVi.Text;
            string DiaChi = txtDiaChi.Text;
            string TrangThai = txtTrangThai.Text;
            
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from DM_DONVI " + "WHERE MA_DONVI LIKE N'%" + MaDonVi + "%' and " +
                                                                        "TEN_DONVI LIKE N'%" + DonVi + "%' AND " +
                                                                        "DIA_CHI LIKE N'%" + DiaChi + "%'AND " +
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
            grvDonVi.DataSource = tb;
            grvDonVi.Refresh();
        }
    }
    #endregion


}
