﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DoujinGameProject.Data;

namespace DoujinGameProject.Action
{
    //★★スクリプトを書く際の注意★★////////////////////////////////
    ////○ラベルを切る行には他のことを書いてはいけない。
    //////////////////////////////////////////////////////////////////

    //★スクリプトの文法ルール////////////////////////////////////////
    ////○ラベル　　If文やJMP文で飛んでくる先。
    ////            　サンプル：
    ////            [L_***_***]:
    ////        ※注意！！ラベルを切る行には他のことを書いてはいけない。
    ////    　　
    ////○If文　　　( )内の条件を満たしたらラベルにジャンプ。
    ////　　　　　　　サンプル１（即値）：
    ////　　　　　　If ( A >= 30 ) [L_***_***]:
    ////         　　↑↑↑ ↑ ↑↑
    ////        　　これらの箇所では半角スペースを置くこと。
    ////            　サンプル２（変数）：
    ////            If ( A >= B ) [L_***_***]:
    ////            ・変数A,Bの型はint型に限る。
    ////
    ////○JMP文 　　指定したラベルにジャンプ。
    ////　　　　　　　サンプル：
    ////        　　JMP [L_***_***]:
    ////        　　  ↑
    ////        　　ここに半角スペースを置くこと。
    ////
    ////○計算　　　変数に値を代入したり、変数の値を変えたりする。
    ////　　　　　　　サンプル１（代入）：
    ////            計算 A <- B:
    ////　　　　　　　サンプル２（計算）：
    ////            計算 A += B:
    ////            計算 A -= B:
    ////            計算 A *= B:
    ////            計算 A /= B:
    ////　　　　　　　サンプル３（二項以上の計算）：
    ////            計算 A = B + C:
    ////            ・どの計算の場合も、右辺の項B,Cは、
    ////            　int型でさえあれば、即値でも変数でも良い。
    ////            ・演算子や等号の両側には半角スペースを置くこと。
    ////            ・割り算の場合、得られる結果は「商,あまり」のうちの商のみ。
	////
	////			（TODO 動作確認）
	////			計算した結果は表示する文字列の中で表示させることが出来る。
	////			その場合、A_REGに計算結果を入れ、文字列中では<汎用Ａ>と表記すれば
	////			＜汎用Ａ＞が計算結果の数値に置き換わる。
	////			他に、＜汎用Ｂ＞＜汎用Ｃ＞に対応している。
    ////
	////○END文		イベントを終了する。イベント終了時には、
	////			性欲限界判定・体力限界判定・気力限界判定が行われ、
	////			必要なら各種限界イベント・魔物イベントが行われる。
	////
	////○ENDRETURN文		各種限界イベント・魔物イベント終了後の戻り処理。
	////
    ////○コメント　"//"でその行のそれ以降の部分は無視される。
    //////////////////////////////////////////////////////////////////

    public static class SE
    {

        /* パラメーター */

		static Defines.fileID fileID_next = Defines.fileID.TXT_INIT;
		static Defines.fileID fileID_forReturn = Defines.fileID.TXT_INIT;

        static int count = 0;              /* テキストファイル全体の中でのカウンタ */
        static int countold = 0;           /* 前回処理終了時点でのカウンタの値 */
        static int inrowcount = 0;         /* 一度に文章バッファに取り込む文章内でのカウンタ */

        static string[] log = new string[101];          /* ログ文字列 */
        static string[] name = new string[101];         /* ログ文字列に対応する名前 */

        static int Slct_ct = 0;                         /* 選択肢表示用カウンタ */
        static int Slct_ct_max = 0;                     /* 選択肢表示個数 */

        static string text;    /* ファイルの中身を文字の配列として取得 */

        static int A_REG = 0;           /* スクリプト上での計算時に値を取っておくのに使うための変数 */
        static int B_REG = 0;           /* スクリプト上での計算時に値を取っておくのに使うための変数 */
        static int C_REG = 0;           /* スクリプト上での計算時に値を取っておくのに使うための変数 */

        static int val_reg = 0;

		static System.Media.SoundPlayer player = null; 

        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：s_ScriptEngine　　　　　　 　　　　　　　 　■ */
        /* ■　入力：sentence_ct　文章の番号を示すカウンタ 　　　　■ */
        /* ■　　　　o_bgpic　　　背景画像のオブジェクト　 　　　　■ */
        /* ■　　　　o_charbox1 　左側キャラ画像のオブジェクト 　　■ */
        /* ■　　　　o_charbox2 　右側キャラ画像のオブジェクト 　　■ */
        /* ■　出力：sentence_ct　次回読み込み用のカウンタの値 　　■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */

        public static int ScriptEngine(Defines.fileID file_no, int sentence_ct, int log_ct, int log_ct_use, int Slct_No)
        {
            int i;
            const int D_CHAR_LAST = 1000000;              /* １ファイルの最大文字数のDefine( ENDコードが無かった時の Fail Safe ) */
            const int D_WORKVAL_MAX = 100000;
            const int VAL_REG_A = 1;
            const int VAL_REG_B = 2;
            const int VAL_REG_C = 3;


			
            if (fileID_next != file_no)
            {
                ChangeFile(file_no);
				fileID_next = file_no;

				Program.Doujin_game_sharp.PlaySoundEffect(0, false);
				Program.Doujin_game_sharp.PlaySoundEffect(1, true);

				//PlayBGM("音楽");
				//PlayBGM("音楽");
            }
			

            /* 乱数取得用クラスインスタンス生成 */
            Random rnd = new Random();
            int rand = rnd.Next(100);           /* 乱数の生成 */

            //フォントオブジェクトの作成
            Font fnt = new Font(Defines.FontName, Defines.MainTextFontSize);

            Brush Color = Brushes.White;

            //SisterData取得
            Sister Sis = GameData.SisterData;

            string textrowbuf;                            /* 文章バッファ */

            count = nowSentHead(sentence_ct);   /* テキスト内の初期値を取得 */
            countold = count;

            for (i = log_ct_use + 1; i <= 100; i++)
            {
                log[i] = " ";
                name[i] = " ";
            }

            while (count < D_CHAR_LAST)
            {

                rand = rnd.Next(100);           /* 乱数の更新 */

                if (text[count] == '/' && text[count + 1] == '/')
                {

                    /*====================*/
                    /*   コメントアウト   */
                    /*====================*/
                    while (checkRowLast(count) == 0)
                    {
                        count++;
                    }
                    count += 2;
                    countold = count;
                    inrowcount = 0;
                }
                else if (text[count] == ':')
                {
                    textrowbuf = text.Substring(countold, inrowcount);



                    /*======================*/
                    /*    選択肢コマンド    */
                    /*======================*/
                    if (textrowbuf.Length >= 4 && textrowbuf.Substring(0, 4) == "選択肢終")
                    {
                        count++;
                        count += 2;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                        /* 選択肢の表示終了 */
                        break;
                    }
                    else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "選択肢")
                    {
                        int work_count;


                        Slct_ct = 0;

                        for (work_count = 0; work_count <= 1000; work_count++)
                        {
                            if (text[count + work_count] == '選'
                             && text[count + work_count + 1] == '択'
                             && text[count + work_count + 2] == '肢'
                             && text[count + work_count + 3] == '終')
                            {
                                int ct;
                                for (ct = 0; ct <= work_count; ct++)
                                {
                                    if (text[count + ct] == ';')
                                    {
                                        Slct_ct++;
                                    }
                                }
                                Slct_ct_max = Slct_ct;
                                break;
                            }
                        }

                        Program.Doujin_game_sharp.dispSlctBox(Slct_ct_max);
                        count++;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                    }

                    /*====================*/
                    /*    文法コマンド    */
					/*====================*/
//					else if (textrowbuf.Length >= 9 && textrowbuf.Substring(0, 9) == "ENDRETURN")
//					{
//						sentence_ct = 0;
//						count = 0;
//						countold = 0;
//
//						/*** 文章表示終了処理 ***/
//						textrowbuf = "";
//
//						/* 立ち絵の消去 */
//						Program.Doujin_game_sharp.delCharacterImageLeft();
//						Program.Doujin_game_sharp.delCharacterImageRight();
//
//
//						if (true == EndEventCheck())
//						{
//							/* 戻り先ファイル名 */
//							//break;
//						}
//						else
//						{
//							count = 0;
//							countold = 0;
//							/* ファイルを戻す */
//							ChangeFile(fileID_forReturn);
//						}
//
//						return sentence_ct;
//					}
                    else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "END")
					{
						sentence_ct = 0;
						count = 0;
						countold = 0;
						
						/*** 文章表示終了処理 ***/
						textrowbuf = "";

						/* 戻り先ファイル名 */
						fileID_forReturn = file_no;


						if (true == EndEventCheck())
						{
							//break;
						}
						else
						{
							/* 立ち絵の消去 */
							Program.Doujin_game_sharp.delCharacterImageLeft();
							Program.Doujin_game_sharp.delCharacterImageRight();

							/* 立ち絵の消去に対応してウィンドウ透過のリセット */
							Program.Doujin_game_sharp.BackgroundDraw2(0);


							return sentence_ct;
						}
					}
                    else if (textrowbuf.Substring(0, 1) == "[")
                    {
                        /*** ラベル ***/
                        /* ラベルはすっ飛ばして次へ */

                        count++;

                        ////ここにcount += 2 を入れるべきかどうか////
                        count += 2;     /* 改行（ラベルの行には、何もコメントを書かないこと） */

                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                    }
                    else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "JMP")
                    {
                        /*** ジャンプ ***/
                        count = text.IndexOf("\r\n" + textrowbuf.Remove(0, 4)) + 2;
                        countold = count;
                        sentence_ct = getNowSent(count);
                        inrowcount = 0;
                    }
                    else if (textrowbuf.Length >= 7 && textrowbuf.Substring(0, 7) == "FILEJMP")
                    {
                        int work_ct = 0;
                        /*** ファイルをまたいだジャンプ ***/
                        inrowcount = 8;
                        while (textrowbuf.Substring(inrowcount + work_ct, 1) != " ")
                        {
                            work_ct++;
                        }
                        file_no = GetFileNo(textrowbuf.Substring(inrowcount, work_ct));
                        ChangeFile(file_no);
                        inrowcount += work_ct;
                        count = text.IndexOf("\r\n" + textrowbuf.Remove(0, ++inrowcount)) + 2;
                        countold = count;
                        sentence_ct = getNowSent(count);
                        inrowcount = 0;
                    }
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "移動")
					{
						string place = textrowbuf.Substring(3, (textrowbuf.Length - 3));
						switch (place)
						{
							case "礼拝堂":
								GameData.ScenarioData.NowPlace = Defines.LOC_CHAPEL;
								break;
							case "サラの部屋":
								GameData.ScenarioData.NowPlace = Defines.LOC_SARAROOM;
								break;
							case "マリーの部屋":
								GameData.ScenarioData.NowPlace = Defines.LOC_MARYROOM;
								break;
							case "リディの部屋":
								GameData.ScenarioData.NowPlace = Defines.LOC_LIDYROOM;
								break;
							case "書庫":
								GameData.ScenarioData.NowPlace = Defines.LOC_LIBRARY;
								break;
							case "商店":
								GameData.ScenarioData.NowPlace = Defines.LOC_STORE;
								break;
							case "酒場":
								GameData.ScenarioData.NowPlace = Defines.LOC_BAR;
								break;
							case "広場":
								GameData.ScenarioData.NowPlace = Defines.LOC_SQUARE;
								break;
							case "路地裏":
								GameData.ScenarioData.NowPlace = Defines.LOC_BACKSTREET;
								break;
							default:
								GameData.ScenarioData.NowPlace = Defines.LOC_CHAPEL;
								break;
						}
						count += 3;
						countold = count;
						sentence_ct = getNowSent(count);
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "計算")
					{
						/*** 計算 ***/

						inrowcount = 3;
						int inrowcountold = inrowcount;
						int work_ct = 0;
						Parameter work_1 = new Parameter();
						int work_1_v = 0;
						int work_2 = 0;
						Parameter right_1 = new Parameter();
						Parameter right_2 = new Parameter();
						int work_value_1 = 0;
						int work_value_2 = 0;
						bool var_flag_L = false;            /*即値フラグ*/
						bool int_flag_R_1 = false;            /*即値フラグ*/
						bool int_flag_R_2 = false;            /*即値フラグ*/

						/** 計算式左辺取得 **/
						while (textrowbuf.Substring(inrowcount, 1) != " ")
						{
							inrowcount++;
							work_ct++;
						}
						if (work_ct >= 2 && "体力" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1 = Sis.HitPoint;
						}
						else if (work_ct >= 2 && "気力" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1 = Sis.MentalPoint;
						}
						else if (work_ct >= 3 && "性欲値" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1 = Sis.PassionPoint;
						}
						else if (work_ct >= 3 && "道徳心" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1 = Sis.MoralPoint;
						}
						else if (work_ct >= 5 && "触手成長度" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1 = Sis.MoralPoint;
						}
						else if (work_ct >= 5 && "姉様パンツ" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							//work_1 = Sis.Panty;
							work_1 = Sis.MoralPoint;
						}
						else if (work_ct >= 3 && "お香数" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1 = Sis.MoralPoint;
						}
						else if (work_ct >= 2 && "酒数" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1 = Sis.MoralPoint;
						}
						else if (work_ct >= 2 && "時刻" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1 = Sis.MoralPoint;
						}
						else if (work_ct >= 2 && "日数" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1_v = GameData.ScenarioData.DayCt;
							val_reg = VAL_REG_A;
							var_flag_L = true;
						}
						else if (work_ct >= 3 && "汎用Ａ" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1_v = A_REG;
							val_reg = VAL_REG_A;
							var_flag_L = true;
						}
						else if (work_ct >= 3 && "汎用Ｂ" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1_v = B_REG;
							val_reg = VAL_REG_B;
							var_flag_L = true;
						}
						else if (work_ct >= 3 && "汎用Ｃ" == textrowbuf.Substring(inrowcountold, work_ct))
						{
							work_1_v = C_REG;
							val_reg = VAL_REG_C;
							var_flag_L = true;
						}
						else
						{
							Console.WriteLine("work_1 該当するパラメーターが存在しないようです");
						}

						inrowcount++;
						inrowcountold = inrowcount;

						/** 計算式等号取得 **/
						while (textrowbuf.Substring(inrowcount, 1) != " ")
						{
							inrowcount++;
							work_2++;
						}

						inrowcount++;
						/* inrowcountoldは、演算子付き等号を後で拾うために動かさない */

						if (2 >= work_2)
						{
							/* += -= *= /= <-(代入) */
							/* 右辺の項は一つ */

							/** 右辺取得 **/
							if ("性欲値" == textrowbuf.Substring(inrowcount))
							{
								right_1 = Sis.PassionPoint;
							}
							else if ("堕落度" == textrowbuf.Substring(inrowcount))
							{
								right_1 = Sis.MoralPoint;
							}
							else if ("お香数" == textrowbuf.Substring(inrowcount))
							{
								right_1 = Sis.MoralPoint;
							}
							else if ("酒数" == textrowbuf.Substring(inrowcount))
							{
								right_1 = Sis.MoralPoint;
							}
							else if ("日数" == textrowbuf.Substring(inrowcount))
							{
								work_value_1 = GameData.ScenarioData.DayCt;
								int_flag_R_1 = true;
							}
							else if ("乱数" == textrowbuf.Substring(inrowcount))
							{
								work_value_1 = rand;
								int_flag_R_1 = true;
							}
							else
							{
								/* 整数値 */
								while (work_value_1 < D_WORKVAL_MAX)
								{
									string aaaa = textrowbuf.Substring(inrowcount);
									//if (textrowbuf.Substring(inrowcount) == work_value_1.ToString())
									if (aaaa == work_value_1.ToString())
									{
										break;
									}
									work_value_1++;
									int_flag_R_1 = true;
								}
							}

							if (var_flag_L == true)
							{
								/* 左辺、汎用パラメーター */
								if (int_flag_R_1 == true)
								{
									/* 右辺は即値 */
									if ("+=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v += work_value_1;
									}
									else if ("-=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v -= work_value_1;
									}
									else if ("*=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v *= work_value_1;
									}
									else if ("/=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v /= work_value_1;
									}
									else if ("<-" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v = work_value_1;
									}
								}
								else
								{
									/* 右辺は変数 */
									if ("+=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v += right_1.CurrentValue;
									}
									else if ("-=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v -= right_1.CurrentValue;
									}
									else if ("*=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v *= right_1.CurrentValue;
									}
									else if ("/=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v /= right_1.CurrentValue;
									}
									else if ("<-" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1_v = right_1.CurrentValue;
									}
								}
								switch (val_reg)        //汎用ＲＡＭに値を入れる
								{
									case VAL_REG_A:
										A_REG = work_1_v;
										break;
									case VAL_REG_B:
										B_REG = work_1_v;
										break;
									case VAL_REG_C:
										C_REG = work_1_v;
										break;
									default:
										Console.WriteLine("error val_reg");
										break;
								}
							}
							else
							{
								if (int_flag_R_1 == true)
								{
									/* 右辺は即値 */
									if ("+=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue += work_value_1;
									}
									else if ("-=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue -= work_value_1;
									}
									else if ("*=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue *= work_value_1;
									}
									else if ("/=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue /= work_value_1;
									}
									else if ("<-" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue = work_value_1;
									}
								}
								else
								{
									/* 右辺は変数 */
									if ("+=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue += right_1.CurrentValue;
									}
									else if ("-=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue -= right_1.CurrentValue;
									}
									else if ("*=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue *= right_1.CurrentValue;
									}
									else if ("/=" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue /= right_1.CurrentValue;
									}
									else if ("<-" == textrowbuf.Substring(inrowcountold, work_2))
									{
										work_1.CurrentValue = right_1.CurrentValue;
									}
								}
							}
						}
						else
						{
							/* = */
							/* 右辺の項は二つ */

							inrowcountold = inrowcount;
							/* こちらの分岐では演算子付き等号は出てこないので、inrowcountoldは更新してしまって良い */

							/** 右辺第一項取得 **/
							while (textrowbuf.Substring(inrowcount, 1) != ":")
							{
								inrowcount++;
								work_ct++;
							}
							if (work_ct >= 2 && "体力" == textrowbuf.Substring(inrowcount, work_ct))
							{
								work_1 = Sis.HitPoint;
							}
							else if (work_ct >= 2 && "気力" == textrowbuf.Substring(inrowcount, work_ct))
							{
								work_1 = Sis.MentalPoint;
							}
							else if (work_ct >= 3 && "性欲値" == textrowbuf.Substring(inrowcount, work_ct))
							{
								work_1 = Sis.PassionPoint;
							}
							else if (work_ct >= 3 && "道徳心" == textrowbuf.Substring(inrowcount, work_ct))
							{
								work_1 = Sis.MoralPoint;
							}
							else if (work_ct >= 3 && "お香数" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_1 = Sis.MoralPoint;
							}
							else if (work_ct >= 2 && "酒数" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_1 = Sis.MoralPoint;
							}
							else if (work_ct >= 5 && "露出癖ＬＶ" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_value_1 = Sis.Skills[0].Level;
								int_flag_R_1 = true;
							}
							else if (work_ct >= 6 && "レズっ気ＬＶ" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_value_1 = Sis.Skills[0].Level;
								int_flag_R_1 = true;
							}
							else if (work_ct >= 6 && "マゾっ気ＬＶ" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_value_1 = Sis.Skills[0].Level;
								int_flag_R_1 = true;
							}
							else if (work_ct >= 6 && "サドっ気ＬＶ" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_value_1 = Sis.Skills[0].Level;
								int_flag_R_1 = true;
							}
							else if (work_ct >= 2 && "汎用Ａ" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_value_1 = A_REG;
								int_flag_R_1 = true;
							}
							else if (work_ct >= 2 && "汎用Ｂ" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_value_1 = B_REG;
								int_flag_R_1 = true;
							}
							else if (work_ct >= 2 && "汎用Ｃ" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_value_1 = C_REG;
								int_flag_R_1 = true;
							}
							else if (work_ct >= 2 && "乱数" == textrowbuf.Substring(inrowcountold, work_ct))
							{
								work_value_1 = rand;
								int_flag_R_1 = true;
							}
							else
							{
								/* 整数値 */
								while (work_value_1 < D_WORKVAL_MAX)
								{
									if (textrowbuf.Substring(inrowcountold, work_ct) == work_value_1.ToString())
									{
										break;
									}
									work_value_1++;
									int_flag_R_1 = true;
								}
							}

							inrowcount++;
							inrowcountold = inrowcount;
							/* こちらの分岐では演算子付き等号は出てこないので、inrowcountoldは更新してしまって良い */

							/* 演算子の分カウンタを進める */
							inrowcount++;
							inrowcount++;
							/* inrowcountoldは、演算子を後で拾うために動かさない */

							/** 右辺第二項取得 **/
							if ("性欲値" == textrowbuf.Substring(inrowcount))
							{
								right_2 = Sis.PassionPoint;
							}
							else if ("堕落度" == textrowbuf.Substring(inrowcount))
							{
								right_2 = Sis.MoralPoint;
							}
							else if ("汎用Ａ" == textrowbuf.Substring(inrowcount - work_ct))
							{
								work_value_2 = A_REG;
								int_flag_R_2 = true;
							}
							else if ("汎用Ｂ" == textrowbuf.Substring(inrowcount - work_ct))
							{
								work_value_2 = B_REG;
								int_flag_R_2 = true;
							}
							else if ("汎用Ｃ" == textrowbuf.Substring(inrowcount - work_ct))
							{
								work_value_2 = C_REG;
								int_flag_R_2 = true;
							}
							else if ("乱数" == textrowbuf.Substring(inrowcount - work_ct))
							{
								work_value_2 = rand;
								int_flag_R_2 = true;
							}
							else
							{
								/* 整数値 */
								while (work_value_2 < D_WORKVAL_MAX)
								{
									if (textrowbuf.Substring(inrowcount) == work_value_2.ToString())
									{
										break;
									}
									work_value_2++;
									int_flag_R_2 = true;
								}
							}

							/** 計算 **/
							if (var_flag_L == true)
							{
								if (int_flag_R_1 == true && int_flag_R_2 == true)
								{
									/* 右辺の両項とも即値 */
									if ("+" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = work_value_1 + work_value_2;
									}
									else if ("-" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = work_value_1 - work_value_2;
									}
									else if ("*" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = work_value_1 * work_value_2;
									}
									else if ("/" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = work_value_1 / work_value_2;
									}
								}
								else if (int_flag_R_1 == true && int_flag_R_2 == false)
								{
									/* 右辺の第一項：即値　第二項：変数 */
									if ("+" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = work_value_1 + right_2.CurrentValue;
									}
									else if ("-" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = work_value_1 - right_2.CurrentValue;
									}
									else if ("*" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = work_value_1 * right_2.CurrentValue;
									}
									else if ("/" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = work_value_1 / right_2.CurrentValue;
									}
								}
								else if (int_flag_R_1 == false && int_flag_R_2 == true)
								{
									/* 右辺の第一項：即値　第二項：変数 */
									if ("+" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = right_1.CurrentValue + work_value_2;
									}
									else if ("-" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = right_1.CurrentValue - work_value_2;
									}
									else if ("*" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = right_1.CurrentValue * work_value_2;
									}
									else if ("/" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = right_1.CurrentValue / work_value_2;
									}
								}
								else
								{
									/* 右辺の両項とも変数 */
									if ("+" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = right_1.CurrentValue + right_2.CurrentValue;
									}
									else if ("-" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = right_1.CurrentValue - right_2.CurrentValue;
									}
									else if ("*" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = right_1.CurrentValue * right_2.CurrentValue;
									}
									else if ("/" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1_v = right_1.CurrentValue / right_2.CurrentValue;
									}
								}

								switch (val_reg)        //汎用ＲＡＭに値を入れる
								{
									case VAL_REG_A:
										A_REG = work_1_v;
										break;
									case VAL_REG_B:
										B_REG = work_1_v;
										break;
									case VAL_REG_C:
										C_REG = work_1_v;
										break;
									default:
										Console.WriteLine("error val_reg");
										break;
								}
							}
							else
							{
								if (int_flag_R_1 == true && int_flag_R_2 == true)
								{
									/* 右辺の両項とも即値 */
									if ("+" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = work_value_1 + work_value_2;
									}
									else if ("-" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = work_value_1 - work_value_2;
									}
									else if ("*" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = work_value_1 * work_value_2;
									}
									else if ("/" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = work_value_1 / work_value_2;
									}
								}
								else if (int_flag_R_1 == true && int_flag_R_2 == false)
								{
									/* 右辺の第一項：即値　第二項：変数 */
									if ("+" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = work_value_1 + right_2.CurrentValue;
									}
									else if ("-" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = work_value_1 - right_2.CurrentValue;
									}
									else if ("*" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = work_value_1 * right_2.CurrentValue;
									}
									else if ("/" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = work_value_1 / right_2.CurrentValue;
									}
								}
								else if (int_flag_R_1 == false && int_flag_R_2 == true)
								{
									/* 右辺の第一項：即値　第二項：変数 */
									if ("+" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = right_1.CurrentValue + work_value_2;
									}
									else if ("-" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = right_1.CurrentValue - work_value_2;
									}
									else if ("*" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = right_1.CurrentValue * work_value_2;
									}
									else if ("/" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = right_1.CurrentValue / work_value_2;
									}
								}
								else
								{
									/* 右辺の両項とも変数 */
									if ("+" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = right_1.CurrentValue + right_2.CurrentValue;
									}
									else if ("-" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = right_1.CurrentValue - right_2.CurrentValue;
									}
									else if ("*" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = right_1.CurrentValue * right_2.CurrentValue;
									}
									else if ("/" == textrowbuf.Substring(inrowcountold, 1))
									{
										work_1.CurrentValue = right_1.CurrentValue / right_2.CurrentValue;
									}
								}
							}
						}

						/* 改行 */
						count++;
						count++;

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
						inrowcountold = 0;

					}
					else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "If(")
					{
						/*** 条件分岐 If文 ***/

						inrowcount = 4;
						int inrowcountold = inrowcount;
						int work_ct_1 = 0;
						int work_ct_2 = 0;
						Parameter work_1 = new Parameter();
						int work_1_v = 0;
						int work_2 = 0;
						int work_3 = 0;
						int work_4 = 0;
						Parameter work_5 = new Parameter();
						int work_value = 0;
						bool int_flag_L = false;              /* 左辺即値 */
						bool int_flag_R = false;              /* 右辺即値 */

						bool work_value_b_L = false;
						bool work_value_b_R = false;
						bool bool_flag_L = false;			  /* 右辺Bool値 */
						bool bool_flag_R = false;			  /* 右辺Bool値 */

						/** 条件左辺取得 **/
						while (textrowbuf.Substring(inrowcount, 1) != " ")
						{
							inrowcount++;
							work_ct_1++;
						}
						if (work_ct_1 >= 2 && "体力" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_1 = Sis.HitPoint;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 2 && "気力" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_1 = Sis.MentalPoint;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 3 && "性欲値" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_1 = Sis.PassionPoint;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 3 && "道徳心" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_1 = Sis.MoralPoint;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 4 && "レズっ気" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = Sis.Skills[Sister.SKL_SMLFETI].Level;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 5 && "匂いフェチ" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = Sis.Skills[Sister.SKL_SMLFETI].Level;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 4 && "ふたなり" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							//////////未作成//////////
							work_3 = Sis.Skills[Sister.SKL_FUTA].Level;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 6 && "サキュバス化" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							//////////未作成//////////
							work_3 = Sis.Skills[Sister.SKL_SUCCUBUS].Level;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 5 && "触手成長度" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_1 = Sis.MoralPoint;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 5 && "姉様パンツ" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							//work_1 = Sis.MaryPanty;
							work_1 = Sis.MoralPoint;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 3 && "お香数" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_1 = Sis.MoralPoint;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 2 && "酒数" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_1 = Sis.MoralPoint;
							int_flag_L = false;
						}
						else if (work_ct_1 >= 4 && "お香経験" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = Sis.ExpNums[Sister.EXP_INCENSE];
							int_flag_L = false;
						}
						else if (work_ct_1 >= 4 && "触手経験" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = Sis.ExpNums[Sister.EXP_LOPER];
							int_flag_L = false;
						}
						else if (work_ct_1 >= 5 && "治療会経験" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = Sis.ExpNums[Sister.EXP_HEAL];
							int_flag_L = false;
						}
						else if (work_ct_1 >= 6 && "姉様レズ経験" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = Sis.ExpNums[Sister.EXP_MARYLSB];
							int_flag_L = false;
						}
						else if (work_ct_1 >= 2 && "日数" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = GameData.ScenarioData.DayCt;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 2 && "時刻" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = GameData.ScenarioData.NowTime;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 4 && "現在位置" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = GameData.ScenarioData.NowPlace;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 3 && "信頼度" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = GameData.ScenarioData.NowPlace;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 3 && "汎用Ａ" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = A_REG;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 3 && "汎用Ｂ" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = B_REG;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 3 && "汎用Ｃ" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = C_REG;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 4 && "選択番号" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = Slct_No;
							int_flag_L = true;
						}
						else if (work_ct_1 >= 2 && "乱数" == textrowbuf.Substring(inrowcountold, work_ct_1))
						{
							work_3 = rand;
							int_flag_L = true;
						}
						else
						{
							Console.WriteLine("if文左辺に想定外の値が入っている");
						}

						inrowcount++;
						inrowcountold = inrowcount;

						/** 比較演算子取得 **/
						while (textrowbuf.Substring(inrowcount, 1) != " ")
						{
							inrowcount++;
							work_2++;
						}

						inrowcount++;
						inrowcountold = inrowcount;

						/** 条件右辺取得 **/
						/* 文字数取得 */
						while (textrowbuf.Substring(inrowcountold + work_ct_2, 1) != " ")
						{
							inrowcount++;
							work_ct_2++;
						}
						if (work_ct_2 >= 2 && "お香数" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5 = Sis.HitPoint;
							int_flag_R = false;
							bool_flag_R = false;
						}
						if (work_ct_2 >= 2 && "体力" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5 = Sis.HitPoint;
							int_flag_R = false;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 2 && "気力" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5 = Sis.MentalPoint;
							int_flag_R = false;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 3 && "性欲値" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5 = Sis.PassionPoint;
							int_flag_R = false;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 3 && "道徳心" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5 = Sis.MoralPoint;
							int_flag_R = false;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 5 && "触手成長度" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5 = Sis.MoralPoint;
							int_flag_R = false;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 5 && "日数" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = GameData.ScenarioData.DayCt;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 3 && "礼拝堂" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_CHAPEL;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 5 && "サラの部屋" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_SARAROOM;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 6 && "マリーの部屋" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_MARYROOM;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 6 && "リディの部屋" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_LIDYROOM;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 2 && "書庫" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_LIBRARY;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 2 && "商店" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_STORE;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 2 && "酒場" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_BAR;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 2 && "広場" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_SQUARE;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 3 && "路地裏" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Defines.LOC_BACKSTREET;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 3 && "汎用Ａ" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = A_REG;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 3 && "汎用Ｂ" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = B_REG;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 3 && "汎用Ｃ" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = C_REG;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 4 && "選択番号" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = Slct_No;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (work_ct_2 >= 2 && "乱数" == textrowbuf.Substring(inrowcountold, work_ct_2))
						{
							work_5.CurrentValue = rand;
							int_flag_R = true;
							bool_flag_R = false;
						}
						else if (textrowbuf.Length >= inrowcountold + 4 && textrowbuf.Substring(inrowcountold, work_ct_2) == "true")
						{
							/* true (bool) */

							work_value_b_R = true;
							int_flag_R = true;
							bool_flag_R = true;
						}
						else if (textrowbuf.Length >= inrowcountold + 5 && textrowbuf.Substring(inrowcountold, work_ct_2) == "false")
						{
							/* false (bool) */

							work_value_b_R = false;
							int_flag_R = true;
							bool_flag_R = true;
						}
						else
						{
							/* 整数値 */
							while (work_value < D_WORKVAL_MAX)
							{
								if (textrowbuf.Substring(inrowcountold, work_ct_2) == work_value.ToString())
								{
									break;
								}
								work_value++;
							}
							int_flag_R = true;
						}

						inrowcount += 3;

						/** ジャンプ先ラベル文字数取得 **/
						while (textrowbuf.Substring(inrowcount + work_4, 1) != "]")
						{
							work_4++;
						}
						work_4++;

						/** 条件式に従ってジャンプ **/

						if (bool_flag_L == true && bool_flag_R == true)
						{
							//bool値
							if ("==" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{
								if (work_value_b_L == work_value_b_R)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
						}
						else if (int_flag_L == false && int_flag_R == false)
						{
							//左辺：変数　右辺：変数

							if ("<" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{
								if (work_1.CurrentValue < work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("<=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue <= work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if (">" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue > work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if (">=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue >= work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("==" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{
								if (work_1.CurrentValue == work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("!=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue != work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}

						}
						else if (int_flag_L == false && int_flag_R == true)
						{
							// 左辺変数
							int_flag_R = false;

							if ("<" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{
								if (work_1.CurrentValue < work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("<=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue <= work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if (">" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue > work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if (">=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue >= work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("==" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue == work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("!=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_1.CurrentValue != work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}

						}
						else if (int_flag_L == true && int_flag_R == false)
						{
							//右辺変数

							if ("<" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{
								if (work_3 < work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("<=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 <= work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if (">" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 > work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if (">=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 >= work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("==" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 == work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("!=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 != work_5.CurrentValue)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}

						}
						else if (int_flag_L == true && int_flag_R == true)
						{
							// 左辺：整数値　右辺：整数値
							int_flag_R = false;

							if ("<" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{
								if (work_3 < work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("<=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 <= work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if (">" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 > work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if (">=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 >= work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("==" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 == work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}
							else if ("!=" == textrowbuf.Substring(5 + work_ct_1, work_2))
							{

								if (work_3 != work_value)
								{
									string aiueo = "\r\n" + textrowbuf.Substring(inrowcount, work_4);
									count = text.IndexOf(aiueo) + 2;
								}
								else
								{
									/* 改行 */
									count++;
									count++;

									count++;
								}
							}

						}

						countold = count;
						sentence_ct = getNowSent(count);
						inrowcount = 0;
						inrowcountold = 0;
					}
					/*======================*/
					/*    発言者コマンド    */
					/*======================*/
					else if (textrowbuf == "Text")
					{
						Color = Brushes.White;

						name[0] = "Text";

						/* キャラ名の消去 */
						Program.Doujin_game_sharp.ClearCharacterName();

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "サラ")
					{
						Color = Brushes.Pink;
						Program.Doujin_game_sharp.setCharacterImageLeft(textrowbuf);

						name[0] = "サラ";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "マリー")
					{
						Color = Brushes.Yellow;

						Program.Doujin_game_sharp.setCharacterImageLeft(textrowbuf);

						name[0] = "マリー";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "リディ")
					{
						Color = Brushes.Orange;
						Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "リディ";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "触手娘")
					{
						Color = Brushes.Green;
						Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "触手娘";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "魔物")
					{
						Color = Brushes.Magenta;
						Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "魔物";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "？？？")
					{
						Color = Brushes.Gray;
						//Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "？？？";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "商人")
					{
						Color = Brushes.Gray;
						//Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "商人";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "店員")
					{
						Color = Brushes.Gray;
						//Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "店員";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "男１")
					{
						Color = Brushes.Gray;
						//Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "男";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "男２")
					{
						Color = Brushes.Gray;
						//Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "男";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "男３")
					{
						Color = Brushes.Gray;
						//Program.Doujin_game_sharp.setCharacterImageRight(textrowbuf);

						name[0] = "男";

						/* キャラ名の表示 */
						Program.Doujin_game_sharp.DrawCharacterName(name[0]);

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf == "Plus")
					{
						Color = Brushes.Blue;

						name[0] = "Plus";

						/* キャラ名の消去 */
						Program.Doujin_game_sharp.ClearCharacterName();

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					else if (textrowbuf == "Minus")
					{
						Color = Brushes.Red;

						name[0] = "Minus";

						/* キャラ名の消去 */
						Program.Doujin_game_sharp.ClearCharacterName();

						count++;
						countold = count;
						sentence_ct++;
						inrowcount = 0;
					}
					/*======================*/
					/*     効果コマンド     */
					/*======================*/
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "IN")
					{
						/* 立ち絵の表示 */
						string char_name = textrowbuf.Substring(3, textrowbuf.Length - 3);

						if (char_name.Length >= 2 && char_name.Substring(0, 2) == "サラ")
						{
							Program.Doujin_game_sharp.setCharacterImageLeft(char_name);
						}
						else
						{
							Program.Doujin_game_sharp.setCharacterImageRight(char_name);
						}
						count += 2;
						countold = count;
						sentence_ct = getNowSent(count);
						inrowcount = 0;
					}
					else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "OUT")
					{
						/* 立ち絵の消去 */
						string char_name = textrowbuf.Substring(4, textrowbuf.Length - 4);

						if (char_name.Length >= 2 && char_name.Substring(0, 2) == "サラ")
						{
							Program.Doujin_game_sharp.delCharacterImageLeft();
						}
						else
						{
							Program.Doujin_game_sharp.delCharacterImageRight();
						}
						Program.Doujin_game_sharp.BackgroundDraw2(0);
						count += 2;
						countold = count;
						sentence_ct = getNowSent(count);
						inrowcount = 0;


					}
					else if (textrowbuf.Length >= 3 && textrowbuf.Substring(0, 3) == "BGM")
					{
						/* BGMの鳴動 */

					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "SE")
					{
						/* 効果音の鳴動 */

					}
					else if (textrowbuf.Length >= 2 && textrowbuf.Substring(0, 2) == "背景")
					{
						string str_BGPic = textrowbuf.Substring(3, textrowbuf.Length - 3);
						/* 背景の変更 */
						Program.Doujin_game_sharp.DispStatus = 0;
						Program.Doujin_game_sharp.BGPicname = str_BGPic;
						Program.Doujin_game_sharp.setBGPic();
						count += 3;
						countold = count;
						sentence_ct = getNowSent(count);
						inrowcount = 0;
					}
                }
                else if (text[count] == ';')
                {

                    if (Slct_ct != 0)
                    {
                        /* 選択肢の表示 */
                        //Bitmap canvas;
                        int no = 0;

                        textrowbuf = text.Substring(countold + 2, inrowcount - 2);

                        //描画先とするImageオブジェクトを作成する
						Bitmap canvas1 = new Bitmap(Defines.SelectBoxWidth, Defines.SelectBoxHeight);
                        Graphics g1 = Graphics.FromImage(canvas1);
                        g1.DrawString(textrowbuf, fnt, Color, 10, 17);

                        //表示する
                        switch (Slct_ct)
                        {
                            case 4:
                                    no = 1;
                                break;
                            case 3:
                                if (Slct_ct_max == 4)      { no = 2; }
                                else                       { no = 1; }
                                break;
                            case 2:
                                if      (Slct_ct_max == 4) { no = 3; }
                                else if (Slct_ct_max == 3) { no = 2; }
                                else                       { no = 1; }
                                break;
                            case 1:
                                if      (Slct_ct_max == 4) { no = 4; }
                                else if (Slct_ct_max == 3) { no = 3; }
                                else                       { no = 2; }
                                break;
                            default:
                                break;
                        }
                        Program.Doujin_game_sharp.setSelectBoxImage(no, canvas1);
                        g1.Dispose();
                        //ImageオブジェクトのGraphicsオブジェクトを作成する



                        count++;                /* ';'分 */
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                        Slct_ct--;
                        if (Slct_ct == 0)
                        {
                            count += 2;         /* 改行記号分 */
                            countold = count;
                            sentence_ct = getNowSent(count);
                            //リソースを解放する
                            fnt.Dispose();
                        }
                    }
                    else
                    {
                        /* ナレーション・セリフの表示 */
                        //描画先とするImageオブジェクトを作成する
                        Bitmap canvas1 = new Bitmap(Defines.TextAreaWidth, Defines.TextAreaHeight);
                        //ImageオブジェクトのGraphicsオブジェクトを作成する
                        Graphics g1 = Graphics.FromImage(canvas1);

                        textrowbuf = text.Substring(countold, inrowcount);

						//計算結果部分を文字列に置き換える
						textrowbuf = textrowbuf.Replace("<汎用Ａ>", A_REG.ToString("D"));
						textrowbuf = textrowbuf.Replace("<汎用Ｂ>", B_REG.ToString("D"));
						textrowbuf = textrowbuf.Replace("<汎用Ｃ>", C_REG.ToString("D"));

                        g1.DrawString(textrowbuf, fnt, Color, 30, 0);
                        //PictureBox1に表示する
                        Program.Doujin_game_sharp.setTextAreaImage(canvas1);

                        //リソースを解放する
                        fnt.Dispose();
                        g1.Dispose();

                        count++;
                        countold = count;
                        sentence_ct++;
                        inrowcount = 0;
                        log[0] = textrowbuf;
                        backlogRenew(log_ct_use);
                        break;
                    }
                }
                else if (count >= 2 && checkRowLast(count) == 1 && checkRowLast(count - 2) == 1)
                {
                    /* 空白行 */
                    count += 2;
                    countold = count;
                    inrowcount = 0;
                }
                else
                {
                    count++;
                    inrowcount++;
                }
            }
            return sentence_ct;
        }


        




        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：ScrollInit    　　　　　　 　　　　　　　 　■ */
        /* ■　内容：バックログ初期表示処理                     　 ■ */
        /* ■　入力：                                        　 　 ■ */
        /* ■　出力：                                        　 　 ■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        public static void ScrollInit(int log_ct_use, PictureBox o_bgpic_0, PictureBox o_bgpic_1, PictureBox o_bgpic_2, PictureBox o_bgpic_3, PictureBox o_bgpic_4)
        {
            //フォントオブジェクトの作成
            Font fnt = new Font("メイリオ", 12);

            int i;

            Brush Color = Brushes.White;

            for (i = 1; i <= log_ct_use; i++)
            {
                if (name[i] == "Text")
                {
                    Color = Brushes.White;
                }
                else if (name[i] == "サラ")
                {
                    Color = Brushes.Pink;
                }
                else if (name[i] == "マリー")
                {
                    Color = Brushes.Yellow;
                }
                else if (name[i] == "リディ")
                {
                    Color = Brushes.Orange;
                }
                else if (name[i] == "触手娘")
                {
                    Color = Brushes.Green;
                }
                else if (name[i] == "魔物")
                {
                    Color = Brushes.Purple;
                }
                else if (name[i] == "Plus")
                {
                    Color = Brushes.Blue;
                }
                else if (name[i] == "Minus")
                {
                    Color = Brushes.Red;
                }

                if (i == 1)
                {
                    /* 描画先とするImageオブジェクトを作成する */
                    Bitmap canvas0 = new Bitmap(o_bgpic_0.Width, o_bgpic_0.Height);
                    /* ImageオブジェクトのGraphicsオブジェクトを作成する */
                    Graphics g0 = Graphics.FromImage(canvas0);
                    /* 描画内容を準備 */
                    g0.DrawString(log[i], fnt, Color, 30, 0);
                    /* PictureBoxに表示*/
                    o_bgpic_0.Image = canvas0;
                    /* リソースを解放 */
                    g0.Dispose();
                }
                else if (i == 2)
                {
                    Bitmap canvas1 = new Bitmap(o_bgpic_1.Width, o_bgpic_1.Height);
                    Graphics g1 = Graphics.FromImage(canvas1);
                    g1.DrawString(log[i], fnt, Color, 30, 0);
                    o_bgpic_1.Image = canvas1;
                    g1.Dispose();
                }
                else if (i == 3)
                {
                    Bitmap canvas2 = new Bitmap(o_bgpic_2.Width, o_bgpic_2.Height);
                    Graphics g2 = Graphics.FromImage(canvas2);
                    g2.DrawString(log[i], fnt, Color, 30, 0);
                    o_bgpic_2.Image = canvas2;
                    g2.Dispose();
                }
                else if (i == 4)
                {
                    Bitmap canvas3 = new Bitmap(o_bgpic_3.Width, o_bgpic_3.Height);
                    Graphics g3 = Graphics.FromImage(canvas3);
                    g3.DrawString(log[i], fnt, Color, 30, 0);
                    o_bgpic_3.Image = canvas3;
                    g3.Dispose();
                }
                else if (i == 5)
                {
                    Bitmap canvas4 = new Bitmap(o_bgpic_4.Width, o_bgpic_4.Height);
                    Graphics g4 = Graphics.FromImage(canvas4);
                    g4.DrawString(log[i], fnt, Color, 30, 0);
                    o_bgpic_4.Image = canvas4;
                    g4.Dispose();
                }

                if (i >= 5)
                {
                    break;
                }
            }
            //リソースを解放する
            fnt.Dispose();
        }


        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：s_ScrollRedraw　　　　　　 　　　　　　　 　■ */
        /* ■　内容：バックログ表示更新処理                     　 ■ */
        /* ■　入力：                                        　 　 ■ */
        /* ■　出力：                                        　 　 ■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        public static void ScrollRedraw(int log_ct, int log_ct_use, PictureBox o_bgpic_0, PictureBox o_bgpic_1, PictureBox o_bgpic_2, PictureBox o_bgpic_3, PictureBox o_bgpic_4)
        {
            //フォントオブジェクトの作成
            Font fnt = new Font("メイリオ", 12);

            int i;


            Brush Color = Brushes.White;

            for (i = log_ct; i <= log_ct_use; i++)
            {
                if (name[i] == "Text")
                {
                    Color = Brushes.White;
                }
                else if (name[i] == "サラ")
                {
                    Color = Brushes.Pink;
                }
                else if (name[i] == "マリー")
                {
                    Color = Brushes.Yellow;
                }
                else if (name[i] == "リディ")
                {
                    Color = Brushes.Orange;
                }
                else if (name[i] == "魔物")
                {
                    Color = Brushes.Purple;
                }
                else if (name[i] == "Plus")
                {
                    Color = Brushes.Blue;
                }
                else if (name[i] == "Minus")
                {
                    Color = Brushes.Red;
                }

                if (i == log_ct)
                {
                    /* 描画先とするImageオブジェクトを作成する */
                    Bitmap canvas0 = new Bitmap(o_bgpic_0.Width, o_bgpic_0.Height);
                    /* ImageオブジェクトのGraphicsオブジェクトを作成する */
                    Graphics g0 = Graphics.FromImage(canvas0);
                    /* 描画内容を準備 */
                    g0.DrawString(log[i], fnt, Color, 30, 0);
                    /* PictureBoxに表示*/
                    o_bgpic_0.Image = canvas0;
                    /* リソースを解放 */
                    g0.Dispose();
                }
                else if (i == log_ct + 1)
                {
                    Bitmap canvas1 = new Bitmap(o_bgpic_1.Width, o_bgpic_1.Height);
                    Graphics g1 = Graphics.FromImage(canvas1);
                    g1.DrawString(log[i], fnt, Color, 30, 0);
                    o_bgpic_1.Image = canvas1;
                    g1.Dispose();
                }
                else if (i == log_ct + 2)
                {
                    Bitmap canvas2 = new Bitmap(o_bgpic_2.Width, o_bgpic_2.Height);
                    Graphics g2 = Graphics.FromImage(canvas2);
                    g2.DrawString(log[i], fnt, Color, 30, 0);
                    o_bgpic_2.Image = canvas2;
                    g2.Dispose();
                }
                else if (i == log_ct + 3)
                {
                    Bitmap canvas3 = new Bitmap(o_bgpic_3.Width, o_bgpic_3.Height);
                    Graphics g3 = Graphics.FromImage(canvas3);
                    g3.DrawString(log[i], fnt, Color, 30, 0);
                    o_bgpic_3.Image = canvas3;
                    g3.Dispose();
                }
                else if (i == log_ct + 4)
                {
                    Bitmap canvas4 = new Bitmap(o_bgpic_4.Width, o_bgpic_4.Height);
                    Graphics g4 = Graphics.FromImage(canvas4);
                    g4.DrawString(log[i], fnt, Color, 30, 0);
                    o_bgpic_4.Image = canvas4;
                    g4.Dispose();
                }

                if (i >= log_ct + 5)
                {
                    break;
                }
            }
            //リソースを解放する
            fnt.Dispose();
        }


        /*////////////////////////ここからサブルーチン的メソッド////////////////////////*/



        public static Defines.fileID GetFileNo(string name)
        {
            Defines.fileID file_no = 0;

            if (name == "オープニング") file_no = Defines.fileID.TXT_OPENING;
            else if (name == "教会") file_no = Defines.fileID.TXT_CHURCH;
			else if (name == "マリーの部屋") file_no = Defines.fileID.TXT_MARY;
			else if (name == "リディの部屋") file_no = Defines.fileID.TXT_LIDY;
            else if (name == "休息") file_no = Defines.fileID.TXT_LEST;
            else if (name == "お店") file_no = Defines.fileID.TXT_SHOP;
            else if (name == "読書") file_no = Defines.fileID.TXT_READ;
            else if (name == "露出") file_no = Defines.fileID.TXT_ROSYUTSU;
            else if (name == "性癖購入") file_no = Defines.fileID.TXT_NEWSKILL;
            else if (name == "性欲限界") file_no = Defines.fileID.TXT_PUSSION_LIMIT;
            else if (name == "体力限界") file_no = Defines.fileID.TXT_HP_RUNOUT;
            else if (name == "気力限界") file_no = Defines.fileID.TXT_MENTAL_RUNOUT;
            else if (name == "エンディング") file_no = Defines.fileID.TXT_ENDING;

            return file_no;
        }

        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：ChangeFile    　　　　　　 　　　　　　　 　■ */
        /* ■　内容：読み取るファイルを変更                     　 ■ */
        /* ■　      file_noに対応したファイルをtext(文字列)に展開 ■ */
        /* ■　入力：file_no                                 　 　 ■ */
        /* ■　出力：                                        　 　 ■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */

        private static void ChangeFile(Defines.fileID file_no)
        {
            switch (file_no)
            {
                case Defines.fileID.TXT_OPENING:
                    text = Properties.Resources.オープニング;
                    break;
                case Defines.fileID.TXT_CHURCH:
                    text = Properties.Resources.教会;
					break;
				case Defines.fileID.TXT_MARY:
					text = Properties.Resources.マリーの部屋;
					break;
				case Defines.fileID.TXT_LIDY:
					text = Properties.Resources.リディの部屋;
					break;
                case Defines.fileID.TXT_LEST:
                    text = Properties.Resources.休息;
                    break;
				case Defines.fileID.TXT_SHOP:
					//text = Properties.Resources.商店;
                    break;
                case Defines.fileID.TXT_READ:
                    text = Properties.Resources.読書;
                    break;
                case Defines.fileID.TXT_ROSYUTSU:
					//text = Properties.Resources.露出;
                    break;
				case Defines.fileID.TXT_NEWSKILL:
					text = Properties.Resources.スキル取得;
                    break;
				case Defines.fileID.TXT_PUSSION_LIMIT:
					text = Properties.Resources.性欲限界;
                    break;
				case Defines.fileID.TXT_HP_RUNOUT:
					text = Properties.Resources.体力切れ;
                    break;
				case Defines.fileID.TXT_MENTAL_RUNOUT:
					text = Properties.Resources.気力切れ;
					break;
				case Defines.fileID.TXT_DEVIL_PART:
					text = Properties.Resources.魔物パート;
					break;
				case Defines.fileID.TXT_ENDING:
					text = Properties.Resources.エンディング;
                    break;
                default:
                    Console.WriteLine("エラー　該当するテキストファイルがありません");
                    break;
            }
            doujin_game_sharp.ChangeNowFile(file_no);
        }


        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：nowSentHead   　　　　　　 　　　　　　　 　■ */
        /* ■　内容：テキストの現在地取得処理                   　 ■ */
        /* ■　      sentence_ctからファイル中の位置を算出する  　 ■ */
        /* ■　　　　文章の送り時や、ロード後の最初の文探しに使用  ■ */
        /* ■　入力：sentence_ct                             　 　 ■ */
        /* ■　出力：                                        　 　 ■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */

        private static int nowSentHead(int ct)
        {
            int i = 0;
            int k = 0;
            bool mark_active = false;
            int j;

            for (j = 0; j < ct; j++)
            {
                /* ;が来るまで開始位置からのカウンタを進める */
                while (i != -1)
                {
                    if (text[i] == ';' || text[i] == ':')
                    {
                        /* ;や:が登場した場合、*/
                        /* コメント内部だったら一文字としてカウント */
                        /* コメント外だったら文章のカウンタをインクリメント */

                        k = i;
                        while (text[k] != '/' || text[k + 1] != '/')
                        {
                            if (text[k] == '\r' && text[k + 1] == '\n')
                            {
                                /* 前回のコメント記号より前に改行 ⇒ 使用中の;や: */
                                mark_active = true;
                                break;
                            }
                            else
                            {
                                k--;
                                if (k <= 0)
                                {
                                    mark_active = false;
                                    break;
                                }
                            }
                        }
                        if (mark_active == true)
                        {
                            /* 使用中の;や: */
                            mark_active = false;

                            /* ;や:出現後、次の文字が改行コードだったらさらに二文字進める */
                            if (text[i + 1] == '\r' && text[i + 2] == '\n')
                            {
                                i++;
                                i++;
                            }
                            break;
                        }
                        else
                        {
                            /* コメント内 */
                            i++;
                        }
                    }
                    else
                    {
                        /* ;や:以外の文字だったら文章のカウンタをインクリメント */
                        i++;
                    }
                }
            }
            i++;
            return i;
        }


        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：getNowSent    　　　　　　 　　　　　　　 　■ */
        /* ■　内容：現在のファイル中の位置から     　             ■ */
        /* ■　　　  sentence_ctの値を算出する処理  　             ■ */
        /* ■　　　　ifやjmpなどでcountが急に変わった後に呼ぶ   　 ■ */
        /* ■　入力：sentence_ct                             　 　 ■ */
        /* ■　出力：                                        　 　 ■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */

        private static int getNowSent(int ct)
        {
            int i = 0;
            int j;
            int k = 0;
            bool mark_active = false;

            for (j = 0; j < (ct); j++)
            {
                //                if ( text[j] == ':' || text[j] == ';' )
                //                {
                //                    i++;
                //                }
                if (text[j] == ':' || text[j] == ';')
                {
                    k = j;
                    while (text[k] != '/' || text[k + 1] != '/')
                    {
                        if (text[k] == '\r' && text[k + 1] == '\n')
                        {
                            /* 前回のコメント記号より前に改行コマンドにぶつかる ⇒ j文字目はコメント外なので使用中の;や: */
                            mark_active = true;
                            break;
                        }
                        else
                        {
                            /* 前回のコメント記号が改行コマンドより先にぶつかる ⇒ j文字目はコメント内 */
                            k--;
                            if (k <= 0)
                            {
                                mark_active = false;
                                break;
                            }
                        }
                    }
                    if (mark_active == true)
                    {
                        /* 使用中の;や: */
                        mark_active = false;
                        i++;
                    }
                    else
                    {
                        /* コメント内 */
                    }

                }
            }

            return i;
        }



        private static int checkRowLast(int ct)
        {
            /* 改行判定処理 */
            /* コメントアウトの終端を判定するのに用いる */

            int i = 0;

            if (text[ct] == '\r' && text[ct + 1] == '\n')
            {
                i = 1;
            }

            return i;
        }

        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
        /* ■　関数名：s_BacklogRenew　　　　　　 　　　　　　　 　■ */
        /* ■　内容：バックログ用文字列配列を更新する処理    　 　 ■ */
        /* ■　入力：log_ct                                  　 　 ■ */
        /* ■　出力：log_ct                                  　 　 ■ */
        /* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */

        private static void backlogRenew(int log_ct_use)
        {
            int i;
            string test_1;
            string test_2;

            for (i = log_ct_use + 1; i > 0; i--)
            {
                if (name[i].Length < name[i - 1].Length)
                {
                    name[i] = name[i].PadRight(name[i - 1].Length);
                }
                else if (name[i].Length > name[i - 1].Length)
                {
                    name[i] = name[i].Substring(0, name[i - 1].Length);
                }
                name[i] = name[i].Replace(name[i], name[i - 1]);


                if (log[i].Length < log[i - 1].Length)
                {
                    log[i] = log[i].PadRight(log[i - 1].Length);
                }
                else if (log[i].Length > log[i - 1].Length)
                {
                    log[i] = log[i].Substring(0, log[i - 1].Length);
                }
                string work_str = string.Copy(log[i]);
                log[i] = work_str.Replace(work_str, log[i - 1]);
                test_2 = log[i - 1];
                test_1 = log[i];
                test_2 = log[i - 1];

            }

            test_1 = log[0];
            test_2 = log[1];
            var test_3 = log[2];
            var test_4 = log[3];

            //return log_ct_use;
        }


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：PlayBGM     　　　　　　 　　　　　　　 　■ */
		/* ■　内容：BGMを鳴動する処理					     　 　 ■ */
		/* ■　入力：wavefilename                            　 　 ■ */
		/* ■　出力：		                                 　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		//WAVEファイルを再生する
		
		private static void PlayBGM(string wavefilename)
		{

			System.IO.UnmanagedMemoryStream music;
			music = GetwaveFile(wavefilename);
			//再生されているときは止める
			if (player != null)
				StopSound();

			//読み込む
			player = new System.Media.SoundPlayer(music);
			//ループ再生する（DON'T KNOW 非同期とは？）
			player.PlayLooping();
			
		}

		private static void StopSound()
		{
			if (player != null)
			{
				player.Stop();
				player.Dispose();
				player = null;
			}
		}


		public static System.IO.UnmanagedMemoryStream GetwaveFile(string wavefilename)
		{
			if (wavefilename == "音楽")
			{
				return Properties.Resources.音楽;
			}
			else if (wavefilename == "ohayougozaimasu_02")
			{
				return Properties.Resources.ohayougozaimasu_02;
			}
			else
			{
				Console.WriteLine("error 該当の音楽がありません");
				return Properties.Resources.音楽;
			}

		}




		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：EndEventCheck  　　　　　 　　　　　　　 　■ */
		/* ■　内容：イベント終了時各特殊イベント判定           　 ■ */
		/* ■　入力：                                        　 　 ■ */
		/* ■　出力：体力切れ…true                          　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static bool EndEventCheck()
		{
			bool ret = false; 

			GameData.ScenarioData.NowTime++;
			if(doujin_game_sharp.IsPassionLimit(GameData.SisterData.PassionPoint))
			{
				/*性欲限界*/
				ChangeFile(Defines.fileID.TXT_PUSSION_LIMIT);
				ret = true;
			}
			else if (2 == doujin_game_sharp.IsStaminaRunout(GameData.SisterData.HitPoint))
			{
				/*体力限界*/
				ChangeFile(Defines.fileID.TXT_HP_RUNOUT);
				ret = true;
			}
			else if (2 == doujin_game_sharp.IsMentalRunout(GameData.SisterData.MentalPoint))
			{
				/*気力限界*/
				ChangeFile(Defines.fileID.TXT_MENTAL_RUNOUT);
				ret = true;
			}
			else if (1 == doujin_game_sharp.IsStaminaRunout(GameData.SisterData.HitPoint))
			{
				/*体力限界間近*/
				ChangeFile(Defines.fileID.TXT_HP_RUNOUT);
				ret = true;
			}
			else if (1 == doujin_game_sharp.IsMentalRunout(GameData.SisterData.MentalPoint))
			{
				/*気力限界間近*/
				ChangeFile(Defines.fileID.TXT_MENTAL_RUNOUT);
				ret = true;
			}
			else if (21 < GameData.ScenarioData.NowTime)
			{
				/*１日終了*/
				ChangeFile(Defines.fileID.TXT_DEVIL_PART);
				GameData.ScenarioData.DayCt++;
				ret = true;

			}
			return ret;
		}
    }
}

