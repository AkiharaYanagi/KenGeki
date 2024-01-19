using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScriptEditor
{
	//------------------------------------------------------------
	// 枠を編集するコントロール
	//------------------------------------------------------------

	//増加ボタン
	public class BTN_ListRect_Add : Button
	{
		//対象枠
		private List<RefRect> listRefRect = null;
		public List<RefRect> ListRefRect { set { listRefRect = value; } }

		//増減のNUDにも変更が及ぶ
		public NumericUpDown nudRect { get; set; }

		//選択枠セットが変更される
//		public DispListRefRect dispListRefRect { get; set; }

		//フォームから描画を更新する
//		public FormMain formMain { get; set; }
		public DispCompend dispCompend { get; set; }

		//[イベント] クリック時
		protected override void OnClick ( EventArgs e )
		{
			if ( null != listRefRect )
			{
				//空白を追加
				listRefRect.Add ( new RefRect () );

				//nudの最大値を更新
				nudRect.Maximum = listRefRect.Count - 1;
				nudRect.Value = nudRect.Maximum;
				dispListRefRect.On ();

				int index = Decimal.ToInt32 ( nudRect.Value );
				dispListRefRect.AssociateRefRect ( listRefRect[ index ] );
				dispListRefRect.Invalidate ();
			}

			//表示の更新
			dispCompend.Disp ();

			base.OnClick ( e );
		}
	}

	//削除ボタン
	public class BTN_ListRect_Rem : Button
	{
		private List<RefRect> listRefRect = null;
		public List<RefRect> ListRefRect { set { listRefRect = value; } }

		//増減のNUDにも変更が及ぶ
		public NumericUpDown nudRect { get; set; }

		//選択枠セットが変更される
		public DispListRefRect rrs { get; set; }

		//フォームから描画を更新する
//		public FormMain formMain { get; set; }
		public DispCompend dispCompend { get; set; }

		//[イベント] クリック時
		protected override void OnClick ( EventArgs e )
		{
			if ( null == listRefRect ) { base.OnClick ( e ); return; }
			if ( 0 == listRefRect.Count ) { base.OnClick ( e ); return; }

			//末尾を削除
			listRefRect.Remove ( listRefRect[ listRefRect.Count - 1 ] );

			if ( 0 < listRefRect.Count )
			{
				//nudの最大値を更新
				nudRect.Maximum = listRefRect.Count - 1;
				rrs.On ();

				int index = Decimal.ToInt32 ( nudRect.Value );
				rrs.AssociateRefRect ( listRefRect[ index ] );
				rrs.Invalidate ();
			}
			else
			{
				rrs.Off ();
				this.Enabled = false;
			}

			//表示の更新
//			formMain.Disp ();
			dispCompend.Disp ();

			base.OnClick ( e );
		}
	}

	//NumericUpDown インデックスを指定する
	public class NUD_ListRect : NumericUpDown
	{
		private List<RefRect> listRefRect = null;
		public List<RefRect> ListRefRect { set { listRefRect = value; } }

		//変更を伝える
		public DispListRefRect dispListRefRect { get; set; }

		//フォームから描画を更新する
//		public FormMain formMain { get; set; }
		public DispCompend dispCompend { get; set; }

		//[イベント] 数値を変更したとき
		protected override void OnValueChanged ( EventArgs e )
		{
			if ( null == listRefRect ) { return; }
			if ( 0 == listRefRect.Count ) { return; }
			if ( null == dispListRefRect ) { return; }

			//指定した数値をintに直す
			int index = Decimal.ToInt32 ( this.Value );

			//関連付け
			dispListRefRect.AssociateRefRect ( listRefRect[ index ] );
	
			//コントロールの再描画
			dispListRefRect.Invalidate ();

			//スクリプトの再描画
//			formMain.Disp ();
			dispCompend.Disp ();
			
			base.OnValueChanged ( e );
		}

		//選択中の枠を返す(個数が０のときはnull)
		public RefRect GetSelectedRefRect ()
		{
			if ( listRefRect.Count == 0 ) { return null; }
			return listRefRect[ Decimal.ToInt32 ( this.Value ) ];
		}
	}

}
