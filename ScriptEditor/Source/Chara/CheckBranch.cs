using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;


namespace ScriptEditor
{

	//ブランチ内のコマンドの現在完成入力数を保持する

	//フレーム動作中においてスクリプトのブランチリストをチェックし、
	//コマンド成立していたら遷移先アクションを返す
	public class CheckBranchList
	{
		//保存用ゲームキー
		//現在フレーム[0]から遡りつつ、20フレーム前[19]までカウントする
		private const int arrayGameKeyNum = 20;
		private GameKey[] arrayGameKey = new GameKey[arrayGameKeyNum];
		public GameKey[] ArrayGameKey { get { return arrayGameKey; } set { arrayGameKey = value; } }

		//各コマンドの入力完成数を記録
//		List<CheckBranch> listCheckCommand = new List<CheckBranch> ();

		//デバッグ表示
		public string strDebug { get;  set; }

		//コンストラクタ
		public CheckBranchList ()
		{
			for ( int i = 0; i < arrayGameKeyNum; ++i )
			{
				arrayGameKey[i] = new GameKey ();
			}
		}

		//初期化
		public void Init ()
		{
		}


		//毎フレームにおけるキー入力
		public void Update ()
		{
			//入力状態を更新
			KeyInput.Instance.Update ();

			//今回のゲームキーに直して保存
			GameKey gameKey = new GameKey ();

			//上下左右
			bool b8 = KeyInput.Instance.IsKey ( GAME_INPUT.P1_UP );
			bool b2 = KeyInput.Instance.IsKey ( GAME_INPUT.P1_DOWN );
			bool b4 = KeyInput.Instance.IsKey ( GAME_INPUT.P1_LEFT );
			bool b6 = KeyInput.Instance.IsKey ( GAME_INPUT.P1_RIGHT );

			//3つ以上同時押しは優先順で処理

			// コマンド指定では12369874順

			//斜め優先
			if ( b8 && b4 ) { gameKey.Direction = GameKey.GameKeyDirection.DIR_7; }
			else if ( b8 && b6 ) { gameKey.Direction = GameKey.GameKeyDirection.DIR_9; }
			else if ( b6 && b2 ) { gameKey.Direction = GameKey.GameKeyDirection.DIR_3; }
			else if ( b4 && b2 ) { gameKey.Direction = GameKey.GameKeyDirection.DIR_1; }
			else if ( b8 ) { gameKey.Direction = GameKey.GameKeyDirection.DIR_8; }
			else if ( b6 ) { gameKey.Direction = GameKey.GameKeyDirection.DIR_6; }
			else if ( b4 ) { gameKey.Direction = GameKey.GameKeyDirection.DIR_4; }
			else if ( b2 ) { gameKey.Direction = GameKey.GameKeyDirection.DIR_2; }
			else { gameKey.Direction = GameKey.GameKeyDirection.NEUTRAL; }

			strDebug = gameKey.Direction.ToString();

			gameKey.Button[0] = ( KeyInput.Instance.IsKey ( GAME_INPUT.P1_BUTTON1 ) ) ? GameKey.GameKeyButton.BTN_ON : GameKey.GameKeyButton.BTN_OFF;
			gameKey.Button[1] = ( KeyInput.Instance.IsKey ( GAME_INPUT.P1_BUTTON2 ) ) ? GameKey.GameKeyButton.BTN_ON : GameKey.GameKeyButton.BTN_OFF;
			gameKey.Button[2] = ( KeyInput.Instance.IsKey ( GAME_INPUT.P1_BUTTON3 ) ) ? GameKey.GameKeyButton.BTN_ON : GameKey.GameKeyButton.BTN_OFF;
			gameKey.Button[3] = ( KeyInput.Instance.IsKey ( GAME_INPUT.P1_BUTTON4 ) ) ? GameKey.GameKeyButton.BTN_ON : GameKey.GameKeyButton.BTN_OFF;

			//ゲーム入力を更新しながら現在フレーム分を保存
			for ( int i = arrayGameKeyNum - 1; i >= 1; --i )
			{
				arrayGameKey[i] = arrayGameKey[i - 1];
			}
			arrayGameKey[0] = gameKey;
		}


		//完成しているかどうか
		
		//遷移先取得（遷移しない場合はnullを返す）
		public Action GetTransit ( BindingList<Branch> listBranch, bool dirRight )
		{
			//ブランチごとにチェック
			foreach ( Branch branch in listBranch )
			{
				if ( branch.Condition.Compare ( arrayGameKey, dirRight ) )
				{
					//完成したら遷移先アクションIDを返す
					return branch.Transit;
				}

			}

			return null;
		}
	}
}
