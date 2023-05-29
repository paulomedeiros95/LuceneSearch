using Lucene.Net.Documents;
using SearchByLucene.DTO;
using SearchByLucene.DTO.CSV;

namespace SearchByLucene.Service.Interfaces
{
    public interface ISearchEngineMyDataService
    {
        void ReloadIndex();

        List<CSVMyDataDTO> Search(FilterParam filterParam);
    }
}