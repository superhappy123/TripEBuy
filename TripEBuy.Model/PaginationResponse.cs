using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripEBuy.Model
{
    public class PaginationResponse
    {
        public DataTable dt { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
    }
}
