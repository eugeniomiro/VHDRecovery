using System;
using CommandLine;
using Vhd;

namespace VHDHdr
{
    using Annotations;

    [UsedImplicitly]
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

            WriteLineTitle("Total Size");
            Console.WriteLine(file.BlockAllocationTable.TotalSize);
            
            if (options.FixFooter && file.Footer.IsEmpty) {
                Console.WriteLine("Fixing '{0}' footer", options.FileName);
                file.FixFooter();
            }

            // write raw file? then write raw data!
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
