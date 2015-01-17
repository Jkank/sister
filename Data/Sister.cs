using System;

namespace DoujinGameProject.Data
{
    // セーブデータ読み込み時に生成するクラス
    [Serializable()]
    public class Sister
    {

        public int Money { get; set; }
        public Parameter HitPoint { get; set; }
		public Parameter MentalPoint { get; set; }
        public Parameter MoralPoint { get; set; }
        public Parameter PassionPoint { get; set; }
        public Parameter MagicPoint { get; set; }
        public int ChargedMagicPoint { get; set; }

        public Skill[] Skills;
        public Item[] Items;
        public bool[] EventFlags;

		public const int SkillNo = 11;

        public Sister ()
        {
            // スキルリストの生成

					/*
		public enum BGPicID
		{
			D_BGP_OPENING,
			D_BGP_CHURCH_DAY,
			D_BGP_CHURCH_EVENING,
			D_BGP_CHURCH_NIGHT,
			D_BGP_STAIRS,
			D_BGP_CAGE,
			D_BGP_ROOM_DAY,
			D_BGP_ROOM_EVENING,
			D_BGP_ROOM_NIGHT,
			D_BGP_LIBRALY,
			D_BGP_TOWN_DAY,
			D_BGP_TOWN_EVENING,
			D_BGP_TOWN_NIGHT
		}
		*/


			const int SKL_ROSYUTSU = 0;	//露出癖
			const int SKL_MB = 1;             //オナニー中毒
			const int SKL_LESB = 2;           //レズっ気
			const int SKL_MASO = 3;    　     //マゾっ気
			const int SKL_SADO = 4;           //サドっ気
			const int SKL_SMLFETI = 5;        //匂いフェチ
			const int SKL_KEMONER = 6;        //ケモナー  
			const int SKL_MONSTER = 7;        //一部魔物化
			const int SKL_BODY_BIYAKU = 8;    //体液媚薬  
			const int SKL_FUTA = 9;           //フタ
			const int SKL_SUCCUBUS = 10;      //サキュバス

            // アイテムリストの生成
            // イベントフラグリストの生成

            // その他値の読み込み
            HitPoint = new Parameter();
            MentalPoint = new Parameter();
            MoralPoint = new Parameter();
            PassionPoint = new Parameter();
            MagicPoint = new Parameter();

			Skills = new Skill[SkillNo];
			
			Skills[0] = new Skill("露出癖", 3, 0);
			Skills[1] = new Skill("オナニー中毒", 3, 0);
			Skills[2] = new Skill("レズっ気", 3, 0);
			Skills[3] = new Skill("マゾっ気", 3, 0);
			Skills[4] = new Skill("サドっ気", 3, 0);
			Skills[5] = new Skill("匂いフェチ", 3, 0);
			Skills[6] = new Skill("ケモナー", 3, 0);
			Skills[7] = new Skill("一部魔族化", 2, 0);
			Skills[8] = new Skill("体液媚薬", 1, 0);
			Skills[9] = new Skill("ふたなり", 1, 0);
			Skills[10] = new Skill("サキュバス化", 1, 0);
			
            Money = 0;
            ChargedMagicPoint = 0;

        }


    
    }
}
