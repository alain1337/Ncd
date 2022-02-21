using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncd.Cli
{

    internal class Program
    {
        static int Main(string[] args)
        {
            var ncdProgram = new NcdProgram(args);
            (var returnCode,  var message) =  ncdProgram.Run();
            Console.WriteLine(message);
            return (int) returnCode;
        }
    }
}
