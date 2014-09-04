using System;

namespace Vhd
{
    public class DiskGeometry
    {
        private readonly    ushort  _cylinders;
        private readonly    byte    _heads;
        private readonly    byte    _sectorPerTrack;

        public DiskGeometry(ushort cylinders, byte heads, byte sectorPerTrack)
        {
            _cylinders      = cylinders;
            _heads          = heads;
            _sectorPerTrack = sectorPerTrack;
        }
        
        public override string ToString()
        {
            return String.Format("C:{0}, H:{1}, S:{2}", _cylinders, _heads, _sectorPerTrack);
        }
    }
}
