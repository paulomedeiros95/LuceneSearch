using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchByLucene.DTO
{
    public class LoaderDTO
    {
        public string CSVFile { get; set; } = string.Empty;

        public int limiteFake { get; set; }
    }
}
