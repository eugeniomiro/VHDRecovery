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

            DataOffset          = LittleEndianInt64FromRaw();
            TableOffset         = LittleEndianInt64FromRaw();
            HeaderVersion       = LittleEndianInt32FromRaw();
            MaxTableEntries     = LittleEndianUInt32FromRaw();
            BlockSize           = LittleEndianUInt32FromRaw();
            Checksum            = LittleEndianUInt32FromRaw();
            ParentUniqueId      = GuidFromRaw();
            ParentTimeStamp     = OriginTimeStamp + new TimeSpan(0, 0, LittleEndianInt32FromRaw());
            var reserved        = LittleEndianInt32FromRaw();
            ParentUnicodeName   = UnicodeStringFromRaw(512);
            LocationEntries     = new LocationEntry[8];
            for (int i = 0; i < LocationEntries.Length; i++) {                
                LocationEntries[i] = new LocationEntry {
                                                      PlatformCode          = Utf8StringFromRaw(4),
                                                      PlatformDataSpace     = LittleEndianInt32FromRaw(),
                                                      PlatformDataLength    = LittleEndianInt32FromRaw(),
                                                      Reserved              = LittleEndianInt32FromRaw(),
                                                      PlatformDataOffset    = LittleEndianInt64FromRaw()
                                                  };
            }
        }

        public  long            TableOffset     { get; set; }
        public  Int32           HeaderVersion   { get; set; }
        public  UInt32          MaxTableEntries { get; set; }
        public  UInt32          BlockSize       { get; set; }
        public  Guid            ParentUniqueId  { get; set; }
        public  DateTime        ParentTimeStamp { get; set; }
        public  LocationEntry[] LocationEntries { get; set; }

        public override string ToString()
        {
            String result = String.Format("Cookie = {0}\n" +
                                          "TableOffset = {1}\n" +
                                          "HeaderVersion = 0x{2:X}\n" +
                                          "DataOffset = 0x{3:X}\n" + 
                                          "MaxTableEntries = {4}\n" +
                                          "BlockSize = {5}\n" +
                                          "CheckSum = {6}\n" +
                                          "ParentUniqueId = {7}\n" +
                                          "ParentTimeStamp = {8}\n" +
                                          "ParentUnicodeName = {9}", 
                                          Cookie, TableOffset, HeaderVersion, DataOffset, MaxTableEntries, BlockSize, Checksum, 
                                          ParentUniqueId, ParentTimeStamp, ParentUnicodeName);
            for (int i = 0; i < LocationEntries.Length; i++) {
                result += String.Format("\nLocationEntry[{0}] = {1}", i, LocationEntries[i]);
            }
            return result;
        }

        public string ParentUnicodeName { get; set; }
    }
}
