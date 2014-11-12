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
using DoujinGameProject.Data;
using DoujinGameProject.Action;

namespace DoujinGameProject
{
    public partial class doujin_game_sharp : Form
    {
        /*--- ここに使用するクラスを置く ---*/
        //public Sister Sara = new Sister();
        
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
            this.logwindow.MouseWheel
                += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseWheel); 

            /* --- オブジェクトの背景色を透明にできるようにする処理 --- */
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            /* --- 各オブジェクトの背景色を透明にする --- */
            panel2.BackColor = Color.Transparent;   /* 行動選択画面のPanel */
            panel3.BackColor = Color.Transparent;   /* メッセージボックスのPanel */
            panel_slct.BackColor = Color.Transparent;   /* 選択肢のPanel */
            //panel4.BackColor = Color.Transparent;   /* メッセージボックスのPanel */
            chara_pos1.BackColor = Color.Transparent;   /* 立ち絵位置１ */

            /* --- データ類初期化 --- */
            //TODO: ここでやるか、NewGame/LoadGame時にやるか検討
            GameData.Initialize();

            GameData.SisterData.PassionPoint.MaxValue = 100;
            GameData.SisterData.MoralPoint.MaxValue = 100;
            GameData.SisterData.PassionPoint.CurrentValue = 80;
            GameData.SisterData.MoralPoint.CurrentValue = 20;

            GameData.ScenarioData.Slct_No = 0;

            //this.DoubleBuffered = true;  // ダブルバッファリング: 画面のちらつきを抑止
            ////////確認中　ちらつき防止にダブルバッファリングを有効にしたい////////
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            ////////////////////////////////////////////////////////////////////////
            SetDoubleBuffered(pictureBox1);
            SetDoubleBuffered(pictureBox2);
            SetDoubleBuffered(pictureBox3);
        }

//        string line;

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            // リモートアクセス中の場合は何もしない
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                return;
            }

            // ダブルバッファ制御用のプロパティを強制的に取得する
            System.Reflection.PropertyInfo p;
            p = typeof(System.Windows.Forms.Control).GetProperty(
                         "DoubleBuffered",
                          System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            // ダブルバッファを有効にする
            p.SetValue(c, true, null);
        }

        /////////////////////* === スタートボタン === */////////////////////
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
                background.Visible = true;
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
        ////////////////////////////////////////////////////////////////////



        //////////////////////* === ロードボタン === *//////////////////////


        ////////////////////////////////////////////////////////////////////



        ////////////////////* === オプションボタン === *////////////////////


        ////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////
        //////////////////////* === 通常選択画面 === *//////////////////////
        ////////////////////////////////////////////////////////////////////

        ///////////////////////* === 教会ボタン === *///////////////////////
        private void church_btn_MouseEnter(object sender, EventArgs e)
        {church_btn.Image = Properties.Resources.g_btn_003_1;}
        private void church_btn_MouseDown(object sender, MouseEventArgs e)
        {if (e.Button == MouseButtons.Left){church_btn.Image = Properties.Resources.g_btn_003_2;MouseLeftPushed = true;}}
        private void church_btn_MouseUp(object sender, MouseEventArgs e)
        {church_btn.Image = Properties.Resources.g_btn_003_1;if (MouseLeftPushed == true){background.Visible = true;}}
        private void church_btn_MouseLeave(object sender, EventArgs e)
        {church_btn.Image = Properties.Resources.g_btn_003_0;MouseLeftPushed = false;}
        private void church_btn_MouseMove(object sender, MouseEventArgs e)
        {if (pictureBox1.ClientRectangle.Contains(pictureBox1.PointToClient(Cursor.Position)) == false){pictureBox1.Image = Properties.Resources.g_btn_000_0;MouseLeftPushed = false;}}
        ////////////////////////////////////////////////////////////////////


        ///////////////////////* === 休息ボタン === *///////////////////////




        ////////////////////////////////////////////////////////////////////
        //////////////////////* === イベント画面 === *//////////////////////
        ////////////////////////////////////////////////////////////////////
        private void textarea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (textarea.Visible == true)
                {
                    /* テキストエリア表示中だったら、表示消す */
                    textarea.Visible = false;
                }
                else
                {
                    /* テキストエリア消去中だったら、表示する */
                    textarea.Visible = true;
                }
            }
            else
            {
                sent_ct = SE.ScriptEngine(sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

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
        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (textarea.Visible == true)
                {
                    /* テキストエリア表示中だったら、表示消す */
                    textarea.Visible = false;
                }
                else
                {
                    /* テキストエリア消去中だったら、表示する */
                    textarea.Visible = true;
                }
            }
            else
            {
                sent_ct = SE.ScriptEngine(sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

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
        }
        private void chara_pos_1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (textarea.Visible == true)
                {
                    /* テキストエリア表示中だったら、表示消す */
                    textarea.Visible = false;
                }
                else
                {
                    /* テキストエリア消去中だったら、表示する */
                    textarea.Visible = true;
                }
            }
            else
            {
                sent_ct = SE.ScriptEngine(sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

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
        }
        private void chara_pos2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (textarea.Visible == true)
                {
                    /* テキストエリア表示中だったら、表示消す */
                    textarea.Visible = false;
                }
                else
                {
                    /* テキストエリア消去中だったら、表示する */
                    textarea.Visible = true;
                }
            }
            else
            {
                sent_ct = SE.ScriptEngine(sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

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
        }

        //バックログ関係
        private void panel3_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 3 && logwindow.Visible == false)
            {
                // バックログウィンドウ（panel4）を展開
                logwindow.Visible = true;
                logwindow.Focus();

                // スクロール画面初期状態
                SE.ScrollInit(log_ct_use, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14);
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
                    logwindow.Visible = false;
                    panel3.Focus();
                }
                else
                {
                    log_ct--;
                    SE.ScrollRedraw(log_ct, log_ct_use, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14);
                }
            }
            else if (e.Delta > 3)
            {
                if (log_ct + 4 < log_ct_use)
                {
                    log_ct++;
                    /* スクロール画面の一番上ではない */
                    SE.ScrollRedraw(log_ct, log_ct_use, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14);
                }
            }
        }



        ////////////////////* === 選択肢１ボタン === *////////////////////


        private void Slctbox_1_MouseEnter(object sender, EventArgs e)
        {
            Slctbox_1.Image = Properties.Resources.g_btn_000_1;
        }

        private void Slctbox_1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Slctbox_1.Image = Properties.Resources.g_btn_000_2;
                MouseLeftPushed = true;
            }
        }

        private void Slctbox_1_MouseUp(object sender, MouseEventArgs e)
        {
            Slctbox_1.Image = Properties.Resources.g_btn_000_1;
            if (MouseLeftPushed == true)
            {
                GameData.ScenarioData.Slct_No = 1;
                panel_slct.Visible = false;
                sent_ct = SE.ScriptEngine(sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

            }
        }

        private void Slctbox_1_MouseLeave(object sender, EventArgs e)
        {
            Slctbox_1.Image = Properties.Resources.g_btn_000_0;
            MouseLeftPushed = false;
        }

        private void Slctbox_1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Slctbox_1.ClientRectangle.Contains(Slctbox_1.PointToClient(Cursor.Position)) == false)
            {
                Slctbox_1.Image = Properties.Resources.g_btn_000_0;
                MouseLeftPushed = false;
            }
        }


        ////////////////////* === 選択肢２ボタン === *////////////////////


        private void Slctbox_2_MouseEnter(object sender, EventArgs e)
        {
            Slctbox_2.Image = Properties.Resources.g_btn_000_1;
        }

        private void Slctbox_2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Slctbox_2.Image = Properties.Resources.g_btn_000_2;
                MouseLeftPushed = true;
            }
        }

        private void Slctbox_2_MouseUp(object sender, MouseEventArgs e)
        {
            Slctbox_2.Image = Properties.Resources.g_btn_000_1;
            if (MouseLeftPushed == true)
            {
                GameData.ScenarioData.Slct_No = 2;
                panel_slct.Visible = false;
                sent_ct = SE.ScriptEngine(sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

            }
        }

        private void Slctbox_2_MouseLeave(object sender, EventArgs e)
        {
            Slctbox_2.Image = Properties.Resources.g_btn_000_0;
            MouseLeftPushed = false;
        }
        
        private void Slctbox_2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Slctbox_2.ClientRectangle.Contains(Slctbox_1.PointToClient(Cursor.Position)) == false)
            {
                Slctbox_2.Image = Properties.Resources.g_btn_000_0;
                MouseLeftPushed = false;
            }
        }

        ////////////////////////////////////////////////////////////////////








        /* == 以下サブルーチン的メソッド == */
        public void setCharacterImageLeft(Defines.CharacterImageID imageID)
        {
            setCharacterImage(chara_pos1, imageID);
        }

        public void setCharacterImageRight(Defines.CharacterImageID imageID)
        {
            setCharacterImage(chara_pos2, imageID);
        }

        public void setTextAreaImage(Bitmap canvas)
        {
            textarea.Image = canvas;
        }

        public void setSelectBoxImage(int i, Bitmap canvas)
        {
            switch (i)
            {
                case 1:
                    Slctbox_1.Image = canvas;
                    break;
                case 2:
                    Slctbox_2.Image = canvas;
                    break;
                case 3:
                    Slctbox_3.Image = canvas;
                    break;
                case 4:
                    Slctbox_4.Image = canvas;
                    break;
                default:
                    break;
            }
            //textarea.Image = canvas;
        }

        private void setCharacterImage(PictureBox chrbox, Defines.CharacterImageID imageID)
        {
            switch (imageID)
            {
                case Defines.CharacterImageID.D_CHR_SARA_00:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_01:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_02:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_03:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_04:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_05:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_06:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_07:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_08:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_09:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_10:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_11:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_12:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_13:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_14:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_15:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_16:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_17:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_18:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_SARA_19:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_LIDY_00:
                    chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_LIDY_01:
                    chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_LIDY_02:
                    chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_LIDY_03:
                    chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_LIDY_04:
                    chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_DEVIL_00:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_DEVIL_01:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_DEVIL_02:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_DEVIL_03:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_DEVIL_04:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                case Defines.CharacterImageID.D_CHR_DEVIL_05:
                    chrbox.BackgroundImage = Properties.Resources.sara_0_0;
                    break;
                default:
                    break;
            }
            chrbox.Visible = true;
        }
    
        //SE.csから移植/TODO:メンテナンス
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：s_BacklogRenew　　　　　　 　　　　　　　 　■ */
        /* ■　内容：バックログ用文字列配列を更新する処理    　 　 ■ */
        /* ■　入力：Slct_ct                                 　 　 ■ */
        /* ■　出力：なし                                    　 　 ■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        public void dispSlctBox(int Slct_ct_max)
        {


            int i;

            // 選択肢ボックス位置
            Point Position = new Point(Slctbox_1.Location.X, Slctbox_1.Location.Y); // 選択肢ボックス位置

            if (Slct_ct_max == 4)
            {
                Slctbox_1.Visible = true;
                Slctbox_2.Visible = true;
                Slctbox_3.Visible = true;
                Slctbox_4.Visible = true;

                Position.X = 175;       /* fail safe のためX座標も再設定 */
                Position.Y = 100;
                Slctbox_1.Location = Position;
                Position.Y = 190;
                Slctbox_2.Location = Position;
                Position.Y = 280;
                Slctbox_3.Location = Position;
                Position.Y = 370;
                Slctbox_4.Location = Position;

            }
            else if (Slct_ct_max == 3)
            {
                Slctbox_1.Visible = true;
                Slctbox_2.Visible = true;
                Slctbox_3.Visible = true;
                Slctbox_4.Visible = false;

                Position.X = 175;       /* fail safe のためX座標も再設定 */
                Position.Y = 130;
                Slctbox_1.Location = Position;
                Position.Y = 235;
                Slctbox_2.Location = Position;
                Position.Y = 340;
                Slctbox_3.Location = Position;
                
            }
            else if (Slct_ct_max == 2)
            {
                Slctbox_1.Visible = true;
                Slctbox_2.Visible = true;
                Slctbox_3.Visible = false;
                Slctbox_4.Visible = false;

                Position.X = 175;       /* fail safe のためX座標も再設定 */
                Position.Y = 145;
                Slctbox_1.Location = Position;
                Position.Y = 325;
                Slctbox_2.Location = Position;
            }


            panel_slct.Visible = true;
        }













    }    
}