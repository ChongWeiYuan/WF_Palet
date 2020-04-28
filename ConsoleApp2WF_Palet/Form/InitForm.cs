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

    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class InitForm : Form
    {
        #region コンストラクタ
        public InitForm()
        {
            InitializeComponent();
        }
        #endregion

        #region メンバ1
        Queue<inputDeligate> InputQue = new Queue<inputDeligate>();
        private int _percent = 0;
        #endregion

        #region プロパティ
        public int Percent
        {
            get { return _percent; }
            set {
                    _percent = value;
                    this.progressBar1.Value = value;
                }
        }
        #endregion
                
        #region イベントハンドラ
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputQue.Enqueue(SetProgressBar);
        }
        private void InputTimer_Tick(object sender, EventArgs e)
        {
            if (InputQue.Count == 0) return;

            var EventHandler = this.InputQue.Peek();
            EventHandler.Invoke(new object(), new EventArgs());
        }
        #endregion

        #region 関数
        private void SetProgressBar(object sender, EventArgs e)
        {
            try
            {
                var setValue = int.Parse(this.textBox1.Text);
                if (setValue < 0 || 100 < setValue) return;
                this.Percent = setValue;
            }
            catch (Exception)
            {
                this.progressBar1.Value = 0;
                return;
            }
        }
        #endregion
    }
}
