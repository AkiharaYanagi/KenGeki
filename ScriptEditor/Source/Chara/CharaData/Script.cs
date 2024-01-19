using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace ScriptEditor
{
	//計算状態
	public enum CLC_ST
	{
		CLC_MAINTAIN,	//持続
		CLC_SUBSTITUDE,	//代入
		CLC_ADD,		//加算
	}

	//================================================================
	//	◆スクリプト		キャラにおけるアクションの１フレームの値
	//		┣フレーム数
	//		┣イメージID (Name)
	//		┣画像表示位置
	//		┣速度
	//		┣加速度
	//		┣計算状態(持続/代入/加算)
	//		┣ブランチ[]
	//		┣接触枠[]
	//		┣攻撃枠[]
	//		┣ヒット枠[]
	//		┣攻撃値
	//		┣エフェクト発生[]
	//
	//================================================================
	//[]指定子でintしか扱えないため
	//配列のインデックスでもuintではなくintにする

	public class Script
	{
		//--------------------------------------------------------------------
		//アクション内スクリプトリストにおける自身のフレーム数(番目)
		public int Frame { get; set; } = 0;

		//編集用グループ値
		public int Group { get; set; } = 0;

		//--------------------------------------------------------------------
		//位置　(参照型：テキストボックスで数値を指定するためintの参照型が必要)
		//--------------------------------------------------------------------

		//キャラ内のイメージリストにおけるインデックス
		public int ImgIndex { get; set; } = 0;
		public string ImgName { get; set; } = "ImgName";

		//--------------------------------------------------------------------
		//位置
		//--------------------------------------------------------------------
		//デリゲートでコントロールから設定する
		public Point Pos { get; set; } = new Point ( 0, 0 );
		public void SetPos ( int x, int y ) { Pos = new Point ( x, y ); }
		public void SetPosX ( int x ) { Pos = new Point ( x, Pos.Y ); }
		public void SetPosY ( int y ) { Pos = new Point ( Pos.X, y ); }

		public Point Vel { get; set; } = new Point ( 0, 0 );
		public void SetVel ( int x, int y ) { Vel = new Point ( x, y ); }
		public void SetVelX ( int x ) { Vel = new Point ( x, Vel.Y ); }
		public void SetVelY ( int y ) { Vel = new Point ( Vel.X, y ); }

		public Point Acc { get; set; } = new Point ( 0, 0 );
		public void SetAcc ( int x, int y ) { Acc = new Point ( x, y ); }
		public void SetAccX ( int x ) { Acc = new Point ( x, Acc.Y ); }
		public void SetAccY ( int y ) { Acc = new Point ( Acc.X, y ); }

		//計算状態(加算/代入/持続)
		public CLC_ST CalcState { get; set; } = new CLC_ST ();

		//------------------------------------------------
		//分岐
		//------------------------------------------------
		//ブランチリスト
		public BindingList<Branch> ListBranch { get; set; } = new BindingList<Branch> ();

		//------------------------------------------------
		//枠
		//------------------------------------------------
		//接触枠(Collision), 攻撃枠(Attack), 当り枠(Hit), 相殺枠(Offset)
		public List<Rectangle> ListCRect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListHRect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListARect { get; set; } = new List<Rectangle> ();
		public List<Rectangle> ListORect { get; set; } = new List<Rectangle> ();

		//------------------------------------------------
		//値
		//------------------------------------------------
		//攻撃値
		public int Power { get; set; } = 0;

		//------------------------------------------------
		//エフェクト生成
		//------------------------------------------------
		public BindingList<EffectGenerate> ListGenerateEf { get; set; } = new BindingList<EffectGenerate> ();


		//================================================================
		//コンストラクタ
		public Script ()
		{
			CalcState = CLC_ST.CLC_MAINTAIN;
		}

		//コピーコンストラクタ
		public Script ( Script s )
		{
			this.Frame = s.Frame;
			this.ImgIndex = s.ImgIndex;
			this.ImgName = s.ImgName;
			this.Pos = s.Pos;
			this.Vel = s.Vel;
			this.Acc = s.Acc;
			this.CalcState = s.CalcState;
			ListBranch = new BindingList < Branch > ( s.ListBranch );
			ListCRect = new List < Rectangle > ( s.ListCRect );
			ListHRect = new List < Rectangle > ( s.ListHRect );
			ListARect = new List < Rectangle > ( s.ListARect );
			ListORect = new List < Rectangle > ( s.ListORect );
			this.Power = s.Power;
			ListGenerateEf = new BindingList < EffectGenerate > ( s.ListGenerateEf );
		}

		//初期化
		public void Clear ()
		{
			Frame = 0;
			ImgIndex = 0;
			ImgName = "Clear";
			CalcState = CLC_ST.CLC_MAINTAIN;
			ListBranch.Clear ();
			ListCRect.Clear ();
			ListARect.Clear ();
			ListHRect.Clear ();
			ListORect.Clear ();
			Power = 0;
			ListGenerateEf.Clear ();
		}

		//コピー
		public void Copy ( Script s )
		{
			this.Frame = s.Frame;
			this.ImgIndex = s.ImgIndex;
			this.ImgName = s.ImgName;
			this.Pos = s.Pos;
			this.Vel = s.Vel;
			this.Acc = s.Acc;
			this.CalcState = s.CalcState;
			ListBranch = new BindingList < Branch > ( s.ListBranch );
			ListCRect = new List < Rectangle > ( s.ListCRect );
			ListHRect = new List < Rectangle > ( s.ListHRect );
			ListARect = new List < Rectangle > ( s.ListARect );
			ListORect = new List < Rectangle > ( s.ListORect );
			this.Power = s.Power;
			ListGenerateEf = new BindingList < EffectGenerate > ( s.ListGenerateEf );
		}

	}

}
