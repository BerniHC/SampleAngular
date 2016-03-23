using System;
using System.Collections.Generic;

namespace HidalgoCastro.WebInterface.Models
{
    public class ListAuctions
    {
        public List<Auction> Data { get; set; }

        public DateTime ServerDateTime { get; set; }
    }
}
