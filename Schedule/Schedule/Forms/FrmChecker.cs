using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace Schedule
{
    public partial class FrmChecker : Skin_DevExpress
    {
        private string checkSolve;

        public FrmChecker(string checkSolve)
        {
            InitializeComponent();
            this.checkSolve = checkSolve;
        }

        private void FrmChecker_Load(object sender, EventArgs e)
        {
            this.txtChecker.Text = checkSolve;
        }
    }
}
