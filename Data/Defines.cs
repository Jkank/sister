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
        public enum CharacterImageID
        {
            D_CHR_SARA_00 = 0,
            D_CHR_SARA_01,
            D_CHR_SARA_02,
            D_CHR_SARA_03,
            D_CHR_SARA_04,
            D_CHR_SARA_05,
            D_CHR_SARA_06,
            D_CHR_SARA_07,
            D_CHR_SARA_08,
            D_CHR_SARA_09,
            D_CHR_SARA_10,
            D_CHR_SARA_11,
            D_CHR_SARA_12,
            D_CHR_SARA_13,
            D_CHR_SARA_14,
            D_CHR_SARA_15,
            D_CHR_SARA_16,
            D_CHR_SARA_17,
            D_CHR_SARA_18,
            D_CHR_SARA_19,
            D_CHR_LIDY_00,
            D_CHR_LIDY_01,
            D_CHR_LIDY_02,
            D_CHR_LIDY_03,
            D_CHR_LIDY_04,
            D_CHR_LIDY_05,
            D_CHR_DEVIL_00,
            D_CHR_DEVIL_01,
            D_CHR_DEVIL_02,
            D_CHR_DEVIL_03,
            D_CHR_DEVIL_04,
            D_CHR_DEVIL_05,
        }

        public enum skillID
        {
            SKL_LESB,
            SKL_MASO,
            SKL_SADO,
            SKL_SMLFETI,
            SKL_FUTA,
            SKL_ROSYUTSU,
            SKL_MB,
            SKL_KEMONER,
            SKL_BODY_BIYAKU,
            SKL_MONSTER,
            SKL_SUCCUBUS,
        }


        public enum expID
        {
            EXP_MB,         //オナニー経験
            EXP_EJAC,       //射精経験
            EXP_STEAL,      //盗難経験
        }

        public const string FontName = "メイリオ";
        public const float MainTextFontSize = 12;

        public const int TextAreaWidth = 523;
        public const int TextAreaHeight = 135;

        public const int SelectBoxWidth = 450;
        public const int SelectBoxHeight = 60;

        
    }
}
