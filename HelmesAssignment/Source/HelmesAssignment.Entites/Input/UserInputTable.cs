using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmesAssignment.Entites.Input
{
    [Table("UserInput")]
    public class UserInputTable
    {
        [Key]
        [Column("Name")]
        public string UserName { get; set; }
        [Column("Sectors")]
        public string SectorList { get; set; }
        [Column("AgreeToTerms")]
        public bool TermsAgreed { get; set; }
    }
}
