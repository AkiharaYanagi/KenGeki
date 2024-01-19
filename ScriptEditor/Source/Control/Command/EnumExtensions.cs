using System;

namespace ScriptEditor
{
	//===========================================================================
	//Enum拡張メソッド
	public static class EnumExtensions
	{
		//次のEnumを返す (ジェネリック型制約で受ける)
		public static T Next < T > ( this T src ) where T : struct
		{
			if ( ! typeof ( T ).IsEnum ) { throw new ArgumentException ( "Argument {0} is not Enum", typeof(T).FullName ); }

			T[] ary = (T[])Enum.GetValues ( src.GetType () );
			int j = Array.IndexOf ( ary, src ) + 1;
			return ( ary.Length == j ) ? ary[0] : ary[j]; 
		}

		//前のEnumを返す (ジェネリック型制約で受ける)
		public static T Prev < T > ( this T src ) where T : struct
		{
			if ( ! typeof ( T ).IsEnum ) { throw new ArgumentException ( "Argument {0} is not Enum", typeof(T).FullName ); }

			T[] ary = (T[])Enum.GetValues ( src.GetType () );
			int j = Array.IndexOf ( ary, src ) - 1;
			return ( -1 == j ) ? ary[0] : ary[j]; 
		}
	}
	//===========================================================================

}
