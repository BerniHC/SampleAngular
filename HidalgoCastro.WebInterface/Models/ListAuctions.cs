using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidalgoCastro.WebInterface.Models
{
    public class ListAuctions
    {
        public List<Auction> Data { get; set; }

        public DateTime ServerDateTime { get; set; }
    }
}
