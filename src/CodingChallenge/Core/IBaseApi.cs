using System;
using System.Threading.Tasks;

namespace CodingChallenge.Core
{
    public interface IBaseApi<T>
    {
        string BaseUri { get; set; }
        Task<ModelList<T>> GetListAsync();
        Task<ModelList<T>> GetListAsync(string partOfUrl);
        Task<T> GetAsync(string id);
    }
}
