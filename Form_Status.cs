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
	}
}
