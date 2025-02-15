using Microsoft.EntityFrameworkCore;
using SPCManagementSystemAPI.Data;
using SPCManagementSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Repositories
{
    public class TenderRepository : ITenderRepository
    {
        private readonly SPCContext _context;

        public TenderRepository(SPCContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tender>> GetAllTenders()
        {
            return await _context.Tenders.ToListAsync();
        }

        public async Task<Tender> GetTenderById(string id)
        {
            return await _context.Tenders.FindAsync(id);
        }

        public async Task<Tender> SubmitTender(Tender tender)
        {
            // Generate a unique Tender ID (TND001, TND002, etc.)
            string newTenderId = GenerateNewTenderId();
            tender.Id = newTenderId;

            _context.Tenders.Add(tender);
            await _context.SaveChangesAsync();
            return tender;
        }

        private string GenerateNewTenderId()
        {
            var lastTender = _context.Tenders.OrderByDescending(t => t.Id).FirstOrDefault();
            if (lastTender == null)
            {
                return "TND001"; // First ID
            }

            string lastId = lastTender.Id.Substring(3);
            int newId = int.Parse(lastId) + 1;
            return $"TND{newId:D3}";
        }
    }
}
