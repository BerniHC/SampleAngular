using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidalgoCastro.WebInterface.Models
{
    public class Auction
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
