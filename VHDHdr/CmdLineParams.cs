using System;
using CommandLine;
using CommandLine.Text;

namespace VHDHdr
{
    class CmdLineParams
    {
        [Option('f', "filename", Required = true)]
        public  String  FileName    { get; set; }
        [Option('o', "fixfooter", Required = false)]
        public  bool    FixFooter   { get; set; }
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
