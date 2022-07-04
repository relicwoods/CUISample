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
        /// 
        /// </summary>
        static Logger logger = LogManager.GetCurrentClassLogger();


        static int Count { get; set; }
        static int Max { get; set; }
        static int Min { get; set; }
        static string FileName { get; set; }
        /// <summary>
        /// 引数解析を行うCommandLineUtilsに処理を渡す。
        /// </summary>
        /// <param name="args"></param>
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
                }
                if(Max < Min || (Max == 0 && Min == 0))
                {
                    Max = int.MaxValue;
                    Min = int.MinValue;
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex,"環境変数が適切に設定されていません");
                Count = 10000;
                Max = int.MaxValue;
                Min = int.MinValue;

                //throw;
            }


            //Application code
            var mid = SlowFunction(Count, Min, Max,FileName);
            Console.WriteLine(mid);

        }

        /// <summary>
        /// ランダムな配列を作成してソートしてファイルに書き出す。
        /// </summary>
        /// <param name="count"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="path"></param>
        /// <returns>だいたい真ん中のカズ</returns>
        static int SlowFunction(int count,int min,int max,string path)
        {
            var rand = new Random();
            var list = Enumerable.Range(0, Count)
                                 .Select(x => rand.Next(min, max))
                                 .ToList();
            list.Sort();

            using (var wr = new StreamWriter(path: path,
                                                      append: false,
                                                      encoding: System.Text.Encoding.UTF8))
            {
                foreach (var num in list)
                {
                    wr.WriteLine(num);
                }
            }

            return list[(int)(list.Count/2)];


        }

    }
}




