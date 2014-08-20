
namespace WindowsFormsApplication1.Properties
{
    // セーブデータ読み込み時に生成するクラス
    public class Sister
    {

        public int Money { get; set; }
        public Parameter HitPoint { get; set; }
        public Parameter EnergyPoint { get; set; }
        public Parameter MoralPoint { get; set; }
        public Parameter PassionPoint { get; set; }
        public Parameter CorruptionPoint { get; set; }
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
            PassionPoint = new Parameter();
            CorruptionPoint = new Parameter();
        }


    
    }
}
