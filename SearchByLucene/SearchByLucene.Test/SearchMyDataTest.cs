using Lucene.Net.Search;
using SearchByLucene.DTO;
using SearchByLucene.DTO.CSV;
using SearchByLucene.Service.Data;
using SearchByLucene.Test.Base;

namespace SearchByLucene.Test
{
    public class SearchMyDataTest: BaseTest
    {
        SearchEngineManagerMyData GenerateSearchEngine { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            GenerateSearchEngine = new SearchEngineManagerMyData();

            GenerateSearchEngine.IndexManager.CreateIndex(GenerateSearchEngine.IndexManager.ReadCSVData<CSVMyDataDTO>, new LoaderDTO() { CSVFile = DataResource.MyData });
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByCorsaHatch_Test()
        {
            var filter = new FilterParam() { FreeText = "Corsa Hatch" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByCorsaHatchWind96_Test()
        {
            var filter = new FilterParam() { FreeText = "Corsa Hatch Wind 96" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByCorsaHatchWind1dot0_Test()
        {
            var filter = new FilterParam() { FreeText = "Corsa Hatch Wind 1.0" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByCorsaSuper1dot0_Test()
        {
            var filter = new FilterParam() { FreeText = "Corsa Super 1.0" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByG1CorsaWind_Test()
        {
            var filter = new FilterParam() { FreeText = "G1 Corsa Wind" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByG1WindCorsa_Test()
        {
            var filter = new FilterParam() { FreeText = "G1 Wind Corsa" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_By96CorsaHatchWind_Test()
        {
            var filter = new FilterParam() { FreeText = "96 Corsa Hatch Wind" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_By95CorsaHatchWind_Test()
        {
            var filter = new FilterParam() { FreeText = "95 Corsa Hatch Wind" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByCorsaHatchWindEFI93_Test()
        {
            var filter = new FilterParam() { FreeText = "Corsa Hatch Wind EFI 93" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertNoMatch(filter, documents);
        }


        [Test]
        public void Search_ByText_AllData_CSVData_ByHB20_2020_Test()
        {
            var filter = new FilterParam() { FreeText = "HB20 2020" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByCarG1_Test()
        {
            var filter = new FilterParam() { FreeText = "G1" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_By2020_Test()
        {
            var filter = new FilterParam() { FreeText = "2020" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_By20_Test()
        {
            var filter = new FilterParam() { FreeText = "20" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_By90_Test()
        {
            var filter = new FilterParam() { FreeText = "90" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertNoMatch(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByHB202020_Test()
        {
            var filter = new FilterParam() { FreeText = "\"HB 20 2020" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175x70r15_v1_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175x70r15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175x70r15_v2_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175X70r15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175x70r15_v3_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175X70R15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175x70r15_v4_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175x70 r15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }


        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175x70r15_v5_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175X70 R15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175Barra70r15_v1_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175/70r15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175Barra70r15_v2_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175/70R15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175Barra70r15_v3_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175/70 R15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175x70raio15_v1_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175X70 ARO 15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPneu175Barra70raio15_v2_Test()
        {
            var filter = new FilterParam() { FreeText = "Pneu 175/70 ARO 15" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByCorsa2001_Test()
        {
            var filter = new FilterParam() { FreeText = "Corsa 2001" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByHB202019_Test()
        {
            var filter = new FilterParam() { FreeText = "HB20 2019" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPalio2005_Test()
        {
            var filter = new FilterParam() { FreeText = "Palio 2005" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByGol2013_Test()
        {
            var filter = new FilterParam() { FreeText = "Gol 2013" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByFordFiesta2008_Test()
        {
            var filter = new FilterParam() { FreeText = "Ford Fiesta 2008" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPeugeot2072010_Test()
        {
            var filter = new FilterParam() { FreeText = "Peugeot 207 2010" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByAmortecedorGol_Test()
        {
            var filter = new FilterParam() { FreeText = "Amortecedor Gol" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByFreioCorsa_Test()
        {
            var filter = new FilterParam() { FreeText = "Freio Corsa" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByLanternaPalio_Test()
        {
            var filter = new FilterParam() { FreeText = "Lanterna Palio" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByFiltroDeOleoHB20_Test()
        {
            var filter = new FilterParam() { FreeText = "Filtro de óleo HB20" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByRadiadorCivic_Test()
        {
            var filter = new FilterParam() { FreeText = "Radiador Civic" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertNoMatch(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByBateriaFiesta_Test()
        {
            var filter = new FilterParam() { FreeText = "Bateria Fiesta" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertNoMatch(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByVelocimetroGolG5_Test()
        {
            var filter = new FilterParam() { FreeText = "Velocímetro Gol G5" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertNoMatch(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByRetrovisorPeugeot206_Test()
        {
            var filter = new FilterParam() { FreeText = "Retrovisor Peugeot 206" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertNoMatch(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByBombaDeCombustivelCelta_Test()
        {
            var filter = new FilterParam() { FreeText = "Bomba de combustível Celta" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertNoMatch(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByFiltroDeAr_Test()
        {
            var filter = new FilterParam() { FreeText = "Filtro de Ar" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByFiltroAr_Test()
        {
            var filter = new FilterParam() { FreeText = "Filtro Ar" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByFiltroDeArDoCorsa_Test()
        {
            var filter = new FilterParam() { FreeText = "Filtro de Ar do Corsa" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByAmortecedorDoHB20_Test()
        {
            var filter = new FilterParam() { FreeText = "Amortecedor do HB20" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByAlavancaFreioDeMão_Test()
        {
            var filter = new FilterParam() { FreeText = "Alavanca Freio de Mão" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByManecoFreio_Sym_AlavancaFreioDeMã_Test()
        {
            var filter = new FilterParam() { FreeText = "Maneco Freio" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_BMW207_Test()
        {
            var filter = new FilterParam() { FreeText = "BMW 207" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }


        [Test]
        public void Search_ByText_AllData_CSVData_ByAlavancaDaMaçaneta_Test()
        {
            var filter = new FilterParam() { FreeText = "Alavanca da Maçaneta" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(filter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByManoplaDaPorta_Sym_AlavancaDaMaçaneta_Test()
        {
            var filter = new FilterParam() { FreeText = "manopla da porta" };
            var correctFilter = new FilterParam() { FreeText = "Alavanca da Maçaneta" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(correctFilter, documents);
        }


        [Test]
        public void Search_ByText_AllData_CSVData_ByPaliu_Typo_Test()
        {
            var filter = new FilterParam() { FreeText = "paliu" };
            var correctFilter = new FilterParam() { FreeText = "palio" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(correctFilter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPalui_Typo_Test()
        {
            var filter = new FilterParam() { FreeText = "palui" };
            var correctFilter = new FilterParam() { FreeText = "palio" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(correctFilter, documents);
        }

        [Test]
        public void Search_ByText_AllData_CSVData_ByPaloi_Typo_Test()
        {
            var filter = new FilterParam() { FreeText = "paloi" };
            var correctFilter = new FilterParam() { FreeText = "palio" };
            var documents = GenerateSearchEngine.SearchEngineByCSVFreeText<CSVMyDataDTO>(filter);

            AssertByAllFieldsContentThatFindDocuments(correctFilter, documents);
        }

    }
}