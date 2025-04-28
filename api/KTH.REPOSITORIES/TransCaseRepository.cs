using KTH.MODELS.Custom.Request.TransCase;
using KTH.REPOSITORIES.Dto;
using KTH.REPOSITORIES.Model.TransCase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsMassage;
using System.Linq.Dynamic.Core;

namespace KTH.REPOSITORIES
{
    #region | Interface |
    public interface ITransCaseRepository
    {
        Task<List<TransCase>> GetTransCaseFilter(GetTransCaseFilterRequest param);
        Task<TransCase> GetTransCaseWithId(Guid id);
        Task<List<TransCase>> GetTransCaseWithClientId(Guid clientId);
        Task<List<TransCase>> GetTransCaseWithDate(DateOnly sDate, DateOnly eDate);
        Task<List<TransCase>> GetTransCaseByYear(int year);
        Task<List<TransCase>> GetTransCaseByYears(List<int> years);
        Task<List<TransCase>> GetTransCaseByYearRange(int yearStart, int yearEnd);



        Task CreateTransCase(TransCase param);
        Task UpdateTransCase(TransCase param);
        Task RemoveTransCase(Guid id);
        Task<int> CountAllAsync();

        Task UpdateStatusCode(Guid transCaseId, string statusCode);

        Task UpdateNewAppointmentCaseId(Guid transCaseId, Guid newTransCaseId);

        Task UpdateIsForeigner(Guid transCaseId, bool foreigner);

        Task UpdateStartConsult(UpdateStartConsultRequest param);

        Task<List<TransCase>> ValidateCaseRG01(Guid id);

        Task<bool> VerifyTranCaseId(Guid id);

        Task<GetTransCaseForSaleFilterModel> GetTransCaseForSaleFilter(GetCaseForSaleRequest param, Guid masterSaleGroupId, Guid transSaleId, List<string> listStatusCode, bool isLookAll);
        Task<GetCountCaseForSaleModel> GetCountCaseForSale(Guid saleId, Guid masterSaleGroupId);

        Task<List<GetTransCaseWithMasterSaleGroupIdModel>> GetTransCaseWithMasterSaleGroupId(Guid masterSaleGroupId);
        Task UpdateTransCaseAsync();
    }
    #endregion

    #region | Class |
    public class TransCaseRepository : ITransCaseRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransCaseRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
            _serverTime = DateTime.Now;
        }

        public async Task<int> CountAllAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "TransCaseCount";
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

        public async Task CreateTransCase(TransCase param)
        {
            try
            {
                _context.TransCases.Add(param);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TransCase>> GetTransCaseFilter(GetTransCaseFilterRequest param)
        {
            List<TransCase> output = new List<TransCase>();

            try
            {
                var queryable = _context.TransCases.Include(a => a.TransClient).AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.CaseNo.Contains(param.TextSearch) ||
                        x.TransClient.FullName.Contains(param.TextSearch) ||
                        x.TransClient.CitizenIdentification.Contains(param.TextSearch) ||
                        x.TransClient.ClientNo.Contains(param.TextSearch) ||
                        x.TransClient.TelephoneNumber.Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.TransClientId) && param.TransClientId.ToLower() != ConstantsData.Undefined)
                {
                    queryable = queryable.Where(x => x.TransClientId == param.TransClientId.ToGuid()).AsQueryable();
                }

                if (param.StartReceiveServiceDate != null)
                {
                    queryable = queryable.Where(x => x.ReceiveServiceDate >= param.StartReceiveServiceDate).AsQueryable();
                }

                if (param.EndReceiveServiceDate != null)
                {
                    queryable = queryable.Where(x => x.ReceiveServiceDate <= param.EndReceiveServiceDate).AsQueryable();
                }

                if (param.StartUpdateDate != null)
                {
                    queryable = queryable.Where(x => x.UpdatedDate.Date >= param.StartUpdateDate.Value.Date).AsQueryable();
                }

                if (param.EndUpdateDate != null)
                {
                    queryable = queryable.Where(x => x.UpdatedDate.Date <= param.EndUpdateDate.Value.Date).AsQueryable();
                }

                if (param.MasterStatusCodes != null && param.MasterStatusCodes.Count() > 0)
                {
                    queryable = queryable.Where(x => param.MasterStatusCodes.Contains(x.MasterStatusCode)).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.MasterConsultRoomId) && param.MasterConsultRoomId.ToLower() != ConstantsData.Undefined)
                {
                    queryable = queryable.Where(x => x.MasterConsultRoomId == param.MasterConsultRoomId.ToGuid()).AsQueryable();
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

                output = await queryable.AsNoTracking().OrderBy(sortOrder)
                    .Include(x => x.MasterConsultRoom)
                    .Include(x => x.TransSale)
                    .Include(x => x.MasterStatusCodeNavigation)
                    .Include(x => x.TransOrders)
                    .ThenInclude(x => x.TransOrderItems)
                    .Include(x => x.TransR8).ToListAsync();

                var cacheKey = "TransCaseCount";

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

        public async Task<List<TransCase>> GetTransCaseByYear(int year)
        {
            List<TransCase> output = new List<TransCase>();

            try
            {
                var queryable = _context.TransCases.AsQueryable();

                #region Filter Data Zone


                if (year != 0)
                {
                    year = year - 543;

                    queryable = queryable.Where(x => x.TransSaleId != null).AsQueryable();

                    queryable = queryable.Where(x => x.MasterSaleGroupId != null).AsQueryable();

                    queryable = queryable.Where(x => x.StartConsultDate.Value.Year == year).AsQueryable();
                }
                else
                {
                    return output;
                }

                #endregion

                output = await queryable.AsNoTracking().Include(x => x.TransSale).Include(x => x.MasterSaleGroup).Include(x => x.MasterReasonNewAppointment).Include(x => x.TransConsultRoom).ToListAsync();

                var cacheKey = "TransCaseCount";

                _cache.Set(cacheKey, output.Count());

                #region Pagination


                #endregion
            }
            catch
            {
                throw;
            }

            return output;

        }

        public async Task<List<TransCase>> GetTransCaseByYears(List<int> years)
        {
            List<TransCase> output = new List<TransCase>();

            try
            {
                var queryable = _context.TransCases.AsQueryable();

                #region Filter Data Zone


                if (years != null && years.Count > 0)
                {
                    for (int i = 0; i < years.Count; i++)
                    {
                        years[i] -= 543;
                    }

                    queryable = queryable.Where(x => x.TransSaleId != null).AsQueryable();

                    queryable = queryable.Where(x => x.MasterSaleGroupId != null).AsQueryable();

                    queryable = queryable.Where(x => x.StartConsultDate.HasValue && years.Contains(x.StartConsultDate.Value.Year)).AsQueryable();
                }
                else
                {
                    return output;
                }

                #endregion

                output = await queryable.AsNoTracking().Include(x => x.TransSale).Include(x => x.MasterSaleGroup).Include(x => x.MasterReasonNewAppointment).ToListAsync();

                var cacheKey = "TransCaseCount";

                _cache.Set(cacheKey, output.Count());

                #region Pagination


                #endregion
            }
            catch
            {
                throw;
            }

            return output;

        }

        public async Task<List<TransCase>> GetTransCaseByYearRange(int yearStart, int yearEnd)
        {
            List<TransCase> output = new List<TransCase>();

            try
            {
                var queryable = _context.TransCases.AsQueryable();

                #region Filter Data Zone


                if (yearStart != 0 && yearEnd != 0)
                {
                    yearStart = yearStart - 543;

                    yearEnd = yearEnd - 543;

                    queryable = queryable.Where(x => x.TransSaleId != null).AsQueryable();

                    queryable = queryable.Where(x => x.MasterSaleGroupId != null).AsQueryable();

                    queryable = queryable.Where(x => x.StartConsultDate.Value.Year >= yearStart).AsQueryable();

                    queryable = queryable.Where(x => x.StartConsultDate.Value.Year <= yearEnd).AsQueryable();

                }
                else
                {
                    return output;
                }

                #endregion

                output = await queryable.AsNoTracking().Include(x => x.TransSale).Include(x => x.MasterSaleGroup).Include(x => x.MasterReasonNewAppointment).Include(x => x.TransConsultRoom).ToListAsync();

                var cacheKey = "TransCaseCount";

                _cache.Set(cacheKey, output.Count());

                #region Pagination


                #endregion
            }
            catch
            {
                throw;
            }

            return output;

        }


        public async Task<List<TransCase>> GetTransCaseWithClientId(Guid clientId)
        {
            try
            {
                return await _context.TransCases.Where(a => a.TransClientId.Equals(clientId) && a.IsActive).ToListAsync();
            }
            catch { throw; }
        }

        public async Task<List<TransCase>> GetTransCaseWithDate(DateOnly sDate, DateOnly eDate)
        {
            try { return await _context.TransCases.Where(a => a.ReceiveServiceDate >= sDate && a.ReceiveServiceDate <= eDate && a.IsActive).ToListAsync(); } catch { throw; }
        }

        public async Task<TransCase> GetTransCaseWithId(Guid id)
        {
            try
            {
                return await _context.TransCases.Include(x => x.TransClient).ThenInclude(x => x.MasterNationality).FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> VerifyTranCaseId(Guid id)
        {
            bool result = false;

            try
            {
                result = await _context.TransCases.Where(a => a.Id.Equals(id)).AnyAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task RemoveTransCase(Guid id)
        {
            try
            {
                var transCase = await _context.TransCases.FirstOrDefaultAsync(a => a.Id.Equals(id) && a.IsActive);
                if (transCase != null)
                {
                    transCase.IsActive = false;
                    _context.TransCases.Update(transCase);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateStatusCode(Guid transCaseId, string statusCode)
        {
            try
            {
                var findTranCase = await _context.TransCases.FirstOrDefaultAsync(a => a.Id == transCaseId);

                if (findTranCase != null)
                {
                    if (StatusFlowCase.ValidateKey(statusCode))
                    {
                        findTranCase.MasterStatusCode = statusCode;

                        _context.Update(findTranCase);

                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch
            {

                throw;
            }
        }

        public async Task UpdateNewAppointmentCaseId(Guid transCaseId, Guid newTransCaseId)
        {
            try
            {
                var findTranCase = await _context.TransCases.FirstOrDefaultAsync(a => a.Id == transCaseId);

                if (findTranCase != null)
                {

                    findTranCase.NewAppointmentCaseId = newTransCaseId;

                    _context.Update(findTranCase);

                    await _context.SaveChangesAsync();

                }
            }
            catch
            {

                throw;
            }
        }

        public async Task UpdateIsForeigner(Guid transCaseId, bool foreigner)
        {
            try
            {
                var findTranCase = await _context.TransCases.FirstOrDefaultAsync(a => a.Id == transCaseId);

                if (findTranCase != null)
                {
                    findTranCase.IsForeigner = foreigner;

                    _context.Update(findTranCase);

                    await _context.SaveChangesAsync();
                }
            }
            catch
            {

                throw;
            }
        }

        public async Task UpdateStartConsult(UpdateStartConsultRequest param)
        {
            try
            {
                var findTranCase = await _context.TransCases.FirstOrDefaultAsync(a => a.Id == param.Id.ToGuid());

                if (findTranCase != null)
                {
                    findTranCase.StartConsultDate = _serverTime;
                    findTranCase.StartConsultRemark = param.StartConsultRemark;

                    findTranCase.MasterStatusCode = StatusFlowCase.GRCx01.Key;

                    _context.Update(findTranCase);

                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateTransCase(TransCase param)
        {
            try
            {
                _context.TransCases.Update(param);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<TransCase>> ValidateCaseRG01(Guid id)
        {
            try
            {
                return await _context.TransCases.Where(a => a.TransClientId.Equals(id) &&
                a.MasterStatusCode == StatusFlowCase.Register.Key &&
                a.IsActive == true).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<GetTransCaseForSaleFilterModel> GetTransCaseForSaleFilter(GetCaseForSaleRequest param, Guid masterSaleGroupId, Guid transSaleId, List<string> listStatusCode, bool isLookAll)
        {
            GetTransCaseForSaleFilterModel result = new GetTransCaseForSaleFilterModel();
            var queryable = _context.TransCases.Where(a => a.IsActive && a.MasterSaleGroupId.Equals(masterSaleGroupId) && a.IsSaleFollow != false).AsQueryable();

            if (!isLookAll)
            {
                queryable = queryable.Where(a => a.TransSaleId.Equals(transSaleId));
            }

            if (param.StartDate != null)
            {
                queryable = queryable.Where(a => a.CreatedDate >= param.StartDate);
            }

            if (param.EndDate != null)
            {
                queryable = queryable.Where(a => a.CreatedDate <= param.EndDate);
            }

            if (param.IsAll)
            {
                result.Data = await queryable.Include(a => a.TransClient).Include(a => a.TransSale).Select(a => new GetTransCaseForSaleFilterModelData
                {
                    CaseNo = a.CaseNo,
                    CongenitalDisease = a.CongenitalDisease,
                    CreatedDate = a.CreatedDate,
                    DrugAllergy = a.DrugAllergy,
                    FullName = a.TransClient.FullName,
                    GestationalAge = a.GestationalAge,
                    HistoryOfCesareanSection = a.HistoryOfCesareanSection,
                    Id = a.Id,
                    InformationToDoctor = a.InformationToDoctor,
                    LastMonthlyPeriod = a.LastMonthlyPeriod,
                    MarriedOrBoyfriend = a.MarriedOrBoyfriend,
                    ReasonTermination = a.ReasonTermination,
                    ReceiveServiceDate = a.ReceiveServiceDate,
                    TransClientTelephone = a.TransClient.TelephoneNumber,
                    TransSaleFullName = a.TransSale != null ? a.TransSale.FullName : string.Empty,
                    TransSaleId = a.TransSale != null ? a.TransSale.Id.ToString() : null,
                    TransSaleRefCode = a.TransSale != null ? a.TransSale.RefCode : string.Empty,
                    UpdatedDate = a.UpdatedDate,
                    MasterStatusCode = a.MasterStatusCode,
                    IsNewAppointment = a.IsNewAppointment,
                    SaleRecord = a.SaleRecord,
                }).ToListAsync();
            }
            else
            {
                result.Data = await queryable.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).Include(a => a.TransClient).Include(a => a.TransSale).Select(a => new GetTransCaseForSaleFilterModelData
                {
                    CaseNo = a.CaseNo,
                    CongenitalDisease = a.CongenitalDisease,
                    CreatedDate = a.CreatedDate,
                    DrugAllergy = a.DrugAllergy,
                    FullName = a.TransClient.FullName,
                    GestationalAge = a.GestationalAge,
                    HistoryOfCesareanSection = a.HistoryOfCesareanSection,
                    Id = a.Id,
                    InformationToDoctor = a.InformationToDoctor,
                    LastMonthlyPeriod = a.LastMonthlyPeriod,
                    MarriedOrBoyfriend = a.MarriedOrBoyfriend,
                    ReasonTermination = a.ReasonTermination,
                    ReceiveServiceDate = a.ReceiveServiceDate,
                    TransClientTelephone = a.TransClient.TelephoneNumber,
                    TransSaleFullName = a.TransSale != null ? a.TransSale.FullName : string.Empty,
                    TransSaleId = a.TransSale != null ? a.TransSale.Id.ToString() : null,
                    TransSaleRefCode = a.TransSale != null ? a.TransSale.RefCode : string.Empty,
                    UpdatedDate = a.UpdatedDate,
                    MasterStatusCode = a.MasterStatusCode,
                    IsNewAppointment = a.IsNewAppointment,
                    SaleRecord = a.SaleRecord,
                }).ToListAsync();
            }

            return result;
        }

        public async Task<GetCountCaseForSaleModel> GetCountCaseForSale(Guid saleId, Guid masterSaleGroupId)
        {
            GetCountCaseForSaleModel result = new GetCountCaseForSaleModel();

            int thisYear = DateTime.Now.Year;
            int thisMonth = DateTime.Now.Month;

            var queryable = _context.TransCases.Where(a => a.IsActive && a.MasterSaleGroupId.Equals(masterSaleGroupId)).AsQueryable();
            result.CountCaseYear = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear);
            result.CountCaseYearBySale = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.TransSaleId.Equals(saleId));
            result.CountCaseCureMonth = await queryable.CountAsync(a => a.CreatedDate.Month == thisMonth);
            result.CountCaseCureMonthBySale = await queryable.CountAsync(a => a.CreatedDate.Month == thisMonth && a.TransSaleId.Equals(saleId));

            var janContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number1) && a.TransSaleId.Equals(saleId));
            var febContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number2) && a.TransSaleId.Equals(saleId));
            var marContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number3) && a.TransSaleId.Equals(saleId));
            var aprContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number4) && a.TransSaleId.Equals(saleId));
            var mayContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number5) && a.TransSaleId.Equals(saleId));
            var junContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number6) && a.TransSaleId.Equals(saleId));
            var julContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number7) && a.TransSaleId.Equals(saleId));
            var augContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number8) && a.TransSaleId.Equals(saleId));
            var sepContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number9) && a.TransSaleId.Equals(saleId));
            var octContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number1 + FixNumberText.Number0) && a.TransSaleId.Equals(saleId));
            var novContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number1 + FixNumberText.Number1) && a.TransSaleId.Equals(saleId));
            var decContract = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number1 + FixNumberText.Number2) && a.TransSaleId.Equals(saleId));

            result.CountContract = $"{janContract},{febContract},{marContract},{aprContract},{mayContract},{junContract},{julContract},{augContract},{sepContract},{octContract},{novContract},{decContract}";

            var janHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number1) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var febHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number2) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var marHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number3) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var aprHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number4) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var mayHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number5) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var junHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number6) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var julHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number7) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var augHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number8) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var sepHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number9) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var octHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number1 + FixNumberText.Number0) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var novHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number1 + FixNumberText.Number1) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));
            var decHealing = await queryable.CountAsync(a => a.CreatedDate.Year == thisYear && a.CreatedDate.Month == int.Parse(FixNumberText.Number1 + FixNumberText.Number2) && a.MasterStatusCode == StatusFlowCase.SYSx01.Key && a.TransSaleId.Equals(saleId));

            result.CountHealing = $"{janHealing},{febHealing},{marHealing},{aprHealing},{mayHealing},{junHealing},{julHealing},{augHealing},{sepHealing},{octHealing},{novHealing},{decHealing}";

            return result;
        }

        public async Task<List<GetTransCaseWithMasterSaleGroupIdModel>> GetTransCaseWithMasterSaleGroupId(Guid masterSaleGroupId)
        {
            return await _context.TransCases.Where(a => a.MasterSaleGroupId == masterSaleGroupId && a.ReceiveServiceDate.Year == DateTime.Now.Year && a.IsActive).Select(a => new GetTransCaseWithMasterSaleGroupIdModel
            {
                TransSaleId = a.TransSaleId,
                Id = a.Id,
                CreatedDate = a.CreatedDate,
                MasterSaleGroupId = a.MasterSaleGroupId,
                ReceiveServiceDate = a.ReceiveServiceDate,
                UpdatedDate = a.UpdatedDate,
                Order = a.TransOrders != null ? a.TransOrders.Select(b => new GetTransCaseWithMasterSaleGroupIdModelOrder
                {
                    Id = b.Id,
                    OrderItem = b.TransOrderItems != null ? b.TransOrderItems.Select(c => new GetTransCaseWithMasterSaleGroupIdModelOrderItem
                    {
                        Id = c.Id,
                        MasterItemOrderId = c.MasterItemOrderId
                    }).ToList() : new List<GetTransCaseWithMasterSaleGroupIdModelOrderItem>(),
                }).ToList() : new List<GetTransCaseWithMasterSaleGroupIdModelOrder>()
            }).ToListAsync();
        }

        public async Task UpdateTransCaseAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
    #endregion
}
