namespace Schedule.Forms
{
    partial class SubFormIn
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.skinLabel1 = new CCWin.SkinControl.SkinLabel();
            this.skinLabel2 = new CCWin.SkinControl.SkinLabel();
            this.btnSure = new CCWin.SkinControl.SkinButton();
            this.skinLabel3 = new CCWin.SkinControl.SkinLabel();
            this.cboSemester = new CCWin.SkinControl.SkinComboBox();
            this.txtFileName = new CCWin.SkinControl.SkinTextBox();
            this.btnBrowse = new CCWin.SkinControl.SkinButton();
            this.dtpSchYear = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new CCWin.SkinControl.SkinButton();
            this.ofdExcelPath = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.BorderSize = 0;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(28, 56);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(44, 17);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "学年：";
            // 
            // skinLabel2
            // 
            this.skinLabel2.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(164, 57);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(44, 17);
            this.skinLabel2.TabIndex = 1;
            this.skinLabel2.Text = "学期：";
            // 
            // btnSure
            // 
            this.btnSure.BackColor = System.Drawing.Color.Transparent;
            this.btnSure.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSure.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSure.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSure.DownBack = null;
            this.btnSure.Location = new System.Drawing.Point(97, 141);
            this.btnSure.MouseBack = null;
            this.btnSure.Name = "btnSure";
            this.btnSure.NormlBack = null;
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 2;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // skinLabel3
            // 
            this.skinLabel3.AutoSize = true;
            this.skinLabel3.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel3.BorderColor = System.Drawing.Color.White;
            this.skinLabel3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel3.Location = new System.Drawing.Point(28, 100);
            this.skinLabel3.Name = "skinLabel3";
            this.skinLabel3.Size = new System.Drawing.Size(56, 17);
            this.skinLabel3.TabIndex = 3;
            this.skinLabel3.Text = "文件名：";
            // 
            // cboSemester
            // 
            this.cboSemester.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cboSemester.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.Items.AddRange(new object[] {
            "春季",
            "秋季"});
            this.cboSemester.Location = new System.Drawing.Point(214, 53);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(139, 22);
            this.cboSemester.TabIndex = 4;
            this.cboSemester.WaterText = "";
            // 
            // txtFileName
            // 
            this.txtFileName.BackColor = System.Drawing.Color.Transparent;
            this.txtFileName.DownBack = null;
            this.txtFileName.Enabled = false;
            this.txtFileName.Icon = null;
            this.txtFileName.IconIsButton = false;
            this.txtFileName.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtFileName.IsPasswordChat = '\0';
            this.txtFileName.IsSystemPasswordChar = false;
            this.txtFileName.Lines = new string[] {
        "请导入待排班的Excel"};
            this.txtFileName.Location = new System.Drawing.Point(87, 94);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(0);
            this.txtFileName.MaxLength = 32767;
            this.txtFileName.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtFileName.MouseBack = null;
            this.txtFileName.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtFileName.Multiline = false;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.NormlBack = null;
            this.txtFileName.Padding = new System.Windows.Forms.Padding(5);
            this.txtFileName.ReadOnly = false;
            this.txtFileName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtFileName.Size = new System.Drawing.Size(202, 28);
            // 
            // 
            // 
            this.txtFileName.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFileName.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileName.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtFileName.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtFileName.SkinTxt.Name = "BaseText";
            this.txtFileName.SkinTxt.Size = new System.Drawing.Size(192, 18);
            this.txtFileName.SkinTxt.TabIndex = 0;
            this.txtFileName.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtFileName.SkinTxt.WaterText = "";
            this.txtFileName.TabIndex = 5;
            this.txtFileName.Text = "请导入待排班的Excel";
            this.txtFileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtFileName.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtFileName.WaterText = "";
            this.txtFileName.WordWrap = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnBrowse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnBrowse.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnBrowse.DownBack = null;
            this.btnBrowse.Location = new System.Drawing.Point(303, 98);
            this.btnBrowse.MouseBack = null;
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.NormlBack = null;
            this.btnBrowse.Size = new System.Drawing.Size(50, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "浏览";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dtpSchYear
            // 
            this.dtpSchYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSchYear.Location = new System.Drawing.Point(78, 54);
            this.dtpSchYear.Name = "dtpSchYear";
            this.dtpSchYear.ShowUpDown = true;
            this.dtpSchYear.Size = new System.Drawing.Size(80, 21);
            this.dtpSchYear.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCancel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnCancel.DownBack = null;
            this.btnCancel.Location = new System.Drawing.Point(214, 141);
            this.btnCancel.MouseBack = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormlBack = null;
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ofdExcelPath
            // 
            this.ofdExcelPath.FileName = "openFileDialog1";
            this.ofdExcelPath.Filter = "空的排班表文件|*.xls;*.xlsx";
            // 
            // SubFormIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(386, 186);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dtpSchYear);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.skinLabel3);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.skinLabel1);
            this.MaximizeBox = false;
            this.Name = "SubFormIn";
            this.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.ShadowColor = System.Drawing.Color.White;
            this.Text = "导入";
            this.Load += new System.EventHandler(this.SubFormIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinButton btnSure;
        private CCWin.SkinControl.SkinLabel skinLabel3;
        private CCWin.SkinControl.SkinComboBox cboSemester;
        private CCWin.SkinControl.SkinTextBox txtFileName;
        private CCWin.SkinControl.SkinButton btnBrowse;
        private System.Windows.Forms.DateTimePicker dtpSchYear;
        private CCWin.SkinControl.SkinButton btnCancel;
        private System.Windows.Forms.OpenFileDialog ofdExcelPath;
    }
}