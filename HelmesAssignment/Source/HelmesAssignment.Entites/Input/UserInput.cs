using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmesAssignment.Entites.Input
{
    public class UserInput
    {
        private string _userName;
        private string _sectorList;
        private bool _termsAgreed;

        public string UserName { get => _userName; set => _userName = value; }
        public string SectorList { get => _sectorList; set => _sectorList = value; }
        public bool TermsAgreed { get => _termsAgreed; set => _termsAgreed = value; }
    }
}
