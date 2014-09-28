using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DoujinGameProject
{
    static class Program
    {
        public static doujin_game_sharp Doujin_game_sharp;

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Doujin_game_sharp = new doujin_game_sharp();
            Application.Run(Doujin_game_sharp);
        }
    }
}
