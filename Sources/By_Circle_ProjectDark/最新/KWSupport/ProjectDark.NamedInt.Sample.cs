//ProjectDark.NamedInt.Sample :: (C)2014 Project Dark* / Hikaru* All Rights Reserved.

using System;

//Start :: NamedIntの利用宣言(using)

//★以下のusingを使用するファイルすべてに書く必要があります。★★また、ファイル同士で内容が変わらないようにしてください！★★
//using 型名 = ProjectDark.NamedInt.NamedInt0～9;
using Square = ProjectDark.NamedInt.NamedInt0; //マス目
using Piece = ProjectDark.NamedInt.NamedInt1; //コマ
//以上

//★StrictNamedIntXと、NamedIntXは別物です！！
using StrictPiece = ProjectDark.NamedInt.StrictNamedInt1; //コマ（暗黙int変換なし）
using StrictMeg = ProjectDark.NamedString.StrictNamedString0;
//End :: NamedIntの利用宣言(using)

using ProjectDark.KWSupport;

namespace ProjectDark.NamedInt {
	public class Sample {
		public static void Main() {


			/* TimeoutReader */
			Console.WriteLine("Please input: ");
			string STR = TimeoutReader.ReadLine(5000);
			Console.WriteLine("Time over!: {0}", STR);


			/* NamedInt の使い方 */

			//もちろん、普通のintとして使えます。
			Square S = 1234;
			Piece P = 5678;

			P++; //++, --に対応しました。

			Console.WriteLine("S = {0}, P = {1}", S, P);

			//ifだって大丈夫です
			if (S == 1234) {
				Console.WriteLine("Sは1234です！");
			}

			//でも別物同士の代入はできません… ★下の行のコメントを外してみてください★
			//S = P; 



			/* StrictNamedInt の使い方 */
			//暗黙の型変換が行われない場合以外は、NamedIntと同じですが…

			P = 2; //前項より使いまわしです、普通のNamedIntです。
			StrictPiece PS = 2; //これが "Strict"なNamedIntです。

			//ただの実験用。
			int[] test = new int[16];

			//こちらは暗黙でintになりますが…
			test[P] = 1234;

			//Strict版は暗黙変換されません）★下の行のコメントを外してみてください★
			//		test[PS] = 1234;

			//もちろん、明示型変換はOKです。
			test[(int)PS] = 5678;



			/* StrictNamedInt と純粋intのみを受け入れる配列 */

			//ここでは、int[]の代わりを作成します。
			//宣言: StrictIndexerArray<配列の型, 受け入れるStrictNamedInt> = new StrictIndexerArray<配列の型>(サイズ, 受け入れるStrictNamedInt);
			
			ProjectDark.NamedInt.StrictIndexerArray<int, StrictPiece> SIA = new ProjectDark.NamedInt.StrictIndexerArray<int, StrictPiece>(16);　//要素数16のint配列
			SIA[PS] = 5678; //キャストすることなく、配列添え字にできます。
			SIA[0] = 1234; //純粋intもいけます。

			//SIA[P} = 9012; //指定以外のStrictNamedはもちろん、NamedIntもだめです。★コメントを外してみてください★
			//ただし、コンパイルエラーが「型の違いを表すもの」ではなく「文法が間違っているもの」として表示されてしまうので分かりにくいいです。

			//Lengthや、
			System.Console.WriteLine("配列の長さ: {0}", SIA.Length);

			//foreachもそのまま使えます
			System.Console.Write("配列の中身: ");
			foreach (int Nakami in SIA) {
				System.Console.Write("{0}, ", Nakami);
			}
			System.Console.WriteLine("以上。");

			//Sort (IComparable)や、IConvertibleも利用できます。
			System.Collections.Generic.List<StrictPiece> keyList = new System.Collections.Generic.List<StrictPiece>();
			keyList.Add(123);
			keyList.Add(456);
			keyList.Sort();　//一応ソートしとく？

			//関数の引数が純粋なArrayしか受け付けない場合はキャストしてしまえばOKです。
			Array A = (Array)SIA;


			//ハッシュテーブルのキーとして使うテスト
			System.Collections.Hashtable H = new System.Collections.Hashtable();
			H.Add((StrictPiece)1, 1);
			H.Add(1, 1);
			Console.WriteLine("Hashtable: {0}", H.ContainsKey((StrictPiece)1));
			Console.WriteLine("Hashtable: {0}", H.ContainsKey(1));

			//Generics版Dicrionaryで使ってみる
			var Dict = new System.Collections.Generic.Dictionary<StrictPiece, StrictPiece>();
			Dict.Add((StrictPiece)1, (StrictPiece)111);
			Dict.Add((StrictPiece)2, (StrictPiece)12);
			Console.WriteLine("Dictionary: {0}", Dict.ContainsKey( (StrictPiece)1) );

			
			//intとのif()
			StrictPiece IFTEST_SI = 1234;
			int IFTEST_INT = 1234;

			if ((int)IFTEST_SI == IFTEST_INT) {
				Console.WriteLine("IfTest: True");
			} else {
				Console.WriteLine("IfTest: False");
			}
			if ((int)IFTEST_SI != IFTEST_INT) {
				Console.WriteLine("IfTest: True");
			} else {
				Console.WriteLine("IfTest: False");
			}

			if (IFTEST_SI == (StrictPiece)IFTEST_INT) {
				Console.WriteLine("IfTest2: True");
			} else {
				Console.WriteLine("IfTest2: False");
			}
			if (IFTEST_SI != (StrictPiece)IFTEST_INT) {
				Console.WriteLine("IfTest2: True");
			} else {
				Console.WriteLine("IfTest2: False");
			}


			//StrictNamedString (非サポート)
			StrictMeg M = "テスト文字列";
			Console.WriteLine(M);

	//		Console.WriteLine("{0}", X.Equals(X));

#if BUILD_VS
			System.Console.WriteLine("*: Press Enter to exit.");
			System.Console.ReadLine(); //VSビルド時、Enter待ちさせる（簡易）
#endif

		}
	}
}