using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;


namespace ScriptEditor
{
	public partial class FormMain : Form
	{
		//==================================================================================
		//	タブ[ｷｬﾗ]
		//==================================================================================

		//選択用アクションリストボックス
		private CB_ActionList[] listCbAction = new CB_ActionList[ Enum.GetNames( typeof ( Chara.BasicAction ) ).Length ];
		
		//初期化
		private void tabChara_Load ()
		{
			//各基本状態アクションの初期化
			listCbAction[ 0 ] = cba_Stand;
			listCbAction[ 1 ] = cba_Poised;
			listCbAction[ 2 ] = cba_Clang;
			listCbAction[ 3 ] = cba_Avoid;
			listCbAction[ 4 ] = cba_Dotty;
			listCbAction[ 5 ] = cba_Damaged;
			listCbAction[ 6 ] = cba_Down;
			listCbAction[ 7 ] = cba_Win;

			int index = 0;
			foreach ( CB_ActionList cba in listCbAction )
			{
				cba.Associate ( editChara.chara.bsAction[ index++ ] );
			}
		}

		//選択時
		private void tabChara_Selected ()
		{
			tabChara_Load ();

			foreach ( CB_ActionList cba in listCbAction )
			{
				cba.Init ( editChara );
			}
		}

	}
}
