using Netwise.Models;

namespace Netwise.Interfaces
{
    public interface IFileService
    {
        Task WriteToFile();
        Task<List<CatFact>> ReadFile();
        Task<string> OpenFile();
    }
}
