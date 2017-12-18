namespace Schedule
{
    partial class FrmChecker
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
            this.txtChecker = new CCWin.SkinControl.SkinTextBox();
            this.SuspendLayout();
            // 
            // txtChecker
            // 
            this.txtChecker.BackColor = System.Drawing.Color.Transparent;
            this.txtChecker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChecker.DownBack = null;
            this.txtChecker.Icon = null;
            this.txtChecker.IconIsButton = false;
            this.txtChecker.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtChecker.IsPasswordChat = '\0';
            this.txtChecker.IsSystemPasswordChar = false;
            this.txtChecker.Lines = new string[] {
        "编译目标平台需要调节成x86，然后安装OLEX86版本驱动"};
            this.txtChecker.Location = new System.Drawing.Point(4, 34);
            this.txtChecker.Margin = new System.Windows.Forms.Padding(0);
            this.txtChecker.MaxLength = 32767;
            this.txtChecker.MinimumSize = new System.Drawing.Size(28, 28);
            this.txtChecker.MouseBack = null;
            this.txtChecker.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.txtChecker.Multiline = true;
            this.txtChecker.Name = "txtChecker";
            this.txtChecker.NormlBack = null;
            this.txtChecker.Padding = new System.Windows.Forms.Padding(5);
            this.txtChecker.ReadOnly = false;
            this.txtChecker.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChecker.Size = new System.Drawing.Size(558, 267);
            // 
            // 
            // 
            this.txtChecker.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChecker.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChecker.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txtChecker.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.txtChecker.SkinTxt.Multiline = true;
            this.txtChecker.SkinTxt.Name = "BaseText";
            this.txtChecker.SkinTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChecker.SkinTxt.Size = new System.Drawing.Size(548, 257);
            this.txtChecker.SkinTxt.TabIndex = 0;
            this.txtChecker.SkinTxt.Text = "编译目标平台需要调节成x86，然后安装OLEX86版本驱动";
            this.txtChecker.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtChecker.SkinTxt.WaterText = "";
            this.txtChecker.TabIndex = 0;
            this.txtChecker.Text = "编译目标平台需要调节成x86，然后安装OLEX86版本驱动";
            this.txtChecker.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtChecker.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.txtChecker.WaterText = "";
            this.txtChecker.WordWrap = true;
            // 
            // FrmChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(566, 305);
            this.Controls.Add(this.txtChecker);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChecker";
            this.Text = "模型评估";
            this.Load += new System.EventHandler(this.FrmChecker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinTextBox txtChecker;
    }
}