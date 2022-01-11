namespace QL_Kho.UserControls
{
    partial class ucBCHangHoa
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CRVHH = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.txtMaHH = new System.Windows.Forms.TextBox();
            this.btnChon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CRVHH
            // 
            this.CRVHH.ActiveViewIndex = -1;
            this.CRVHH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CRVHH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CRVHH.Cursor = System.Windows.Forms.Cursors.Default;
            this.CRVHH.Location = new System.Drawing.Point(4, 4);
            this.CRVHH.Name = "CRVHH";
            this.CRVHH.Size = new System.Drawing.Size(746, 414);
            this.CRVHH.TabIndex = 0;
            // 
            // txtMaHH
            // 
            this.txtMaHH.Location = new System.Drawing.Point(248, 426);
            this.txtMaHH.Name = "txtMaHH";
            this.txtMaHH.Size = new System.Drawing.Size(134, 21);
            this.txtMaHH.TabIndex = 1;
            // 
            // btnChon
            // 
            this.btnChon.Location = new System.Drawing.Point(400, 424);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(75, 23);
            this.btnChon.TabIndex = 2;
            this.btnChon.Text = "Chọn";
            this.btnChon.UseVisualStyleBackColor = true;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(121, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nhập mã hàng hóa:";
            // 
            // ucBCHangHoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.txtMaHH);
            this.Controls.Add(this.CRVHH);
            this.Name = "ucBCHangHoa";
            this.Size = new System.Drawing.Size(750, 460);
            this.Load += new System.EventHandler(this.ucBCHangHoa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer CRVHH;
        private System.Windows.Forms.TextBox txtMaHH;
        private System.Windows.Forms.Button btnChon;
        private System.Windows.Forms.Label label1;
    }
}
