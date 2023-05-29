using Lucene.Net.Documents;
using SearchByLucene.DTO;
using SearchByLucene.DTO.CSV;

namespace SearchByLucene.Service.Interfaces
{
    public interface ISearchEngine1MFakeService
    {
        void ReloadIndex();

        List<CSVMCSmallDataDTO> Search(FilterParam filterParam);
    }
}