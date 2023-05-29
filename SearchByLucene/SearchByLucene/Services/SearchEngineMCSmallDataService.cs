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
    public class SearchEngineMCSmallDataService : ISearchEngineMCSmallDataService
    {
        public void ReloadIndex()
        {
            var generateSearchEngine = new SearchEngineManagerMCSmallData();

            generateSearchEngine.IndexManager.CreateIndex(generateSearchEngine.IndexManager.ReadCSVData<CSVMCSmallDataDTO>, new LoaderDTO() { CSVFile = DataResource.MCSmallData });
        }

        public List<CSVMCSmallDataDTO> Search(FilterParam filterParam)
        {
            List<CSVMCSmallDataDTO> documents = new SearchEngineManagerMCSmallData().SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filterParam).Select(doc => doc.ToDocumentResponseV2DTO()).ToList();

            return documents;
        }
    }
}
