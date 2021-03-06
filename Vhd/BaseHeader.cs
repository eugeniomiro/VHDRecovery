﻿using System;
using System.Linq;
using System.Text;

namespace Vhd
{
    public class BaseHeader
    {
        public  String      Cookie      { get; set; }
        public  Int64       DataOffset  { get; set; }
        public  UInt32      Checksum    { get; set; }

        static  readonly
        public  DateTime    OriginTimeStamp = new DateTime(2000, 1, 1);

        readonly
        public  byte[]      Raw;

        protected BaseHeader(byte[] raw)
        {
            if (raw == null)
                return;

            Raw = new Byte[raw.Length];
            raw.CopyTo(Raw, 0);
            FieldsOffset = 0;

            Cookie = Utf8StringFromRaw(8);
        }
        protected string Utf8StringFromRaw(int size)
        {
            var result = Encoding.UTF8.GetString(Raw, FieldsOffset, size).Trim('\0');

            FieldsOffset += size;
            return result;
        }
        protected String UnicodeStringFromRaw(int size)
        {
            var result = Encoding.Unicode.GetString(Raw, FieldsOffset, size).Trim('\0');

            FieldsOffset += size;
            return result;
        }
        protected UInt32 LittleEndianUInt32FromRaw()
        {
            var result = BitConverter.ToUInt32(Raw.Skip(FieldsOffset).Take(sizeof(UInt32)).Reverse().ToArray(), 0); // bigendian
            FieldsOffset += sizeof(UInt32);
            return result;
        }

        protected Int32 LittleEndianInt32FromRaw()
        {
            var result = BitConverter.ToInt32(Raw.Skip(FieldsOffset).Take(sizeof(Int32)).Reverse().ToArray(), 0);   // bigendian
            FieldsOffset += sizeof(Int32);
            return result;
        }
        protected Int64 LittleEndianInt64FromRaw()
        {
            var result = BitConverter.ToInt64(Raw.Skip(FieldsOffset).Take(sizeof(Int64)).Reverse().ToArray(), 0);   // bigendian
            FieldsOffset += sizeof(Int64);
            return result;
        }
        protected DiskGeometry DiskGeometryFromRaw()
        {
            var result = new DiskGeometry(BitConverter.ToUInt16(Raw, FieldsOffset), Raw[2], Raw[3]);
            FieldsOffset += sizeof(Int16) + 2 * sizeof(Byte);
            return result;
        }
        protected Guid GuidFromRaw()
        {
            var result = new Guid(Raw.Take(16).ToArray());
            FieldsOffset += 16;
            return result;
        }

        protected               Int32   FieldsOffset;
    }
}
