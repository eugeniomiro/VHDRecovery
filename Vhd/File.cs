using System;
using System.IO;

namespace Vhd
{
    public class File
    {
        private readonly    string          _file;
        private readonly    Footer          _footer;
        private readonly    Footer          _backupFooter;
        private readonly    DynamicHeader   _dinamicHeader;

        public File(string file)
        {
            _file = file;

            using (var f = System.IO.File.OpenRead(_file)) {
                // go to the end of the file and look for the footer
                f.Seek(-Footer.Size, SeekOrigin.End);

                var footerBuffer    = new byte[Footer.Size];
                var bytesRead       = f.Read(footerBuffer, 0, footerBuffer.Length);

                if (bytesRead < Footer.Size)
                    throw new ApplicationException("could not read footer");

                // read footer content
                _footer = new Footer(footerBuffer);

                if (IsFixedSize)
                    return;

                // read first copy of the footer at the beggining of the file
                f.Seek(0, SeekOrigin.Begin);
                bytesRead = f.Read(footerBuffer, 0, footerBuffer.Length);

                if (bytesRead < Footer.Size)
                    throw new ApplicationException("could not read footer backup");

                // read the backupFooter
                _backupFooter = new Footer(footerBuffer);
                var dynamicHeaderBuffer = new byte[DynamicHeader.Size];

                // read the dynamic header
                bytesRead = f.Read(dynamicHeaderBuffer, 0, DynamicHeader.Size);
                if (bytesRead < DynamicHeader.Size)
                    throw new ApplicationException("could not read dynamic header");

                _dinamicHeader = new DynamicHeader(dynamicHeaderBuffer);
            }
        }

        public  Footer          Footer        { get { return _footer; } }
        public  Footer          BackupFooter  { get { return _backupFooter; } }
        public  DynamicHeader   DynamicHeader { get { return _dinamicHeader; } }

        public void FixFooter()
        {
            // fix footer with backup header
            using(var f = System.IO.File.OpenWrite(_file)) {
                f.Seek(-Footer.Size, SeekOrigin.End);
                f.Write(_backupFooter.Raw, 0, _backupFooter.Raw.Length);
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
    }
}
