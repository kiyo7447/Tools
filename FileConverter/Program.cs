using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace FileConverter
{
	class Program
	{
		static void Main(string[] args)
		{
			//.NET CoreでShift_JISを使用するために使用
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			var path = @"D:\dev\sample_gomi\20200831_データサイエンス";
			var inlinenum = 0;
			var outlinenum = 0;
			using (var fsr = new FileStream(Path.Combine(path, @"売上_20190918_3.csv"), FileMode.Open))
			using (var swr = new StreamReader(fsr))
			using (var fsw = new FileStream(Path.Combine(path, @"売上_20190918_4.csv"), FileMode.Create))
			using (var sww = new StreamWriter(fsw, Encoding.GetEncoding("Shift_JIS")))
			{
				var l = swr.ReadLine();
				while (l != null)
				{
					inlinenum++;
					if (inlinenum == 0)
					{
						//Header
					}
					else
					{
						if (inlinenum % 10000 == 0)
						{
							//進捗を画面に表示
							Console.WriteLine($"{inlinenum:N0}");
						}

						//エラーチェック処理
						if (l.Split(new char[] { ',' }).Length != 20)
						{
							//エラーは画面出力
							Console.WriteLine($"error lineno={inlinenum:N0}, line={l}");
						}
						else
						{
							sww.WriteLine(l.Replace("\t", " ").Replace(",", "\t"));
							outlinenum++;
						}
					}
					l = swr.ReadLine();
				}
				Console.WriteLine($"In Line Num={inlinenum:N0}, Out Line Num={outlinenum:N0}");
			}

			while (true)
			{
				Console.Write("Press E:");
				var k = Console.ReadKey();
				if (k.Key == ConsoleKey.E) break;
			}
		}
	}
}
