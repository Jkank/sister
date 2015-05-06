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
				InitializeVers();

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


		private void InitializeVers()
		{
			Sister Sis = GameData.SisterData;
			Scenario Scn = GameData.ScenarioData;
 
			//シナリオ変数初期化
			Scn.InitializeScenarioVars();
			

			//サラ変数初期化
			Sis.InitializeSisterVars();

		}

	}
}
