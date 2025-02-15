using Microsoft.EntityFrameworkCore;
using SPCManagementSystemAPI.Data;
using SPCManagementSystemAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public class DrugRepository : IDrugRepository
    {
        private readonly SPCContext _context;

        public DrugRepository(SPCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Drug>> GetAllDrugs()
        {
            return await _context.Drugs.ToListAsync();
        }

        public async Task<Drug> GetDrugById(string id)
        {
            return await _context.Drugs.FindAsync(id);
        }

        public async Task<Drug> AddDrug(Drug drug)
        {
            // Auto-generate a unique Drug ID (P001, P002, etc.)
            string newDrugId = GenerateNewDrugId();
            drug.Id = newDrugId; // Assign the generated Drug ID

            _context.Drugs.Add(drug);
            await _context.SaveChangesAsync();
            return drug;
        }

        public async Task<bool> UpdateDrug(Drug drug)
        {
            _context.Drugs.Update(drug);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDrug(string id)
        {
            var drug = await _context.Drugs.FindAsync(id);
            if (drug == null) return false;
            _context.Drugs.Remove(drug);
            return await _context.SaveChangesAsync() > 0;
        }

        private string GenerateNewDrugId()
        {
            var lastDrug = _context.Drugs.OrderByDescending(d => d.Id).FirstOrDefault();
            if (lastDrug == null)
            {
                return "P001"; // First ID
            }

            string lastId = lastDrug.Id.Substring(1); // Remove 'P'
            int newId = int.Parse(lastId) + 1; // Increment number
            return $"P{newId:D3}"; // Format as P001, P002, etc.
        }
    }
}
