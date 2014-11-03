using System;

namespace Vhd
{
    public class BlockAllocationTable
    {
        private byte[]  _buffer;
        private uint    _validEntries;
        private uint    _blockSize;

        public BlockAllocationTable(byte[] buffer, uint maxEntries, uint blockSize)
        {
            _buffer         = new byte[buffer.Length]; buffer.CopyTo(_buffer, 0);
            _validEntries   = maxEntries;
            _blockSize      = blockSize;
            TotalSize       = _validEntries * blockSize;
        }

        public Int64 TotalSize { get; private set; }
    }
}
