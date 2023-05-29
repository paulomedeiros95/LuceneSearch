using Bogus.DataSets;
using CsvHelper.Configuration;
using CsvHelper;
using Faker;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using SearchByLucene.DTO.CSV;
using SearchByLucene.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Br;
using Lucene.Net.Util;
using SearchByLucene.Service.Data;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Store;
using SearchByLucene.Service.DTO.CSV;

namespace SearchByLucene.Service
{
    public class IndexManager
    {
        #region Properties

        public FSDirectory IndexDirectory { get; set; }

        public BrazilianAnalyzer BrazilianAnalyzer { get; set; } = new BrazilianAnalyzer(LuceneVersion.LUCENE_48, new CharArraySet(LuceneVersion.LUCENE_48, 128, true));

        #endregion

        #region Constructors

        public IndexManager(string indexPath)
        {
            IndexDirectory = FSDirectory.Open(indexPath);
        }

        #endregion


        public void CreateIndex(Action<IndexWriter, LoaderDTO> createDocuments, LoaderDTO createDocumentsParam)
        {
            IndexWriterConfig config = new IndexWriterConfig(LuceneVersion.LUCENE_48, BrazilianAnalyzer);
            config.OpenMode = OpenMode.CREATE_OR_APPEND;
            config.MaxBufferedDocs = 1000;
            config.MergePolicy.NoCFSRatio = 0.1;
            config.RAMBufferSizeMB = 128;

            var indexWriter = new IndexWriter(IndexDirectory, config);
            indexWriter.DeleteAll();

            Synonyms = LoadSynonyms(DataResource.SynonymsNamesV2);

            createDocuments(indexWriter, createDocumentsParam);

            indexWriter.Dispose();
        }

        private Dictionary<string, List<string>> Synonyms { get; set; } = new Dictionary<string, List<string>>();

        public void ReadCSVData<TCSV>(IndexWriter indexWriter, LoaderDTO loaderDTO)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            using var reader = new StringReader(loaderDTO.CSVFile);
            using var csv = new CsvReader(reader, config);

            var documents = new List<Document>();

            var records = csv.GetRecords<TCSV>().ToList();
            var properties = typeof(TCSV).GetProperties();

            foreach (TCSV record in records)
            {
                var document = new Document();

                foreach (PropertyInfo property in properties)
                {
                    var type = property.PropertyType;
                    if (Nullable.GetUnderlyingType(type) != null)
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }

                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.Int32:
                        case TypeCode.Int16:
                            var intValue = Int32.Parse(property.GetValue(record)?.ToString() ?? "0");

                            if (property.Name == "AnoFim" && intValue == 0)
                                document.Add(new Int32Field(property.Name, DateTime.Now.Year + 1, Field.Store.YES));
                            else
                                document.Add(new Int32Field(property.Name, Int32.Parse(property.GetValue(record)?.ToString() ?? "0"), Field.Store.YES));
                            break;
                        case TypeCode.String:
                            var propertyValue = Convert.ToString(property.GetValue(record)) ?? string.Empty;
                            document.Add(new TextField(property.Name, propertyValue, Field.Store.YES));

                            //V2 - Tratar frases
                            var synonyms = Synonyms.ContainsKey(propertyValue) ? Synonyms[propertyValue] : new List<string>();
                            foreach (var synonym in synonyms)
                            {
                                document.Add(new TextField(property.Name, synonym, Field.Store.YES));
                            }
                            break;
                    }
                }

                documents.Add(document);
            }

            indexWriter.AddDocuments(documents);
        }

        public void CreateDataFake<TCSV>(IndexWriter indexWriter, LoaderDTO loaderDTO)
        {
            var documents = new List<Document>();

            var properties = typeof(TCSV).GetProperties();

            for (int i = 0; i <= loaderDTO.limiteFake; i++)
            {
                var document = new Document();

                var vehicle = new Vehicle();
                var commerce = new Commerce();

                var dto = new CSVMCSmallDataDTO
                {
                    SKU = i,
                    Produto = commerce.Product(),
                    Fabricante = Faker.Company.Name(),
                    CodFabricante = RandomNumber.Next(10000, 99999).ToString(),
                    CodMercadocar = RandomNumber.Next(10000, 99999).ToString(),
                    CodReferencia = RandomNumber.Next(10000, 99999).ToString(),
                    Montadora = Faker.Company.Name(),
                    Veiculo = vehicle.Manufacturer(),
                    Modelo = vehicle.Model(),
                    Versao = RandomNumber.Next(1, 10).ToString(),
                    Motor = RandomNumber.Next(1, 10).ToString(),
                    Valvula = RandomNumber.Next(1, 10).ToString(),
                    Cilindrada = RandomNumber.Next(1000, 5000).ToString(),
                    AnoInicio = RandomNumber.Next(1990, 2010),
                    AnoFim = RandomNumber.Next(2010, 2030)
                };

                document.Add(new Int32Field("SKU", dto.SKU, Field.Store.YES));

                if (dto.AnoInicio == null)
                    document.Add(new TextField("AnoInicio", string.Empty, Field.Store.YES));
                else
                    document.Add(new Int32Field("AnoInicio", dto.AnoInicio.Value, Field.Store.YES));

                if (dto.AnoFim == null)
                    document.Add(new TextField("AnoFim", string.Empty, Field.Store.YES));
                else
                    document.Add(new Int32Field("AnoFim", dto.AnoFim.Value, Field.Store.YES));

                document.Add(new TextField("Produto", dto.Produto, Field.Store.YES));
                document.Add(new TextField("Fabricante", dto.Fabricante, Field.Store.YES));
                document.Add(new TextField("CodFabricante", dto.CodFabricante, Field.Store.YES));
                document.Add(new TextField("CodMercadocar", dto.CodMercadocar, Field.Store.YES));
                document.Add(new TextField("CodReferencia", dto.CodReferencia, Field.Store.YES));
                document.Add(new TextField("Montadora", dto.Montadora, Field.Store.YES));
                document.Add(new TextField("Modelo", dto.Modelo, Field.Store.YES));
                document.Add(new TextField("Versao", dto.Versao, Field.Store.YES));
                document.Add(new TextField("Motor", dto.Motor, Field.Store.YES));
                document.Add(new TextField("Valvula", dto.Valvula, Field.Store.YES));
                document.Add(new TextField("Cilindrada", dto.Cilindrada, Field.Store.YES));

                documents.Add(document);
            }

            indexWriter.AddDocuments(documents);
        }

        private Dictionary<string, List<string>> LoadSynonyms(string csvPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            using var reader = new StringReader(csvPath);
            using var csv = new CsvReader(reader, config);

            var records = csv.GetRecords<SynonymDTO>().ToList();

            var synonyms = new Dictionary<string, List<string>>();

            foreach (var record in records)
            {
                if (!synonyms.ContainsKey(record.Product))
                {
                    synonyms.Add(record.Product, record.Synonym.Split(',').ToList());
                }
            }

            return synonyms;
        }
    }
}
