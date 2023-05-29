using Lucene.Net.Documents;
using SearchByLucene.DTO.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchByLucene.Service.Extensions
{
    public static class LuceneExtensions
    {
        public static CSVMyDataDTO ToDocumentResponseDTO(this Document document)
        {
            int.TryParse(document.Get("SKU"), out int sku);
            int.TryParse(document.Get("AnoInicio"), out int anoInicio);
            int.TryParse(document.Get("AnoFim"), out int anoFim);

            var documentResponseDTO = new CSVMyDataDTO
            {
                SKU = sku,
                Produto = document.Get("Produto"),
                Veiculo = document.Get("Veiculo"),
                Modelo = document.Get("Modelo"),
                Versao = document.Get("Versao"),
                Motor = document.Get("Motor"),
                Valvula = document.Get("Valvula"),
                Cilindrada = document.Get("Cilindrada"),
                AnoInicio = anoInicio,
                AnoFim = anoFim,
            };

            return documentResponseDTO;
        }

        public static CSVMCSmallDataDTO ToDocumentResponseV2DTO(this Document document)
        {
            int.TryParse(document.Get("SKU"), out int sku);
            int.TryParse(document.Get("AnoInicio"), out int anoInicio);
            int.TryParse(document.Get("AnoFim"), out int anoFim);

            var documentResponseDTO = new CSVMCSmallDataDTO
            {
                SKU = sku,
                Produto = document.Get("Produto"),
                Fabricante = document.Get("Fabricante"),
                CodFabricante = document.Get("CodFabricante"),
                CodMercadocar = document.Get("CodMercadocar"),
                CodReferencia = document.Get("CodReferencia"),
                Montadora = document.Get("Montadora"),
                Veiculo = document.Get("Veiculo"),
                Modelo = document.Get("Modelo"),
                Versao = document.Get("Versao"),
                Motor = document.Get("Motor"),
                Valvula = document.Get("Valvula"),
                Cilindrada = document.Get("Cilindrada"),
                AnoInicio = anoInicio,
                AnoFim = anoFim,
            };

            return documentResponseDTO;
        }
    }
}
