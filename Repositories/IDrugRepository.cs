using SPCManagementSystemAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public interface IDrugRepository
    {
        Task<IEnumerable<Drug>> GetAllDrugs();
        Task<Drug> GetDrugById(string id);  
        Task<Drug> AddDrug(Drug drug);
        Task<bool> UpdateDrug(Drug drug);
        Task<bool> DeleteDrug(string id);
    }
}
