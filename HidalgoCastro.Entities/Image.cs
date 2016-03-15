using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HidalgoCastro.Entities
{
    public class Image : Timespan
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public string Description { get; set; }

    }
}
