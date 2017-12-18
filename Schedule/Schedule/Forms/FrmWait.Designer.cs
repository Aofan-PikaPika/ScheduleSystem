namespace Schedule
{
    partial class FrmWait
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
            CCWin.SkinControl.SkinRollingBarThemeBase skinRollingBarThemeBase1 = new CCWin.SkinControl.SkinRollingBarThemeBase();
            this.skinRollingBar1 = new CCWin.SkinControl.SkinRollingBar();
            this.SuspendLayout();
            // 
            // skinRollingBar1
            // 
            this.skinRollingBar1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(163)))), ((int)(((byte)(220)))));
            this.skinRollingBar1.Location = new System.Drawing.Point(7, 37);
            this.skinRollingBar1.Name = "skinRollingBar1";
            this.skinRollingBar1.Radius2 = 24;
            this.skinRollingBar1.Size = new System.Drawing.Size(140, 58);
            this.skinRollingBar1.TabIndex = 0;
            this.skinRollingBar1.TabStop = false;
            skinRollingBarThemeBase1.BackColor = System.Drawing.Color.Transparent;
            skinRollingBarThemeBase1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(163)))), ((int)(((byte)(220)))));
            skinRollingBarThemeBase1.DiamondColor = System.Drawing.Color.White;
            skinRollingBarThemeBase1.PenWidth = 2F;
            skinRollingBarThemeBase1.Radius1 = 10;
            skinRollingBarThemeBase1.Radius2 = 24;
            skinRollingBarThemeBase1.SpokeNum = 12;
            this.skinRollingBar1.XTheme = skinRollingBarThemeBase1;
            this.skinRollingBar1.Click += new System.EventHandler(this.skinRollingBar1_Click);
            // 
            // FrmWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(154, 102);
            this.CloseBoxSize = new System.Drawing.Size(0, 0);
            this.Controls.Add(this.skinRollingBar1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWait";
            this.Text = "请等待";
            this.Load += new System.EventHandler(this.FrmWait_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinRollingBar skinRollingBar1;
    }
}