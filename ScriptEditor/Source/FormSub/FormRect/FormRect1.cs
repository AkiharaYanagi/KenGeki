using System.Windows.Forms;
using System.Drawing;

namespace ScriptEditor
{
#if true
	public sealed partial class FormRect : EditorForm
	{
		//---------------------------------------------------------------------
		//シングルトン実体
		public static FormRect Inst { get; set; } = new FormRect ();
		
		//プライベートコンストラクタ
		private FormRect ()
		{
			base.InitPt = new Point ( 0, 330 );

			InitializeComponent ();
			base.LoadObject ();

			ctrl_ListCRect.SetName ( "接触枠" );
			ctrl_ListHRect.SetName ( "当り枠" );
			ctrl_ListARect.SetName ( "攻撃枠" );
			ctrl_ListORect.SetName ( "相殺枠" );
		}
		
		//---------------------------------------------------------------------

		//コントロールの設定
		public void SetCtrl ( EditScript es )
		{
			ctrl_ListCRect.EditScript = es;
			ctrl_ListHRect.EditScript = es;
			ctrl_ListARect.EditScript = es;
			ctrl_ListORect.EditScript = es;
#if false
			ctrl_ListCRect.GroupSetter = es.GroupSetterCRect;
			ctrl_ListHRect.GroupSetter = es.GroupSetterHRect;
			ctrl_ListARect.GroupSetter = es.GroupSetterARect;
			ctrl_ListORect.GroupSetter = es.GroupSetterORect;
#endif
		}

		//関連付け
		public void Assosiate ( Script scp )
		{
			ctrl_ListCRect.Assosiate ( scp.ListCRect );
			ctrl_ListHRect.Assosiate ( scp.ListHRect );
			ctrl_ListARect.Assosiate ( scp.ListARect );
			ctrl_ListORect.Assosiate ( scp.ListORect );
		}

		//更新
		public void UpdateData ()
		{
			ctrl_ListCRect.UpdateData ();
			ctrl_ListHRect.UpdateData ();
			ctrl_ListARect.UpdateData ();
			ctrl_ListORect.UpdateData ();
		}

		//対象データの設定
		public void Set ( Script scp )
		{
			ctrl_ListCRect.SetCount ( scp.ListCRect.Count );
			ctrl_ListHRect.SetCount ( scp.ListHRect.Count );
			ctrl_ListARect.SetCount ( scp.ListARect.Count );
			ctrl_ListORect.SetCount ( scp.ListORect.Count );
		}

	}
#endif
}
