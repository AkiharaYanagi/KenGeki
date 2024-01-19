using System;

namespace ScriptEditor
{
	//------------------------------------------------------------
	//	RefPtに関連するコントロールをまとめて、関連付けと表示を扱うクラス
	//------------------------------------------------------------
	public class DispRefPt
	{
		//コントロール
		public TB_Number tbaPtX { get; set; }
		public TB_Number tbaPtY { get; set; }

		//起動時
		public void Load (
			EditCompend ec, DispCompend dc,
			TB_Number _tbaPtX, TB_Number _tbaPtY,
			ScriptAddress scriptAddressPtX,
			ScriptAddress scriptAddressPtY
		)
		{
			tbaPtX = _tbaPtX;
			tbaPtX.Load ( ec, dc );
			tbaPtX.scriptAddress = scriptAddressPtX;

			tbaPtY = _tbaPtY;
			tbaPtY.Load ( ec, dc );
			tbaPtY.scriptAddress = scriptAddressPtY;
		}

		//関連付け
		public void Associate ( RefPt refPt )
		{
			tbaPtX.refInt = refPt.x;
			tbaPtY.refInt = refPt.y;
		}

		//表示
		public void Disp ( RefPt refPt )
		{
			tbaPtX.Text = refPt.x.i.ToString ();
			tbaPtY.Text = refPt.y.i.ToString ();
		}

		//消去
		public void Clear ()
		{
			tbaPtX.refInt.i = 0;
			tbaPtY.refInt.i = 0;
			tbaPtX.Text = "";
			tbaPtY.Text = "";
		}
	}
}
