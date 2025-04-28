using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;

namespace KTH.REPOSITORIES
{
    public interface ITransResultPtRepository
    {
        Task<List<TransResultPt>> GetAll();
        Task<TransResultPt> GetById(Guid id);
        Task<TransResultPt> GetByCaseId(Guid caseId);
        Task InsertAsync(TransResultPt transResultPt);
        Task UpdateAsync();
    }

    public class TransResultPtRepository : ITransResultPtRepository
    {
        private readonly KthContext _context;
        public TransResultPtRepository(KthContext kthContext)
        {
            _context = kthContext;
        }

        public async Task<List<TransResultPt>> GetAll()
        {
            return await _context.TransResultPts.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<TransResultPt> GetByCaseId(Guid caseId)
        {
            return await _context.TransResultPts.FirstOrDefaultAsync(a => a.TransCaseId == caseId);
        }

        public async Task<TransResultPt> GetById(Guid id)
        {
            return await _context.TransResultPts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task InsertAsync(TransResultPt transResultPt)
        {
            await _context.TransResultPts.AddAsync(transResultPt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
