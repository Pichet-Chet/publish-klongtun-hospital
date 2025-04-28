using KTH.MODELS.Custom.Request.TransClient;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.REPOSITORIES
{
    #region | Interface |
    public interface ITranClientRepository
    {
        Task<List<TransClient>> GetTransClientFilter(GetTranClientFilterRequest param);

        Task<TransClient> GetTransClientWithId(Guid id);

        Task RegisterTransClient(TransClient tranClient, TransCase transCase);

        Task CreateTransClient(TransClient param);

        Task UpdateTransClient(TransClient param);

        Task<TransClient> GetTransClientWithTelephoneNumber(FindWithPhoneRequuest param);

        Task<TransClient> GetTransClientUpdatePhone(UpdatePhoneTransClientRequest param);

        Task<bool> IsTransClientWithTelephoneNumber(FindWithPhoneRequuest param);

        Task<bool> GetTransClientWithTelephoneNumberWithOutCode(string phone);

        Task<bool> GetTransClientWithTelephoneNumberAndDateOfBirth(FindWithPhoneAndDateOfBirthRequuest param);

        Task<TransClient> GetTransClientLogin(GetTransClientLoginRequest param);

        public Task<int> CountAllAsync();

    }

    #endregion

    #region | Class |
    public class TranClientRepository : ITranClientRepository
    {
        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TranClientRepository(KthContext conext, IMemoryCache cache)
        {
            _context = conext;

            _cache = cache;
        }


        public async Task RegisterTransClient(TransClient tranClient, TransCase transCase)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    #region execute

                    await _context.TransClients.AddAsync(tranClient);
                    await _context.TransCases.AddAsync(transCase);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    #endregion

                }
                catch
                {
                    await transaction.RollbackAsync();

                    throw;
                }
            }
        }


        public async Task CreateTransClient(TransClient param)
        {
            try
            {
                await _context.TransClients.AddAsync(param);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TransClient>> GetTransClientFilter(GetTranClientFilterRequest param)
        {
            List<TransClient> output = new List<TransClient>();

            try
            {
                var queryable = _context.TransClients.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                                .Where(x =>
                                    x.Id.ToString().Contains(param.TextSearch) ||
                                    (x.ClientNo ?? "").ToLower().Contains(param.TextSearch) ||
                                    (x.HnNo ?? "").ToLower().Contains(param.TextSearch) ||
                                    x.FullName.ToLower().Contains(param.TextSearch) ||
                                    (x.CitizenIdentification ?? "").ToLower().Contains(param.TextSearch) ||
                                    (x.PassportNumber ?? "").ToLower().Contains(param.TextSearch) ||
                                    x.TelephoneCode.Contains(param.TextSearch))
                                .AsQueryable();

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

                output = await queryable.OrderBy(sortOrder).AsNoTracking().ToListAsync();

                var cacheKey = "TransClientCount";

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

        public async Task<TransClient> GetTransClientLogin(GetTransClientLoginRequest param)
        {
            try
            {
                return await _context.TransClients.FirstOrDefaultAsync(a => a.TelephoneNumber.Equals(param.TelephoneNumber)
                    && a.DateOfBirth.Equals(param.DateOfBirth)
                    && a.TelephoneCode.Equals(param.TelephoneCode)
                    && a.IsActive
                    && a.IsDelete == false);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TransClient> GetTransClientWithId(Guid id)
        {
            try
            {
                return await _context.TransClients.FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch
            {

                throw;
            }
        }

        public async Task<TransClient> GetTransClientWithTelephoneNumber(FindWithPhoneRequuest param)
        {
            TransClient result = new TransClient();

            try
            {
                result = await _context.TransClients.Where(x => x.TelephoneCode == param.Code && x.TelephoneNumber == param.Phone).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }


        public async Task<TransClient> GetTransClientUpdatePhone(UpdatePhoneTransClientRequest param)
        {
            TransClient result = new TransClient();

            try
            {
                result = await _context.TransClients.Where(x => x.TelephoneNumber == param.NewPhone).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<bool> IsTransClientWithTelephoneNumber(FindWithPhoneRequuest param)
        {
            bool result = false;

            try
            {
                result = await _context.TransClients.Where(x => x.TelephoneCode == param.Code && x.TelephoneNumber == param.Phone).AnyAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<bool> GetTransClientWithTelephoneNumberWithOutCode(string phone)
        {
            bool result = false;

            try
            {
                result = await _context.TransClients.Where(x => x.TelephoneNumber == phone).AnyAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<bool> GetTransClientWithTelephoneNumberAndDateOfBirth(FindWithPhoneAndDateOfBirthRequuest param)
        {
            bool result = false;

            try
            {
                result = await _context.TransClients.Where(x => x.DateOfBirth == param.DateOfBirth && x.TelephoneNumber == param.Phone).AnyAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task UpdateTransClient(TransClient param)
        {
            try
            {
                _context.TransClients.Update(param);
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
            var cacheKey = "TranClientCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransClients.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }

    #endregion
}
