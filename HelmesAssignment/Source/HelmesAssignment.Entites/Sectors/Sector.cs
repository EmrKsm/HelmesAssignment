using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmesAssignment.Entites.Sectors
{
    public class Sector
    {
        private int _sectorId;
        private string _sectorName;
        private int _parentId;
        private List<Sector> _childSectors;

        public int SectorId { get => _sectorId; set => _sectorId = value; }
        public string SectorName { get => _sectorName; set => _sectorName = value; }
        public int ParentId { get; set; }
        public List<Sector> ChildSectors { get => _childSectors; set => _childSectors = value; }
    }
}
