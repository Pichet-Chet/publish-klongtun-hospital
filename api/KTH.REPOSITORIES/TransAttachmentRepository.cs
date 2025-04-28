using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;

namespace KTH.REPOSITORIES
{

    public interface ITransAttachmentRepository
    {
        Task<List<TransAttachment>> GetAll();
        Task<TransAttachment> GetById(Guid id);
        Task<List<TransAttachment>> GetByReferanceId(Guid referanceId);
        Task<List<TransAttachment>> GetByReferanceId(List<Guid> list);
        Task<List<TransAttachment>> GetByNameAndPath(string fileName, string filePath);
        Task InsertAsync(TransAttachment transAttachment);
        Task UpdateAsync();
    }


    public class TransAttachmentRepository : ITransAttachmentRepository
    {
        private readonly KthContext _context;
        public TransAttachmentRepository(KthContext kthContext)
        {
            _context = kthContext;
        }


        public async Task<List<TransAttachment>> GetAll()
        {
            return await _context.TransAttachments.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<TransAttachment> GetById(Guid id)
        {
            return await _context.TransAttachments.FirstOrDefaultAsync(a => a.Id == id && a.IsActive);
        }

        public async Task<List<TransAttachment>> GetByNameAndPath(string fileName, string filePath)
        {
            return await _context.TransAttachments.Where(a => a.FileName == fileName && a.FilePath == filePath).ToListAsync();
        }

        public async Task<List<TransAttachment>> GetByReferanceId(Guid referanceId)
        {
            return await _context.TransAttachments.Where(a => a.ReferanceId == referanceId && a.IsActive).ToListAsync();
        }

        public async Task<List<TransAttachment>> GetByReferanceId(List<Guid> list)
        {
            return await _context.TransAttachments.Where(a => a.IsActive && list.Contains(a.ReferanceId)).ToListAsync();
        }

        public async Task InsertAsync(TransAttachment transAttachment)
        {
            await _context.TransAttachments.AddAsync(transAttachment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
