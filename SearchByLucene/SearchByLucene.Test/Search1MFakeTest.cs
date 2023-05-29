using Lucene.Net.Search;
using SearchByLucene.DTO;
using SearchByLucene.DTO.CSV;
using SearchByLucene.Service.Data;
using SearchByLucene.Test.Base;

namespace SearchByLucene.Test
{
    public class Search1MFakeTest: BaseTest
    {
        SearchEngineManager1MFake GenerateSearchEngineFake { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            GenerateSearchEngineFake = new SearchEngineManager1MFake();

            //GenerateSearchEngineFake.IndexManager.CreateIndex(GenerateSearchEngineFake.IndexManager.CreateDataFake<CSVMainSimpleV2DTO>, new LoaderDTO() { limiteFake = 1000000 });
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByVeumLLCV1_Test()
        {
            var filter = new FilterParam() { FreeText = "Veum LLC" };
            var documents = GenerateSearchEngineFake.SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByComputer22BotsfordCamry_Test()
        {
            var filter = new FilterParam() { FreeText = "Computer 22 Botsford Camry" };
            var documents = GenerateSearchEngineFake.SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByBotsfordTowelsRanchero_Test()
        {
            var filter = new FilterParam() { FreeText = "Botsford Towels Ranchero" };
            var documents = GenerateSearchEngineFake.SearchEngineByCSVFreeText<CSVMCSmallDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }
    }
}