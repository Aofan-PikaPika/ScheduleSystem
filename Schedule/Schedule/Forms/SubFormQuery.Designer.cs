namespace Schedule.Forms
{
    partial class SubFormQuery
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
            this.cboSchoolYear = new CCWin.SkinControl.SkinComboBox();
            this.cboSemester = new CCWin.SkinControl.SkinComboBox();
            this.btnSure = new CCWin.SkinControl.SkinButton();
            this.btnCancel = new CCWin.SkinControl.SkinButton();
            this.SuspendLayout();
            // 
            // skinLabel1
            // 
            this.skinLabel1.ArtTextStyle = CCWin.SkinControl.ArtTextStyle.None;
            this.skinLabel1.AutoSize = true;
            this.skinLabel1.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel1.BorderColor = System.Drawing.Color.White;
            this.skinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel1.Location = new System.Drawing.Point(20, 47);
            this.skinLabel1.Name = "skinLabel1";
            this.skinLabel1.Size = new System.Drawing.Size(32, 17);
            this.skinLabel1.TabIndex = 0;
            this.skinLabel1.Text = "学年";
            // 
            // skinLabel2
            // 
            this.skinLabel2.AutoSize = true;
            this.skinLabel2.BackColor = System.Drawing.Color.Transparent;
            this.skinLabel2.BorderColor = System.Drawing.Color.White;
            this.skinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.skinLabel2.Location = new System.Drawing.Point(166, 47);
            this.skinLabel2.Name = "skinLabel2";
            this.skinLabel2.Size = new System.Drawing.Size(32, 17);
            this.skinLabel2.TabIndex = 1;
            this.skinLabel2.Text = "学期";
            // 
            // cboSchoolYear
            // 
            this.cboSchoolYear.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cboSchoolYear.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cboSchoolYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSchoolYear.FormattingEnabled = true;
            this.cboSchoolYear.Location = new System.Drawing.Point(58, 47);
            this.cboSchoolYear.Name = "cboSchoolYear";
            this.cboSchoolYear.Size = new System.Drawing.Size(91, 22);
            this.cboSchoolYear.TabIndex = 3;
            this.cboSchoolYear.WaterText = "";
            this.cboSchoolYear.SelectedIndexChanged += new System.EventHandler(this.cboSchoolYear_SelectedIndexChanged);
            // 
            // cboSemester
            // 
            this.cboSemester.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cboSemester.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cboSemester.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSemester.FormattingEnabled = true;
            this.cboSemester.Location = new System.Drawing.Point(204, 47);
            this.cboSemester.Name = "cboSemester";
            this.cboSemester.Size = new System.Drawing.Size(107, 22);
            this.cboSemester.TabIndex = 4;
            this.cboSemester.WaterText = "";

            // 
            // btnSure
            // 
            this.btnSure.BackColor = System.Drawing.Color.Transparent;
            this.btnSure.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSure.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSure.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnSure.DownBack = null;
            this.btnSure.Location = new System.Drawing.Point(58, 96);
            this.btnSure.MouseBack = null;
            this.btnSure.Name = "btnSure";
            this.btnSure.NormlBack = null;
            this.btnSure.Size = new System.Drawing.Size(75, 23);
            this.btnSure.TabIndex = 5;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnCancel.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.btnCancel.DownBack = null;
            this.btnCancel.Location = new System.Drawing.Point(186, 96);
            this.btnCancel.MouseBack = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormlBack = null;
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SubFormQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(332, 141);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.cboSemester);
            this.Controls.Add(this.cboSchoolYear);
            this.Controls.Add(this.skinLabel2);
            this.Controls.Add(this.skinLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SubFormQuery";
            this.Text = "查询历史记录";
            this.Load += new System.EventHandler(this.SubFormQuery_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CCWin.SkinControl.SkinLabel skinLabel1;
        private CCWin.SkinControl.SkinLabel skinLabel2;
        private CCWin.SkinControl.SkinComboBox cboSchoolYear;
        private CCWin.SkinControl.SkinComboBox cboSemester;
        private CCWin.SkinControl.SkinButton btnSure;
        private CCWin.SkinControl.SkinButton btnCancel;
    }
}