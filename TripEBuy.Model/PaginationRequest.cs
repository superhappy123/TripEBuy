using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripEBuy.Model
{
    public class PaginationRequest
    {
        public string Tables { get; set; }
        public string PrimaryKey { get; set; }
        public int CurrentPage { get; set; }
        public string Sort { get; set; }
        public int PageSize { get; set; }
        public string Fields { get; set; }
        public string Filter { get; set; }
        public string Group { get; set; }
    }
}
