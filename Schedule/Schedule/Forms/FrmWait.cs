using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using System.Threading;

namespace Schedule
{
    public partial class FrmWait : Skin_DevExpress
    {
        public FrmWait()
        {
            InitializeComponent();
        }

        private void FrmWait_Load(object sender, EventArgs e)
        {
            skinRollingBar1.Enabled = true;
            skinRollingBar1.StartRolling();
        }

        private void skinRollingBar1_Click(object sender, EventArgs e)
        {

        }
        


        public void CloseMe()
        {
            this.Close();
            this.Dispose();
        }
    }
}
