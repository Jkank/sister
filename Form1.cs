using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class doujin_game_sharp : Form
    {
        /*--- ここに使用するクラスを置く ---*/
        public Properties.Sister Sara = new Properties.Sister();
        public Properties.SE SE1 = new Properties.SE();
        
        public int sent_ct = 0;
        public int log_ct = 0;          /* ログ現在位置カウンタ */
        public int log_ct_use = 0;      /* ログ最古位置カウンタ */

        public bool MouseLeftPushed = false;

        public doujin_game_sharp()
        {
            InitializeComponent();
            //ホイールイベントの追加  
            this.panel3.MouseWheel
                += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseWheel);
            this.panel4.MouseWheel
                += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseWheel); 

            /* --- オブジェクトの背景色を透明にできるようにする処理 --- */
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            /* --- 各オブジェクトの背景色を透明にする --- */
            panel2.BackColor = Color.Transparent;   /* 行動選択画面のPanel */
            panel3.BackColor = Color.Transparent;   /* メッセージボックスのPanel */
            //panel4.BackColor = Color.Transparent;   /* メッセージボックスのPanel */
            chara_pos_1.BackColor = Color.Transparent;   /* 立ち絵位置１ */

            Sara.PassionPoint.MaxValue = 100;
            Sara.CorruptionPoint.MaxValue = 100;
            Sara.PassionPoint.CurrentValue = 10;
            Sara.CorruptionPoint.CurrentValue = 30;

        }

//        string line;

        /* === スタートボタン === */
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_1;

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox1.Image = Properties.Resources.g_btn_000_2;
                MouseLeftPushed = true;
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_1;
            if ( MouseLeftPushed == true )
            {
                panel1.Visible = true;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_0;
            MouseLeftPushed = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.ClientRectangle.Contains(pictureBox1.PointToClient(Cursor.Position)) == false)
            {
                pictureBox1.Image = Properties.Resources.g_btn_000_0;
                MouseLeftPushed = false;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }



        /* === ロードボタン === */


        /* === オプションボタン === */






        private void pictureBox9_Click(object sender, EventArgs e)
        {
            sent_ct = SE1.s_ScriptEngine(sent_ct, log_ct, log_ct_use, textarea, chara_pos_1, chara_pos2, Sara);

            if (sent_ct == 0)
            {
                log_ct = 0;
                log_ct_use = 0;
            }
            else if (log_ct_use < 99)
            {
                log_ct_use++;
            }

            /* パネル３にフォーカス */
            panel3.Focus();

        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            sent_ct = SE1.s_ScriptEngine(sent_ct, log_ct, log_ct_use, textarea, chara_pos_1, chara_pos2, Sara);

            if (sent_ct == 0)
            {
                log_ct = 0;
                log_ct_use = 0;
            }
            else if (log_ct_use < 99)
            {
                log_ct_use++;
            }
            /* パネル３にフォーカス */
            panel3.Focus();
        }
        private void chara_pos_1_MouseDown(object sender, MouseEventArgs e)
        {
            sent_ct = SE1.s_ScriptEngine(sent_ct, log_ct, log_ct_use, textarea, chara_pos_1, chara_pos2, Sara);

            if (sent_ct == 0)
            {
                log_ct = 0;
                log_ct_use = 0;
            }
            else if (log_ct_use < 99)
            {
                log_ct_use++;
            }
            /* パネル３にフォーカス */
            panel3.Focus();
        }
        private void chara_pos2_MouseDown(object sender, MouseEventArgs e)
        {
            sent_ct = SE1.s_ScriptEngine(sent_ct, log_ct, log_ct_use, textarea, chara_pos_1, chara_pos2, Sara);

            if (sent_ct == 0)
            {
                log_ct = 0;
                log_ct_use = 0;
            }
            else if (log_ct_use < 99)
            {
                log_ct_use++;
            }
            /* パネル３にフォーカス */
            panel3.Focus();
        }

        //バックログ関係
        private void panel3_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 3 && panel4.Visible == false)
            {
                // バックログウィンドウ（panel4）を展開
                panel4.Visible = true;
                panel4.Focus();

                // スクロール画面初期状態
                SE1.s_ScrollInit(log_ct_use, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14);
            }
            int a = 1;
        }
        private void panel4_MouseWheel(object sender, MouseEventArgs e)
        {
            // スクロール画面更新処理
            if (e.Delta < 3)
            {
                if (log_ct <= 1)
                {
                    /* スクロール画面の一番下でホイールを下に動かした */
                    /* ⇒テキスト画面に戻る */
                    panel4.Visible = false;
                    panel3.Focus();
                }
                else
                {
                    log_ct--;
                    SE1.s_ScrollRedraw(log_ct, log_ct_use, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14);
                }
            }
            else if (e.Delta > 3)
            {
                if (log_ct + 4 < log_ct_use)
                {
                    log_ct++;
                    /* スクロール画面の一番上ではない */
                    SE1.s_ScrollRedraw(log_ct, log_ct_use, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14);
                }
            }
        }







        /* == 以下サブルーチン的メソッド == */

    }



}
