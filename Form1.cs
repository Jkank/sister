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
        Properties.Sister Sister1 = new Properties.Sister();
        Properties.SE SE1 = new Properties.SE();

        
        public int sent_ct = 0;


        public doujin_game_sharp()
        {
            InitializeComponent();

            /* --- オブジェクトの背景色を透明にできるようにする処理 --- */
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            /* --- 各オブジェクトの背景色を透明にする --- */
            panel2.BackColor = Color.Transparent;   /* 行動選択画面のPanel */
            panel3.BackColor = Color.Transparent;   /* メッセージボックスのPanel */
            chara_pos_1.BackColor = Color.Transparent;   /* 立ち絵位置１ */
            
        }

//        string line;

        /* === スタートボタン === */
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_1;

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_2;
            panel1.Visible = true;

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_1;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_0;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.ClientRectangle.Contains(pictureBox1.PointToClient(Cursor.Position)) == false)
            {
                pictureBox1.Image = Properties.Resources.g_btn_000_0;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }



        /* === ロードボタン === */


        /* === オプションボタン === */






        private void pictureBox9_Click(object sender, EventArgs e)
        {
            sent_ct = SE1.s_ScriptEngine( sent_ct, textarea, chara_pos_1, chara_pos2);
        }



        /* == 以下サブルーチン的メソッド == */

    }



}
