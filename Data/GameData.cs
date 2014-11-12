using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoujinGameProject.Properties;

namespace DoujinGameProject.Data
{
    /// <summary>
    /// ステータス・ゲーム進捗等のデータをすべて管理するクラスです
    /// </summary>
    public static class GameData
    {
        /// <summary>ステータス情報</summary>
        public static Sister SisterData;
        /// <summary>シナリオ情報</summary>
        public static Scenario ScenarioData;
        //選択肢や既読状態等のデータをもつ必要があればScenarioDataをつくる
        /// <summary>イベントフラグ情報</summary>
        public static bool[] EventFlags;


        /// <summary>データ作成</summary>
        public static void Initialize()
        {
            SisterData = new Sister();
            ScenarioData = new Scenario();
        }


    }
}
