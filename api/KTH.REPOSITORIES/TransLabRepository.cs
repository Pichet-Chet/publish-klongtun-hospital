using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;

namespace KTH.REPOSITORIES
{
    public interface ITransLabRepository
    {
        Task<List<TransLab>> GetAll();
        Task<List<TransLab>> GetByCaseId(Guid caseId);
        Task<TransLab> GetById(Guid id);
        Task InsertAsync(TransLab transLab);
        Task InsertAsync(List<TransLab> list);
        Task UpdateAsync();
    }

    public class TransLabRepository : ITransLabRepository
    {
        private readonly KthContext _context;

        public TransLabRepository(KthContext context)
        {
            _context = context;
        }

        public async Task<List<TransLab>> GetAll()
        {
            return await _context.TransLabs.Where(a => a.IsActive).Include(a => a.MasterItemOrder).ToListAsync();
        }

        public async Task<List<TransLab>> GetByCaseId(Guid caseId)
        {
            return await _context.TransLabs.Where(a => a.IsActive && a.TransCaseId == caseId).Include(a => a.MasterItemOrder).ToListAsync();
        }

        public async Task<TransLab> GetById(Guid id)
        {
            return await _context.TransLabs.FirstOrDefaultAsync(a => a.IsActive && a.Id == id);
        }

        public async Task InsertAsync(TransLab transLab)
        {
            await _context.TransLabs.AddAsync(transLab);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(List<TransLab> list)
        {
            await _context.TransLabs.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
