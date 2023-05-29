using Lucene.Net.Analysis.Br;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.QueryParsers.Classic;
using SearchByLucene.DTO;
using CsvHelper;
using Lucene.Net.QueryParsers.Flexible.Core.Config;
using System.Globalization;
using System.Reflection.Metadata;
using CsvHelper.Configuration;
using Document = Lucene.Net.Documents.Document;
using SearchByLucene.DTO.CSV;
using System.Reflection;
using System.Xml.Linq;
using SearchByLucene.Util;
using Faker;
using Bogus.DataSets;
using Company = Faker.Company;
using SearchByLucene.Service.Data;
using SearchByLucene.Service.DTO.CSV;
using SearchByLucene.Service;
using Lucene.Net.QueryParsers.Surround.Parser;

namespace SearchByLucene
{
    public abstract class SearchEngineManagerBase
    {
        #region Properties

        public IndexManager IndexManager { get; set; }

        private List<string> NoSearchWords { get; set; } = new List<string> { "a", "ainda", "alem", "ambas", "ambos", "antes", "ao", "aonde", "aos", "apos", "aquele", "aqueles", "as", "assim", "com", "como", "contra", "contudo", "cuja", "cujas", "cujo", "cujos", "da", "das", "de", "dela", "dele", "deles", "demais", "depois", "desde", "desta", "deste", "dispoe", "dispoem", "diversa", "diversas", "diversos", "do", "dos", "durante", "e", "ela", "elas", "ele", "eles", "em", "entao", "entre", "essa", "essas", "esse", "esses", "esta", "estas", "este", "estes", "ha", "isso", "isto", "logo", "mais", "mas", "mediante", "menos", "mesma", "mesmas", "mesmo", "mesmos", "na", "nas", "nao", "nas", "nem", "nesse", "neste", "nos", "o", "os", "ou", "outra", "outras", "outro", "outros", "pelas", "pelas", "pelo", "pelos", "perante", "pois", "por", "porque", "portanto", "proprio", "propios", "quais", "qual", "qualquer", "quando", "quanto", "que", "quem", "quer", "se", "seja", "sem", "sendo", "seu", "seus", "sob", "sobre", "sua", "suas", "tal", "tambem", "teu", "teus", "toda", "todas", "todo", "todos", "tua", "tuas", "tudo", "um", "uma", "umas", "uns", "para" };


        #endregion

        #region Fields

        private const int fuzziness = 2;

        protected int HitLimit { get; set; }

        #endregion

        #region Constructors

        public SearchEngineManagerBase(string indexPath, int hitLimit = 100)
        {
            IndexManager = new IndexManager(indexPath);
            HitLimit = hitLimit;
        }

        #endregion

        #region Public Methods




        public List<Document> SearchEngineByCSV<TCSV>(FilterParam filterParam)
        {
            List<Document> documents = new List<Document>();

            var indexReader = DirectoryReader.Open(IndexManager.IndexDirectory);
            var indexSearcher = new IndexSearcher(indexReader);

            var fieldNames = typeof(TCSV).GetProperties().Select(property => property.Name).ToArray();

            Query query;
            if (string.IsNullOrEmpty(filterParam.FreeText))
            {
                query = new MatchAllDocsQuery();
            }
            else
            {
                var queryParser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48, fieldNames, IndexManager.BrazilianAnalyzer);
                query = queryParser.Parse(filterParam.FreeText);

                /*
                var freeTextBooleanQuery = new BooleanQuery();
                foreach (var field in fieldNames)
                {
                    var fuzzyFreeTextQuery = new FuzzyQuery(new Term(field, filterParam.FreeText), fuzziness);
                    freeTextBooleanQuery.Add(fuzzyFreeTextQuery, Occur.SHOULD);
                }
                query = freeTextBooleanQuery;
                */
            }

            var booleanQuery = new BooleanQuery();
            booleanQuery.Add(query, Occur.MUST);

            if (!string.IsNullOrEmpty(filterParam.Line))
            {
                var fuzzyLineQuery = new FuzzyQuery(new Term("Line", filterParam.Line), fuzziness);
                booleanQuery.Add(fuzzyLineQuery, Occur.MUST);
            }

            if (!string.IsNullOrEmpty(filterParam.Assembler))
            {
                var fuzzyAssemblerQuery = new FuzzyQuery(new Term("Assembler", filterParam.Assembler), fuzziness);
                booleanQuery.Add(fuzzyAssemblerQuery, Occur.MUST);
            }

            if (!string.IsNullOrEmpty(filterParam.Model))
            {
                var fuzzyModelQuery = new FuzzyQuery(new Term("Modelo", filterParam.Model), fuzziness);
                booleanQuery.Add(fuzzyModelQuery, Occur.MUST);
            }

            if (!string.IsNullOrEmpty(filterParam.Year))
            {
                var fuzzyYearQuery = new FuzzyQuery(new Term("Year", filterParam.Year), fuzziness);
                booleanQuery.Add(fuzzyYearQuery, Occur.MUST);
            }

            if (!string.IsNullOrEmpty(filterParam.Engine))
            {
                var fuzzyEngineQuery = new FuzzyQuery(new Term("Engine", filterParam.Engine), fuzziness);
                booleanQuery.Add(fuzzyEngineQuery, Occur.SHOULD);
            }

            var topDocs = indexSearcher.Search(booleanQuery, HitLimit);

            foreach (var scoreDoc in topDocs.ScoreDocs)
            {
                var document = indexSearcher.Doc(scoreDoc.Doc);
                documents.Add(document);
            }

            indexReader.Dispose();

            return documents;
        }

        public List<Document> SearchEngineByCSVFreeText<TCSV>(FilterParam filterParam)
        {
            List<Document> documents = new List<Document>();

            var indexReader = DirectoryReader.Open(IndexManager.IndexDirectory);
            var indexSearcher = new IndexSearcher(indexReader);

            var fieldNames = typeof(TCSV).GetProperties().Select(property => property.Name).ToArray();

            documents.AddRange(FindAllByFilter(filterParam, indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerNameWithNumber(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerNameWithNumber(isReverse: true), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerConcatFull(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerConcatFull(isReverse: true), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerAroR(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerNameWithNumber().HandlerXBar(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerNameWithNumber(isReverse: true).HandlerXBar(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerConcatFull().HandlerXBar(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerConcatFull(isReverse: true).HandlerXBar(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerNameWithNumber().HandlerXBar().HandlerAroR(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerNameWithNumber(isReverse: true).HandlerXBar().HandlerAroR(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerConcatFull().HandlerXBar().HandlerAroR(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilter(filterParam.HandlerNoSearchWords(NoSearchWords).HandlerConcatFull(isReverse: true).HandlerXBar().HandlerAroR(), indexSearcher, fieldNames));
            if (documents.Count == 0) documents.AddRange(FindAllByFilterWithSmallMistakes(filterParam, indexSearcher, new[] { "Produto", "Veiculo", "Modelo" }));
            

            documents = documents.Distinct(new DocumentComparer()).ToList();

            indexReader.Dispose();

            return documents;
        }

        private List<Document> FindAllByFilter(FilterParam filterParam, IndexSearcher indexSearcher, string[] fieldNames)
        {
            List<Document> documents = new List<Document>();

            Query query;
            if (string.IsNullOrEmpty(filterParam.FreeText))
            {
                query = new MatchAllDocsQuery();
            }
            else
            {
                var queryParser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48, fieldNames, IndexManager.BrazilianAnalyzer);

                var booleanQuery = new BooleanQuery();


                foreach (var term in filterParam.FreeText.Split(' '))
                {
                    int year = -1;
                    if (int.TryParse(term, out int parsedYear) && (parsedYear >= 0 && parsedYear <= 99))
                    {
                        year = ((parsedYear >= DateTime.Now.Year - 2000 + 2) ? 1900 : 2000) + parsedYear;
                    }
                    else if (int.TryParse(term, out parsedYear) && (parsedYear >= 1900 && parsedYear <= 2099))
                    {
                        year = parsedYear;
                    }
                    else
                    {
                        var termQuery = queryParser.Parse(QueryParserBase.Escape(term));
                        booleanQuery.Add(termQuery, Occur.MUST);
                    }

                    if (year != -1)
                    {
                        var yearRangeQuery = BuildYearRangeQuery("AnoInicio", "AnoFim", year);
                        booleanQuery.Add(yearRangeQuery, Occur.MUST);
                    }
                }

                query = booleanQuery;
            }

            var topDocs = indexSearcher.Search(query, HitLimit);

            foreach (var scoreDoc in topDocs.ScoreDocs)
            {
                var document = indexSearcher.Doc(scoreDoc.Doc);
                documents.Add(document);
            }

            return documents;
        }


        private List<Document> FindAllByFilterWithSmallMistakes(FilterParam filterParam, IndexSearcher indexSearcher, string[] fields)
        {
            List<Document> documents = new List<Document>();

            Query query;
            if (string.IsNullOrEmpty(filterParam.FreeText))
            {
                query = new MatchAllDocsQuery();
            }
            else
            {
                var booleanQuery = new BooleanQuery();

                foreach (var term in filterParam.FreeText.Split(' '))
                {
                    string termQuery = term;

                    if (int.TryParse(term, out int parsedYear) && (parsedYear >= 0 && parsedYear <= 99))
                    {
                        termQuery = (((parsedYear >= DateTime.Now.Year - 2000 + 2) ? 1900 : 2000) + parsedYear).ToString();
                    }

                    var subQuery = new BooleanQuery();

                    foreach (var field in fields)
                    {
                        var fuzzyTerm = new Term(field, termQuery);
                        var fuzzyQuery = new FuzzyQuery(fuzzyTerm, maxEdits: 2, prefixLength: 0, maxExpansions: 50, transpositions: true);
                        subQuery.Add(new BooleanClause(fuzzyQuery, Occur.SHOULD));
                    }

                    booleanQuery.Add(subQuery, Occur.MUST);
                }

                query = booleanQuery;
            }

            var topDocs = indexSearcher.Search(query, HitLimit);

            foreach (var scoreDoc in topDocs.ScoreDocs)
            {
                var document = indexSearcher.Doc(scoreDoc.Doc);
                documents.Add(document);
            }

            return documents;
        }


        #endregion

        #region Private Methods



        private Query BuildYearRangeQuery(string yearFieldStart, string yearFieldEnd, int year)
        {
            var lowerRangeQuery = NumericRangeQuery.NewInt32Range(yearFieldStart, 1900, year, true, true);
            var upperRangeQuery = NumericRangeQuery.NewInt32Range(yearFieldEnd, year, 2999, true, true);

            var booleanQuery = new BooleanQuery();
            booleanQuery.Add(lowerRangeQuery, Occur.MUST);
            booleanQuery.Add(upperRangeQuery, Occur.MUST);

            return booleanQuery;
        }



        #endregion
    }

}