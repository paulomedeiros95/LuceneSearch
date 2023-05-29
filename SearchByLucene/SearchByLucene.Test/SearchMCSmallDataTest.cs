using Lucene.Net.Documents;
using Lucene.Net.Documents.Extensions;
using Lucene.Net.Search;
using SearchByLucene.DTO;
using SearchByLucene.DTO.CSV;
using SearchByLucene.Service.Data;
using SearchByLucene.Test.Base;
using System.Diagnostics.Metrics;
using static Lucene.Net.Util.Fst.Util;

namespace SearchByLucene.Test
{
    public class SearchMCSmallDataTest: BaseTest
    {
        SearchEngineManagerMCSmallData GenerateSearchEngineV2 { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            GenerateSearchEngineV2 = new SearchEngineManagerMCSmallData();

            GenerateSearchEngineV2.IndexManager.CreateIndex(GenerateSearchEngineV2.IndexManager.ReadCSVData<CSVMCSmallDataDTO>, new LoaderDTO() { CSVFile = DataResource.MCSmallData });
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPalio_Test()
        {
            var filter = new FilterParam() { FreeText = "Palio" };
            var documents = GenerateSearchEngineV2.SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }


        [Test]
        public void Search_ByText_AllData_CSVData_ByHonda06_Test()
        {
            var filter = new FilterParam() { FreeText = "Honda 06" };
            var documents = GenerateSearchEngineV2.SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByRolamentoDoFord_Test()
        {
            var filter = new FilterParam() { FreeText = "rolamento do ford" };
            var documents = GenerateSearchEngineV2.SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }


        [Test]
        public void Search_ByText_AllData_CSVData_ByDiscoFreio_Test()
        {
            var filter = new FilterParam() { FreeText = "rolamento do ford" };
            var documents = GenerateSearchEngineV2.SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByTerminaDeDireçãoDenaultDust2012_Test()
        {
            var filter = new FilterParam() { FreeText = "Terminal de direção Renault Dust 2012" };
            var documents = GenerateSearchEngineV2.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }
    }
}