using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using QL_Kho.UserControls;

namespace QL_Kho
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        string TenDangNhap = "", MatKhau = "", Quyen = "", TenNhanVien = "";
        public frmMain()
        {
            InitializeComponent();
            trangchu();
            //txtName.Caption = Name;
        }
        public frmMain(string TenDangNhap, string TenNhanVien, string MatKhau, string Quyen)
        {
            InitializeComponent();
            trangchu();
            this.TenDangNhap = TenDangNhap;
            this.TenNhanVien = TenNhanVien;
            this.MatKhau = MatKhau;
            this.Quyen = Quyen;
            txtName.Caption =  "Xin chào"+TenNhanVien;
        }
        public void trangchu()
        {
            if (!pnlMain.Controls.Contains(ucTrangChu._instrance))
            {
                pnlMain.Controls.Add(ucTrangChu.Instrance);
                ucTrangChu.Instrance.Dock = DockStyle.Fill;
                ucTrangChu.Instrance.BringToFront();
            }
            else
            {
                ucTrangChu.Instrance.BringToFront();
            }
        }
        private void btnKhachHang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucKhachHang.Instance))
            {
                pnlMain.Controls.Add(ucKhachHang.Instance);
                ucKhachHang.Instance.Dock = DockStyle.Fill;
                ucKhachHang.Instance.BringToFront();
            }
            else
                ucKhachHang.Instance.BringToFront();
        }

        private void btnNhanVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucNguoiDung.Instance))
            {
                pnlMain.Controls.Add(ucNguoiDung.Instance);
                ucNguoiDung.Instance.Dock = DockStyle.Fill;
                ucNguoiDung.Instance.BringToFront();
            }
            else
                ucNguoiDung.Instance.BringToFront();
        }

        private void btnChucVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (!pnlMain.Controls.Contains(ucChucVu.Instance))
            {
                pnlMain.Controls.Add(ucChucVu.Instance);
                ucChucVu.Instance.Dock = DockStyle.Fill;
                ucChucVu.Instance.BringToFront();
            }
            else
                ucChucVu.Instance.BringToFront();
        }

        private void btnDonVi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucDonVi.Instance))
            {
                pnlMain.Controls.Add(ucDonVi.Instance);
                ucDonVi.Instance.Dock = DockStyle.Fill;
                ucDonVi.Instance.BringToFront();
            }
            else
                ucDonVi.Instance.BringToFront();
        }

        private void btnNhaCungCap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucNhaCungCap.Instance))
            {
                pnlMain.Controls.Add(ucNhaCungCap.Instance);
                ucNhaCungCap.Instance.Dock = DockStyle.Fill;
                ucNhaCungCap.Instance.BringToFront();
            }
            else
                ucNhaCungCap.Instance.BringToFront();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucHT_ThanhToan.Instance))
            {
                pnlMain.Controls.Add(ucHT_ThanhToan.Instance);
                ucHT_ThanhToan.Instance.Dock = DockStyle.Fill;
                ucHT_ThanhToan.Instance.BringToFront();
            }
            else
                ucHT_ThanhToan.Instance.BringToFront();
        }

        private void btnHangHoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucHangHoa.Instance))
            {
                pnlMain.Controls.Add(ucHangHoa.Instance);
                ucHangHoa.Instance.Dock = DockStyle.Fill;
                ucHangHoa.Instance.BringToFront();
            }
            else
                ucHangHoa.Instance.BringToFront();



        }

        private void btnNhapKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucNhapKho.Instance))
            {
                pnlMain.Controls.Add(ucNhapKho.Instance);
                ucNhapKho.Instance.Dock = DockStyle.Fill;
                ucNhapKho.Instance.BringToFront();
            }
            else
                ucNhapKho.Instance.BringToFront();
        }

        private void btnBaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucBaoCao.Instance))
            {
                pnlMain.Controls.Add(ucBaoCao.Instance);
                ucBaoCao.Instance.Dock = DockStyle.Fill;
                ucBaoCao.Instance.BringToFront();
            }
            else
                ucBaoCao.Instance.BringToFront();
        }

        private void btnXuatKho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucXuatKho.Instance))
            {
                pnlMain.Controls.Add(ucXuatKho.Instance);
                ucXuatKho.Instance.Dock = DockStyle.Fill;
                ucXuatKho.Instance.BringToFront();
            }
            else
                ucXuatKho.Instance.BringToFront();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnTaiKhoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucTaiKhoan.Instance))
            {
                pnlMain.Controls.Add(ucTaiKhoan.Instance);
                ucTaiKhoan.Instance.Dock = DockStyle.Fill;
                ucTaiKhoan.Instance.BringToFront();
            }
            else
                ucTaiKhoan.Instance.BringToFront();
        }

        private void btnBCHangHoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucBCHangHoa.Instance))
            {
                pnlMain.Controls.Add(ucBCHangHoa.Instance);
                ucBCHangHoa.Instance.Dock = DockStyle.Fill;
                ucBCHangHoa.Instance.BringToFront();
            }
            else
                ucBCHangHoa.Instance.BringToFront();
        }

        private void btnTrangChu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucTrangChu._instrance))
            {
                pnlMain.Controls.Add(ucTrangChu.Instrance);
                ucTrangChu.Instrance.Dock = DockStyle.Fill;
                ucTrangChu.Instrance.BringToFront();
            }
            else
            {
                ucTrangChu.Instrance.BringToFront();
            }
        }

        private void btnHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!pnlMain.Controls.Contains(ucHoaDon.Instance))
            {
                pnlMain.Controls.Add(ucHoaDon.Instance);
                ucHoaDon.Instance.Dock = DockStyle.Fill;
                ucHoaDon.Instance.BringToFront();
            }
            else
                ucHoaDon.Instance.BringToFront();
            }
        }
    }