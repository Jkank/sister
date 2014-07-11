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
        
        int count = 0;
        int countold = 0;
        int inlawcount = 0;

        /*--- ここに使用するクラスを置く ---*/
        Properties.Sister Sister1 = new Properties.Sister();

        public doujin_game_sharp()
        {
            InitializeComponent();

            /* --- オブジェクトの背景色を透明にできるようにする処理 --- */
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            /* --- 各オブジェクトの背景色を透明にする --- */
            panel2.BackColor = Color.Transparent;   /* メッセージボックスのPanel */
        }

//        int counter = 0;
//        string line;

        /* === スタートボタン === */
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_1;

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_2;
            const int D_CHAR_LAST = 1000;
            bool linelastflg = false;

            //フォントオブジェクトの作成
            Font fnt = new Font("MS UI Gothic", 10);
            //文字列を位置(0,0)、白色で表示

            string text = Properties.Resources.texttest;    /* ファイルの中身を文字の配列として取得 */
            string textlawbuf;

            while (count < D_CHAR_LAST)
            {
                Console.Write(text[count]);
                if (text[count] == '@')
                {
                    count++;
                    inlawcount++;
                    linelastflg = true;
                }
                else if (text[count] == 'f' && linelastflg == true)
                {
                    linelastflg = false;
                    count++;
                    //描画先とするImageオブジェクトを作成する
                    Bitmap canvas = new Bitmap(pictureBox9.Width, pictureBox9.Height);
                    //ImageオブジェクトのGraphicsオブジェクトを作成する
                    Graphics g = Graphics.FromImage(canvas);

                    if (countold == 0)
                    {
                        textlawbuf = text.Substring(countold, inlawcount - 1);
                    }
                    else
                    {
                        textlawbuf = text.Substring(countold + 2, inlawcount - 3);
                    }

                    g.DrawString(textlawbuf, fnt, Brushes.White, 0, 0);
                    //PictureBox1に表示する
                    pictureBox9.Image = canvas;

                    //リソースを解放する
                    fnt.Dispose();
                    g.Dispose();

                    count = 0;
                    countold = 0;
                    inlawcount = 0;
                    break;
                }
                else if (text[count] == 'c' && linelastflg == true)
                {
                    linelastflg = false;
                    count++;

                    //描画先とするImageオブジェクトを作成する
                    Bitmap canvas = new Bitmap(pictureBox9.Width, pictureBox9.Height);
                    //ImageオブジェクトのGraphicsオブジェクトを作成する
                    Graphics g = Graphics.FromImage(canvas);

                    if (countold == 0)
                    {
                        textlawbuf = text.Substring(countold, inlawcount - 1);
                    }
                    else
                    {
                        textlawbuf = text.Substring(countold + 2, inlawcount - 3);
                    }

                    g.DrawString(textlawbuf, fnt, Brushes.White, 0, 0);
                    //PictureBox1に表示する
                    pictureBox9.Image = canvas;

                    //リソースを解放する
                    fnt.Dispose();
                    g.Dispose();

                    countold = count;
                    inlawcount = 0;

                    break;
                }
                else
                {
                    count++;
                    inlawcount++;
                }
            }





        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.g_btn_000_1;

            Sister1.Money = 2;

            Console.WriteLine("" + Sister1.Money);
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

                Sister1.Money = 1;

                Console.WriteLine("" + Sister1.Money);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }


        /* === ロードボタン === */


        /* === オプションボタン === */

   
    }



}
