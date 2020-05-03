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
        /// <summary>
        /// プログレスバー表示数値(%)
        /// </summary>
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
            this.textBox1.ForeColor = Color.Black;

            //キュー登録で、実行間隔調整
            InputQue.Enqueue(SetProgressBar);
        }
        /// <summary>
        /// タイマー
        /// </summary>
        ///<remarks>インターバル設定間隔で実行される</remarks>
        private void InputTimer_Tick(object sender, EventArgs e)
        {
            if (InputQue.Count == 0) return;

            var EventHandler = this.InputQue.Peek();
            EventHandler.Invoke(new object(), new EventArgs());

            this.InputQue.Clear();
        }
        /// <summary>
        /// Base64変換
        /// </summary>        
        private void btnConvert_Click(object sender, EventArgs e)
        {
            txtAfter.Text = new Base64("UTF-8").Encode(txtBefore.Text);
        }
        #endregion

        #region 関数
        /// <summary>
        /// プログレスバーに値を設定
        /// </summary>        
        private void SetProgressBar(object sender, EventArgs e)
        {
            try
            {
                var setValue = int.Parse(this.textBox1.Text);

                //値域チェック
                if (setValue < 0 || 100 < setValue)
                {
                    this.textBox1.ForeColor = Color.Red;
                    return;
                } 

                this.Percent = setValue;
            }
            //intキャスト失敗時
            catch (Exception)
            {   
                this.progressBar1.Value = 0;
                this.textBox1.ForeColor = Color.Red;
                return;
            }
        }
        #endregion

        //画像ファイルを読み込む(画像はexeと同じフォルダ内に配置)
        Image[] images = new Image[] {Image.FromFile(@"pic1.bmp") };
        Image currentImage = null;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (currentImage != null)
            {
                //DrawImageメソッドで画像を描画する
                e.Graphics.DrawImage(currentImage,
                     0, 0, currentImage.Width, currentImage.Height);
            }
            //表示する画像を入れ替える
            if (currentImage != images[0])
            {
                currentImage = images[0];
            }
            else
            {
                currentImage = images[0];
            }
            //コントロールを再描画する。これがないと、新しい画像が表示されない。
            this.pictureBox1.Invalidate();
        }
    }
}
