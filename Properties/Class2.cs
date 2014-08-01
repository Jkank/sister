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

namespace WindowsFormsApplication1.Properties
{
    public class SE
    {
        
        /* 定数定義 */
        public const int D_CHR_SARA_00 = 0;
        public const int D_CHR_SARA_01 = 1;
        public const int D_CHR_SARA_02 = 2;
        public const int D_CHR_SARA_03 = 3;
        public const int D_CHR_SARA_04 = 4;
        public const int D_CHR_SARA_05 = 5;
        public const int D_CHR_SARA_06 = 6;
        public const int D_CHR_SARA_07 = 7;
        public const int D_CHR_SARA_08 = 8;
        public const int D_CHR_SARA_09 = 9;
        public const int D_CHR_SARA_10 = 10;
        public const int D_CHR_SARA_11 = 11;
        public const int D_CHR_SARA_12 = 12;
        public const int D_CHR_SARA_13 = 13;
        public const int D_CHR_SARA_14 = 14;
        public const int D_CHR_SARA_15 = 15;
        public const int D_CHR_SARA_16 = 16;
        public const int D_CHR_SARA_17 = 17;
        public const int D_CHR_SARA_18 = 18;
        public const int D_CHR_SARA_19 = 19;
        public const int D_CHR_DEVIL_00 = 20;
        public const int D_CHR_DEVIL_01 = 21;
        public const int D_CHR_DEVIL_02 = 22;
        public const int D_CHR_DEVIL_03 = 23;
        public const int D_CHR_DEVIL_04 = 24;
        public const int D_CHR_DEVIL_05 = 25;

        int count = 0;
        int countold = 0;
        int inlawcount = 0;
        int ifdpth = 0;         /* スクリプトif文の深度 */
        int elseifdpth = 0;

        string text = Properties.Resources.opening;    /* ファイルの中身を文字の配列として取得 */
        

        public int s_ScriptEngine(int sentence_ct, PictureBox o_bgpic, PictureBox o_chrbox1, PictureBox o_chrbox2)
        {

            
            const int D_CHAR_LAST = 20000;
            
            //フォントオブジェクトの作成
            Font fnt = new Font("メイリオ", 15);

            Brush Color = Brushes.White;

            string textlawbuf;

            count = s_nowsenthead(sentence_ct);   /* テキスト内の初期値を取得 */

            while (count < D_CHAR_LAST)
            {
                if (this.text[count] == '/' && text[count+1] == '/')
                {

                    /*====================*/
                    /*   コメントアウト   */
                    /*====================*/
                    while( checkrowlast(count) == 0 )
                    {
                            count++;
                    }
                    count++;
                    countold = count;
                    inlawcount = 0;
                }
                else if (text[count] == ':')
                {
                    count++;
                    textlawbuf = text.Substring(countold + 2, inlawcount - 2);

                    /*====================*/
                    /*    文法コマンド    */
                    /*====================*/
                    if (textlawbuf.Length >= 3 && textlawbuf.Substring(0, 3) == "end")
                    {
                        sentence_ct = 0;
                        return sentence_ct;
                    }
                    else if (textlawbuf.Length >= 3 && textlawbuf.Substring(0,3) == "If(")
                    {
                        ifdpth++;
                    }

                    else if (textlawbuf.Length >= 8 && textlawbuf.Substring(0, 8) == "Else If(")
                    {
                        if (ifdpth <= 0)
                        {
                            Console.WriteLine("エラー:対応するIf文がありません");
                            break;
                        }
                        elseifdpth++;

                    }

                    else if (textlawbuf.Length >= 5 && textlawbuf.Substring(0, 5) == "EndIf")
                    {

                        if (ifdpth <= 0)
                        {
                            Console.WriteLine("エラー:対応するIf文がありません");
                            break;
                        }
                        ifdpth--;
                    }

                    /*======================*/
                    /*    発言者コマンド    */
                    /*======================*/
                    else if (textlawbuf == "Text")
                    {
                        Color = Brushes.White;
                        countold = count;
                        inlawcount = 0;
                    }
                    else if (textlawbuf == "サラ")
                    {
                        Color = Brushes.Pink;
                        s_disptachie(o_chrbox1, D_CHR_SARA_00);

                        countold = count;
                        inlawcount = 0;
                    }
                    else if (textlawbuf == "マリー")
                    {
                        Color = Brushes.Yellow;

                        //    s_disptachie(o_chrbox1, D_CHR_SARA_00);

                        countold = count;
                        inlawcount = 0;
                    }
                    else if (textlawbuf == "モフィ")
                    {
                        Color = Brushes.Orange;

                    //    s_disptachie(o_chrbox1, D_CHR_SARA_00);

                        countold = count;
                        inlawcount = 0;
                    }
                    else if (textlawbuf == "魔物")
                    {
                        Color = Brushes.Purple;

                        //    s_disptachie(o_chrbox1, D_CHR_SARA_00);

                        countold = count;
                        inlawcount = 0;
                    }
                }
                else if (text[count] == ';')
                {

                    //描画先とするImageオブジェクトを作成する
                    Bitmap canvas = new Bitmap(o_bgpic.Width, o_bgpic.Height);
                    //ImageオブジェクトのGraphicsオブジェクトを作成する
                    Graphics g = Graphics.FromImage(canvas);

                    textlawbuf = text.Substring(countold, inlawcount);

                    g.DrawString(textlawbuf, fnt, Color, 0, 0);
                    //PictureBox1に表示する
                    o_bgpic.Image = canvas;

                    //リソースを解放する
                    fnt.Dispose();
                    g.Dispose();

                    count += 3;
                    countold = count;
                    sentence_ct++;
                    inlawcount = 0;
                    break;
                }
                else if (checkrowlast(count) == 1 && checkrowlast(count + 2) == 1)
                {
                    count += 2;
                    countold = count;
                    inlawcount = 0;
                }
                else
                {
                    count++;
                    inlawcount++;
                }
            }
            return sentence_ct;
        }


        /*//////////ここからサブルーチン的メソッド///////////*/

        public int s_nowsenthead(int ct)
        {
            /* テキストの現在地取得処理 */
            /* ロード後の文章表示で使用予定 */

            int i = 0;
            int j;

            for (j = 0; j < ct; j++ )
            {
                /* ;が来るまで開始位置からのカウンタを進める */
                while (text[i] != ';')
                {
                    i++;
                }

                i++;

                /* 次の文字が改行コードだったら、さらに一文字進める */
                if ( text[i + 1] == '\r' )
                {
                    i++;
                }
            }

            return i;
        }


        public int checkrowlast(int ct)
        {
            /* 改行判定処理 */
            /* コメントアウトの終端を判定するのに用いる */

            int i = 0;

            if (text[ct] == '\r' && text[ct+1] == '\n')
            {
                i = 1;
            }

            return i;
        }

        public void s_disptachie(PictureBox chrbox, int chrnum)
        {
            switch (chrnum)
            {
                case D_CHR_SARA_00:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_01:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_02:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_03:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_04:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_05:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_06:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_07:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_08:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_09:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_10:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case D_CHR_SARA_11:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;

            }
            chrbox.Visible = true;
        }
    }
}
