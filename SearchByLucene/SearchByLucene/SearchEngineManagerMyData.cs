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

namespace SearchByLucene
{
    public class SearchEngineManagerMyData : SearchEngineManagerBase
    {
        public SearchEngineManagerMyData() : base(@"C:\MercadoCar\Lucene\IndexTest")
        {
        }
    }
}