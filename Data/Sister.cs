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
		public Parameter TrustPoint { get; set; }
        public int ChargedMagicPoint { get; set; }

        public Skill[] Skills;
        public Item[] Items;
        public bool[] EventFlags;

		/* スキル番号 */
		public const int SKL_ROSYUTSU		= 0;						//露出癖
		public const int SKL_MB				= SKL_ROSYUTSU		+ 1;    //オナニー中毒
		public const int SKL_LESB			= SKL_MB			+ 1;    //レズっ気
		public const int SKL_MASO			= SKL_LESB			+ 1;    //マゾっ気
		public const int SKL_SADO			= SKL_MASO			+ 1;	//サドっ気
		public const int SKL_SMLFETI		= SKL_SADO			+ 1;	//匂いフェチ
		public const int SKL_KEMONER		= SKL_SMLFETI		+ 1;	//ケモナー  
		public const int SKL_MONSTER		= SKL_KEMONER		+ 1;    //一部魔物化
		public const int SKL_BODY_BIYAKU	= SKL_MONSTER		+ 1;    //体液媚薬  
		public const int SKL_FUTA			= SKL_BODY_BIYAKU	+ 1;    //フタ
		public const int SKL_SUCCUBUS		= SKL_FUTA			+ 1;   //サキュバス

		public const int SKILL_NUM = SKL_SUCCUBUS+1;

		/* アイテム番号 */

		public const int ITEM_NUM = 1;

		/* 経験番号 */
		public const int EVFLG_SHOKUSYU = 0;		//触手経験済フラグ
		public const int EVFLG_STEAL	= EVFLG_SHOKUSYU + 1;		//器盗難経験済フラグ
		public const int EVFLG_PANTS	= EVFLG_STEAL + 1;		//パンツ盗難経験済フラグ
		public const int EVFLG_HEAL = EVFLG_PANTS + 1;	//怪我治療の会経験済フラグ

		public const int EVFLG_NUM = EVFLG_HEAL + 1;


		/* ステータス番号 */
		public const int STATUS_HITPOINT	= 0;											// 0 ヒットポイント
		public const int STATUS_MENTALPOINT	= STATUS_HITPOINT		+ 1;					// 1 メンタルポイント
		public const int STATUS_MAGICPOINT	= STATUS_MENTALPOINT	+ 1;					// 2 モラルポイント
		public const int STATUS_PATTIONPOINT= STATUS_MAGICPOINT		+ 1;					// 3 パッションポイント
		public const int STATUS_TRUSTPOINT	= STATUS_PATTIONPOINT	+ 1;					// 4 トラストポイント
		public const int STATUS_ROSYUTSU	= SKL_ROSYUTSU		+ STATUS_TRUSTPOINT + 1;	// 5 露出癖
		public const int STATUS_MB			= SKL_MB			+ STATUS_TRUSTPOINT + 1;	// 6 オナニー中毒
		public const int STATUS_LESB		= SKL_LESB			+ STATUS_TRUSTPOINT + 1;	// 7 レズっ気
		public const int STATUS_MASO		= SKL_MASO			+ STATUS_TRUSTPOINT + 1;	// 8 マゾっ気
		public const int STATUS_SADO		= SKL_SADO			+ STATUS_TRUSTPOINT + 1;	// 9 サドっ気
		public const int STATUS_SMLFETI		= SKL_SMLFETI		+ STATUS_TRUSTPOINT + 1;	//10 匂いフェチ
		public const int STATUS_KEMONER		= SKL_KEMONER		+ STATUS_TRUSTPOINT + 1;	//11 ケモナー  
		public const int STATUS_MONSTER		= SKL_MONSTER		+ STATUS_TRUSTPOINT + 1;	//12 一部魔物化
		public const int STATUS_BODY_BIYAKU	= SKL_BODY_BIYAKU	+ STATUS_TRUSTPOINT + 1;	//13 体液媚薬  
		public const int STATUS_FUTA		= SKL_FUTA			+ STATUS_TRUSTPOINT + 1;	//14 フタ
		public const int STATUS_SUCCUBUS	= SKL_SUCCUBUS		+ STATUS_TRUSTPOINT + 1;	//15 サキュバス

		public const int STATUS_NUM = STATUS_SUCCUBUS + 1;

		public string[][] T_StatusExpress = new string[][]
		{
			new string[]
			{
				/* HitPoint */
				"どれほど動き回っても疲れる気がしない",
				"体力が有り余っている",
				"特に疲れは感じていない",
				"激しく体を動かすには疲れすぎている",
				"疲れきっていて体が重い"
			},
			new string[]
			{
				/* MentalPoint*/
				"どんなことでもできそうな万能感に満ち溢れている",
				"気力は充実している",
				"気力面で特に問題は抱えていない",
				"ストレスを感じて精神的に疲れている",
				"何もする気力が湧かない"
			},
			new string[]
			{
				/* MoralPoint */
				"常に人々の事を考え、慈愛に満ちている",
				"他者の苦しみを自分の苦しみとして捉えることができる",
				"他人のために自分が苦しむことに疑問を覚えている",
				"他者の事情より、自分の都合や感情を優先する",
				"自分の欲望のためなら誰がどうなろうが関係ない"
			},
			new string[]
			{
				/* PassionPoint */
				"人間だったら即座に発狂するような性欲に苛まれている",
				"立っているのも辛いほど体中が疼き異常に火照っている",
				"体が火照り、汗で服が体にまとわりついている",
				"少しムラムラとしている",
				"性欲はほとんど覚えず、シスターとしては理想的な状態"
			},
			new string[]
			{
				/* TrustPoint */
				"周囲の人間にあつく信頼されている",
				"周囲の人間に信頼されている",
				"周囲の人間は違和感を覚えているようだ",
				"周囲の人間に疑いの目を向けられている",
				"周囲の人間に強く警戒されている"
			},
			new string[]
			{
				/* 露出癖 */
				"人がいないところで裸になりたくなってしまう",
				"周りに人がいるところで隠れて裸になりたくなってしまう",
				"沢山の人に秘所を見てほしくてたまらない"
			},
			new string[]
			{
				/* オナニー中毒 */
				"気が付くとオナニーのことを考えている",
				"気を抜くと、無意識に手が胸や股間に伸びてしまう",
				"股間に手をやっていないと正気を保つのが難しい"
			},
			new string[]
			{
				/* レズっ気 */
				"同性である女性のことを少し性的な目で見てしまう",
				"女性を性的対象と捉え、いやらしい視線を向けてしまう",
				"女性なら誰でもいいので性交したいと考えている"
			},
			new string[]
			{
				/* マゾっ気 */
				"痛みに対しての抵抗が他の人よりも薄い",
				"よほどの痛みでない限りは快感としてとらえる",
				"苦痛が大きいほど、より大きな快楽を感じる"
			},
			new string[]
			{
				/* サドっ気 */
				"人が嫌がる姿を見ると快感を覚える",
				"無抵抗な相手をいたぶると性的に興奮する",
				"自分より弱い人間をいたぶりたくて仕方ない"
			},
			new string[]
			{
				/* 匂いフェチ */
				"自分や他人の体臭に魅力を感じている",
				"自分や他人の体臭に強く惹かれ、顔を埋めたくなる",
				"体臭に異様に執着し、人と話す際には不自然に歩み寄る"
			},
			new string[]
			{
				/* ケモナー */
				"フワフワとした生き物に魅力を感じる",
				"毛に覆われた生き物を見ると異様な興奮を覚える",
				"獣人を性欲の対象として認識している"
			},
			new string[]
			{
				/* 一部魔物化 */
				"身体能力が高まり、最大体力が増幅する",
				"瞳の構造が変化し、暗いところでもよく見えるようになる"
			},
			new string[]
			{
				/* 体液媚薬化 */
				"血液、唾液、愛液などの体液が媚薬となっている"
			},
			new string[]
			{
				/* フタ */
				"成人男性のそれよりも大きなペニスが生えている"
			},
			new string[]
			{
				/* サキュバス */
				"性交した相手から魔力を奪い取ることができる"
			}
		};

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


            // アイテムリストの生成
            // イベントフラグリストの生成

            // その他値の読み込み
            HitPoint = new Parameter();
            MentalPoint = new Parameter();
            MoralPoint = new Parameter();
            PassionPoint = new Parameter();
            MagicPoint = new Parameter();
			TrustPoint = new Parameter();


			// スキル値初期化
			Skills = new Skill[SKILL_NUM];
			Skills[SKL_ROSYUTSU]	= new Skill("露出癖", 3, 0);
			Skills[SKL_MB]			= new Skill("オナニー中毒", 3, 0);
			Skills[SKL_LESB]		= new Skill("レズっ気", 3, 0);
			Skills[SKL_MASO]		= new Skill("マゾっ気", 3, 0);
			Skills[SKL_SADO]		= new Skill("サドっ気", 3, 0);
			Skills[SKL_SMLFETI]		= new Skill("匂いフェチ", 3, 0);
			Skills[SKL_KEMONER]		= new Skill("ケモナー", 3, 0);
			Skills[SKL_MONSTER]		= new Skill("一部魔族化", 2, 0);
			Skills[SKL_BODY_BIYAKU] = new Skill("体液媚薬", 1, 0);
			Skills[SKL_FUTA]		= new Skill("ふたなり", 1, 0);
			Skills[SKL_SUCCUBUS]	= new Skill("サキュバス化", 1, 0);

			// アイテム所持数初期化
			Items = new Item[ITEM_NUM];


			// 経験数初期化
			EventFlags = new bool[EVFLG_NUM];
			EventFlags[EVFLG_SHOKUSYU]	= false;
			EventFlags[EVFLG_STEAL]		= false;
			EventFlags[EVFLG_PANTS]		= false;
			
            Money = 0;
            ChargedMagicPoint = 0;


			
    
        }

    }
}
