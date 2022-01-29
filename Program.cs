using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncd
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("usage: Ncd path [index]");
                return 9;
            }

            var dirs = args[0].Split('\\');
            var dirpaths = new[] { @"C:\Users\abart\OneDrive\Source", @"C:\Users\abart\OneDrive\Source\Tools", @"C:\Users\abart\OneDrive\Source\csmirror" };

            var found = DirectoryFinder.Find(Directory.GetCurrentDirectory(), dirs);
            if (!found.Any())
                found = dirpaths.SelectMany(d => DirectoryFinder.Find(d, dirs)).ToArray();

            if (!found.Any())
            {
                Console.Error.WriteLine("Nothing found");
                return 2;
            }

            if (found.Length == 1)
            {
                Console.WriteLine($"{found.First()}");
                return 0;
            }

            if (args.Length > 1)
            {
                var index = int.Parse(args[1]);
                if (index >= found.Length)
                {
                    Console.Error.WriteLine("Invalid index");
                    return 3;
                }

                Console.WriteLine(found[index]);
                return 0;
            }

            Console.WriteLine("Found:");
            for (var i = 0; i < found.Length; i++)
                    Console.WriteLine($"[{i}] {found[i]}");

            return 1;
        }
    }
}
