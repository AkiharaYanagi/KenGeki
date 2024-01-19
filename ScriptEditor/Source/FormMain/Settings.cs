using System.IO;
using System.Xml.Serialization;

namespace ScriptEditor
{
	//----------------------------------------------------------
	//	フォームの基本情報を保存するファイル
	//----------------------------------------------------------
	public class Settings
	{
		public string SettingFilename { set; get; } = "setting.xml";

		public string LastDirectory { set; get; } //前回のディレクトリ
		public string LastFilename { set; get; }  = "chara.dat";	//前回のファイル

		public void Save ()
		{
			//クラスをXMLファイルに保存
			XmlSerializer serializer = new XmlSerializer ( typeof ( Settings ) );

			//書出
			FileStream fs = new FileStream ( SettingFilename, FileMode.Create );
			serializer.Serialize ( fs, this );
			fs.Close ();
		}

		public void Load ()
		{
			//ファイルの存在しないとき
			if ( ! File.Exists ( SettingFilename ) )
			{
				//デフォルトファイルの作成
				LastDirectory = Directory.GetCurrentDirectory ();
				Save ();
			}

			//XMLファイルの設定
			XmlSerializer serializer = new XmlSerializer ( typeof ( Settings ) );

			//読込
			FileStream fs = new FileStream ( SettingFilename, FileMode.Open );

			Settings tempSettings = ( Settings ) serializer.Deserialize ( fs );
			this.SettingFilename = tempSettings.SettingFilename;
			this.LastDirectory = tempSettings.LastDirectory;
			this.LastFilename = tempSettings.LastFilename;

			//カレントディレクトリの移動
			Directory.SetCurrentDirectory ( LastDirectory );

			fs.Close ();
		}
	}

}
