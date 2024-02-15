//=================================================================================================
//	プリコンパイル済みヘッダファイル
//=================================================================================================
//
//ライブラリプロジェクトでは用いない
//末端のプロジェクトのみ用いる
//
//プロジェクトのプロパティ
//"構成プロパティ" -> "C/C++" -> ""プリコンパイル済みヘッダーの作成/使用"
//で指定する
//
//C/C++ 
//	詳細設定
//		必ず使用されるインクルードファイルに"stdafx.h"を指定
//
//	ディレクトリ構造に関係なく、
//	#include "stdafx.h"
//	(大文字小文字も関係ない "stdafx.h"でも通る)
//=================================================================================================
#pragma once

//ネットワーク(windows.hより前でインクルード)
#include <winsock2.h>
//Windows
#include <windows.h>

//デバッグ情報　メモリリーク検出
#define _CRTDBG_MAP_ALLOC
#include <crtdbg.h>

#include <time.h>

//STL
#include <vector>
#include <list>
#include <map>
#include <set>

//ゲームライブラリ
#include "Game.h"

