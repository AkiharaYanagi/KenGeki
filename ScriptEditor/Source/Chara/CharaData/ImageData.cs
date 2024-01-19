using System.Drawing;

namespace ScriptEditor
{
	//イメージと名前を持つ
	public class ImageData
	{
		//イメージ
		public Image Img { set; get; }

		//ID(イメージ管理リストにおけるインデックス)
//		public int Index { get; set; } = 0;

		//名前
		public string Name { set; get; }

		//引数付コンストラクタ
//		public ImageData ( string name, Image img, int index )
		public ImageData ( string name, Image img )
		{
			Name = name;
			Img = img;
//			Index = index;
		}
		public ImageData ( ImageData imageData )
		{
			this.Img = imageData.Img;
			this.Name = imageData.Name;
//			this.Index = imageData.Index;
		}

		//文字列変換
		public override string ToString ()
		{
			return Name;
		}
	}
	

}
