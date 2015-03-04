using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DoujinGameProject.Data;
using DoujinGameProject.Action;
using System.Runtime.InteropServices;

namespace DoujinGameProject
{
	public partial class doujin_game_sharp : Form
	{
		/*--- ここに使用するクラスを置く ---*/

		public static Defines.fileID nowfile = 0;
		public int sent_ct = 0;
		public int log_ct = 0;          /* ログ現在位置カウンタ */
		public int log_ct_use = 0;      /* ログ最古位置カウンタ */

		public bool MouseLeftPushed = false;
		public bool fadein = false;
		public int fadestage = 0;
		public int currentAlphaPercent = 0;
		public Image currentImage1;
		public Control pictboxfade1;
		public Control pictboxfade2;

		static Image canvas_tb1;
		static Image canvas_tb2;

		DirectSound dsound;
		
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
			PIC_NameBox.BackColor = Color.Transparent;   /* 名前ボックス */
			PIC_TextArea.BackColor = Color.Transparent;   /* メッセージボックス */
			PIC_Chara_pos1.BackColor = Color.Transparent;   /* 立ち絵位置１ */
			PIC_Chara_pos2.BackColor = Color.Transparent;   /* 立ち絵位置２ */

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
			//EnableDoubleBuffering(PNL_Background);
			EnableDoubleBuffering(PNL_Event);
		}

		//        string line;


		public static void EnableDoubleBuffering(Control control)
		{
			control.GetType().InvokeMember(
			   "DoubleBuffered",
			   BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
			   null,
			   control,
			   new object[] { true });
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
			if (MouseLeftPushed == true)
			{
				nowfile = Defines.fileID.TXT_OPENING;
				
				/* テキストエリアの半透明表示 */
				ShowHalfClearly(PIC_NameBox);
				ShowHalfClearly(PIC_TextArea);

				sent_ct = SE.ScriptEngine(nowfile, sent_ct, log_ct, log_ct_use, GameData.ScenarioData.Slct_No);
				PNL_Background.Visible = true;
				PNL_Event.Visible = true;
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
		{ BTN_Church.Image = Properties.Resources.g_btn_003_1; }
		private void church_btn_MouseDown(object sender, MouseEventArgs e)
		{ if (e.Button == MouseButtons.Left) { BTN_Church.Image = Properties.Resources.g_btn_003_2; MouseLeftPushed = true; } }
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
		{ BTN_Church.Image = Properties.Resources.g_btn_003_0; MouseLeftPushed = false; }
		private void church_btn_MouseMove(object sender, MouseEventArgs e)
		{ if (BTN_Church.ClientRectangle.Contains(BTN_Church.PointToClient(Cursor.Position)) == false) { BTN_Church.Image = Properties.Resources.g_btn_003_0; MouseLeftPushed = false; } }
		////////////////////////////////////////////////////////////////////


		///////////////////////* === 休息ボタン === *///////////////////////
		private void BTN_Lest_MouseEnter(object sender, EventArgs e)
		{ BTN_Lest.Image = Properties.Resources.g_btn_004_1; }
		private void BTN_Lest_MouseDown(object sender, MouseEventArgs e)
		{ if (e.Button == MouseButtons.Left) { BTN_Lest.Image = Properties.Resources.g_btn_004_2; MouseLeftPushed = true; } }
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
				if (PIC_TextArea.Visible == true)
				{
					/* テキストエリア表示中だったら、表示消す */
					PIC_TextArea.Visible = false;
					PIC_NameBox.Visible = false;
				}
				else
				{
					/* テキストエリア消去中だったら、表示する */
					PIC_TextArea.Visible = true;
					PIC_NameBox.Visible = true;
				}
			}
			else if (PIC_TextArea.Visible == true)
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

		private void PIC_NameBox_MouseDown_1(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (PIC_TextArea.Visible == true)
				{
					/* テキストエリア表示中だったら、表示消す */
					PIC_TextArea.Visible = false;
					PIC_NameBox.Visible = false;
				}
				else
				{
					/* テキストエリア消去中だったら、表示する */
					PIC_TextArea.Visible = true;
					PIC_NameBox.Visible = true;
				}
			}
			else if (PIC_TextArea.Visible == true)
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
				if (PIC_TextArea.Visible == true)
				{
					/* テキストエリア表示中だったら、表示消す */
					PIC_TextArea.Visible = false;
					PIC_NameBox.Visible = false;
				}
				else
				{
					/* テキストエリア消去中だったら、表示する */
					PIC_TextArea.Visible = true;
					PIC_NameBox.Visible = true;
				}
			}
			else if (PIC_TextArea.Visible == true)
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
				if (PIC_TextArea.Visible == true)
				{
					/* テキストエリア表示中だったら、表示消す */
					PIC_TextArea.Visible = false;
					PIC_NameBox.Visible = false;
				}
				else
				{
					/* テキストエリア消去中だったら、表示する */
					PIC_TextArea.Visible = true;
					PIC_NameBox.Visible = true;
				}
			}
			else if (PIC_TextArea.Visible == true)
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
				if (PIC_TextArea.Visible == true)
				{
					/* テキストエリア表示中だったら、表示消す */
					PIC_TextArea.Visible = false;
					PIC_NameBox.Visible = false;
				}
				else
				{
					/* テキストエリア消去中だったら、表示する */
					PIC_TextArea.Visible = true;
					PIC_NameBox.Visible = true;
				}
			}
			else if (PIC_TextArea.Visible == true)
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
		private void PIC_NameBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				if (PIC_TextArea.Visible == true)
				{
					/* テキストエリア表示中だったら、表示消す */
					PIC_TextArea.Visible = false;
					PIC_NameBox.Visible = false;
				}
				else
				{
					/* テキストエリア消去中だったら、表示する */
					PIC_TextArea.Visible = true;
					PIC_NameBox.Visible = true;
				}
			}
			else if (PIC_TextArea.Visible == true)
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
				// バックログパネルを展開
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
					/* ⇒イベントテキストパネルに戻る */
					PNL_log.Visible = false;
					PNL_Event.Focus();

					// イベントテキストパネルを展開
					PNL_Event.Visible = true;
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
		/* ■　関数名：ChangeNowFile		　  　　　　　　　　　		■ */
		/* ■　内容：参照ファイル変更処理						   ■ */
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
			PIC_TextArea.Image = canvas;
		}

		public void setNameBoxImage(Bitmap canvas)
		{
			PIC_NameBox.Image = canvas;
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
			else if (tachie_name == "サラ(後ろめたい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(悪)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
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

			if (chrbox.Visible == false)
			{
				fadein = true;
				currentAlphaPercent = 0;


				pictboxfade1 = chrbox;
				currentImage1 = chrbox.BackgroundImage;

				//最初に透明度100%の画像を描画してしまう。（一瞬透明度0%の画像が表示されるのを防ぐ）
				Image nowimg = CreateTranslucentImage(currentImage1, currentAlphaPercent * 0.01f);
				pictboxfade1.BackgroundImage = nowimg;

				FadeInTimer.Interval = 10;
				//タイマーをスタート
				FadeInTimer.Start();
				chrbox.Visible = true;
			}
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
					PNL_Event.BackgroundImage = Properties.Resources.g_bg_000_0;
					//PNL_Background.BackgroundImage = Properties.Resources.g_bg_000_0;
					break;
				case "教会昼":
					PNL_Event.BackgroundImage = Properties.Resources.kyoukai_dt;
					//PNL_Background.BackgroundImage = Properties.Resources.kyoukai_dt;
					break;
				case "教会夕":
					PNL_Event.BackgroundImage = Properties.Resources.kyoukai_ev;
					//PNL_Background.BackgroundImage = Properties.Resources.kyoukai_ev;
					break;
				case "教会夜":
					PNL_Event.BackgroundImage = Properties.Resources.kyoukai_nt;
					//PNL_Background.BackgroundImage = Properties.Resources.kyoukai_nt;
					break;
				case "階段":
					PNL_Event.BackgroundImage = Properties.Resources.dungeon06;
					//PNL_Background.BackgroundImage = Properties.Resources.dungeon06;
					break;
				case "檻":
					PNL_Event.BackgroundImage = Properties.Resources.rouya_nt;
					//PNL_Background.BackgroundImage = Properties.Resources.rouya_nt;
					break;
				case "部屋昼":
					PNL_Event.BackgroundImage = Properties.Resources.yadoya_dt;
					//PNL_Background.BackgroundImage = Properties.Resources.yadoya_dt;
					break;
				case "部屋夕":
					PNL_Event.BackgroundImage = Properties.Resources.yadoya_ev;
					//PNL_Background.BackgroundImage = Properties.Resources.yadoya_ev;
					break;
				case "部屋夜":
					PNL_Event.BackgroundImage = Properties.Resources.yadoya_ntr;
					//PNL_Background.BackgroundImage = Properties.Resources.yadoya_ntr;
					break;
				case "書庫":
					PNL_Event.BackgroundImage = Properties.Resources.syoko_dt;
					//PNL_Background.BackgroundImage = Properties.Resources.syoko_dt;
					break;
				case "町昼":
					PNL_Event.BackgroundImage = Properties.Resources.hiroba_dt;
					//PNL_Background.BackgroundImage = Properties.Resources.hiroba_dt;
					break;
				case "町夕":
					PNL_Event.BackgroundImage = Properties.Resources.hiroba_ev;
					//PNL_Background.BackgroundImage = Properties.Resources.hiroba_ev;
					break;
				case "町夜":
					PNL_Event.BackgroundImage = Properties.Resources.hiroba_nt;
					//PNL_Background.BackgroundImage = Properties.Resources.hiroba_nt;
					break;
				default:
					Console.WriteLine("背景画像の指定値がテーブル上に用意されていません");
					break;

			}

		}

		void ShowHalfClearly(PictureBox NowPicturebox)
		{
			Graphics g_tb = null;
			if (NowPicturebox == PIC_TextArea)
			{
				//描画先とするImageオブジェクトを作成する
				canvas_tb1 = new Bitmap(NowPicturebox.Width, NowPicturebox.Height);
				//ImageオブジェクトのGraphicsオブジェクトを作成する
				g_tb = Graphics.FromImage(canvas_tb1);
			}
			else if (NowPicturebox == PIC_NameBox)
			{
				canvas_tb2 = new Bitmap(NowPicturebox.Width, NowPicturebox.Height);
				//ImageオブジェクトのGraphicsオブジェクトを作成する
				g_tb = Graphics.FromImage(canvas_tb2);
			}


			//画像を読み込む
			Image img_tb = NowPicturebox.BackgroundImage;

			//ColorMatrixオブジェクトの作成
			System.Drawing.Imaging.ColorMatrix cm =
				new System.Drawing.Imaging.ColorMatrix();
			//ColorMatrixの行列の値を変更して、アルファ値が0.5に変更されるようにする
			cm.Matrix00 = 1;
			cm.Matrix11 = 1;
			cm.Matrix22 = 1;
			cm.Matrix33 = 0.5F;
			cm.Matrix44 = 1;

			//ImageAttributesオブジェクトの作成
			System.Drawing.Imaging.ImageAttributes ia =
				new System.Drawing.Imaging.ImageAttributes();
			//ColorMatrixを設定する
			ia.SetColorMatrix(cm);

			//ImageAttributesを使用して画像を描画
			g_tb.DrawImage(img_tb, new Rectangle(0, 0, img_tb.Width, img_tb.Height),
				0, 0, img_tb.Width, img_tb.Height, GraphicsUnit.Pixel, ia);

			//リソースを解放する
			img_tb.Dispose();
			g_tb.Dispose();

			//PictureBox1に表示する
			if (NowPicturebox == PIC_TextArea)
			{
				NowPicturebox.BackgroundImage = canvas_tb1;
			}
			else if (NowPicturebox == PIC_NameBox)
			{
				NowPicturebox.BackgroundImage = canvas_tb2;
			}
		}


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：IsPassionLimit  　　　　　 　　　　　　　 　■ */
		/* ■　内容：性欲限界判定                               　 ■ */
		/* ■　入力：                                        　 　 ■ */
		/* ■　出力：体力切れ…true                          　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static bool IsPassionLimit(Parameter passion)
		{
			if (passion.CurrentValue >= passion.MaxValue)
			{
				return true;
			}
			else
			{
				return false;
			}
		}



		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：IsStaminaRunout  　　　　　 　　　　　　　 　■ */
		/* ■　内容：体力切れ判定                               　 ■ */
		/* ■　入力：                                        　 　 ■ */
		/* ■　出力：体力切れ…true                          　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static int IsStaminaRunout(Parameter stamina)
		{
			if (stamina.CurrentValue < 0)
			{
				return 2;
			}
			else if (stamina.CurrentValue <= 10)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：IsMentalRunout  　　　　　 　　　　　　　 　■ */
		/* ■　内容：気力切れ判定                               　 ■ */
		/* ■　入力：                                        　 　 ■ */
		/* ■　出力：体力切れ…true                          　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static int IsMentalRunout(Parameter energy)
		{
			if (energy.CurrentValue < 0)
			{
				return 2;
			}
			else if (energy.CurrentValue <= 10)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}



		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：IsDayFin      　　　　　　 　　　　　　　 　■ */
		/* ■　内容：１日終了判定                               　 ■ */
		/* ■　入力：                                        　 　 ■ */
		/* ■　出力：１日終了…true                          　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static bool IsDayFin(int time)
		{
			if (time > 24)
			{
				return true;
			}
			else
			{
				return false;
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


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：DrawCharacterName 　　　　 　　　　　　　 　■ */
		/* ■　内容：キャラクター名を表示する処理            　 　 ■ */
		/* ■　入力：Slct_ct                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void DrawCharacterName(string Name)
		{
			Font fnt = new Font("メイリオ", 16);
			Bitmap canvas0 = new Bitmap(PIC_NameBox.Width, PIC_NameBox.Height);
			/* ImageオブジェクトのGraphicsオブジェクトを作成する */
			Graphics g0 = Graphics.FromImage(canvas0);
			/* 描画内容を準備 */
			g0.DrawString(Name, fnt, Brushes.White, 20, 10);
			/* PictureBoxに表示*/
			PIC_NameBox.Image = canvas0;
			/* リソースを解放 */
			fnt.Dispose();
			g0.Dispose();
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：ClearCharacterName 　　　　 　　　　　　　  ■ */
		/* ■　内容：キャラクター名をクリアする処理          　 　 ■ */
		/* ■　入力：Slct_ct                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void ClearCharacterName()
		{
			Font fnt = new Font("メイリオ", 16);
			Bitmap canvas0 = new Bitmap(PIC_NameBox.Width, PIC_NameBox.Height);
			/* ImageオブジェクトのGraphicsオブジェクトを作成する */
			Graphics g0 = Graphics.FromImage(canvas0);
			/* 描画内容を準備 */
			g0.DrawString(" ", fnt, Brushes.White, 0, 10);
			/* PictureBoxに表示*/
			PIC_NameBox.Image = canvas0;
			/* リソースを解放 */
			fnt.Dispose();
			g0.Dispose();
		}


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：CreateTranslucentImage 　　 　　　　　　　  ■ */
		/* ■　内容：指定された画像をフェードインさせる処理  　 　 ■ */
		/* ■　入力：Slct_ct                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static Image CreateTranslucentImage(Image img, float alpha)
		{

			//半透明の画像の描画先となるImageオブジェクトを作成
			Bitmap transImg = new Bitmap(img.Width, img.Height);
			//transImgのGraphicsオブジェクトを取得
			Graphics g = Graphics.FromImage(transImg);
			///Graphics g = Graphics.FromImage(img);

			//imgを半透明にしてtransImgに描画
			System.Drawing.Imaging.ColorMatrix cm =
				new System.Drawing.Imaging.ColorMatrix();
			cm.Matrix00 = 1;
			cm.Matrix11 = 1;
			cm.Matrix22 = 1;
			cm.Matrix33 = alpha;
			cm.Matrix44 = 1;
			System.Drawing.Imaging.ImageAttributes ia =
				new System.Drawing.Imaging.ImageAttributes();
			ia.SetColorMatrix(cm);
			g.DrawImage(img,
				new Rectangle(0, 0, img.Width, img.Height),
				0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

			//リソースを解放する
			g.Dispose();

			return transImg;
			///return img;
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：FadeinTimer_Tick		 　　 　　　　　　　  ■ */
		/* ■　内容：フェードイン用タイマ処理				　 　 ■ */
		/* ■　入力：Slct_ct                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		private void FadeinTimer_Tick(object sender, EventArgs e)
		{
			//透明度を決定
			if (fadein)
			{
				currentAlphaPercent += 5;
				if (100 <= currentAlphaPercent)
				{
					fadein = false;
				}
			}
			else
			{
				//タイマーを停止
				((Timer)sender).Stop();
			}

			//半透明の画像を作成
			Image nowimg = CreateTranslucentImage(currentImage1, currentAlphaPercent * 0.01f);

			//半透明の画像を表示
			if (pictboxfade1.BackgroundImage != null)
			{
				//pictboxfade1.BackgroundImage.Dispose();
			}
			pictboxfade1.BackgroundImage = nowimg;
			BackgroundDraw(PIC_NameBox);
			BackgroundDraw(PIC_TextArea);
		}


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：BackgroundDraw			 　　 　　　　　　 ■ */
		/* ■　内容：画像・パネルを描画する際、					   ■ */
		/* ■　　　　見た目上他の画像が消えないようにする処理　 　 ■ */
		/* ■　入力：targetpanel_id                          　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void BackgroundDraw(PictureBox NowPicturebox)
		{

			//現在の背景画像の状況を再現する。
			Bitmap canvas = new Bitmap(NowPicturebox.Width, NowPicturebox.Height);
			Graphics g = Graphics.FromImage(canvas);

			Image Image1 = PIC_Chara_pos1.BackgroundImage;
			Image Image2 = PIC_Chara_pos2.BackgroundImage;
			Image canvas_img;
			if (NowPicturebox == PIC_TextArea)
			{
				canvas_img = canvas_tb1;
			}
			else if (NowPicturebox == PIC_NameBox)
			{
				canvas_img = canvas_tb2;
			}
			else
			{
				canvas_img = null;
			}

			if (Image1 != null && PIC_Chara_pos1.Visible == true)
			{
				g.DrawImage(Image1, PIC_Chara_pos1.Location.X - NowPicturebox.Location.X, PIC_Chara_pos1.Location.Y - NowPicturebox.Location.Y, Image1.Width, Image1.Height);
			}
			if (Image2 != null && PIC_Chara_pos2.Visible == true)
			{
				g.DrawImage(Image2, PIC_Chara_pos2.Location.X - NowPicturebox.Location.X, PIC_Chara_pos2.Location.Y - NowPicturebox.Location.Y, Image2.Width, Image2.Height);
			}

			g.DrawImage(canvas_img, 0, 0, canvas_img.Width, canvas_img.Height);


			//Graphicsオブジェクトのリソースを解放する
			g.Dispose();

			//PictureBoxに表示する
			NowPicturebox.BackgroundImage = canvas;
		}

		public void BackgroundDraw2(int a)
		{
			BackgroundDraw(PIC_NameBox);
			BackgroundDraw(PIC_TextArea);
		}

		/////////////////////////////////////////////////////////////////////////////////////
		//		ステータス画面
		/////////////////////////////////////////////////////////////////////////////////////

		private void Gauge_HP_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.HitPoint.CurrentValue > 150)
			{
				txt = "どれほど動き回っても疲れる気がしない";
			}
			else if (GameData.SisterData.HitPoint.CurrentValue > 100)
			{
				txt = "体力が有り余っている";
			}
			else if (GameData.SisterData.HitPoint.CurrentValue > 70)
			{
				txt = "特に疲れは感じていない";
			}
			else if (GameData.SisterData.HitPoint.CurrentValue > 30)
			{
				txt = "激しく体を動かすには疲れすぎている";
			}
			else
			{
				txt = "疲れきっていて体が重い";
			}

			PrintSklTxt(txt);
		}
		private void Gauge_HP_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void Gauge_Mental_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.MentalPoint.CurrentValue > 150)
			{
				txt = "どんなことでもできそうな万能感に満ち溢れている";
			}
			else if (GameData.SisterData.MentalPoint.CurrentValue > 100)
			{
				txt = "気力は充実している";
			}
			else if (GameData.SisterData.MentalPoint.CurrentValue > 70)
			{
				txt = "気力面で特に問題は抱えていない";
			}
			else if (GameData.SisterData.MentalPoint.CurrentValue > 30)
			{
				txt = "ストレスを感じて精神的に疲れている";
			}
			else
			{
				txt = "何もする気力が湧かない";
			}

			PrintSklTxt(txt);
		}

		private void Gauge_Mental_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void Gauge_Moral_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.MoralPoint.CurrentValue > 80)
			{
				txt = "常に人々の事を考え、慈愛に満ちている";
			}
			else if (GameData.SisterData.MoralPoint.CurrentValue > 60)
			{
				txt = "他者の苦しみを自分の苦しみとして捉えることができる";
			}
			else if (GameData.SisterData.MoralPoint.CurrentValue > 40)
			{
				txt = "他人のために自分が苦しむことに疑問を覚えている";
			}
			else if (GameData.SisterData.MoralPoint.CurrentValue > 20)
			{
				txt = "他者の事情より、自分の都合や感情を優先する";
			}
			else
			{
				txt = "自分の欲望のためなら誰がどうなろうが関係ない";
			}

			PrintSklTxt(txt);
		}

		private void Gauge_Moral_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void Gauge_Passion_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.PassionPoint.CurrentValue > 160)
			{
				txt = "人間だったら即座に発狂するような性欲に苛まれている";
			}
			else if (GameData.SisterData.PassionPoint.CurrentValue > 90)
			{
				txt = "立っているのも辛いほど体中が疼き異常に火照っている";
			}
			else if (GameData.SisterData.PassionPoint.CurrentValue > 60)
			{
				txt = "体が火照り、汗で服が体にまとわりついている";
			}
			else if (GameData.SisterData.PassionPoint.CurrentValue > 20)
			{
				txt = "少しムラムラとしている";
			}
			else
			{
				txt = "性欲はほとんど覚えず、シスターとしては理想的な状態";
			}

			PrintSklTxt(txt);

		}

		private void Gauge_Passion_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void Gauge_Shinrai_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.PassionPoint.CurrentValue > 80)
			{
				txt = "周囲の人間にあつく信頼されている";
			}
			else if (GameData.SisterData.PassionPoint.CurrentValue > 60)
			{
				txt = "周囲の人間に信頼されている";
			}
			else if (GameData.SisterData.PassionPoint.CurrentValue > 40)
			{
				txt = "周囲の人間は違和感を覚えているようだ";
			}
			else if (GameData.SisterData.PassionPoint.CurrentValue > 20)
			{
				txt = "周囲の人間に疑いの目を向けられている";
			}
			else
			{
				txt = "周囲の人間に強く警戒されている";
			}

			PrintSklTxt(txt);
		}

		private void Gauge_Shinrai_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Rosyutsu_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[0].Level)
			{
				case 1:
					txt = "人がいないところで裸になりたくなってしまう";
					break;
				case 2:
					txt = "周りに人がいるところで隠れて裸になりたくなってしまう";
					break;
				case 3:
					txt = "沢山の人に秘所を見てほしくてたまらない";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklName_Rosyutsu_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklLv_Rosyutsu_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[0].Level)
			{
				case 1:
					txt = "人がいないところで裸になりたくなってしまう";
					break;
				case 2:
					txt = "周りに人がいるところで隠れて裸になりたくなってしまう";
					break;
				case 3:
					txt = "沢山の人に秘所を見てほしくてたまらない";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklLv_Rosyutsu_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_MB_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[1].Level)
			{
				case 1:
					txt = "気が付くとオナニーのことを考えている";
					break;
				case 2:
					txt = "気を抜くと、無意識に手が胸や股間に伸びてしまう";
					break;
				case 3:
					txt = "オナニーをせずに正気を保つのが難しい";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklName_MB_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklLv_MB_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[1].Level)
			{
				case 1:
					txt = "気が付くとオナニーのことを考えている";
					break;
				case 2:
					txt = "気を抜くと、無意識に手が胸や股間に伸びてしまう";
					break;
				case 3:
					txt = "オナニーをせずに正気を保つのが難しい";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklLv_MB_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Lesb_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[2].Level)
			{
				case 1:
					txt = "同性である女性のことを少し性的な目で見てしまう";
					break;
				case 2:
					txt = "女性を性的対象と捉え、いやらしい視線を向けてしまう";
					break;
				case 3:
					txt = "女性なら誰でもいいので性交したいと考えている";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklName_Lesb_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklLv_Lesb_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[2].Level)
			{
				case 1:
					txt = "同性である女性のことを少し性的な目で見てしまう";
					break;
				case 2:
					txt = "女性を性的対象と捉え、いやらしい視線を向けてしまう";
					break;
				case 3:
					txt = "女性なら誰でもいいので性交したいと考えている";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklLv_Lesb_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Maso_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[3].Level)
			{
				case 1:
					txt = "痛みに対しての抵抗が他の人よりも薄い";
					break;
				case 2:
					txt = "よほどの痛みでない限りは快感としてとらえる";
					break;
				case 3:
					txt = "苦痛が大きいほど、より大きな快楽を感じる";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklName_Maso_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklLv_Maso_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[3].Level)
			{
				case 1:
					txt = "痛みに対しての抵抗が他の人よりも薄い";
					break;
				case 2:
					txt = "よほどの痛みでない限りは快感としてとらえる";
					break;
				case 3:
					txt = "苦痛が大きいほど、より大きな快楽を感じる";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklLv_Maso_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Sado_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[4].Level)
			{
				case 1:
					txt = "人が嫌がる姿を見ると快感を覚える";
					break;
				case 2:
					txt = "無抵抗な相手をいたぶると性的に興奮する";
					break;
				case 3:
					txt = "自分より弱い人間をいたぶりたくて仕方ない";
					break;

				case 0:
				default:
					txt = "";
					break;

			}
			PrintSklTxt(txt);
		}

		private void PIC_SklName_Sado_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklLv_Sado_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[4].Level)
			{
				case 1:
					txt = "人が嫌がる姿を見ると快感を覚える";
					break;
				case 2:
					txt = "無抵抗な相手をいたぶると性的に興奮する";
					break;
				case 3:
					txt = "自分より弱い人間をいたぶりたくて仕方ない";
					break;

				case 0:
				default:
					txt = "";
					break;
			}
			PrintSklTxt(txt);
		}

		private void PIC_SklLv_Sado_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}
		private void PIC_SklName_Smell_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[5].Level)
			{
				case 1:
					txt = "自分や他人の体臭に魅力を感じている";
					break;
				case 2:
					txt = "自分や他人の体臭に強く惹かれ、顔を埋めたくなる";
					break;
				case 3:
					txt = "体臭に異様に執着し、人と話す際には不自然に歩み寄る";
					break;

				case 0:
				default:
					txt = "";
					break;
			}
			PrintSklTxt(txt);
		}

		private void PIC_SklName_Smell_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklLv_Smell_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[5].Level)
			{
				case 1:
					txt = "自分や他人の体臭に魅力を感じている";
					break;
				case 2:
					txt = "自分や他人の体臭に強く惹かれ、顔を埋めたくなる";
					break;
				case 3:
					txt = "体臭に異様に執着し、人と話す際には不自然に歩み寄る";
					break;

				case 0:
				default:
					txt = "";
					break;
			}
			PrintSklTxt(txt);
		}

		private void PIC_SklLv_Smell_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Fur_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[6].Level)
			{
				case 1:
					txt = "フワフワとした生き物に魅力を感じる";
					break;
				case 2:
					txt = "毛に覆われた生き物を見ると異様な興奮を覚える";
					break;
				case 3:
					txt = "獣人を性欲の対象として認識している";
					break;

				case 0:
				default:
					txt = "";
					break;
			}
			PrintSklTxt(txt);
		}

		private void PIC_SklName_Fur_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklLv_Fur_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[6].Level)
			{
				case 1:
					txt = "フワフワとした生き物に魅力を感じる";
					break;
				case 2:
					txt = "毛に覆われた生き物を見ると異様な興奮を覚える";
					break;
				case 3:
					txt = "獣人を性欲の対象として認識している";
					break;

				case 0:
				default:
					txt = "";
					break;
			}
			PrintSklTxt(txt);
		}

		private void PIC_SklLv_Fur_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Monster_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[7].Level)
			{
				case 1:
					txt = "身体能力が高まり、最大体力が増幅する";
					break;
				case 2:
					txt = "瞳の構造が変化し、暗いところでもよく見えるようになる";
					break;
				case 0:
				default:
					txt = "";
					break;
			}
			PrintSklTxt(txt);
		}

		private void PIC_SklName_Monster_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklLv_Monster_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[7].Level)
			{
				case 1:
					txt = "身体能力が高まり、最大体力が増幅する";
					break;
				case 2:
					txt = "瞳の構造が変化し、暗いところでもよく見えるようになる";
					break;
				case 0:
				default:
					txt = "";
					break;
			}
			PrintSklTxt(txt);
		}

		private void PIC_SklLv_Monster_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Biyaku_MouseEnter(object sender, EventArgs e)
		{
			if (GameData.SisterData.Skills[8].IsEnabled)
			{
				PrintSklTxt("血液、唾液、愛液などの体液が媚薬となっている");
			}
		}

		private void PIC_SklName_Biyaku_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Futa_MouseEnter(object sender, EventArgs e)
		{
			if (GameData.SisterData.Skills[9].IsEnabled)
			{
				PrintSklTxt("成人男性のそれよりも大きなペニスが生えている");
			}
		}

		private void PIC_SklName_Futa_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		private void PIC_SklName_Succubus_MouseEnter(object sender, EventArgs e)
		{
			if (GameData.SisterData.Skills[10].IsEnabled)
			{
				PrintSklTxt("性交した相手から魔力を奪い取ることができる");
			}
		}

		private void PIC_SklName_Succubus_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/////////////////////////////////////////////////////////////////////////////////////
		//		魔物との取引画面
		/////////////////////////////////////////////////////////////////////////////////////




		private void PrintSklTxt(string txt)
		{
			Font fnt = new Font("メイリオ", 12);
			Bitmap canvas0 = new Bitmap(PIC_stus_text.Width, PIC_stus_text.Height);
			/* ImageオブジェクトのGraphicsオブジェクトを作成する */
			Graphics g0 = Graphics.FromImage(canvas0);
			/* 描画内容を準備 */
			g0.DrawString(txt, fnt, Brushes.White, 3, 3);
			/* PictureBoxに表示*/
			PIC_stus_text.Image = canvas0;
			/* リソースを解放 */
			fnt.Dispose();
			g0.Dispose();
		}

		public void doujin_game_sharp_Shown(object sender, EventArgs e)
		{
			// DirectSoundの初期化
			dsound = new DirectSound(this);
			// 利用する音楽の数を指定する。
			// 今回は3種類なので3を設定。
			dsound.init(2);
			// 効果音の読み込み。

			dsound.loadWave(0, "ohayougozaimasu_02.wav");
			dsound.loadWave(1, "musicbox.wav");
		}

		public void PlaySoundEffect(int idx, bool loop)
		{
			dsound.play(idx, loop);
		}
		

	}


}
