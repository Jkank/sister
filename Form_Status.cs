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
		
		/////////////////////////////////////////////////////////////////////////////////////
		//		ステータス画面
		/////////////////////////////////////////////////////////////////////////////////////

		/* HitPoint */
		private void Gauge_HP_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.HitPoint.CurrentValue > 150)			txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_HITPOINT][0];
			else if (GameData.SisterData.HitPoint.CurrentValue > 100)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_HITPOINT][1];
			else if (GameData.SisterData.HitPoint.CurrentValue > 70)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_HITPOINT][2];
			else if (GameData.SisterData.HitPoint.CurrentValue > 30)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_HITPOINT][3];
			else															txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_HITPOINT][4];
			PrintSklTxt(txt);
		}
		private void Gauge_HP_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* MentalPoint */
		private void Gauge_Mental_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.MentalPoint.CurrentValue > 150)			txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][0];
			else if (GameData.SisterData.MentalPoint.CurrentValue > 100)	txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][1];
			else if (GameData.SisterData.MentalPoint.CurrentValue > 70)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][2];
			else if (GameData.SisterData.MentalPoint.CurrentValue > 30)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][3];
			else															txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][4];
			PrintSklTxt(txt);
		}
		private void Gauge_Mental_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* MoralPoint */
		private void Gauge_Moral_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.MoralPoint.CurrentValue > 80)			txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MAGICPOINT][0];
			else if (GameData.SisterData.MoralPoint.CurrentValue > 60)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MAGICPOINT][1];
			else if (GameData.SisterData.MoralPoint.CurrentValue > 40)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MAGICPOINT][2];
			else if (GameData.SisterData.MoralPoint.CurrentValue > 20)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MAGICPOINT][3];
			else															txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MAGICPOINT][4];
			PrintSklTxt(txt);
		}
		private void Gauge_Moral_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* PassionPoint */
		private void Gauge_Passion_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.PassionPoint.CurrentValue > 160)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][0];
			else if (GameData.SisterData.PassionPoint.CurrentValue > 90)	txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][1];
			else if (GameData.SisterData.PassionPoint.CurrentValue > 60)	txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][2];
			else if (GameData.SisterData.PassionPoint.CurrentValue > 20)	txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][3];
			else															txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MENTALPOINT][4];
			PrintSklTxt(txt);
		}
		private void Gauge_Passion_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* TrustPoint */
		private void Gauge_Shinrai_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			if (GameData.SisterData.TrustPoint.CurrentValue > 80)			txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_TRUSTPOINT][0];
			else if (GameData.SisterData.TrustPoint.CurrentValue > 60)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_TRUSTPOINT][1];
			else if (GameData.SisterData.TrustPoint.CurrentValue > 40)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_TRUSTPOINT][2];
			else if (GameData.SisterData.TrustPoint.CurrentValue > 20)		txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_TRUSTPOINT][3];
			else															txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_TRUSTPOINT][4];
			PrintSklTxt(txt);
		}
		private void Gauge_Shinrai_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* 露出狂 */
		private void PIC_SklName_Rosyutsu_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[Sister.SKL_ROSYUTSU].Level)
			{
				case 1:
					txt = GameData.SisterData.T_StatusExpress[Sister.SKL_ROSYUTSU][0];
					break;
				case 2:
					txt = GameData.SisterData.T_StatusExpress[Sister.SKL_ROSYUTSU][1];
					break;
				case 3:
					txt = GameData.SisterData.T_StatusExpress[Sister.SKL_ROSYUTSU][2];
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
			PIC_SklName_Rosyutsu_MouseEnter(sender, e);		//PIC_SklName_Rosyutsu_MouseEnterと同じ処理を行う
		}
		private void PIC_SklLv_Rosyutsu_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* オナニー中毒 */
		private void PIC_SklName_MB_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[Sister.SKL_MB].Level)
			{
				case 1:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MB][0];
					break;
				case 2:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MB][1];
					break;
				case 3:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MB][2];
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
			PIC_SklName_MB_MouseEnter(sender, e);		//PIC_SklName_MB_MouseEnterと同じ処理を行う
		}
		private void PIC_SklLv_MB_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* レズっ気 */
		private void PIC_SklName_Lesb_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[Sister.SKL_LESB].Level)
			{
				case 1:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_LESB][0];
					break;
				case 2:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_LESB][1];
					break;
				case 3:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_LESB][2];
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
			PIC_SklName_Lesb_MouseEnter(sender, e);		//PIC_SklName_Lesb_MouseEnterと同じ処理を行う
		}
		private void PIC_SklLv_Lesb_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* マゾっ気 */
		private void PIC_SklName_Maso_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[Sister.SKL_MASO].Level)
			{
				case 1:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MASO][0];
					break;
				case 2:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MASO][1];
					break;
				case 3:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MASO][2];
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
			PIC_SklName_Maso_MouseEnter(sender, e);		//PIC_SklName_Maso_MouseEnterと同じ処理を行う
		}
		private void PIC_SklLv_Maso_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* サドっ気 */
		private void PIC_SklName_Sado_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[Sister.SKL_SADO].Level)
			{
				case 1:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_SADO][0];
					break;
				case 2:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_SADO][1];
					break;
				case 3:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_SADO][2];
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
			PIC_SklName_Sado_MouseEnter(sender, e);		//PIC_SklName_Sado_MouseEnterと同じ処理を行う	
		}
		private void PIC_SklLv_Sado_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* 匂いフェチ */
		private void PIC_SklName_Smell_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[Sister.SKL_SMLFETI].Level)
			{
				case 1:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_SMLFETI][0];
					break;
				case 2:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_SMLFETI][1];
					break;
				case 3:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_SMLFETI][2];
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
			PIC_SklName_Smell_MouseEnter(sender, e);
		}
		private void PIC_SklLv_Smell_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* ケモナー */
		private void PIC_SklName_Fur_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[Sister.SKL_KEMONER].Level)
			{
				case 1:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_KEMONER][0];
					break;
				case 2:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_KEMONER][1];
					break;
				case 3:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_KEMONER][2];
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
			PIC_SklName_Fur_MouseEnter(sender,e);
		}
		private void PIC_SklLv_Fur_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* 一部魔物化 */
		private void PIC_SklName_Monster_MouseEnter(object sender, EventArgs e)
		{
			string txt;

			switch (GameData.SisterData.Skills[Sister.SKL_MONSTER].Level)
			{
				case 1:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MONSTER][0];
					break;
				case 2:
					txt = GameData.SisterData.T_StatusExpress[Sister.STATUS_MONSTER][1];
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
			PIC_SklName_Monster_MouseEnter(sender, e);
		}
		private void PIC_SklLv_Monster_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* 体液媚薬 */
		private void PIC_SklName_Biyaku_MouseEnter(object sender, EventArgs e)
		{
			if (GameData.SisterData.Skills[Sister.SKL_BODY_BIYAKU].IsEnabled)
			{
				PrintSklTxt(GameData.SisterData.T_StatusExpress[Sister.STATUS_BODY_BIYAKU][0]);
			}
		}
		private void PIC_SklName_Biyaku_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* ふたなり */
		private void PIC_SklName_Futa_MouseEnter(object sender, EventArgs e)
		{
			if (GameData.SisterData.Skills[Sister.SKL_FUTA].IsEnabled)
			{
				PrintSklTxt(GameData.SisterData.T_StatusExpress[Sister.STATUS_FUTA][0]);
			}
		}
		private void PIC_SklName_Futa_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* サキュバス */
		private void PIC_SklName_Succubus_MouseEnter(object sender, EventArgs e)
		{
			if (GameData.SisterData.Skills[Sister.SKL_SUCCUBUS].IsEnabled)
			{
				PrintSklTxt(GameData.SisterData.T_StatusExpress[Sister.STATUS_SUCCUBUS][0]);
			}
		}
		private void PIC_SklName_Succubus_MouseLeave(object sender, EventArgs e)
		{
			PrintSklTxt("");
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：PrintSklTxt				 　　 　　　　　　 ■ */
		/* ■　内容：スキル内容の説明テキストを表示する処理		   ■ */
		/* ■　入力：targetpanel_id                          　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
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
	}
}
