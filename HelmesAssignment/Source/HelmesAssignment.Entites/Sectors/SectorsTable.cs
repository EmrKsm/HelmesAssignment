using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelmesAssignment.Entites.Sectors
{
    [Table("Sectors")]
    public class SectorsTable
    {
        [Key]
        public int Id { get; set; }
        [Column("SectorId")]
        public int SectorId { get; set; }
        [Column("Name")]
        public string SectorName { get; set; }
        [Column("ParentId")]
        public int SectorParent { get; set; }
    }
}
