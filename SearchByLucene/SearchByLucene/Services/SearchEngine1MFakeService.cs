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
    public class SearchEngine1MFakeService : ISearchEngine1MFakeService
    {
        public void ReloadIndex()
        {
            var generateSearchEngine = new SearchEngineManager1MFake();

            generateSearchEngine.IndexManager.CreateIndex(generateSearchEngine.IndexManager.CreateDataFake<CSVMCSmallDataDTO>, new LoaderDTO() { limiteFake = 1000000 });
        }

        public List<CSVMCSmallDataDTO> Search(FilterParam filterParam)
        {
            List<CSVMCSmallDataDTO> documents = new SearchEngineManager1MFake(filterParam.Limite).SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filterParam).Select(doc => doc.ToDocumentResponseV2DTO()).ToList();

            return documents;
        }
    }
}
