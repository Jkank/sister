using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;
using System.Windows.Forms;

namespace DoujinGameProject
{
	/*
	*	DirectSoundを使用するためのクラス
	*/
	public class DirectSound
	{
		public Device devSound = null;
		public SecondaryBuffer[] bufSec = null;
		public static string sErr = "";

		public DirectSound(Form owner)
		{
			try
			{
				//	デバイスの作成
				devSound = new Device();

				//協調レベルの設定
				devSound.SetCooperativeLevel(owner, CooperativeLevel.Priority);
			}
			catch (Exception e)
			{
				e.ToString();
			}

		}

		//解放処理
		~DirectSound()
		{
			releaseBufSec();
			if (devSound != null)
			{
				devSound.Dispose();
				devSound = null;
			}
		}

		// セカンダリバッファを解放する
		public void releaseBufSec()
		{
			int i;
			if (bufSec != null)
			{
				for (i = 0; i < bufSec.Length; i++)
				{
					if (bufSec[i] != null)
					{
						bufSec[i].Dispose();
						bufSec[i] = null;
					}
				}
				bufSec = null;
			}
		}

		/** 読み込むファイルの上限数を設定する。*/
		public void init(int max)
		{
			int i;
			releaseBufSec();

			// 新しく領域を定義
			bufSec = new SecondaryBuffer[max];
			for (i = 0; i < max; i++)
			{
				bufSec[i] = null;
			}
		}

		/** 指定の音声データを読み込む*/
		public bool loadWave(int idx, string fname)
		{
			try
			{
				bufSec[idx] = new SecondaryBuffer(fname, devSound);
			}
			catch (Exception e)
			{
				sErr = "[loadWaveエラー]" + e.ToString();
				return false;
			}
			return true;
		}

		/** 指定の音声を再生する*/
		public void play(int idx, bool loop)
		{
			if ((bufSec != null) && (bufSec[idx] != null))
			{
				bufSec[idx].SetCurrentPosition(0);
				if (!loop)
				{
					bufSec[idx].Play(0, BufferPlayFlags.Default);
				}
				else
				{
					bufSec[idx].Play(0, BufferPlayFlags.Looping);
				}
			}
		}

		/** 指定のサウンドを停止する*/
		public void stop(int idx)
		{
			if ((bufSec != null) && (bufSec[idx] != null))
			{
				bufSec[idx].Stop();
			}
		}


	}
}
