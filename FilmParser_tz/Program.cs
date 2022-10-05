using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FilmParser_tz
{
    class Program
    {
        static void Main(string[] args)
        {
            //Type[] Classes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "FilmParser_tz").ToArray();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Data> ObjectList = Generate<Data>.GenObjects();
            string FormatMask = "[Name] {/ [OriginalName]} {([ProgramType]<+>[ProdCountry]<+>[ProdYear])} [Producer] {серия [EpisodeNum]}";
            List<string> Parsed = new MainParser<Data>().FormatList(ObjectList, FormatMask);
            for (int i = 0; i < Parsed.Count; i++)
            {
                Console.WriteLine($"{i}) {Parsed[i]}");
            }
            stopwatch.Stop();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n------------------" +
                              $"\nTotal time of work: {stopwatch.ElapsedMilliseconds/1000} sec");
            Console.ReadLine();
        }
    }
}