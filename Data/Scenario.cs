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

        public Scenario ()
        {
            // スキルリストの生成
            // アイテムリストの生成
            // イベントフラグリストの生成

            // その他値の読み込み

			Slct_No = 0;
			DayCt = 0;
			NowTime = 8;

			/*** 既読フラグ ***/
			//既読スキップ機能で参照



        }
    }
}
