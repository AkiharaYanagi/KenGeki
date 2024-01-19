using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace ScriptEditor
{
	//参照型ジェネリック
	public class Ref < T >
	{ 
		public T t;
		public Ref () {}
		public Ref ( T _t ) { t = _t; }
		public void Set ( T _t ) { t = _t; }
	}

#if false
#endif

	//----------------------------------------------------------------
	//整数の参照型
	public class RefInt
	{
		public int i = 0;

		public RefInt () { i = 0; }
		public RefInt ( int _i ) { i = _i; }

		public void Set ( int _i ) { i = _i; }
	}

	//----------------------------------------------------------------
	//不動小数点の参照型
	public class RefFlaot
	{
		public float f = 0;

		public RefFlaot () { f = 0; }
		public RefFlaot ( float _f ) { f = _f; }

		public void Set ( float _f ) { f = _f; }
	}

	//----------------------------------------------------------------
	//整数の参照型による座標の定義
	public class RefPt
	{
		public RefInt x = new RefInt ( 0 );
		public RefInt y = new RefInt ( 0 );

		public RefPt () { x = new RefInt ( 0 ); y = new RefInt ( 0 ); }
		public RefPt ( int ix, int iy ) { x.i = ix; y.i = iy; }
		public RefPt ( RefInt refx, RefInt refy ) { x = refx; y = refy; }

		//設定(値を代入)
		public void Set ( int ix, int iy ) { x = new RefInt ( ix ); y = new RefInt ( iy ); }
		public void Set ( Point pt ) { x = new RefInt ( pt.X ); y = new RefInt ( pt.Y ); }

		//関連付け(参照を代入)
		public void Associate ( RefPt refPt ) { x = refPt.x; y = refPt.y; }

		//コピー(値を代入)
		public void CopyValue ( RefPt refpt ) { x.i = refpt.x.i; y.i = refpt.y.i; }

		public Point Get () { return new Point ( x.i, y.i ); }
	}

	//----------------------------------------------------------------
	//整数の参照型による矩形の定義
	public class RefRect
	{
		public RefInt x = new RefInt ( 0 );
		public RefInt y = new RefInt ( 0 );
		public RefInt w = new RefInt ( 0 );
		public RefInt h = new RefInt ( 0 );

		public RefRect () { Set ( 0, 0, 0, 0 ); }
		public RefRect ( int ix, int iy, int iw, int ih ) { Set ( ix, iy, iw, ih ); }
		public RefRect ( RefInt refx, RefInt refy, RefInt refw, RefInt refh )
		{
			x = refx; y = refy; w = refw; h = refh;
		}
		public void Set ( int ix, int iy, int iw, int ih )
		{
			RefInt refx = new RefInt ( ix );
			RefInt refy = new RefInt ( iy );
			RefInt refw = new RefInt ( iw );
			RefInt refh = new RefInt ( ih );
			x = refx; y = refy; w = refw; h = refh;
		}
		public void Set ( Rectangle rect )
		{
			Set ( rect.X, rect.Y, rect.Width, rect.Height );
		}

		//関連付け(参照を代入)
		public void Assosiate ( RefRect refRect )
		{
			x = refRect.x; y = refRect.y; w = refRect.w; h = refRect.h; 
		}
	}

	//----------------------------------------------------------------

}
