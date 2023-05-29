using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchByLucene.DTO.CSV
{
    public class CSVMyDataDTO
    {
        public int SKU { get; set; }

        public string Veiculo { get; set; } = string.Empty;

        public string Modelo { get; set; } = string.Empty;

        public string Versao { get; set; } = string.Empty;

        public string Motor { get; set; } = string.Empty;

        public string Valvula { get; set; } = string.Empty;

        public string Cilindrada { get; set; } = string.Empty;

        public int? AnoInicio { get; set; }

        public int? AnoFim { get; set; }

        public string Produto { get; set; } = string.Empty;
    }
}
