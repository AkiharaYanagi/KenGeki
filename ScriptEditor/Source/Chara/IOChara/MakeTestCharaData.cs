using System.Drawing;


namespace ScriptEditor
{
	public partial class TestCharaData
	{
		public void SetCharaData ( Chara chara )
		{
		}

		private Action NewAction ( Chara chara, string name, int nextIndex, int category, int posture, int balance )
		{
			Action action = new Action
			{
				Name = name,
				NextIndex = nextIndex,
				Category = ( ActionCategory ) category,
				Posture = ( ActionPosture ) posture,
				_Balance = balance,
			};
			return action;
		}

		private Script NewScript ( Action action, int imgIndex, Point pt, Point vel, Point acc, int power )
		{
			Script script = new Script
			{
				ImgIndex = imgIndex,
				Pos = pt,
				Vel = vel,
				Acc = acc,
				Power = power,
			};
			return script;
		}
	}
}