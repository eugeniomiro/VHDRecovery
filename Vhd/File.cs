using System;
using System.IO;

namespace Vhd
{
    public class File
    {
        public File(string file)
        {
            _file = file;

            using (var f = System.IO.File.OpenRead(_file)) {
                LoadMainFooter(f);

                if (IsFixedSize)
                    return;

                LoadBackupFooter(f);
                LoadDynamicHeader(f);
                LoadBlockAllocationTable(f);
            }
        }
        
        public void FixFooter()
        {
            // fix footer with backup header
            using(var f = System.IO.File.OpenWrite(_file)) {
                f.Seek(-Footer.Size, SeekOrigin.End);
                f.Write(BackupFooter.Raw, 0, BackupFooter.Raw.Length);
            }
        }

        public bool IsDynamic 
        {
            get { return Footer.DiskType == 3; }
        }

        public bool IsFixedSize
        {
            get { return Footer.DiskType == 2; }
        }

        public  Footer                  Footer                  { get; private set; }
        public  Footer                  BackupFooter            { get; private set; }
        public  DynamicHeader           DynamicHeader           { get; private set; }
        public  BlockAllocationTable    BlockAllocationTable    { get; private set; }

        void LoadBlockAllocationTable(Stream f)
        {
            f.Seek(DynamicHeader.TableOffset, SeekOrigin.Begin);

            var     batSize     = Footer.OriginalSize / DynamicHeader.BlockSize * 4;
            var     buffer      = new Byte[batSize];
            var     bytesToRead = batSize;
            var     chunkSize   = (Int32) (bytesToRead % Int32.MaxValue);
            Int64   bytesRead   = 0;

            while (bytesToRead > chunkSize) {
                bytesRead += f.Read(buffer, 0, chunkSize);
                bytesToRead = (Int32) ((bytesToRead - chunkSize) % Int32.MaxValue);
            }

            bytesRead += f.Read(buffer, 0, chunkSize);

            if (bytesRead < batSize)
                throw new ApplicationException("could not read Block Allocation Table");

            BlockAllocationTable = new BlockAllocationTable(buffer, DynamicHeader.MaxTableEntries, DynamicHeader.BlockSize);
        }

        void LoadDynamicHeader(Stream f)
        {
            var dynamicHeaderBuffer = new byte[DynamicHeader.Size];
            var bytesRead           = f.Read(dynamicHeaderBuffer, 0, DynamicHeader.Size);

            if (bytesRead < DynamicHeader.Size)
                throw new ApplicationException("could not read dynamic header");

            DynamicHeader = new DynamicHeader(dynamicHeaderBuffer);
        }

        // read first copy of the footer at the beggining of the file
        void LoadBackupFooter(Stream f)
        {
            f.Seek(0, SeekOrigin.Begin);
            BackupFooter = Footer.Load(f);
        }

        // go to the end of the file and look for the footer
        void LoadMainFooter(Stream f)
        {
            f.Seek(-Footer.Size, SeekOrigin.End);
            Footer = Footer.Load(f);
        }

        readonly    string          _file;
    }
}
