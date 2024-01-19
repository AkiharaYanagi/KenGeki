using System.ComponentModel;
using System.Collections.Generic;

namespace ScriptEditor
{
	using BL_Sqc = BindingList<Sequence>;
	using L_Scp = List<Script>;

	//---------------------------------------------------------------------
	// コンペンド(アクション/エフェクトの集合)を受けて編集する
	//	選択中のシークエンスとスクリプト位置を保持
	//---------------------------------------------------------------------
	public class EditCompend
	{
		//編集対象
		public Compend Compend { get; set; } = null;

		//コントロール
		Ctrl_Compend Ctrl_Cmpd { get; set; } = null;

		//部分編集
		public EditSequence EditSequence { get; set; } = new EditSequence ();
		public EditScript EditScript { get; set; } = new EditScript ();			 


		//---------------------------------------------------------------------
		//コントロールの設定
		public void SetCtrl ( Ctrl_Compend cc )
		{
			Ctrl_Cmpd = cc;
		}

		//対象設定
		public void SetCharaData ( Chara ch, Compend cmpd )
		{
			Compend = cmpd;
			Ctrl_Cmpd.SetCharaData ( ch );
		}

		//---------------------------------------------------------------------
		public bool IsCopy { get; set; } = false;		//	コピー中フラグ
		public bool AllScript { get; set; } = false;	//	スクリプト全体の変更トグル
		public bool SpanScript { get; set; } = false;	//	スクリプト範囲の変更トグル

		//---------------------------------------------------------------------
		//	選択
		//		ここでのインデックスは見た目からの位置なのでBL_DCTには影響なく用いることができる
		//---------------------------------------------------------------------
		//選択位置
		public Sequence SelectedSequence { get; set; } = null;
		public Script SelectedScript { get; set; } = null;

		public int SelectedScriptIndex { get; set; } = 0;
		public int SelectedSpanStart { get; set; } = 0;
		public int SelectedSpanEnd { get; set; } = 0;

		//---------------------------------------------------------------------

		//数値指定(範囲チェック付)
		public void SelectScript ( int sequence, int frame )
		{
			BL_Sqc ls = Compend.Bldct_sqc.GetBindingList ();
			if ( sequence < 0 || ls.Count <= sequence ) { return; }
			SelectedSequence = ls [ sequence ];

			L_Scp lscp = SelectedSequence.ListScript;
			if ( frame < 0 || SelectedSequence.ListScript.Count <= 0 ) { return; }
			SelectedScript = lscp [ frame ];

			Assosiate ();
		}

		//フレームのみ選択
		public void SelectFrame ( int frame )
		{
			L_Scp lscp = SelectedSequence.ListScript;
			if ( frame < 0 || lscp.Count <= frame ) { return; }
			SelectedScriptIndex = frame;
			SelectedScript =  lscp [ frame ];

			Assosiate ();
		}

		//シークエンスのみ選択
		public void SelectSequence ( int sequence )
		{
			BL_Sqc ls = Compend.Bldct_sqc.GetBindingList ();
			if ( sequence < 0 || ls.Count <= sequence ) { return; }
			SelectedSequence = ls [ sequence ];

			//スクリプトは０を選択
			L_Scp lscp = SelectedSequence.ListScript;
			if ( lscp.Count <= 0 ) { return; }
			SelectFrame ( 0 );
		}

		//名前からシークエンス選択
		public void SelectSequence ( string name )
		{
			//アクション名以外のとき何もしない
			Sequence sq = Compend.Bldct_sqc.Get ( name );
			if ( null == sq ) { return; }

			SelectedSequence = sq;

			//スクリプトは０を選択
			L_Scp lscp = SelectedSequence.ListScript;
			if ( lscp.Count <= 0 ) { return; }
			SelectFrame ( 0 );
		}

		//関連付け(選択からの大元)
		public void Assosiate ()
		{
			L_Scp lscp = SelectedSequence.ListScript;
			//関連付け
			Ctrl_Cmpd.Assosiate ( SelectedScript );
			EditScript.Restruct ( lscp, SelectedScript.Frame );
			FormRect.Inst.Assosiate ( SelectedScript );
			FormEfGnrt.Inst.Assosiate ( SelectedScript );

			DispChara.Inst.Disp ();
		}

		//---------------------------------------------------------------------
		//	シークエンス
		//---------------------------------------------------------------------
		//末尾にシークエンス追加
		public void AddSequence ( Sequence s )
		{
			Compend.Bldct_sqc.Add ( s );
		}

		public virtual void Add ()
		{
		}
		public void Add ( Sequence s )
		{
			Compend.Bldct_sqc.Add ( s );
		}

		//選択している後にシークエンス挿入
		public void InsertSequence ( Sequence s )
		{
//			BL_DCT_Sqc bldct_sqc = Compend.Bldct_sqc;
//			int i = bldct_sqc.GetBindingList().IndexOf ( SelectedSequence );
			Compend.Bldct_sqc.Insert ( s.Name, s );
		}
		public virtual void Insert ()
		{
//			int i = Compend.ListSequence.IndexOf ( SelectedSequence );
//			Compend.ListSequence.Insert ( i, new Sequence ( "new Inserted Sequence" ) );
			string name = "new Inserted Sequence";
			Compend.Bldct_sqc.Insert ( name, new Sequence ( name ) );
		}
		public void Insert ( Sequence s )
		{
//			int i = Compend.ListSequence.IndexOf ( SelectedSequence );
//			Compend.ListSequence.Insert ( i, s );
			Compend.Bldct_sqc.Insert ( s.Name, s );
		}

		//選択中のシークエンスを削除
		public void RemoveSequence ()
		{
//			int i = Compend.ListSequence.IndexOf ( SelectedSequence );
//			Compend.ListSequence.RemoveAt ( i );
			Compend.Bldct_sqc.Remove ( SelectedSequence.Name );
		}
		public void Remove ()
		{
//			int i = Compend.ListSequence.IndexOf ( SelectedSequence );
//			Compend.ListSequence.RemoveAt ( i );
			Compend.Bldct_sqc.Remove ( SelectedSequence.Name );
		}

		//---------------------------------------------------------------------
		//	スクリプト
		//---------------------------------------------------------------------
		//スクリプト追加
		public void AddScript ()
		{
			SelectedSequence.AddScript ();
		}
		public void AddScript ( Script script )
		{
			SelectedSequence.AddScript ( script );
		}

		//スクリプト挿入
		public void InsertScript ()
		{
			int i = SelectedSequence.ListScript.IndexOf ( SelectedScript );
			SelectedSequence.ListScript.Insert ( i, new Script () );
		}
		public void InsertScript ( Script script )
		{
			int i = SelectedSequence.ListScript.IndexOf ( SelectedScript );
			SelectedSequence.ListScript.Insert ( i, script );
		}

		//選択中のスクリプトを削除
		public void RemScript ()
		{
			int i = SelectedSequence.ListScript.IndexOf ( SelectedScript );
			SelectedSequence.ListScript.RemoveAt ( i );
#if false

			//左に移項
			if ( 1 < selectedScript.frame )
			{
				--( selectedScript.frame );
			}
#endif

#if false
			SELECTED_SCRIPT_SPAN sss = selectedSpan;
			if ( 1 < sss.end && 1 < sss.start )
			{
				SetSpan ( sss.sequence, sss.start - 1, sss.end - 1 );
			}
#endif
		}

		//	コピー用スクリプト
		private SELECTED_SCRIPT copiedScript = new SELECTED_SCRIPT ( 0, 0 );
		public SELECTED_SCRIPT CopiedScript { get { return copiedScript; } }

		//対象スクリプトを保存
		public void CopyTargetScript ()
		{
//			copiedScript.Copy ( selectedScript );
			IsCopy = true;
		}

		//コピー元スクリプトを取得
		private Script GetCopiedScript ()
		{
			//対象シークエンス
			BL_Sqc bl_sqc = Compend.Bldct_sqc.GetBindingList ();
			int s = copiedScript.sequence;
			if ( s < 0 || bl_sqc.Count <= s ) { return null; }
			Sequence seq = bl_sqc [ s ];

			//フレーム
			int f = copiedScript.frame;
			if ( f < 0 || seq.ListScript.Count <= f ) { return null; }

			return seq.ListScript[ f ];
		}

		//ペースト
		public void PasteScript ()
		{
			//範囲にコピー
			for ( int i = SelectedSpanStart; i < SelectedSpanEnd + 1; ++i )
			{
				Script script = SelectedSequence.ListScript[ i ];
				script.Copy ( new Script ( GetCopiedScript () ) );
			}
		}
		//追加してペースト
		public void AddAndPasteScript ()
		{
//			AddScript ( new Script ( GetCopiedScript () ) );
		}

		//挿入してペースト
		public void InsertAndPasteScript ()
		{
//			InsertScript ( new Script ( GetCopiedScript () ) );
		}

		//ブランチ
		public void SetBranch ( Script scp )
		{
			BindingList < Branch > bl_brc = scp.ListBranch;
			foreach ( Script s in SelectedSequence.ListScript )
			{
				s.ListBranch = new BindingList<Branch> ( bl_brc );
			}
		}

#if false

		//---------------------------------------------------------------------
		//	範囲選択
		//---------------------------------------------------------------------
		public struct SELECTED_SCRIPT_SPAN
		{
			public int sequence;
			public int start;		//script start
			public int end;			//script end
		}
		private SELECTED_SCRIPT_SPAN selectedSpan = new SELECTED_SCRIPT_SPAN ();
		public SELECTED_SCRIPT_SPAN SelectedSpan { get { return selectedSpan; } }

		//範囲選択を設定
		//引数
		//	int action : 選択アクション
		//	int start : スクリプト選択開始位置
		//	int end : スクリプト選択終了位置
		public void SetSpan ( int action, int start, int end )
		{
			if ( start > end ) { return; }
			if ( action < 0 || Compend.ListSequence.Count <= action ) { return; }
			Sequence seq = Compend.ListSequence[ selectedScript.sequence ];
			if ( start < 0 || seq.ListScript.Count <= start ) { return; }
			if ( end < 0 || seq.ListScript.Count <= end ) { return; }

			selectedSpan.sequence = action;
			selectedSpan.start = start;
			selectedSpan.end = end;
		}


		//---------------------------------------------------------------------
		//	スクリプト変更後に他スクリプトにコピーするかチェックする
		//---------------------------------------------------------------------
		public void CheckSetOtherScript ()
		{
			//スクリプトの全てに項目全てをコピー
			if ( AllScript )
			{
				CopyToAllScript ();
			}
			//スクリプトの選択範囲に項目全てをコピー
			else if ( SpanScript )
			{
				CopyToSpanScript ();
			}
		}

		//---------------------------------------------------------------------
		//	選択スクリプトに変更項目のみコピー
		//---------------------------------------------------------------------
		//変更されたスクリプト内の項目アドレスと変更値を受け取り、
		//アクション内すべてのスクリプトに変更を伝える
		public void EditSelectedContents ( ScriptAddress scriptAdress, int value )
		{
			if ( null == scriptAdress ) { return; }
			Sequence seq = GetSequence ();
			if ( null == seq ) { return; }
			Script selectedScript = GetScript ();
			if ( null == selectedScript ) { return; }

			//スクリプト選択範囲
			for ( int i = selectedSpan.start; i < selectedSpan.end + 1; ++i )
			{
				//各スクリプトにアドレスと値を渡す
				Script s = seq.ListScript[ i ];
				if ( s == selectedScript ) { continue; }	//コピー元は飛ばす
				s.SetValueInAdress ( scriptAdress, value );
			}
		}

		//シークエンス全てのスクリプトにコピー
		public void CopyToAllScript ()
		{
			Sequence seq = GetSequence ();
			if ( null == seq ) { return; }
			Script selectedScript = GetScript ();
			if ( null == selectedScript ) { return; }

			foreach ( Script s in seq.ListScript )
			{
				if ( s == selectedScript ) { continue; }
				s.Clear ();
				s.Copy ( selectedScript );
			}
		}

		//スクリプトの選択範囲に項目全てをコピー
		public void CopyToSpanScript ()
		{
			Sequence action = GetSequence ();
			if ( null == action ) { return; }
			Script selectedScript = GetScript ();
			if ( null == selectedScript ) { return; }
			EditCompend.SELECTED_SCRIPT_SPAN sss = selectedSpan;

			for ( int i = sss.start; i < sss.end + 1; ++i )
			{
				Script s = action.ListScript[ i ];
				if ( s == selectedScript ) { continue; }
				s.Clear ();
				s.Copy ( selectedScript );
			}
		}

		//---------------------------------------------------------------------
		//スクリプト変更後に他スクリプトにコピーするかチェックする
		public void CheckSetScript ()
		{
			//スクリプトの全てに項目全てをコピー
			if ( AllScript )
			{
				CopyToAllScript ();
			}
			//スクリプトの選択範囲に項目全てをコピー
			else if ( SpanScript )
			{
				CopyToSpanScript ();
			}
		}

		//---------------------------------------------------------------------
		//	エフェクトジェネレート選択位置
		//---------------------------------------------------------------------
		public int selectedIndexEfGnrt = new int ();
#endif
	}

}
