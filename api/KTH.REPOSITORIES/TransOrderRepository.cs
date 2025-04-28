using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES
{

    public interface ITransOrderRepository
    {
        Task<TransOrder> GetById(Guid id);
        Task<List<TransOrder>> GetAll();
        Task<List<TransOrder>> GetByCaseId(Guid caseId);
        Task<List<TransOrder>> GetByCaseIds(List<Guid> caseIds);

    }

    public class TransOrderRepository : ITransOrderRepository
    {
        private readonly DateTime _datetime;
        private readonly KthContext _context;
        public TransOrderRepository(KthContext kthContext)
        {
            _datetime = DateTime.Now;
            _context = kthContext;
        }

        public async Task<List<TransOrder>> GetAll()
        {
            return await _context.TransOrders.Where(a => a.IsActive).Include(a => a.TransOrderItems).ToListAsync();
        }

        public async Task<List<TransOrder>> GetByCaseId(Guid caseId)
        {
            return await _context.TransOrders.Where(a => a.IsActive && a.TransCaseId.Equals(caseId)).Include(a => a.TransOrderItems).ToListAsync();
        }

        public async Task<List<TransOrder>> GetByCaseIds(List<Guid> caseIds)
        {
            return await _context.TransOrders
                .Where(a => a.IsActive && caseIds.Contains(a.TransCaseId)).Include(x => x.TransOrderItems).ThenInclude(x => x.MasterItemOrder).ThenInclude(x => x.MasterItemsOrderGroup)
                .ToListAsync();
        }


        public async Task<TransOrder> GetById(Guid id)
        {
            return await _context.TransOrders.FirstOrDefaultAsync(a => a.IsActive && a.Id.Equals(id));
        }
    }
}
