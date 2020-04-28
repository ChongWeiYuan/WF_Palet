using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp2WF_Palet
{
    delegate void inputDeligate(object sender, EventArgs e);
    public partial class InitForm : Form
    {
        Queue<inputDeligate> InputQue = new Queue<inputDeligate>();
        int percent = 0;
        
        public InitForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputQue.Enqueue(SetProgressBar);
        }

        private void SetProgressBar(object sender, EventArgs e)
        {
            try
            {
                percent = int.Parse(this.textBox1.Text);
                if (percent < 0 || 100 < percent) return;
                this.progressBar1.Value = int.Parse(this.textBox1.Text);
            }
            catch (Exception)
            {
                this.progressBar1.Value = 0;
                return;
            }
        }

        private void InputTimer_Tick(object sender, EventArgs e)
        {
            if (InputQue.Count == 0) return;

            var EventHandler = this.InputQue.Peek();
            EventHandler.Invoke(new object(), new EventArgs());
        }
    }
}
