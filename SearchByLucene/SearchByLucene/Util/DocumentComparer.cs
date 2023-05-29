using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchByLucene.Util
{
    public class DocumentComparer : IEqualityComparer<Document>
    {
        public bool Equals(Document x, Document y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.Get("SKU") == y.Get("SKU");
        }

        public int GetHashCode(Document obj)
        {
            return obj.Get("SKU").GetHashCode();
        }
    }
}
