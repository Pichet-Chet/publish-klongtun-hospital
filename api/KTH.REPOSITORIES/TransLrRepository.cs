using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;

namespace KTH.REPOSITORIES
{
    public interface ITransLrRepository
    {
        Task<List<TransLr>> GetAll();
        Task<TransLr> GetByCaseId(Guid caseId);
        Task<List<TransLr>> GetByClientId(Guid clientId);
        Task<TransLr> GetById(Guid id);
        Task InsertAsync(TransLr transLr);
        Task InsertAsyne(List<TransLr> list);
        Task UpdateAsync();
    }

    public class TransLrRepository : ITransLrRepository
    {
        private KthContext _context;

        public TransLrRepository(KthContext kthContext)
        {
            _context = kthContext;
        }

        public async Task<List<TransLr>> GetAll()
        {
            return await _context.TransLrs.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<TransLr> GetByCaseId(Guid caseId)
        {
            return await _context.TransLrs.Include(a=> a.TransCase).Include(a=> a.TransCliend).FirstOrDefaultAsync(a => a.TransCaseId == caseId);
        }

        public async Task<List<TransLr>> GetByClientId(Guid clientId)
        {
            return await _context.TransLrs.Where(a => a.TransCliendId == clientId).Include(a => a.TransCase).Include(a => a.TransCliend).ToListAsync();
        }

        public async Task<TransLr> GetById(Guid id)
        {
            return await _context.TransLrs.Include(a => a.TransCase).Include(a => a.TransCliend).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task InsertAsync(TransLr transLr)
        {
            await _context.TransLrs.AddAsync(transLr);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsyne(List<TransLr> list)
        {
            await _context.TransLrs.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
