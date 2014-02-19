using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace VHDHdr
{
    class CmdLineParams
    {
        [Option('f', "filename", Required = true)]
        public String FileName { get; set; }
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
        [Option('o', "fixfooter", Required = false)]
        public bool FixFooter { get; set; }
    }
}
