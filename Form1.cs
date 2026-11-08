using System;
using System.Windows.Forms;

namespace cs2_things
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);
            SuspendLayout();
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            ResumeLayout(true);
            base.OnResizeEnd(e);
        }

        private void SayHelloButton_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
