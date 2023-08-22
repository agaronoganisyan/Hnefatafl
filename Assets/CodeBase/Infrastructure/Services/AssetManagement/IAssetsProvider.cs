using System.Threading.Tasks;

namespace CodeBase.Infrastructure.Services.AssetManagement
{
    public interface IAssetsProvider
    {
        void Initialize();
        void CleanUp();
        Task<T> Load<T>(string address) where T : class;
    }
}