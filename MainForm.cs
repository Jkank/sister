using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DoujinGameProject.Data;
using DoujinGameProject.Action;
using System.Runtime.InteropServices;

namespace DoujinGameProject
{
	public partial class doujin_game_sharp : Form
	{
		/*--- ここに使用するクラスを置く ---*/

		public static Defines.fileID nowfile = 0;
		public int sent_ct = 0;
		public int log_ct = 0;          /* ログ現在位置カウンタ */
		public int log_ct_use = 0;      /* ログ最古位置カウンタ */

		public bool MouseLeftPushed = false;
		public bool fadein = false;
		public int fadestage = 0;
		public int currentAlphaPercent = 0;
		public Image currentImage1;
		public Control pictboxfade1;
		public Control pictboxfade2;

		static Image canvas_tb1;
		static Image canvas_tb2;
		public int DispStatus;
		public string BGPicname;


		DirectSound dsound;
		
		public doujin_game_sharp()
		{
			InitializeComponent();
			//ホイールイベントの追加  
			this.PNL_Event.MouseWheel
				+= new System.Windows.Forms.MouseEventHandler(this.panel3_MouseWheel);
			this.PNL_Log.MouseWheel
				+= new System.Windows.Forms.MouseEventHandler(this.panel4_MouseWheel);

			/* --- オブジェクトの背景色を透明にできるようにする処理 --- */
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);

			/* --- 各オブジェクトの背景色を透明にする --- */
			PNL_Eventslct.BackColor = Color.Transparent;   /* 行動選択画面のPanel */
			PNL_Mainselect.BackColor = Color.Transparent;   /* メイン選択画面のPanel */
			PNL_Event.BackColor = Color.Transparent;   /* メッセージボックスのPanel */
			PIC_Buffer.BackColor = Color.Transparent;   /* 行動選択画面のPanel */
			PIC_NameBox.BackColor = Color.Transparent;   /* 名前ボックス */
			PIC_TextArea.BackColor = Color.Transparent;   /* メッセージボックス */
			PIC_Chara_pos1.BackColor = Color.Transparent;   /* 立ち絵位置１ */
			PIC_Chara_pos2.BackColor = Color.Transparent;   /* 立ち絵位置２ */

			/* --- データ類初期化 --- */
			//TODO: ここでやるか、NewGame/LoadGame時にやるか検討
			GameData.Initialize();


			/* 体力 */
			GameData.SisterData.HitPoint.MaxValue = 100;
			GameData.SisterData.HitPoint.CurrentValue = 100;

			/* 気力 */
			GameData.SisterData.MentalPoint.MaxValue = 100;
			GameData.SisterData.MentalPoint.CurrentValue = 100;

			/* 性欲値 */
			GameData.SisterData.PassionPoint.MaxValue = 100;
			GameData.SisterData.PassionPoint.CurrentValue = 10;

			/* 道徳心 */
			GameData.SisterData.MoralPoint.MaxValue = 100;
			GameData.SisterData.MoralPoint.CurrentValue = 100;

			/* 信頼度 */
			GameData.SisterData.TrustPoint.MaxValue = 100;
			GameData.SisterData.TrustPoint.CurrentValue = 100;

			GameData.ScenarioData.Slct_No = 0;

			//EnableDoubleBuffering(PNL_Background);
			EnableDoubleBuffering(PNL_Event);
		}

		//        string line;


		public static void EnableDoubleBuffering(Control control)
		{
			control.GetType().InvokeMember(
			   "DoubleBuffered",
			   BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
			   null,
			   control,
			   new object[] { true });
		}


		/* == 以下サブルーチン的メソッド == */




		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：ChangeNowFile		　  　　　　　　　　　■ */
		/* ■　内容：参照ファイル変更処理						   ■ */
		/* ■　入力：file_id                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static void ChangeNowFile(Defines.fileID file_id)
		{
			nowfile = file_id;
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：setCharacterImageLeft　  　　　　　　　　　■ */
		/* ■　内容：左キャラボックスにキャラ画像を				   ■ */
		/* ■　　　　設定・表示する処理							   ■ */
		/* ■　入力：imagename                               　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void setCharacterImageLeft(string imagename)
		{
			setCharacterImage(PIC_Chara_pos1, imagename);
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：setCharacterImageRight　  　　　　　　　　　■ */
		/* ■　内容：右キャラボックスにキャラ画像を				   ■ */
		/* ■　　　　設定・表示する処理							   ■ */
		/* ■　入力：imagename                               　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void setCharacterImageRight(string imagename)
		{
			setCharacterImage(PIC_Chara_pos2, imagename);
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：delCharacterImageLeft　  　　　　　　　　　■ */
		/* ■　内容：左キャラボックスにキャラ画像を				   ■ */
		/* ■　　　　消去する処理								   ■ */
		/* ■　入力：imagename                               　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void delCharacterImageLeft()
		{
			PIC_Chara_pos1.Visible = false;
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：delCharacterImageLeft　  　　　　　　　　　■ */
		/* ■　内容：右キャラボックスにキャラ画像を				   ■ */
		/* ■　　　　消去する処理								   ■ */
		/* ■　入力：imagename                               　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void delCharacterImageRight()
		{
			PIC_Chara_pos2.Visible = false;
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：delCharacterImageLeft　  　　　　　　　　　■ */
		/* ■　内容：テキストボックスに画像を設定する処理		   ■ */
		/* ■　入力：imagename                               　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void setTextAreaImage(Bitmap canvas)
		{
			PIC_TextArea.Image = canvas;
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：delCharacterImageLeft　  　　　　　　　　　■ */
		/* ■　内容：名前ボックスに画像を設定する処理			   ■ */
		/* ■　入力：imagename                               　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void setNameBoxImage(Bitmap canvas)
		{
			PIC_NameBox.Image = canvas;
		}
		
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：setCharacterImage　  　　　　　　　　　　   ■ */
		/* ■　内容：立ち絵設定・表示開始処理					   ■ */
		/* ■　入力：chrbox                                  　 　 ■ */
		/* ■　      imageID                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		private void setCharacterImage(PictureBox chrbox, string tachie_name)
		{
			if (tachie_name == "サラ") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(恥じらい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(落ち込み)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_2;
			else if (tachie_name == "サラ(後ろめたい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(悪)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸恥じらい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(シナ)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(シナ恥じらい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(シナ発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(シナ驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(シナ悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸シナ)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸シナ怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸シナ恥じらい)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸シナ発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸シナ驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(裸シナ悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(淫魔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(淫魔怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(淫魔驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(淫魔発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "サラ(淫魔悪笑顔)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "マリー") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "マリー(驚き)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "マリー(怒り)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "マリー(発情)") chrbox.BackgroundImage = Properties.Resources.sara_0_0;
			else if (tachie_name == "リディ") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "リディ(笑顔)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "リディ(恥じらい)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "リディ(恐怖)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "リディ(苦しみ)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "触手娘") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "触手娘(恥じらい)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "触手娘(苦しみ)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "魔物") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "魔物(悪笑い)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;
			else if (tachie_name == "魔物(驚き)") chrbox.BackgroundImage = Properties.Resources.lidy_0_0;

			if (chrbox.Visible == false)
			{
				fadein = true;
				currentAlphaPercent = 0;


				pictboxfade1 = chrbox;
				currentImage1 = chrbox.BackgroundImage;

				//最初に透明度100%の画像を描画してしまう。（一瞬透明度0%の画像が表示されるのを防ぐ）
				Image nowimg = CreateTranslucentImage(currentImage1, currentAlphaPercent * 0.01f);
				pictboxfade1.BackgroundImage = nowimg;

				Timer_FadeIn.Interval = 10;
				//タイマーをスタート
				Timer_FadeIn.Start();
				chrbox.Visible = true;
			}
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：setBGPic         　  　　　　　　　　　　 　■ */
		/* ■　内容：背景画像表示処理							   ■ */
		/* ■　入力：なし                                    　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void setBGPic()
		{
			switch(DispStatus)
			{
				case 0:
					ShowPicforDisp();
					break;
				case 1:
					PIC_Buffer.Visible = true;
					switch (BGPicname)
					{
						case "オープニング":
							PNL_Background.BackgroundImage = Properties.Resources.g_bg_000_0;
							break;
						case "教会昼":
							PNL_Background.BackgroundImage = Properties.Resources.kyoukai_dt;
							break;
						case "教会夕":
							PNL_Background.BackgroundImage = Properties.Resources.kyoukai_ev;
							break;
						case "教会夜":
							PNL_Background.BackgroundImage = Properties.Resources.kyoukai_nt;
							break;
						case "階段":
							PNL_Background.BackgroundImage = Properties.Resources.dungeon06;
							break;
						case "檻":
							PNL_Background.BackgroundImage = Properties.Resources.rouya_nt;
							break;
						case "部屋昼":
							PNL_Background.BackgroundImage = Properties.Resources.yadoya_dt;
							break;
						case "部屋夕":
							PNL_Background.BackgroundImage = Properties.Resources.yadoya_ev;
							break;
						case "部屋夜":
							PNL_Background.BackgroundImage = Properties.Resources.yadoya_ntr;
							break;
						case "書庫":
							PNL_Background.BackgroundImage = Properties.Resources.syoko_dt;
							break;
						case "町昼":
							PNL_Background.BackgroundImage = Properties.Resources.hiroba_dt;
							break;
						case "町夕":
							PNL_Background.BackgroundImage = Properties.Resources.hiroba_ev;
							break;
						case "町夜":
							PNL_Background.BackgroundImage = Properties.Resources.hiroba_nt;
							break;
						default:
							Console.WriteLine("背景画像の指定値がテーブル上に用意されていません");
							break;

					}
					break;
				case 2:
					Timer_Wait.Stop();
					DispStatus = 0;
					PIC_Buffer.Visible = false;
					break;

			}
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：ShowPicforDisp　  　　　　　　　　　　   　■ */
		/* ■　内容：背景画像をちらつかせずに変更する処理		   ■ */
		/* ■　入力：なし		                             　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		void ShowPicforDisp()
		{
			BackgroundDraw_All(PIC_Buffer);
			Timer_Wait.Start();
		}


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：Timer_Wait_Tick　  　　　　　　　　　　    ■ */
		/* ■　内容：背景画像変更の待ち時間経過					   ■ */
		/* ■　入力：なし		                             　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		private void Timer_Wait_Tick(object sender, EventArgs e)
		{
			DispStatus++;
			setBGPic();
		}


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：ShowHalfClearly		 　　　　　　　　　 　■ */
		/* ■　内容：画像半透明表示処理							   ■ */
		/* ■　入力：NowPicturebox                           　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		void ShowHalfClearly(PictureBox NowPicturebox)
		{
			Graphics g_tb = null;
			if (NowPicturebox == PIC_TextArea)
			{
				//描画先とするImageオブジェクトを作成する
				canvas_tb1 = new Bitmap(NowPicturebox.Width, NowPicturebox.Height);
				//ImageオブジェクトのGraphicsオブジェクトを作成する
				g_tb = Graphics.FromImage(canvas_tb1);
			}
			else if (NowPicturebox == PIC_NameBox)
			{
				canvas_tb2 = new Bitmap(NowPicturebox.Width, NowPicturebox.Height);
				//ImageオブジェクトのGraphicsオブジェクトを作成する
				g_tb = Graphics.FromImage(canvas_tb2);
			}


			//画像を読み込む
			Image img_tb = NowPicturebox.BackgroundImage;

			//ColorMatrixオブジェクトの作成
			System.Drawing.Imaging.ColorMatrix cm =
				new System.Drawing.Imaging.ColorMatrix();
			//ColorMatrixの行列の値を変更して、アルファ値が0.5に変更されるようにする
			cm.Matrix00 = 1;
			cm.Matrix11 = 1;
			cm.Matrix22 = 1;
			cm.Matrix33 = 0.5F;
			cm.Matrix44 = 1;

			//ImageAttributesオブジェクトの作成
			System.Drawing.Imaging.ImageAttributes ia =
				new System.Drawing.Imaging.ImageAttributes();
			//ColorMatrixを設定する
			ia.SetColorMatrix(cm);

			//ImageAttributesを使用して画像を描画
			g_tb.DrawImage(img_tb, new Rectangle(0, 0, img_tb.Width, img_tb.Height),
				0, 0, img_tb.Width, img_tb.Height, GraphicsUnit.Pixel, ia);

			//リソースを解放する
			img_tb.Dispose();
			g_tb.Dispose();

			//PictureBox1に表示する
			if (NowPicturebox == PIC_TextArea)
			{
				NowPicturebox.BackgroundImage = canvas_tb1;
			}
			else if (NowPicturebox == PIC_NameBox)
			{
				NowPicturebox.BackgroundImage = canvas_tb2;
			}
		}


		
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：CreateTranslucentImage 　　 　　　　　　　  ■ */
		/* ■　内容：指定された画像をフェードインさせる処理  　 　 ■ */
		/* ■　入力：Slct_ct                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public static Image CreateTranslucentImage(Image img, float alpha)
		{

			//半透明の画像の描画先となるImageオブジェクトを作成
			Bitmap transImg = new Bitmap(img.Width, img.Height);
			//transImgのGraphicsオブジェクトを取得
			Graphics g = Graphics.FromImage(transImg);
			///Graphics g = Graphics.FromImage(img);

			//imgを半透明にしてtransImgに描画
			System.Drawing.Imaging.ColorMatrix cm =
				new System.Drawing.Imaging.ColorMatrix();
			cm.Matrix00 = 1;
			cm.Matrix11 = 1;
			cm.Matrix22 = 1;
			cm.Matrix33 = alpha;
			cm.Matrix44 = 1;
			System.Drawing.Imaging.ImageAttributes ia =
				new System.Drawing.Imaging.ImageAttributes();
			ia.SetColorMatrix(cm);
			g.DrawImage(img,
				new Rectangle(0, 0, img.Width, img.Height),
				0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

			//リソースを解放する
			g.Dispose();

			return transImg;
			///return img;
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：FadeinTimer_Tick		 　　 　　　　　　　  ■ */
		/* ■　内容：フェードイン用タイマ処理				　 　 ■ */
		/* ■　入力：Slct_ct                                 　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		private void TimerFadein_Tick(object sender, EventArgs e)
		{
			//透明度を決定
			if (fadein)
			{
				currentAlphaPercent += 5;
				if (100 <= currentAlphaPercent)
				{
					fadein = false;
				}
			}
			else
			{
				//タイマーを停止
				((Timer)sender).Stop();
			}

			//半透明の画像を作成
			Image nowimg = CreateTranslucentImage(currentImage1, currentAlphaPercent * 0.01f);

			//半透明の画像を表示
			if (pictboxfade1.BackgroundImage != null)
			{
				//pictboxfade1.BackgroundImage.Dispose();
			}
			pictboxfade1.BackgroundImage = nowimg;
			BackgroundDraw(PIC_NameBox);
			BackgroundDraw(PIC_TextArea);
		}


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：BackgroundDraw			 　　 　　　　　　 ■ */
		/* ■　内容：透過部分画像・パネルを描画する際、			   ■ */
		/* ■　　　　透過部分に被られた奥の画像が				   ■ */
		/* ■		 見た目上消えないようにする処理			　 　  ■ */
		/* ■　入力：targetpanel_id                          　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void BackgroundDraw(PictureBox NowPicturebox)
		{

			//現在の背景画像の状況を再現する。
			Bitmap canvas = new Bitmap(NowPicturebox.Width, NowPicturebox.Height);
			Graphics g = Graphics.FromImage(canvas);

			Image Image1 = PIC_Chara_pos1.BackgroundImage;
			Image Image2 = PIC_Chara_pos2.BackgroundImage;
			Image canvas_img;
			if (NowPicturebox == PIC_TextArea)
			{
				canvas_img = canvas_tb1;
			}
			else if (NowPicturebox == PIC_NameBox)
			{
				canvas_img = canvas_tb2;
			}
			else
			{
				canvas_img = null;
			}

			if (Image1 != null && PIC_Chara_pos1.Visible == true)
			{
				g.DrawImage(Image1, PIC_Chara_pos1.Location.X - NowPicturebox.Location.X, PIC_Chara_pos1.Location.Y - NowPicturebox.Location.Y, Image1.Width, Image1.Height);
			}
			if (Image2 != null && PIC_Chara_pos2.Visible == true)
			{
				g.DrawImage(Image2, PIC_Chara_pos2.Location.X - NowPicturebox.Location.X, PIC_Chara_pos2.Location.Y - NowPicturebox.Location.Y, Image2.Width, Image2.Height);
			}

			g.DrawImage(canvas_img, 0, 0, canvas_img.Width, canvas_img.Height);


			//Graphicsオブジェクトのリソースを解放する
			g.Dispose();

			//PictureBoxに表示する
			NowPicturebox.BackgroundImage = canvas;
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：BackgroundDraw2			 　　 　　　　　　 ■ */
		/* ■　内容：名前表示ウィンドウとテキストウィンドウの	   ■ */
		/* ■　　　　両方に、BackgroundDraw処理を適用			   ■ */
		/* ■　入力：targetpanel_id                          　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void BackgroundDraw2(int a)
		{
			BackgroundDraw(PIC_NameBox);
			BackgroundDraw(PIC_TextArea);
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：BackgroundDraw_All		 　　 　　　　　　 ■ */
		/* ■　内容：名前表示ウィンドウとテキストウィンドウ、	   ■ */
		/* ■　		 そして二つのキャラクター画像のすべてに、	   ■ */
		/* ■　　　　BackgroundDraw処理を適用					   ■ */
		/* ■　入力：targetpanel_id                          　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void BackgroundDraw_All(PictureBox NowPicturebox)
		{

			//現在の背景画像の状況を再現する。
			Bitmap canvas = new Bitmap(NowPicturebox.Width, NowPicturebox.Height);
			Graphics g = Graphics.FromImage(canvas);

			Image Image1 = PIC_Chara_pos1.BackgroundImage;
			Image Image2 = PIC_Chara_pos2.BackgroundImage;
			Image Image3 = PIC_NameBox.BackgroundImage;
			Image Image4 = PIC_TextArea.BackgroundImage;
			Image canvas_img;

			canvas_img = new Bitmap(NowPicturebox.Width, NowPicturebox.Height); ;


			if (Image1 != null && PIC_Chara_pos1.Visible == true)
			{
				g.DrawImage(Image1, PIC_Chara_pos1.Location.X - NowPicturebox.Location.X, PIC_Chara_pos1.Location.Y - NowPicturebox.Location.Y, Image1.Width, Image1.Height);
			}
			if (Image2 != null && PIC_Chara_pos2.Visible == true)
			{
				g.DrawImage(Image2, PIC_Chara_pos2.Location.X - NowPicturebox.Location.X, PIC_Chara_pos2.Location.Y - NowPicturebox.Location.Y, Image2.Width, Image2.Height);
			}
			if (Image3 != null && PIC_NameBox.Visible == true)
			{
				g.DrawImage(Image3, PIC_NameBox.Location.X - NowPicturebox.Location.X, PIC_NameBox.Location.Y - NowPicturebox.Location.Y, Image3.Width, Image3.Height);
			}
			if (Image4 != null && PIC_TextArea.Visible == true)
			{
				g.DrawImage(Image4, PIC_TextArea.Location.X - NowPicturebox.Location.X, PIC_TextArea.Location.Y - NowPicturebox.Location.Y, Image4.Width, Image4.Height);
			}

			g.DrawImage(canvas_img, 0, 0, canvas_img.Width, canvas_img.Height);


			//Graphicsオブジェクトのリソースを解放する
			g.Dispose();

			//PictureBoxに表示する
			NowPicturebox.BackgroundImage = canvas;
		}


		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：BackgroundDraw_All		 　　 　　　　　　 ■ */
		/* ■　内容：名前表示ウィンドウとテキストウィンドウ、	   ■ */
		/* ■　		 そして二つのキャラクター画像のすべてに、	   ■ */
		/* ■　　　　BackgroundDraw処理を適用					   ■ */
		/* ■　入力：targetpanel_id                          　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void doujin_game_sharp_Shown(object sender, EventArgs e)
		{
			// DirectSoundの初期化
			dsound = new DirectSound(this);
			// 利用する音楽の数を指定する。
			// 今回は3種類なので3を設定。
			dsound.init(2);
			// 効果音の読み込み。

			dsound.loadWave(0, "ohayougozaimasu_02.wav");
			dsound.loadWave(1, "musicbox.wav");
		}

		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		/* ■　関数名：PlaySoundEffect			 　　 　　　　　　 ■ */
		/* ■　内容：サウンド鳴動開始処理						   ■ */
		/* ■　入力：targetpanel_id                          　 　 ■ */
		/* ■　出力：なし                                    　 　 ■ */
		/* ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■ */
		public void PlaySoundEffect(int idx, bool loop)
		{
			dsound.play(idx, loop);
		}




		

	}


}
