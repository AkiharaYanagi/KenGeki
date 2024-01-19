using System;
using System.Drawing;
using System.Collections.Generic;

namespace ScriptEditor
{
	using PrmInt = ScriptParam < int >;
	using PrmPoint = ScriptParam < Point >;
	using LsRect = List < Rectangle >;
	using GS_LsRect = System.Action < Script, List < Rectangle > >;


	//---------------------------------------------------------------------
	//	Setter, Getter
	//---------------------------------------------------------------------
	//スクリプトにおけるパラメータに対し、型に応じたセッタとゲッタを保持するクラス
	public class ScriptParam < T >
	{
		public Action < Script, T > Setter { get; set; } = null;
		public Func < Script, T > Getter { get; set; } = null;

		public ScriptParam ( Action < Script, T > setter, Func < Script, T > getter )
		{
			Setter = setter;
			Getter = getter;
		}
	}

	//スクリプトの構造を以て、上記クラスをまとめて持つ
	//コンストラクタで各パラメータの設定用デリゲートをラムダ式で初期化する
	public class ScriptSetter
	{
		public PrmInt imageIndex = new PrmInt ( (s, i)=> s.ImgIndex = i, s=> s.ImgIndex );

		public PrmInt pos_x = new PrmInt ( (s, i)=> s.SetPosX ( i ), s=>s.Pos.X );
		public PrmInt pos_y = new PrmInt ( (s, i)=> s.SetPosY ( i ), s=>s.Pos.Y );
		public PrmPoint pos = new PrmPoint ( (s, pt)=> s.Pos = pt, s=>s.Pos );

		public PrmInt vel_x = new PrmInt ( (s, i)=> s.SetVelX ( i ), s=>s.Vel.X );
		public PrmInt vel_y = new PrmInt ( (s, i)=> s.SetVelY ( i ), s=>s.Vel.Y );
		public PrmPoint vel = new PrmPoint ( (s, pt)=> s.Vel = pt, s=>s.Vel );

		public PrmInt acc_x = new PrmInt ( (s, i)=> s.SetAccX ( i ), s=>s.Acc.X );
		public PrmInt acc_y = new PrmInt ( (s, i)=> s.SetAccY ( i ), s=>s.Acc.Y );
		public PrmPoint acc = new PrmPoint ( (s, pt)=> s.Acc = pt, s=>s.Acc );

		public PrmInt power = new PrmInt ( (s, i)=> s.Power = i, s=>s.Power );

		//-------------------------------------------------------------------------------
		//枠リストのグループへの変更はリストのコピーを行う
		public GS_LsRect GroupSettterCRect = (s,l)=>s.ListCRect = new LsRect ( l );
		public GS_LsRect GroupSettterHRect = (s,l)=>s.ListHRect = new LsRect ( l );
		public GS_LsRect GroupSettterARect = (s,l)=>s.ListARect = new LsRect ( l );
		public GS_LsRect GroupSettterORect = (s,l)=>s.ListORect = new LsRect ( l );
	}

	//---------------------------------------------------------------------
	// スクリプトの編集をする
	//---------------------------------------------------------------------
	public partial class EditScript
	{
		//Setter, Getter
		public ScriptSetter ScpSetter { get; set; } = new ScriptSetter ();

		//選択中グループに対して、グループセッタを用いて値を設定する
		private void DoGroupSetter ( System.Action < Script, int > groupSetter, int v )
		{
			foreach ( Script s in SelectedGroup ) { groupSetter ( s, v ); }
		}

		//汎用
		public void DoGroupSetterT < T > ( System.Action < Script, T > groupSetter, T t )
		{
			foreach ( Script s in SelectedGroup ) { groupSetter ( s, t ); }
		}

		//各グループセッタの呼出
		public void GroupSetterImageIndex ( int v ) { DoGroupSetter ( ScpSetter.imageIndex.Setter, v ); }
		public void GroupSetterPosX ( int v ) { DoGroupSetter ( ScpSetter.pos_x.Setter, v ); }
		public void GroupSetterPosY ( int v ) { DoGroupSetter ( ScpSetter.pos_y.Setter, v ); }
		public void GroupSetterVelX ( int v ) { DoGroupSetter ( ScpSetter.vel_x.Setter, v ); }
		public void GroupSetterVelY ( int v ) { DoGroupSetter ( ScpSetter.vel_y.Setter, v ); }
		public void GroupSetterAccX ( int v ) { DoGroupSetter ( ScpSetter.acc_x.Setter, v ); }
		public void GroupSetterAccY ( int v ) { DoGroupSetter ( ScpSetter.acc_y.Setter, v ); }
		public void GroupSetterPower ( int v ) { DoGroupSetter ( ScpSetter.power.Setter, v ); }

		public void GroupSetterCRect ( LsRect l ) { DoGroupSetterT ( ScpSetter.GroupSettterCRect, l ); }
		public void GroupSetterHRect ( LsRect l ) { DoGroupSetterT ( ScpSetter.GroupSettterHRect, l ); }
		public void GroupSetterARect ( LsRect l ) { DoGroupSetterT ( ScpSetter.GroupSettterARect, l ); }
		public void GroupSetterORect ( LsRect l ) { DoGroupSetterT ( ScpSetter.GroupSettterORect, l ); }
	}

}
