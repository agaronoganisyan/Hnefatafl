using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.AssetManagement
{
    public interface IAssetsProvider : IService
    {
        void CleanUp();
        Task<T> Load<T>(string address) where T : class;
    }
}