using System;

namespace DoujinGameProject.Data
{
    // セーブデータ読み込み時に生成するクラス
    [Serializable()]
    public class Sister
    {

        public int Money { get; set; }
        public Parameter HitPoint { get; set; }
        public Parameter EnergyPoint { get; set; }
        public Parameter MoralPoint { get; set; }
        public Parameter PassionPoint { get; set; }
        public Parameter MagicPoint { get; set; }
        public int ChargedMagicPoint { get; set; }

        public Skill[] Skills;
        public Item[] Items;
        public bool[] EventFlags;

        public Sister ()
        {
            // スキルリストの生成
            // アイテムリストの生成
            // イベントフラグリストの生成

            // その他値の読み込み
            HitPoint = new Parameter();
            EnergyPoint = new Parameter();
            MoralPoint = new Parameter();
            PassionPoint = new Parameter();
            MagicPoint = new Parameter();

            Money = 0;
            ChargedMagicPoint = 0;
        }


    
    }
}
