// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NLog;



namespace CUISample // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        /// <summary>
        /// NLogのロガー
        /// </summary>
        static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 生成する乱数列の数
        /// </summary>
        static int Count { get; set; }
        /// <summary>
        /// 乱数の上限値
        /// </summary>
        static int Max { get; set; }
        /// <summary>
        /// 乱数の下限値
        /// </summary>
        static int Min { get; set; }
        /// <summary>
        /// ファイルの出力先
        /// </summary>
        static string FileName { get; set; } = String.Empty;



        static void Main(string[] args)
    {

            try
            {
                Count= Convert.ToInt32(Environment.GetEnvironmentVariable("LIST_COUNT"));
                Max = Convert.ToInt32(Environment.GetEnvironmentVariable("RANGE_MAX"));
                Min = Convert.ToInt32(Environment.GetEnvironmentVariable("RANGE_MIN"));
                FileName = Environment.GetEnvironmentVariable("FILE_NAME") ?? "test.txt";

                if (Count == 0)
                {
                    logger.Error("環境変数が適切に設定されていません");
                    Count = 100000;

                    //Environment.Exit(-1);
                }
                if (Max < Min || (Max == 0 && Min == 0))
                {
                    Max = int.MaxValue;
                    Min = int.MinValue;

                    //Environment.Exit(-1);
                }
                if (File.Exists(FileName))
                {
                    logger.Error("出力ファイルがすでに存在しています");
                    Environment.Exit(-1);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex,"環境変数が適切に設定されていません");
                Count = 10000;
                Max = int.MaxValue;
                Min = int.MinValue;

                //Environment.Exit(-1);
            }


            //Application code
            var mid = SlowFunction(Count, Min, Max,FileName);
            Console.WriteLine(mid);

            logger.Debug("処理が正常に終了しました");
        }

        /// <summary>
        /// ランダムな配列を作成してソートしてファイルに書き出す。
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="path"></param>
        /// <returns>だいたい真ん中の数</returns>
        static int SlowFunction(int count,int min,int max,string path)
        {
            //ランダムデータのリスト生成
            var rand = new Random();
            var list = Enumerable.Range(0, Count)
                                 .Select(x => rand.Next(min, max))
                                 .ToList();
            //ソート
            list.Sort();

            //ファイルに書き出し
            using (var wr = new StreamWriter(path: path,
                                             append: false,
                                             encoding: System.Text.Encoding.UTF8))
            {
                foreach (var num in list)
                {
                    wr.WriteLine(num);
                }
            }

            //概ね真ん中を返す。
            return list[(int)(list.Count/2)];


        }

    }
}




