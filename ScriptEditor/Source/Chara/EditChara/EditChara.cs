namespace ScriptEditor
{
	//==================================================================================
	//	キャラの編集
	//		Edit-系のクラスはキャラデータと同様なツリー構造を持ち、
	//		FormやControlから必要な部分だけ参照される
	//		シングルトンとしてグローバルからアクセス可能
	//==================================================================================
	public sealed partial class EditChara
	{
		//シングルトン実体
		public static EditChara Inst { get; } = new EditChara ();

		//編集中のキャラ
		public Chara Chara { get; set; } = null;

		//部分編集
		public EditBehavior EditBehavior { get; set; } = null;
		public EditGarnish EditGarnish { get; set; } = null;

		//---------------------------------------------------------------------
		//プライベートコンストラクタ
		private EditChara ()
		{
			EditBehavior = new EditBehavior ();
			EditGarnish = new EditGarnish ();
		}

		//セット
		public void SetCharaDara ( Chara ch )
		{
			Chara = ch;
			EditBehavior.SetCharaData ( ch );
			EditGarnish.SetCharaData ( ch );
		}

		//クリア
		public void Clear ()
		{
			Chara.Clear ();
		}
		
		//	自分が持つオブジェクトに引数オブジェクトをディープコピーする
		public void Copy ( Chara chara )
		{
			chara.Copy ( chara );
		}
	}


}

