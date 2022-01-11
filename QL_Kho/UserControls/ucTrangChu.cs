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

namespace QL_Kho.UserControls
{
    public partial class ucTrangChu : DevExpress.XtraEditors.XtraUserControl
    {
        public static ucTrangChu _instrance;
        public static ucTrangChu Instrance
        {
            get
            {
                if (_instrance == null)
                    _instrance = new ucTrangChu();
                return _instrance;
            }
        }
        public ucTrangChu()
        {
            InitializeComponent();
        }
    }
}
