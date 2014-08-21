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

        /* ------------------ */
        /*      定数定義      */
        /* ------------------ */

        /* キャラ立ち絵番号 */
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

        /* パラメーター */


        int count = 0;              /* テキストファイル全体の中でのカウンタ */
        int countold = 0;           /* 前回処理終了時点でのカウンタの値 */
        int inrowcount = 0;         /* 一度に文章バッファに取り込む文章内でのカウンタ */

        string text = Properties.Resources.休息;    /* ファイルの中身を文字の配列として取得 */


        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：s_ScriptEngine　　　　　　 　　　　　　　 　■ */
        /* ■　入力：sentence_ct　文章の番号を示すカウンタ 　　　　■ */
        /* ■　　　　o_bgpic　　　背景画像のオブジェクト　 　　　　■ */
        /* ■　　　　o_charbox1 　左側キャラ画像のオブジェクト 　　■ */
        /* ■　　　　o_charbox2 　右側キャラ画像のオブジェクト 　　■ */
        /* ■　出力：sentence_ct　次回読み込み用のカウンタの値 　　■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */

        public int s_ScriptEngine(int sentence_ct, PictureBox o_bgpic, PictureBox o_chrbox1, PictureBox o_chrbox2, Sister Sis)
        {
            const long D_CHAR_LAST = 100000;              /* １ファイルの最大文字数のDefine( ENDコードが無かった時の Fail Safe ) */
            const short D_WORKVAL_MAX = 100;

            //フォントオブジェクトの作成
            Font fnt = new Font("メイリオ", 15);

            Brush Color = Brushes.White;

            string textlawbuf;                            /* 文章バッファ */

            count = s_nowsenthead(sentence_ct);   /* テキスト内の初期値を取得 */
            countold = count;

            while (count < D_CHAR_LAST)
            {
                if (this.text[count] == '/' && text[count + 1] == '/')
                {

                    /*====================*/
                    /*   コメントアウト   */
                    /*====================*/
                    while (checkrowlast(count) == 0)
                    {
                        count++;
                    }
                    count++;
                    countold = count;
                    inrowcount = 0;
                }
                else if (text[count] == ':')
                {
                    textlawbuf = text.Substring(countold + 1, inrowcount - 1);

                    /*====================*/
                    /*    文法コマンド    */
                    /*====================*/
                    if (textlawbuf.Length >= 3 && textlawbuf.Substring(0, 3) == "end")
                    {
                        /*** 文章表示終了処理 ***/
                        sentence_ct = 0;
                        return sentence_ct;
                    }
                    else if (textlawbuf.Substring(0, 1) == "[")
                    {
                        /*** ラベル ***/
                        /* ラベルはすっ飛ばして次へ */

                        count += 2;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                    }
                    else if (textlawbuf.Length >= 3 && textlawbuf.Substring(0, 3) == "JMP")
                    {
                        /*** ジャンプ ***/
                        count = 1 + textlawbuf.Length + text.IndexOf(textlawbuf.Remove(0, 4)) + 1;
                        count = text.IndexOf(textlawbuf.Remove(0, 4));
                        countold = count;
                        sentence_ct = s_getnowsent(count);
                        inrowcount = 0;
                    }
                    else if (textlawbuf.Length >= 3 && textlawbuf.Substring(0, 3) == "If(")
                    {
                        /*** 条件分岐 ***/

                        inrowcount = 4;
                        int inrowcountold = inrowcount;
                        int work_ct = 0;
                        Parameter work_1 = new Parameter();
                        int work_2 = 0;
                        int work_3 = 0;
                        int work_4 = 0;
                        int work_value = 0;

                        /** 条件左辺取得 **/
                        while (textlawbuf.Substring(inrowcount, 1) != " ")
                        {
                            inrowcount++;
                            work_ct++;
                        }
                        if (work_ct >= 3 && "性欲値" == textlawbuf.Substring(inrowcountold, work_ct))
                        {
                            work_1 = Sis.PassionPoint;
                        }
                        else if (work_ct >= 3 && "堕落度" == textlawbuf.Substring(inrowcountold, work_ct))
                        {
                            work_1 = Sis.CorruptionPoint;
                        }
                        else
                        {
                            Console.WriteLine("work_1 該当するパラメーターが存在しないようです");
                        }

                        inrowcount++;
                        inrowcountold = inrowcount;

                        /** 比較演算子取得 **/
                        while (textlawbuf.Substring(inrowcount, 1) != " ")
                        {
                            inrowcount++;
                            work_2++;
                        }

                        inrowcount++;
                        inrowcountold = inrowcount;

                        /** 条件右辺取得 **/
                        /* 文字数取得 */
                        while (textlawbuf.Substring(inrowcountold + work_3, 1) != " " && textlawbuf.Substring(inrowcountold + work_3, 1) != ")")
                        {
                            inrowcount++;
                            work_3++;
                        }

                        /* 値取得 */
                        if (textlawbuf.Length >= inrowcountold + 4 && textlawbuf.Substring(inrowcountold, work_3) == "true")
                        {
                            /* true (bool) */
                            /* bool値は、true⇒1　false⇒0 と変換して使用する */

                            work_value = 1;
                            inrowcount++;
                        }
                        else if (textlawbuf.Length >= inrowcountold + 5 && textlawbuf.Substring(inrowcountold, work_3) == "false")
                        {
                            /* false (bool) */

                            work_value = 0;
                            inrowcount++;
                        }
                        else
                        {
                            /* 整数値 */
                            while (work_value < D_WORKVAL_MAX)
                            {
                                if (textlawbuf.Substring(inrowcountold, work_3) == work_value.ToString())
                                {
                                    break;
                                }
                                work_value++;
                            }
                        }

                        /** ジャンプ先ラベル文字数取得 **/
                        while (textlawbuf.Substring(inrowcount + work_3 + 2 + work_4, 1) != "]")
                        {
                            work_4++;
                        }
                        work_4 += 3;

                        /** 条件式に従ってジャンプ **/
                        if ("<" == textlawbuf.Substring(5 + work_ct, work_2))
                        {
                            if (work_1.CurrentValue < work_value)
                            {
                                count = text.IndexOf(textlawbuf.Substring(inrowcount + work_3, work_4));
                            }
                            else
                            {
                                count++;
                            }
                        }
                        else if ("<=" == textlawbuf.Substring(5 + work_ct, work_2))
                        {

                            if (work_1.CurrentValue <= work_value)
                            {
                                count = text.IndexOf(textlawbuf.Substring(inrowcount + work_3, work_4));
                            }
                            else
                            {
                                count++;
                            }
                        }
                        else if (">" == textlawbuf.Substring(5 + work_ct, work_2))
                        {

                            if (work_1.CurrentValue > work_value)
                            {
                                count = text.IndexOf(textlawbuf.Substring(inrowcount + work_3, work_4));
                            }
                            else
                            {
                                count++;
                            }
                        }
                        else if (">=" == textlawbuf.Substring(5 + work_ct, work_2))
                        {

                            if (work_1.CurrentValue >= work_value)
                            {
                                string aiueo = "\r\n" + textlawbuf.Substring(inrowcount + work_3 + 1, work_4 - 1);
                                count = text.IndexOf(aiueo) + 1;
                            }
                            else
                            {
                                count++;
                            }
                        }
                        else if ("==" == textlawbuf.Substring(5 + work_ct, work_2))
                        {

                            if (work_1.CurrentValue == work_value)
                            {
                                count = text.IndexOf(textlawbuf.Substring(inrowcount + work_3, work_4));
                            }
                            else
                            {
                                count++;
                            }
                        }
                        else if ("!=" == textlawbuf.Substring(5 + work_ct, work_2))
                        {

                            if (work_1.CurrentValue != work_value)
                            {
                                count = text.IndexOf(textlawbuf.Substring(inrowcount + work_3, work_4));
                            }
                            else
                            {
                                count++;
                            }
                        }
                        countold = count;
                        sentence_ct = s_getnowsent(count);
                        inrowcount = 0;
                        inrowcountold = 0;
                    }

                    /*======================*/
                    /*    発言者コマンド    */
                    /*======================*/
                    else if (textlawbuf == "Text")
                    {
                        Color = Brushes.White;

                        count++;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                    }
                    else if (textlawbuf == "サラ")
                    {
                        Color = Brushes.Pink;
                        s_disptachie(o_chrbox1, D_CHR_SARA_00);

                        count++;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                    }
                    else if (textlawbuf == "マリー")
                    {
                        Color = Brushes.Yellow;

                        //    s_disptachie(o_chrbox1, D_CHR_SARA_00);

                        count++;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                    }
                    else if (textlawbuf == "モフィ")
                    {
                        Color = Brushes.Orange;

                        //    s_disptachie(o_chrbox1, D_CHR_SARA_00);

                        count++;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                    }
                    else if (textlawbuf == "魔物")
                    {
                        Color = Brushes.Purple;

                        //    s_disptachie(o_chrbox1, D_CHR_SARA_00);

                        count++;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                    }
                }
                else if (text[count] == ';')
                {

                    //描画先とするImageオブジェクトを作成する
                    Bitmap canvas = new Bitmap(o_bgpic.Width, o_bgpic.Height);
                    //ImageオブジェクトのGraphicsオブジェクトを作成する
                    Graphics g = Graphics.FromImage(canvas);

                    textlawbuf = text.Substring(countold, inrowcount);

                    g.DrawString(textlawbuf, fnt, Color, 0, 0);
                    //PictureBox1に表示する
                    o_bgpic.Image = canvas;

                    //リソースを解放する
                    fnt.Dispose();
                    g.Dispose();

                    count += 2;
                    countold = count;
                    sentence_ct++;
                    inrowcount = 0;
                    break;
                }
                else if ( count >= 2 && checkrowlast(count) == 1 && checkrowlast(count - 2) == 1 )
                {
                    /* 空白行 */
                    count += 1;
                    countold = count;
                    inrowcount = 0;
                }
                else
                {
                    count++;
                    inrowcount++;
                }
            }
            return sentence_ct;
        }


        /*////////////////////////ここからサブルーチン的メソッド////////////////////////*/


        public int s_nowsenthead(int ct)
        {
            /* テキストの現在地取得処理 */
            /* ロード後の文章表示や文章振り返りで使用予定 */

            int i = 0;
            int j;

            for (j = 0; j < ct; j++)
            {
                /* ;が来るまで開始位置からのカウンタを進める */
                while (text[i] != ';' && text[i] != ':')
                {
                    i++;
                }

                i++;

                /* 次の文字が改行コードだったら、さらに一文字進める */
                if (text[i + 1] == '\r' && text[i + 2] == '\n')
                {
                    i++;
                    i++;
                }
            }
            i++;
            return i;
        }

        public int s_getnowsent(int ct)
        {
            /* 現在の行からsentence_ctの値を取得する処理 */
            /* ifやjmpなどでcountが急に変わった後に呼ぶ */

            int i = 0;
            int j;

            for (j = 0; j < (ct); j++)
            {
                if ( text[j] == ':' || text[j] == ';' )
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

            if (text[ct] == '\r' && text[ct + 1] == '\n')
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

