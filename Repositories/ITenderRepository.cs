using SPCManagementSystemAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public interface ITenderRepository
    {
        Task<IEnumerable<Tender>> GetAllTenders();
        Task<Tender> GetTenderById(string id);
        Task<Tender> SubmitTender(Tender tender);
    }
}
