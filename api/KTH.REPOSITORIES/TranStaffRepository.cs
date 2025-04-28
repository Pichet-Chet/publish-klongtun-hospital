using KTH.MODELS.Custom.Request.TranStaff;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq.Dynamic.Core;
using static KTH.MODELS.Constants.ConstantsMassage;
namespace KTH.REPOSITORIES
{
    #region | Interface |
    public interface ITranStaffRepository
    {
        Task<List<TransStaff>> GetTransStaffFilter(GetTransStaffFilterRequest param);
        Task<TransStaff> GetTransStaffWithId(Guid id);
        Task<TransStaff> GetTransStaffLogin(GetTransStaffLoginRequest param);
        Task CreateTransStaff(TransStaff param);
        Task UpdateTransStaff(TransStaff param);
        Task DeleteTransStaff(Guid id);
        Task<int> CountAllAsync();
        Task<List<TransStaff>> GetTransStaffWithRole(Guid roleId);
        Task<List<TransStaff>> GetAll();
        Task<TransStaff> GetByUserName(string userName);
    }
    #endregion

    #region | Class |
    public class TranStaffRepository : ITranStaffRepository
    {
        private readonly KthContext _context;
        private readonly IMemoryCache _cache;
        public TranStaffRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task CreateTransStaff(TransStaff param)
        {
            try
            {
                await _context.TransStaffs.AddAsync(param);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteTransStaff(Guid id)
        {
            try
            {
                var tranStaff = await _context.TransStaffs.FirstOrDefaultAsync(a => a.Id.Equals(id) && a.IsActive);
                if (tranStaff != null)
                {
                    tranStaff.IsActive = false;
                    _context.TransStaffs.Update(tranStaff);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TransStaff>> GetTransStaffFilter(GetTransStaffFilterRequest param)
        {
            List<TransStaff> output = new List<TransStaff>();

            try
            {
                var queryable = _context.TransStaffs.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.Username.ToLower().Contains(param.TextSearch) ||
                        x.FullName.ToLower().Contains(param.TextSearch) ||
                        (x.NickName ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.SysRoleId))
                {
                    queryable = queryable.Where(x => x.SysRoleId != null).AsQueryable();

                    queryable = queryable.Where(x => x.SysRoleId == param.SysRoleId.ToGuid()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                #endregion

                string sortOrder = string.Empty;

                if (!string.IsNullOrEmpty(param.SortName))
                {
                    sortOrder = param.SortName;
                }
                else
                {
                    sortOrder = "CreatedDate";
                }

                if (!string.IsNullOrEmpty(param.SortType))
                {
                    sortOrder += $" {param.SortType}";
                }
                else
                {
                    sortOrder += " desc";
                }

                output = await queryable.AsNoTracking().OrderBy(sortOrder).ToListAsync();

                var cacheKey = "TransStaffCount";

                _cache.Set(cacheKey, output.Count());

                #region Pagination

                if (param.IsAll == true)
                {
                    output = output.ToList();
                }
                else
                {
                    output = output
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }

                #endregion
            }
            catch
            {
                throw;
            }

            return output;
        }

        public async Task<TransStaff> GetTransStaffLogin(GetTransStaffLoginRequest param)
        {
            try
            {
                return await _context.TransStaffs.FirstOrDefaultAsync(a => a.Username.Equals(param.UserName) && a.Password.Equals(param.Password) && a.IsActive);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TransStaff> GetTransStaffWithId(Guid id)
        {
            try
            {
                return await _context.TransStaffs.Include(a => a.SysRole).FirstOrDefaultAsync(a => a.Id.Equals(id) && a.IsDelete == false);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateTransStaff(TransStaff param)
        {
            try
            {
                _context.Update(param);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CountAllAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "TransStaffCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransStaffs.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<TransStaff>> GetTransStaffWithRole(Guid roleId)
        {
            return await _context.TransStaffs.Where(a => a.SysRoleId.Equals(roleId)).OrderBy(a=> a.FullName).ToListAsync();
        }

        public async Task<List<TransStaff>> GetAll()
        {
            return await _context.TransStaffs.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<TransStaff> GetByUserName(string userName)
        {
            return await _context.TransStaffs.FirstOrDefaultAsync(a=> a.Username.ToLower() == userName.ToLower());
        }
    }

    #endregion
}

