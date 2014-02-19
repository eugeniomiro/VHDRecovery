using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vhd
{
    public class Footer : BaseHeader
    {
        public  const Int32  Size = 512;

        public Footer(byte[] buffer)
            : base(buffer)
        {
            if (Raw == null || Raw.Length < Size)
                return;

            Features            = LittleEndianInt32FromRaw();
            FileFormatVersion   = LittleEndianInt32FromRaw();
            DataOffset          = LittleEndianInt64FromRaw();
            TimeStamp           = OriginTimeStamp + new TimeSpan(0, 0, 0, LittleEndianInt32FromRaw());
            CreatorApplication  = Utf8StringFromRaw(4);
            CreatorVersion      = LittleEndianInt32FromRaw(); 
            CreatorHostOs       = Utf8StringFromRaw(4);
            OriginalSize        = LittleEndianInt64FromRaw(); 
            CurrentSize         = LittleEndianInt64FromRaw(); 
            DiskGeometry        = DiskGeometryFromRaw(); 
            DiskType            = LittleEndianInt32FromRaw(); 
            Checksum            = LittleEndianUInt32FromRaw();
            UniqueId            = GuidFromRaw();
            SavedState          = Raw[FieldsOffset];
        }
        
        public bool IsEmpty 
        {
            get
            {
                return String.IsNullOrWhiteSpace(Cookie) && Features == 0 && FileFormatVersion == 0 && DataOffset == 0 && 
                       TimeStamp == OriginTimeStamp && String.IsNullOrWhiteSpace(CreatorApplication) && CreatorVersion == 0 &&
                       String.IsNullOrWhiteSpace(CreatorHostOs) && OriginalSize == 0 && CurrentSize == 0;
            }
        }

        public Int32 Features { get; set; }
        public Int32 FileFormatVersion { get; set; }
        public DateTime TimeStamp { get; set; }
        public String CreatorApplication { get; set; }
        public Int32 CreatorVersion { get; set; }
        public String CreatorHostOs { get; set; }
        public Int64 OriginalSize { get; set; }
        public Int64 CurrentSize { get; set; }
        public DiskGeometry DiskGeometry { get; set; }
        public Int32 DiskType { get; set; }
        public Guid UniqueId { get; set; }
        public Byte SavedState { get; set; }
        public Byte[] Reserved { get; set; }

        public override string ToString()
        {
            return String.Format("Cookie = {0}\n" +
                                 "Features = 0x{1:X}\n" +
                                 "FileFormatVersion = 0x{2:X}\n" +
                                 "DataOffset = {3}\n" +
                                 "TimeStamp = {4}\n" +
                                 "CreatorApplication = {5}\n" +
                                 "CreatorVersion = 0x{6:X}\n" +
                                 "CreatorHostOs = {7}\n" +
                                 "OriginalSize = {8}\n" +
                                 "CurrentSize = {9}\n" +
                                 "DiskGeometry = {10}\n" +
                                 "DiskType = {11}\n" +
                                 "Checksum = {12}\n" +
                                 "UniqueId = {13}\n" +
                                 "SavedState = {14}",
                Cookie, Features, FileFormatVersion, DataOffset, TimeStamp, CreatorApplication, CreatorVersion, CreatorHostOs,
                OriginalSize, CurrentSize, DiskGeometry, DiskType, Checksum, UniqueId, SavedState);
        }
    }
}
