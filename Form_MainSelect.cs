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





	}
}
