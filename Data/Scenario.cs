using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoujinGameProject.Data
{
    public class Scenario
    {
		public int Slct_No { get; set; }
		public int DayCt { get; set; }		//日数カウンタ
		public int NowTime { get; set; }
		public int NowPlace { get; set; }
		public int ChargedMagicPoint { get; set; }

        public Scenario ()
        {
            // スキルリストの生成
            // アイテムリストの生成
            // イベントフラグリストの生成

            // その他値の読み込み


			/*** 既読フラグ ***/
			//既読スキップ機能で参照



        }

		public void InitializeScenarioVars()
		{
			/* 選択番号初期化 */
			Slct_No = 0;

			/* 日数初期化 */
			DayCt = 0;

			/* 現在時刻初期化 */
			NowTime = 8;

			/* 現在地初期化 */
			NowPlace = Defines.LOC_CHAPEL;

			/* チャージ済魔力初期化 */
			ChargedMagicPoint = 0;

			/*  */

		}
    }
}
