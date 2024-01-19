using System.Drawing;

namespace ScriptEditor
{
	//---------------------------------------------------------------------
	//	スクリプト内でエフェクトを生成するクラス
	//---------------------------------------------------------------------
	
	public class EffectGenerate
	{
		public string Name { get; set; } = "EfGnrt";    //表示用エフェクト名
#if false
		public RefInt id { get; set; }		//エフェクトID
		public RefPt ptGnrt { get; set; }	//生成位置
		public RefFlaot z { get; set; }		//グラフィック描画Z位置
#endif
		public int Id { get; set; } = 0;
		public Point Pt { get; set; } = new Point ( 0, 0 );
		public int Z = 50;	//100分の１でfloat

		public bool Gnrt { get; set; } = false;		//生成(または非生成で繰返)
		public bool Loop { get; set; } = false;		//ループ
		public bool Sync { get; set; } = false;		//位置同期

		public EffectGenerate ()
		{
		}

	}
}
