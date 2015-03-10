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
	public partial class doujin_game_sharp
	{
		static int LogFinStatus = 0; 
		////////////////////////////////////////////////////////////////////
		//////////////////////* === イベント画面 === *//////////////////////
		////////////////////////////////////////////////////////////////////

		// テキストエリア
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

		// 名前表示エリア
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

		// 背景
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

		// 左キャラエリア
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

		// 右キャラエリア
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
		////////////////////////////////////////////////////////////////////
		/////////////////////* === バックログ画面 === */////////////////////
		////////////////////////////////////////////////////////////////////
		private void panel3_MouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta > 3 && PNL_Log.Visible == false)
			{

				// バックログパネルを展開

				PIC_LogBox_0.Visible = true;
				PIC_LogBox_1.Visible = true;
				PIC_LogBox_2.Visible = true;
				PIC_LogBox_3.Visible = true;
				PIC_LogBox_4.Visible = true;
				PNL_Log.Visible = true;
				PNL_Log.Focus();

				// スクロール画面初期状態
				SE.ScrollInit(log_ct_use, PIC_LogBox_0, PIC_LogBox_1, PIC_LogBox_2, PIC_LogBox_3, PIC_LogBox_4);
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

					// イベントテキストパネルを展開
					PNL_Event.Visible = true;

					/* スクロール画面の一番下でホイールを下に動かした */
					/* ⇒イベントテキストパネルに戻る */
					DispStatus = 0;
					DispLogPnl();
					PNL_Event.Focus();

				}
				else
				{
					log_ct--;
					SE.ScrollRedraw(log_ct, log_ct_use, PIC_LogBox_0, PIC_LogBox_1, PIC_LogBox_2, PIC_LogBox_3, PIC_LogBox_4);
				}
			}
			else if (e.Delta > 3)
			{
				if (log_ct + 4 < log_ct_use)
				{
					log_ct++;
					/* スクロール画面の一番上ではない */
					SE.ScrollRedraw(log_ct, log_ct_use, PIC_LogBox_0, PIC_LogBox_1, PIC_LogBox_2, PIC_LogBox_3, PIC_LogBox_4);
				}
			}
		}


		public void DispLogPnl()
		{
			switch (LogFinStatus)
			{
				case 0:
					ShowPicforLogEnd();
					break;
				case 1:
					PIC_Buffer.Visible = true;
					break;
				case 2:
					Timer_LogFin.Stop();
					LogFinStatus = 0;
					PIC_LogBox_0.Visible = false;
					PIC_LogBox_1.Visible = false;
					PIC_LogBox_2.Visible = false;
					PIC_LogBox_3.Visible = false;
					PIC_LogBox_4.Visible = false;
					PNL_Log.Visible = false;
					PIC_Buffer.Visible = false;
					break;

			}
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：ShowPicforLogEnd　  　　　　　　　　　　 　■ */
		/* ■　内容：背景画像表示処理							   ■ */
		/* ■　入力：なし		                             　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		void ShowPicforLogEnd()
		{
			BackgroundDraw_All(PIC_Buffer);
			Timer_LogFin.Start();
		}

		private void Timer_LogFin_Tick(object sender, EventArgs e)
		{
			LogFinStatus++;
			DispLogPnl();
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


	}
}
