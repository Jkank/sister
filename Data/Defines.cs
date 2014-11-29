using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DoujinGameProject.Data
{
    /// <summary>定数定義</summary>
    public static class Defines
    {
        /// <summary>立ち絵画像を示すID</summary>
        /*
		public enum CharacterImageID
        {
            サラ = 0,
            サラ笑顔,
			サラ怒り,
			サラ恥じらい,
            サラ落ち込み,
			サラ発情,
			サラ驚き,
            サラ悪笑顔,
            サラ裸,
			サラ裸怒り,
			サラ裸恥じらい,
			サラ裸発情,
			サラ裸驚き,
			サラ裸悪笑顔,
			サラシナ,
			サラシナ恥じらい,
			サラシナ発情,
			サラシナ驚き,
			サラシナ悪笑顔,
			サラ裸シナ,
			サラ裸シナ怒り,
			サラ裸シナ恥じらい,
			サラ裸シナ発情,
			サラ裸シナ驚き,
			サラ裸シナ悪笑顔,
            サラ淫魔,
			サラ淫魔怒り,
			サラ淫魔驚き,
			サラ淫魔発情,
            サラ淫魔悪笑顔,
			マリー,
			マリー驚き,
			マリー怒り,
			マリー発情,
            リディ,
            リディ笑顔,
            リディ恥じらい,
            リディ恐怖,
            リディ苦しみ,
            魔物,
            魔物悪笑い,
            魔物驚き
        }
		*/

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

        public enum skillID
        {
            SKL_LESB,           //レズっ気
            SKL_MASO,           //マゾっ気
            SKL_SADO,           //サドっ気
            SKL_SMLFETI,        //匂いフェチ
            SKL_FUTA,           //フタ
            SKL_ROSYUTSU,       //露出癖
            SKL_MB,             //オナニー中毒
            SKL_KEMONER,        //ケモナー
            SKL_BODY_BIYAKU,    //体液媚薬    
            SKL_MONSTER,        //一部魔物化
            SKL_SUCCUBUS,       //サキュバス
        }


        public enum expID
        {
            EXP_MB,         //オナニー経験
            EXP_EJAC,       //射精経験
            EXP_STEAL,      //盗難経験
            EXT_PUNCH        //腹パン経験
        }

        public enum fileID
        {
            TXT_OPENING = 0,    //オープニング
            TXT_CHURCH,         //教会
            TXT_LEST,           //休憩
            TXT_SHOP,           //店
            TXT_READ,           //読書
            TXT_ROSYUTSU,       //露出
            TXT_NEWSKILL,       //魔物
            TXT_PUSSION_LIMIT,  //性欲限界
            TXT_HP_RUNOUT,      //体力限界
            TXT_MENTAL_RUNOUT,  //気力限界
            TXT_ENDING,         //エンディング
            TXT_INIT            //初期化処理用破壊値
        }

        public const string FontName = "メイリオ";
        public const float MainTextFontSize = 12;

        public const int TextAreaWidth = 523;
        public const int TextAreaHeight = 135;

        public const int SelectBoxWidth = 450;
        public const int SelectBoxHeight = 60;

        
    }
}
