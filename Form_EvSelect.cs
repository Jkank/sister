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
		////////////////////* === 選択肢３ボタン === *////////////////////
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

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：setSelectBoxImage　  　　　　　　　　　　 　■ */
		/* ■　内容：選択肢セット処理							   ■ */
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

		//SE.csから移植/TODO:メンテナンス
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：dispSlctBox　  　　　　　　 　　　　　　　 ■ */
		/* ■　内容：選択肢パネルを表示する処理              　 　 ■ */
		/* ■　入力：Slct_ct                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void dispSlctBox(int Slct_ct_max)
		{


			int i;

			// 選択肢ボックス位置
			Point Position = new Point(Slctbox_1.Location.X, Slctbox_1.Location.Y); // 選択肢ボックス位置


			Position.X = 175;       /* fail safe のためX座標も再設定 */

			if (Slct_ct_max == 4)
			{
				Slctbox_1.Visible = true;
				Slctbox_2.Visible = true;
				Slctbox_3.Visible = true;
				Slctbox_4.Visible = true;

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

				Position.Y = 145;
				Slctbox_1.Location = Position;
				Position.Y = 325;
				Slctbox_2.Location = Position;
			}


			PNL_Eventslct.Visible = true;
//			PNL_Event.Visible = false;
		}
	}
}
