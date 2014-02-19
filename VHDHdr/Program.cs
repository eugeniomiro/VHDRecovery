using System;
using System.Runtime.CompilerServices;
using CommandLine;
using Vhd;

namespace VHDHdr
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new CmdLineParams();

            if (!Parser.Default.ParseArguments(args, options)) {
                Console.WriteLine(options.GetUsage());
                return;
            }

            var file = new File(options.FileName);
            Console.WriteLine("Footer");
            Console.WriteLine();
            Console.WriteLine(file.Footer);

            if (file.IsFixedSize) return;

            Console.WriteLine();
            Console.WriteLine("Backup Footer");
            Console.WriteLine();
            Console.WriteLine(file.BackupFooter);

            Console.WriteLine();
            Console.WriteLine("Dynamic Header");
            Console.WriteLine();
            Console.WriteLine(file.DynamicHeader);

            if (options.FixFooter && file.Footer.IsEmpty) {
                Console.WriteLine("Fixing '{0}' footer", options.FileName);
                file.FixFooter();
            }
        }
    }
}
