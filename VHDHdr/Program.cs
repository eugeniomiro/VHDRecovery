using System;
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
            WriteTitle("Footer");
            Console.WriteLine(file.Footer);

            if (file.IsFixedSize) return;

            WriteLineTitle("Backup Footer");
            Console.WriteLine(file.BackupFooter);

            WriteLineTitle("Dynamic Header");
            Console.WriteLine(file.DynamicHeader);

            if (options.FixFooter && file.Footer.IsEmpty) {
                Console.WriteLine("Fixing '{0}' footer", options.FileName);
                file.FixFooter();
            }
        }

        static void WriteLineTitle(String title)
        {
            Console.WriteLine();
            WriteTitle(title);
        }
        static void WriteTitle(String title)
        {
            Console.WriteLine(title);
            Console.WriteLine(new String('-', title.Length));
        }
    }
}
