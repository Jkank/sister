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

        public static Defines.fileID nowfile = 0;
        public int sent_ct = 0;
        public int log_ct = 0;          /* ログ現在位置カウンタ */
        public int log_ct_use = 0;      /* ログ最古位置カウンタ */

        public bool MouseLeftPushed = false;

        public doujin_game_sharp()
        {
            InitializeComponent();
            //ホイールイベントの追加  
            this.PNL_Event.MouseWheel
                += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseWheel);
            this.PNL_log.MouseWheel
                += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseWheel); 

            /* --- オブジェクトの背景色を透明にできるようにする処理 --- */
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            /* --- 各オブジェクトの背景色を透明にする --- */
			PNL_Eventslct.BackColor = Color.Transparent;   /* 行動選択画面のPanel */
			PNL_Mainselect.BackColor = Color.Transparent;   /* メイン選択画面のPanel */
            PNL_Event.BackColor = Color.Transparent;   /* メッセージボックスのPanel */
			PIC_Chara_pos1.BackColor = Color.Transparent;   /* 立ち絵位置１ */
			PIC_Chara_pos2.BackColor = Color.Transparent;   /* 立ち絵位置１ */

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
            SetDoubleBuffered(BTN_Start);
            SetDoubleBuffered(BTN_Load);
            SetDoubleBuffered(BTN_OpnOption);
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
            BTN_Start.Image = Properties.Resources.g_btn_000_1;

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                BTN_Start.Image = Properties.Resources.g_btn_000_2;
                MouseLeftPushed = true;
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            BTN_Start.Image = Properties.Resources.g_btn_000_1;
            if ( MouseLeftPushed == true )
			{
				nowfile = Defines.fileID.TXT_OPENING;
				ParmInit();

                PNL_Background.Visible =true;
				PNL_Event.Visible = true;
				sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            BTN_Start.Image = Properties.Resources.g_btn_000_0;
            MouseLeftPushed = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (BTN_Start.ClientRectangle.Contains(BTN_Start.PointToClient(Cursor.Position)) == false)
            {
                BTN_Start.Image = Properties.Resources.g_btn_000_0;
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
        {BTN_Church.Image = Properties.Resources.g_btn_003_1;}
        private void church_btn_MouseDown(object sender, MouseEventArgs e)
        {if (e.Button == MouseButtons.Left){BTN_Church.Image = Properties.Resources.g_btn_003_2;MouseLeftPushed = true;}}
        private void church_btn_MouseUp(object sender, MouseEventArgs e)
        {
            BTN_Church.Image = Properties.Resources.g_btn_003_1;
            if (MouseLeftPushed == true)
            {
				nowfile = Defines.fileID.TXT_CHURCH;
                PNL_Background.Visible = true;
				PNL_Event.Visible = true;
				sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);
            }
        }
        private void church_btn_MouseLeave(object sender, EventArgs e)
        {BTN_Church.Image = Properties.Resources.g_btn_003_0;MouseLeftPushed = false;}
        private void church_btn_MouseMove(object sender, MouseEventArgs e)
        { if (BTN_Church.ClientRectangle.Contains(BTN_Church.PointToClient(Cursor.Position)) == false) { BTN_Church.Image = Properties.Resources.g_btn_003_0; MouseLeftPushed = false; } }
        ////////////////////////////////////////////////////////////////////


        ///////////////////////* === 休息ボタン === *///////////////////////
		private void BTN_Lest_MouseEnter(object sender, EventArgs e)
        {BTN_Lest.Image = Properties.Resources.g_btn_004_1;}
        private void BTN_Lest_MouseDown(object sender, MouseEventArgs e)
		{if (e.Button == MouseButtons.Left) { BTN_Lest.Image = Properties.Resources.g_btn_004_2; MouseLeftPushed = true; }}
		private void BTN_Lest_MouseUp(object sender, MouseEventArgs e)
        {
            BTN_Lest.Image = Properties.Resources.g_btn_004_1;
            if (MouseLeftPushed == true)
			{
				nowfile = Defines.fileID.TXT_LEST;
				PNL_Background.Visible = true;
				PNL_Event.Visible = true;
				sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);
            }
        }
		private void BTN_Lest_MouseLeave(object sender, EventArgs e)
        { BTN_Lest.Image = Properties.Resources.g_btn_004_0; MouseLeftPushed = false; }
		private void BTN_Lest_MouseMove(object sender, MouseEventArgs e)
        { if (BTN_Lest.ClientRectangle.Contains(BTN_Lest.PointToClient(Cursor.Position)) == false) { BTN_Lest.Image = Properties.Resources.g_btn_004_0; MouseLeftPushed = false; } }
        ////////////////////////////////////////////////////////////////////

        ///////////////////////* === 外出ボタン === *///////////////////////
        ////////////////////////////////////////////////////////////////////

        ///////////////////////* === 読書ボタン === *///////////////////////
        ////////////////////////////////////////////////////////////////////

        ////////////////* === オプションボタン（日常） === *////////////////
        ////////////////////////////////////////////////////////////////////


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
                sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

				if (sent_ct == 0)
				{
					log_ct = 0;
					log_ct_use = 0;
					PNL_Event.Visible = false;
					PNL_Mainselect.Visible = true;
				}
				else
				{
					if (log_ct_use < 99)
					{
						log_ct_use++;
					}
					/* パネル３にフォーカス */
					PNL_Event.Focus();
				}
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
                sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

                if (sent_ct == 0)
                {
                    log_ct = 0;
                    log_ct_use = 0;
					PNL_Event.Visible = false;
					PNL_Mainselect.Visible = true;
                }
                else 
                {
					if (log_ct_use < 99)
					{
						log_ct_use++;
					}
					/* パネル３にフォーカス */
					PNL_Event.Focus();
                }
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
                sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

				if (sent_ct == 0)
				{
					log_ct = 0;
					log_ct_use = 0;
					PNL_Event.Visible = false;
					PNL_Mainselect.Visible = true;
				}
				else
				{
					if (log_ct_use < 99)
					{
						log_ct_use++;
					}
					/* パネル３にフォーカス */
					PNL_Event.Focus();
				}
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
                sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

				if (sent_ct == 0)
				{
					log_ct = 0;
					log_ct_use = 0;
					PNL_Event.Visible = false;
					PNL_Mainselect.Visible = true;
				}
				else
				{
					if (log_ct_use < 99)
					{
						log_ct_use++;
					}
					/* パネル３にフォーカス */
					PNL_Event.Focus();
				}
            }
        }

        //バックログ関係
        private void panel3_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 3 && PNL_log.Visible == false)
            {
                // バックログウィンドウ（panel4）を展開
                PNL_log.Visible = true;
                PNL_log.Focus();

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
                    PNL_log.Visible = false;
                    PNL_Event.Focus();
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



		//////////////////////////////////////////////////////////////////
		////////////////////* === 選択肢１ボタン === *////////////////////
		//////////////////////////////////////////////////////////////////


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
                PNL_Eventslct.Visible = false;
                PNL_Event.Visible = true;
                sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

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


		//////////////////////////////////////////////////////////////////
		////////////////////* === 選択肢２ボタン === *////////////////////
		//////////////////////////////////////////////////////////////////


        private void Slctbox_2_MouseEnter(object sender, EventArgs e)
		{
			Slctbox_2.Image = Properties.Resources.g_btn_001_1;
        }

        private void Slctbox_2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Slctbox_2.Image = Properties.Resources.g_btn_001_2;
                MouseLeftPushed = true;
            }
        }

        private void Slctbox_2_MouseUp(object sender, MouseEventArgs e)
        {
            Slctbox_2.Image = Properties.Resources.g_btn_001_1;
            if (MouseLeftPushed == true)
            {
                GameData.ScenarioData.Slct_No = 2;
                PNL_Eventslct.Visible = false;
                PNL_Event.Visible = true;
                sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

            }
        }

        private void Slctbox_2_MouseLeave(object sender, EventArgs e)
        {
            Slctbox_2.Image = Properties.Resources.g_btn_001_0;
            MouseLeftPushed = false;
        }
        
        private void Slctbox_2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Slctbox_2.ClientRectangle.Contains(Slctbox_2.PointToClient(Cursor.Position)) == false)
            {
                Slctbox_2.Image = Properties.Resources.g_btn_001_0;
                MouseLeftPushed = false;
            }
        }

		//////////////////////////////////////////////////////////////////
		////////////////////* === 選択肢２ボタン === *////////////////////
		//////////////////////////////////////////////////////////////////

		private void Slctbox_3_MouseEnter(object sender, EventArgs e)
		{
			Slctbox_3.Image = Properties.Resources.g_btn_001_1;
		}

		private void Slctbox_3_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Slctbox_3.Image = Properties.Resources.g_btn_001_2;
				MouseLeftPushed = true;
			}
		}

		private void Slctbox_3_MouseLeave(object sender, EventArgs e)
		{
			Slctbox_3.Image = Properties.Resources.g_btn_001_0;
			MouseLeftPushed = false;
		}

		private void Slctbox_3_MouseMove(object sender, MouseEventArgs e)
		{
			if (Slctbox_3.ClientRectangle.Contains(Slctbox_3.PointToClient(Cursor.Position)) == false)
			{
				Slctbox_3.Image = Properties.Resources.g_btn_001_0;
				MouseLeftPushed = false;
			}
		}

		private void Slctbox_3_MouseUp(object sender, MouseEventArgs e)
		{
			Slctbox_3.Image = Properties.Resources.g_btn_001_1;
			if (MouseLeftPushed == true)
			{
				GameData.ScenarioData.Slct_No = 3;
				PNL_Eventslct.Visible = false;
				PNL_Event.Visible = true;
				sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);

			}
		}








        /* == 以下サブルーチン的メソッド == */


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：ParmInit			　  　　　　　　　　　　 　■ */
		/* ■　内容：変数初期化	処理							   ■ */
		/* ■　入力：		                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static void ParmInit()
		{

		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：ChangeNowFile		　  　　　　　　　　　　 　■ */
		/* ■　内容：ファイル変更処理							   ■ */
		/* ■　入力：file_id                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        public static void ChangeNowFile(Defines.fileID file_id)
        {
            nowfile = file_id;
        }

        public void setCharacterImageLeft(string imagename)
        {
			setCharacterImage(PIC_Chara_pos1, imagename);
        }

		public void setCharacterImageRight(string imagename)
        {
			setCharacterImage(PIC_Chara_pos2, imagename);
        }

		public void delCharacterImageLeft()
		{
			PIC_Chara_pos1.Visible = false;
		}

		public void delCharacterImageRight()
		{
			PIC_Chara_pos2.Visible = false;
		}

        public void setTextAreaImage(Bitmap canvas)
        {
            textarea.Image = canvas;
        }

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：setSelectBoxImage　  　　　　　　　　　　 　■ */
		/* ■　内容：選択肢表示処理								   ■ */
		/* ■　入力：i                                       　 　 ■ */
		/* ■　		 canvas                                  　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
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

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：setCharacterImage　  　　　　　　　　　　 　■ */
		/* ■　内容：立ち絵設定処理								   ■ */
		/* ■　入力：chrbox                                  　 　 ■ */
		/* ■　      imageID                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        private void setCharacterImage(PictureBox chrbox, string tachie_name)
        {
            if (tachie_name == "サラ") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(恥じらい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(落ち込み)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_2;
			else if (tachie_name == "サラ(驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸恥じらい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(シナ)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(シナ恥じらい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(シナ発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(シナ驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(シナ悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸シナ)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸シナ怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸シナ恥じらい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸シナ発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸シナ驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(裸シナ悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(淫魔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(淫魔怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(淫魔驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(淫魔発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "サラ(淫魔悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "マリー") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "マリー(驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "マリー(怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "マリー(発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
            else if (tachie_name == "リディ") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
            else if (tachie_name == "リディ(笑顔)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
            else if (tachie_name == "リディ(恥じらい)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
            else if (tachie_name == "リディ(恐怖)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "リディ(苦しみ)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "触手娘") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "触手娘(恥じらい)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "触手娘(苦しみ)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
            else if (tachie_name == "魔物") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
            else if (tachie_name == "魔物(悪笑い)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
            else if (tachie_name == "魔物(驚き)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
            chrbox.Visible = true;
        }

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：setBGPic         　  　　　　　　　　　　 　■ */
		/* ■　内容：背景画像表示処理							   ■ */
		/* ■　入力：BGPicname                               　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void setBGPic(string BGPicname)
		{

			switch (BGPicname)
            {
				case "オープニング":
					PNL_Background.BackgroundImage = Properties.Resources.g_bg_000_0;
					break;
				case "教会昼":
					PNL_Background.BackgroundImage = Properties.Resources.kyoukai_dt;
					break;
				case "教会夕":
					PNL_Background.BackgroundImage = Properties.Resources.kyoukai_ev;
					break;
				case "教会夜":
					PNL_Background.BackgroundImage = Properties.Resources.kyoukai_nt;
					break;
				case "階段":
					PNL_Background.BackgroundImage = Properties.Resources.dungeon06;
					break;
				case "檻":
					PNL_Background.BackgroundImage = Properties.Resources.rouya_nt;
					break;
				case "部屋昼":
					PNL_Background.BackgroundImage = Properties.Resources.yadoya_dt;
					break;
				case "部屋夕":
					PNL_Background.BackgroundImage = Properties.Resources.yadoya_ev;
					break;
				case "部屋夜":
					PNL_Background.BackgroundImage = Properties.Resources.yadoya_ntr;
					break;
				case "書庫":
					PNL_Background.BackgroundImage = Properties.Resources.syoko_dt;
					break;
				case "町昼":
					PNL_Background.BackgroundImage = Properties.Resources.hiroba_dt;
					break;
				case "町夕":
					PNL_Background.BackgroundImage = Properties.Resources.hiroba_ev;
					break;
				case "町夜":
					PNL_Background.BackgroundImage = Properties.Resources.hiroba_nt;
					break;
				default:
					Console.WriteLine("背景画像の指定値がテーブル上に用意されていません");
					break;
					
			}
		}
    
        //SE.csから移植/TODO:メンテナンス
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：dispSlctBox　  　　　　　　 　　　　　　　 　■ */
        /* ■　内容：選択肢パネルを表示する処理              　 　 ■ */
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


            PNL_Eventslct.Visible = true;
            PNL_Event.Visible = false;
        }
    }    
}
