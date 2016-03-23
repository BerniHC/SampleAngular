using System.Collections.Generic;

namespace HidalgoCastro.Entities
{
    public class Pagination<T>
    {
        public List<T> List { get; set; }

        public int Page { get; set; }

        public int RowsPerPage { get; set; }

        public int TotalRows { get; set; }

        public string Sort { get; set; }

        public string Search { get; set; }

        public string Order { get; set; }

    }
    
}
