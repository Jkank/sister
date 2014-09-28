using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using DoujinGameProject.Properties;

namespace DoujinGameProject.Data
{
    /// <summary>
    /// セーブ・ロード処理を行うクラス
    /// </summary>
    public static class DataManager
    {
        const string TESTPATH = "C:\\Temp\\tes.dat";
        /// <summary>
        /// データを読み込みます
        /// </summary>
        public static bool LoadData(int no)
        {
            //ステータス情報の取得
            Sister sData;
            //ファイルパスの生成

            using (FileStream fs = new FileStream(TESTPATH, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter f = new BinaryFormatter();
                //読み込んで逆シリアル化する
                sData = (Sister)f.Deserialize(fs);
            }

            //GameDataに格納
            GameData.SisterData = sData;

            return true;
        }

        /// <summary>
        /// データを保存します
        /// </summary>
        public static bool SaveData()
        {

            using (FileStream fs = new FileStream(TESTPATH, FileMode.Create, FileAccess.Write))
            {
                BinaryFormatter bf = new BinaryFormatter();
                //シリアル化して書き込む
                bf.Serialize(fs, GameData.SisterData);
            }
            return true;
        }
    }
}
