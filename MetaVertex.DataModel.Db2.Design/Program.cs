using System;
using System.Reflection;

namespace MetaVertex.DataModel.Db2.Design
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 3)
                return ShowUsage();

            var connStr = args[1];
            var query = args[2];

            var design = new Db2ModelDesigner(connStr);

            var str = design.GenerateResultModel(query);

            Console.WriteLine(str);

            return 0;
        }

        private static int ShowUsage()
        {
            Console.Error.WriteLine($"Usage: {ExeName} <connString> <query>");
            return 1;
        }

        private static string ExeName => typeof(Program).GetTypeInfo()?.Assembly?.GetName().Name ?? "db2-model-design.exe";
    }
}