using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;

namespace KTH.REPOSITORIES
{
    public interface ITransR8Repository
    {
        Task<List<TransR8>> GetAll();
        Task<TransR8> GetById(Guid id);
        Task<TransR8> GetByCaseId(Guid caseId);
        Task InsertAsync(TransR8 transR8);
        Task InsertAsync(List<TransR8> list);
        Task UpdateAsync();
    }


    public class TransR8Repository : ITransR8Repository
    {
        private readonly KthContext _context;

        public TransR8Repository(KthContext kthContext)
        {
            _context = kthContext;
        }


        public async Task<List<TransR8>> GetAll()
        {
            return await _context.TransR8s.Where(a => a.IsActive.HasValue && a.IsActive.Value).ToListAsync();
        }

        public async Task<TransR8> GetByCaseId(Guid caseId)
        {
            return await _context.TransR8s.Include(a => a.Case).FirstOrDefaultAsync(a => a.CaseId == caseId);
        }

        public async Task<TransR8> GetById(Guid id)
        {
            return await _context.TransR8s.Include(a => a.Case).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task InsertAsync(TransR8 transR8)
        {
            await _context.TransR8s.AddAsync(transR8);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(List<TransR8> list)
        {
            await _context.TransR8s.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
