using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vhd
{
    public class LocationEntry
    {
        public  String  PlatformCode        { get; set; }
        public  int     PlatformDataSpace   { get; set; }
        public  int     PlatformDataLength  { get; set; }
        public  int     Reserved            { get; set; }
        public  long    PlatformDataOffset  { get; set; }

        public override string ToString()
        {
            return String.Format("{{ PlatformCode = '{0}', " +
                                    "PlatformDataSpace = {1}, " +
                                    "PlatformDataLength = {2}, " +
                                    "Reserved = {3}, " +
                                    "PlatformDataOffset = {4} }}", PlatformCode, PlatformDataSpace, PlatformDataLength, Reserved, PlatformDataOffset);
        }
    }
}
