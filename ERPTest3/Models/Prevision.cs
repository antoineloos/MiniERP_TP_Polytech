using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPTest3.Models
{
    public class Prevision
    {
        public string SemaineId { get; set; }
        public string Article { get; set; }
        public int NbPrevision { get; set; }
        public int NbCommande { get; set; }
    }
}
