namespace Schedule
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.importButton = new CCWin.SkinControl.SkinButton();
            this.workButton = new CCWin.SkinControl.SkinButton();
            this.exportButton = new CCWin.SkinControl.SkinButton();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.importButton);
            this.flowLayoutPanel1.Controls.Add(this.workButton);
            this.flowLayoutPanel1.Controls.Add(this.exportButton);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 34);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(297, 513);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // importButton
            // 
            this.importButton.BackColor = System.Drawing.Color.Transparent;
            this.importButton.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.importButton.DownBack = null;
            this.importButton.FadeGlow = false;
            this.importButton.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.importButton.ForeColor = System.Drawing.Color.White;
            this.importButton.IsDrawBorder = false;
            this.importButton.IsDrawGlass = false;
            this.importButton.Location = new System.Drawing.Point(15, 15);
            this.importButton.Margin = new System.Windows.Forms.Padding(15);
            this.importButton.MouseBack = null;
            this.importButton.Name = "importButton";
            this.importButton.NormlBack = null;
            this.importButton.Radius = 15;
            this.importButton.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.importButton.Size = new System.Drawing.Size(266, 128);
            this.importButton.TabIndex = 0;
            this.importButton.Text = "导入EXCEL文件";
            this.importButton.UseVisualStyleBackColor = false;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // workButton
            // 
            this.workButton.BackColor = System.Drawing.Color.Transparent;
            this.workButton.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.workButton.DownBack = null;
            this.workButton.FadeGlow = false;
            this.workButton.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.workButton.ForeColor = System.Drawing.Color.White;
            this.workButton.IsDrawBorder = false;
            this.workButton.IsDrawGlass = false;
            this.workButton.Location = new System.Drawing.Point(15, 173);
            this.workButton.Margin = new System.Windows.Forms.Padding(15);
            this.workButton.MouseBack = null;
            this.workButton.Name = "workButton";
            this.workButton.NormlBack = null;
            this.workButton.Radius = 15;
            this.workButton.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.workButton.Size = new System.Drawing.Size(266, 128);
            this.workButton.TabIndex = 1;
            this.workButton.Text = "开始自动排班";
            this.workButton.UseVisualStyleBackColor = false;
            this.workButton.Click += new System.EventHandler(this.workButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.Color.Transparent;
            this.exportButton.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.exportButton.DownBack = null;
            this.exportButton.FadeGlow = false;
            this.exportButton.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exportButton.ForeColor = System.Drawing.Color.White;
            this.exportButton.IsDrawBorder = false;
            this.exportButton.IsDrawGlass = false;
            this.exportButton.Location = new System.Drawing.Point(15, 331);
            this.exportButton.Margin = new System.Windows.Forms.Padding(15);
            this.exportButton.MouseBack = null;
            this.exportButton.Name = "exportButton";
            this.exportButton.NormlBack = null;
            this.exportButton.Radius = 15;
            this.exportButton.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.exportButton.Size = new System.Drawing.Size(266, 128);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "导出EXCEL文件";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 474);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(305, 551);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "监考排班软件";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private CCWin.SkinControl.SkinButton importButton;
        private CCWin.SkinControl.SkinButton workButton;
        private CCWin.SkinControl.SkinButton exportButton;
        private System.Windows.Forms.Label label1;

    }
}

