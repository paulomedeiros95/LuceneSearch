using Lucene.Net.Documents;
using Lucene.Net.Index;
using SearchByLucene.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchByLucene.Test.Base
{
    public abstract class BaseTest
    {
        #region Methods

        protected static void AssertByAllFieldsContentThatFindDocuments(FilterParam filter, List<Document> documents)
        {
            Console.WriteLine($"Documents: {documents.Count} FilteredBy {filter.FreeText}");

            bool textExists = false;
            foreach (var document in documents)
            {
                if (document.Fields.Any(field => ExisteValue(filter, field)))
                {
                    textExists = true;
                    break;
                }
            }

            Assert.IsTrue(textExists, "The value of FreeText exists in at least one field.");
            Assert.IsTrue(documents.Count != 0, documents.Count.ToString());
        }

        protected static void AssertNoMatch(FilterParam filter, List<Document> documents)
        {
            Console.WriteLine($"Documents: {documents.Count} FilteredBy {filter.FreeText}");

            Assert.IsTrue(documents.Count == 0, documents.Count.ToString());
        }

        private static bool ExisteValue(FilterParam filter, IIndexableField field)
        {
            if(field.GetStringValue() == null) return false;
           

            foreach (var textPart in filter.FreeText.Split(" "))
            {
                if (field.GetStringValue().ToUpper().Contains(textPart.ToUpper().Trim()))
                {
                    Console.WriteLine($"Field: {field.Name} Value: {field.GetStringValue()}");
                    return true;
                }
                
            }

            return false;
        }

        #endregion
    }
}
