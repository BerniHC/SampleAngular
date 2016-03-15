using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidalgoCastro.Entities
{
    public class Product : Timespan
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public decimal? Price { get; set; }

        public virtual List<Image> Images { get; set; }
    }
}
