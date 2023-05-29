using SearchByLucene.DTO.CSV;
using SearchByLucene.DTO;
using SearchByLucene.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using SearchByLucene.Service.Interfaces;
using SearchByLucene.Service.Extensions;
using CsvHelper.Configuration;
using CsvHelper;
using Lucene.Net.Index;
using System.Globalization;
using System.Reflection;

namespace SearchByLucene.Service.Services
{
    public class SearchEngineMyDataService : ISearchEngineMyDataService
    {
        public void ReloadIndex()
        {
            var generateSearchEngine = new SearchEngineManagerMyData();

            generateSearchEngine.IndexManager.CreateIndex(generateSearchEngine.IndexManager.ReadCSVData<CSVMyDataDTO>, new LoaderDTO() { CSVFile = DataResource.MyData });
        }

        public List<CSVMyDataDTO> Search(FilterParam filterParam)
        {
            List<CSVMyDataDTO> documents = new SearchEngineManagerMyData().SearchEngineByCSVFreeText<CSVMyDataDTO>(filterParam)
                                                    .Select(doc => doc.ToDocumentResponseDTO()).ToList();

            return documents;
        }
    }
}
