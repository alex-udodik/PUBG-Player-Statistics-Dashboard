using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUBG_Application.Forms
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
            StartSpin();
        }

        private void StartSpin()
        {
            this.circularProgressBar1.Style = ProgressBarStyle.Marquee;
            this.circularProgressBar1.MarqueeAnimationSpeed = 2000;
            this.circularProgressBar1.Visible = true;
        }

        public void StopSpin()
        {
            this.circularProgressBar1.Visible = false;
            //this.circularProgressBar1.Style = ProgressBarStyle.Continuous;
            //this.circularProgressBar1.MarqueeAnimationSpeed = 0;

        }
    }
}
