using System;

namespace Vhd
{
    public class DynamicHeader : BaseHeader
    {
        public  const Int32  Size = 1024;

        public DynamicHeader(byte[] buffer)
            : base(buffer)
        {
            if (Raw == null || Raw.Length < Size)
                return;

            DataOffset      = LittleEndianInt64FromRaw();
            TableOffset     = LittleEndianInt64FromRaw();
            HeaderVersion   = LittleEndianInt32FromRaw();
            MaxTableEntries = LittleEndianInt32FromRaw();
            BlockSize       = LittleEndianInt32FromRaw();
            Checksum        = LittleEndianInt32FromRaw();
        }

        public long TableOffset { get; set; }
        public Int32 HeaderVersion { get; set; }
        public Int32 MaxTableEntries { get; set; }
        public Int32 BlockSize { get; set; }

        public override string ToString()
        {
            return String.Format("Cookie = {0}\n" +
                                 "TableOffset = {1}\n" +
                                 "HeaderVersion = 0x{2:X}\n" +
                                 "DataOffset = 0x{3:X}\n" + 
                                 "MaxTableEntries = {4}\n" +
                                 "BlockSize = {5}\n" +
                                 "CheckSum = {6}", Cookie, TableOffset, HeaderVersion, DataOffset, MaxTableEntries, BlockSize, Checksum);
        }
    }
}
