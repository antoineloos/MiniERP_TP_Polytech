using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPTest3.Models
{
    public class Stock
    {
        public string CodeArticle { get; set; }
        public string TypeGestion { get; set; }
        public int LotdeLancement { get; set; }
        public int StockDispo { get; set; }
        public int StockSecu { get; set; }
        public string Unite { get; set; }
        public int Delai { get; set; }
        public int ArrivePrevS1 { get; set; }
        public int ArrivePrevS2 { get; set; }

    }
}
