using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES
{
    #region | Interface |
    public interface IMasterMessageConfigurationRepository
    {
        List<MasterMessageConfiguration> GetAll();
        Task<MasterMessageConfiguration> GetWithId(Guid id);
        Task<MasterMessageConfiguration> GetWithCode(string code);
    }
    #endregion

    #region | Class |
    public class MasterMessageConfigurationRepository : IMasterMessageConfigurationRepository
    {
        private readonly KthContext _context;
        public MasterMessageConfigurationRepository(KthContext kthContext)
        {
            _context = kthContext;
        }

        public  List<MasterMessageConfiguration> GetAll()
        {
            try
            {
                return  _context.MasterMessageConfigurations.Where(a => a.IsActive).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<MasterMessageConfiguration> GetWithCode(string code)
        {
            try
            {
                return await _context.MasterMessageConfigurations.FirstOrDefaultAsync(a => a.IsActive && a.Code.Equals(code));
            }
            catch
            {
                throw;
            }
        }

        public async Task<MasterMessageConfiguration> GetWithId(Guid id)
        {
            try
            {
                return await _context.MasterMessageConfigurations.FirstOrDefaultAsync(a => a.Id.Equals(id) && a.IsActive);
            }
            catch
            {
                throw;
            }
        }
    }
    #endregion
}
