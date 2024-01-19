using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.IO;


namespace ScriptEditor
{

	public class Attribute
	{
		//Element内の属性を表す
		//名前と値を持つ

		private string _name;
		public string Name
		{
			set { this._name = value; }
			get { return this._name; }
		}

		private string _value;
		public string Value
		{
			set { this._value = value; }
			get { return this._value; }
		}

		public Attribute ( string name, string strValue )
		{
			_name = name;
			_value = strValue;
		}

		public void Print ()
		{
			Console.Write ( "\t( " + _name + " = " + _value + " )\n" );
		}
	}


	public class Element
	{
		//AttributeとElementを持つことができる

		private string _name;
		public string Name
		{
			set { this._name = value; }
			get { return this._name; }
		}

		private List<Attribute> attributes = new List<Attribute> ();
		public List<Attribute> Attributes { get { return attributes; } }

		public void AddAttribute ( Attribute attribute )
		{
			attributes.Add ( attribute );
		}

		private List<Element> elements = new List<Element> ();
		public List<Element> Elements { get { return elements; } }


		//コンストラクタ
		public Element () { }
		public Element ( string name )
		{
			_name = name;
		}
		public void AddElement ( Element element )
		{
			elements.Add ( element );
		}

		//親Elementの参照
		private Element _parent;
		public Element Parent
		{
			set { this._parent = value; }
			get { return this._parent; }
		}


		public void Print ()
		{
			Console.Write ( _name + "\n" );
			foreach ( Attribute attribute in attributes )
			{
				attribute.Print ();
			}
			foreach ( Element element in elements )
			{
				element.Print ();
			}
		}
	}


	//------------------------------------------------------------
	//ドキュメント
	//	データのまとまり
	//	木構造をなすエレメントのルートを持つ
	//------------------------------------------------------------
	public class Document
	{
		//木構造の根
		private Element root = new Element ( "root" );
		public Element Root { get { return root; } }

		//読取状態
		enum STATE
		{
			START,
			ELEMENT_START,
			ELEMENT,
			ELEMENT_END,
			ATTRIBUTE_NAME_START,
			ATTRIBUTE_NAME_END,
			ATTRIBUTE_VALUE_START,
			ATTRIBUTE_VALUE,
			ATTRIBUTE_VALUE_END,
			ELEMENT_CLOSING,
		}

		//コンストラクタ
		//引数：テキストファイル名
		public Document ( string fileName )
		{
			//ファイルが存在しないとき
			if ( ! File.Exists ( fileName ) ) { return; }

			//ストリームの作成
			StreamReader streamReader = new StreamReader ( fileName, Encoding.UTF8 );
			DocumentFromStream ( streamReader );
		}

		//コンストラクタ
		//引数：ストリーム
		public Document ( Stream stream )
		{
			StreamReader streamReader = new StreamReader ( stream );
			DocumentFromStream ( streamReader );
		}

		//ストリームから読込
		public void DocumentFromStream ( StreamReader strmRdr )
		{
			string str;
			string strElementName = "";
			string strAttributeName = "";
			string strAttributeValue = "";
			STATE mode = STATE.START;

			Element element = root;
			Element nextElement;

			while ( ( str = strmRdr.ReadLine () ) != null )
			{
				//1文字ずつ読込
				for ( int i = 0; i < str.Length; ++i )
				{
					switch ( mode )
					{
					case STATE.START:
						if ( str[i] == '<' )
						{
//							Console.Write ( "START\n" );
							mode = STATE.ELEMENT_START;
						}
						break;

					case STATE.ELEMENT_START:
						if ( str[i] == '/' )
						{
							element = element.Parent;
//							Console.Write ( "ELEMENT_START if /\n" );
							mode = STATE.ELEMENT_CLOSING;
						}
						else
						{
							//新規生成
							nextElement = new Element ();
							element.AddElement ( nextElement );
							nextElement.Parent = element;

							element = nextElement;

//							Console.Write ( "ELEMENT_START new\n" );
							mode = STATE.ELEMENT;

							//最初の一文字
							strElementName += str[i];
						}
						break;

					case STATE.ELEMENT:
						if ( str[i] == ' ' )
						{
							element.Name = strElementName;
//							Console.Write ( "ELEMENT.Name: " + strElementName + "\n" );
							strElementName = "";

							mode = STATE.ATTRIBUTE_NAME_START;
						}
						else if ( str[i] == '>' )
						{
							//アトリビュートなしでエレメント終了
							mode = STATE.ELEMENT_END;
						}
						else
						{	//通常文字列
							//							Console.Write ( str[i] );
							strElementName += str[i];
						}
						break;

					case STATE.ATTRIBUTE_NAME_START:
						//						Console.Write ( "ATTRIBUTE_NAME_START\n" );
						if ( str[i] == '=' )
						{
							mode = STATE.ATTRIBUTE_VALUE_START;
						}
						else
						{
							strAttributeName += str[i];
						}
						break;

					case STATE.ATTRIBUTE_VALUE_START:
						//						Console.Write ( "ATTRIBUTE_VALUE_START\n" );
						if ( str[i] == '\"' )
						{
							mode = STATE.ATTRIBUTE_VALUE;
						}
						break;

					case STATE.ATTRIBUTE_VALUE:
						//						Console.Write ( "ATTRIBUTE_VALUE\n" );
						if ( str[i] == '\"' )
						{
							mode = STATE.ATTRIBUTE_VALUE_END;
						}
						else
						{
							strAttributeValue += str[i];
						}
						break;


					case STATE.ATTRIBUTE_VALUE_END:
						if ( str[i] == ' ' )
						{
//							Console.Write ( "ATTRIBUTE_VALUE_END new:" + strAttributeName + ", " + strAttributeValue + "\n" );
							//アトリビュートをエレメントに加える
							Attribute attribute = new Attribute ( strAttributeName, strAttributeValue );
							element.AddAttribute ( attribute );
							strAttributeName = "";
							strAttributeValue = "";

							mode = STATE.ATTRIBUTE_NAME_START;
						}
						else if ( str[i] == '>' )
						{
//							Console.Write ( "ATTRIBUTE_VALUE_END new (" + strAttributeName + ", " + strAttributeValue +")\n" );
							//アトリビュートをエレメントに加える
							Attribute attribute = new Attribute ( strAttributeName, strAttributeValue );
							element.AddAttribute ( attribute );
							strAttributeName = "";
							strAttributeValue = "";

							mode = STATE.ELEMENT_END;
						}
						break;

					case STATE.ELEMENT_END:
						if ( str[i] == '<' )
						{
							mode = STATE.ELEMENT_START;
						}
						break;

					case STATE.ELEMENT_CLOSING:
						if ( str[i] == '>' )
						{
							mode = STATE.START;
						}
						break;

					default:
						break;
					}
				}
			}

			strmRdr.Close ();

//			Console.Write ( "\nroot.Print()\n" );
//			root.Print ();
		}

	}

}
