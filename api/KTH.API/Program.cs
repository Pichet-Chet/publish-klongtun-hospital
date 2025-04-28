using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using KTH.API;
using KTH.API.Controller;
using KTH.API.Controller.AutoInterface;
using KTH.MODELS;
using KTH.MODELS.Constants;
using KTH.REPOSITORIES;
using KTH.REPOSITORIES.AutoInterface;
using KTH.REPOSITORIES.Dto;
using KTH.REPOSITORIES.ThirthParty;
using KTH.REPOSITORIES.ThirthParty.MicrosoftGraph;
using KTH.SERVICE;
using KTH.SERVICE.AutoInterface;
using KTH.SERVICE.Cache;
using KTH.SERVICE.ThirthParty;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WatchDog;
using WatchDog.src.Enums;
using static KTH.MODELS.Custom.AppSettingConfig;

IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

var connectionString = configuration.GetConnectionString("ConnectionStr");
var connectionStrWatchdog = configuration.GetConnectionString("ConnectionStrWatchdog");
var connectionStrHangFire = configuration.GetConnectionString("ConnectionStrHangFire");



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(x => x.UsePostgreSqlStorage(connectionStrHangFire));
builder.Services.AddHangfireServer();


builder.Services.AddDbContext<KthContext>();

builder.Services.AddDbContext<KthContext>(options =>
    options.UseNpgsql(connectionString));

var endpoint = configuration["APP:END_POINT"];

//var coreUrl = configuration["APP:CORE"];

var title = configuration["APP:TITLE"];
var version = configuration["APP:VERSION"];
var versionDescription = configuration["APP:VERSION_DESCRIPTION"];


#region | Logging Watchdog Zone |

builder.Logging.AddWatchDogLogger();

builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = true;
    opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Every6Hours;
    opt.SetExternalDbConnString = connectionStrWatchdog;
    opt.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;
});

#endregion

#region | Scope Zone |
builder.Services.AddControllers();

builder.Services.AddScoped<IMasterCountryRepository, MasterCountryRepository>();
builder.Services.AddScoped<IMasterCountryService, MasterCountryService>();

builder.Services.AddScoped<ITranClientRepository, TranClientRepository>();
builder.Services.AddScoped<ITranClientService, TranClientService>();

builder.Services.AddScoped<ISysConfigurationRepository, SysConfigurationRepository>();
builder.Services.AddScoped<ISysConfigurationService, SysConfigurationService>();

builder.Services.AddScoped<IMasterHospitalRepository, MasterHospitalRepository>();
builder.Services.AddScoped<IMasterHospitalService, MasterHospitalService>();

builder.Services.AddScoped<IMasterThaiDistrictRepository, MasterThaiDistrictRepository>();
builder.Services.AddScoped<IMasterThaiDistrictService, MasterThaiDistrictService>();

builder.Services.AddScoped<IMasterThaiProvinceRepository, MasterThaiProvinceRepository>();
builder.Services.AddScoped<IMasterThaiProvinceService, MasterThaiProvinceService>();

builder.Services.AddScoped<IMasterThaiSubDistrictRepository, MasterThaiSubDistrictRepository>();
builder.Services.AddScoped<IMasterThaiSubDistrictService, MasterThaiSubDistrictService>();

builder.Services.AddScoped<ISmsMktRepository, SmsMktRepository>();
builder.Services.AddScoped<ISmsMktService, SmsMktService>();

builder.Services.AddScoped<ITranStaffRepository, TranStaffRepository>();
builder.Services.AddScoped<ITranStaffService, TranStaffService>();

builder.Services.AddScoped<ITransSaleRepository, TransSaleRepository>();
builder.Services.AddScoped<ITransSaleService, TransSaleService>();

builder.Services.AddScoped<IMasterSaleGroupRepository, MasterSaleGroupRepository>();
builder.Services.AddScoped<IMasterSaleGroupService, MasterSaleGroupService>();

builder.Services.AddScoped<IMasterRightTreatmentRepository, MasterRightTreatmentRepository>();
builder.Services.AddScoped<IMasterRightTreatmentService, MasterRightTreatmentService>();

builder.Services.AddScoped<IMasterNationalityRepository, MasterNationalityRepository>();
builder.Services.AddScoped<IMasterNationalityService, MasterNationalityService>();

builder.Services.AddScoped<ITransClientCommentRepository, TransClientCommentRepository>();
builder.Services.AddScoped<ITransClientCommentService, TransClientCommentService>();

builder.Services.AddScoped<ITransConsultCommentRepository, TransConsultCommentRepository>();
builder.Services.AddScoped<ITransConsultCommentService, TransConsultCommentService>();

builder.Services.AddScoped<ITransCaseRepository, TransCaseRepository>();
builder.Services.AddScoped<ITransCaseService, TransCaseService>();

builder.Services.AddScoped<IMasterGestationalAgeRepository, MasterGestationalAgeRepository>();
builder.Services.AddScoped<IMasterGestationalAgeService, MasterGestationalAgeService>();

builder.Services.AddScoped<IMasterItemsOrderRepository, MasterItemsOrderRepository>();
builder.Services.AddScoped<IMasterItemsOrderService, MasterItemsOrderService>();

builder.Services.AddScoped<IMasterItemsOrderGroupRepository, MasterItemsOrderGroupRepository>();
builder.Services.AddScoped<IMasterItemsOrderGroupService, MasterItemsOrderGroupService>();

builder.Services.AddScoped<IMasterReasonNotTreatmentRepository, MasterReasonNotTreatmentRepository>();
builder.Services.AddScoped<IMasterReasonNotTreatmentService, MasterReasonNotTreatmentService>();

builder.Services.AddScoped<IMasterPhysicianRepository, MasterPhysicianRepository>();
builder.Services.AddScoped<IMasterPhysicianService, MasterPhysicianService>();

builder.Services.AddScoped<IMasterConsultRoomRepository, MasterConsultRoomRepository>();
builder.Services.AddScoped<IMasterConsultRoomService, MasterConsultRoomService>();

builder.Services.AddScoped<IMasterStatusRepository, MasterStatusRepository>();
builder.Services.AddScoped<IMasterStatusService, MasterStatusService>();

builder.Services.AddScoped<IMasterMessageConfigurationRepository, MasterMessageConfigurationRepository>();
builder.Services.AddScoped<IMessageConfigurationCache, MessageConfigurationCache>();

builder.Services.AddScoped<IGmailSmtpRepository, GmailSmtpRepository>();
builder.Services.AddScoped<IGmailSmtpService, GmailSmtpService>();

builder.Services.AddScoped<ITransActionHistoryRepository, TransActionHistoryRepository>();
builder.Services.AddScoped<ITransActionHistoryService, TransActionHistoryService>();

builder.Services.AddScoped<IAutoInterfaceRepository, AutoInterfaceRepository>();
builder.Services.AddScoped<IAutoInterfaceService, AutoInterfaceService>();

builder.Services.AddScoped<ISysRoleRepository, SysRoleRepository>();
builder.Services.AddScoped<ISysRoleService, SysRoleService>();

builder.Services.AddScoped<ISysPermissionRepository, SysPermissionRepository>();
builder.Services.AddScoped<ISysPermissionService, SysPermissionService>();

builder.Services.AddScoped<IMasterCenterService, MasterCenterService>();

builder.Services.AddScoped<IMasterPaymentChannelRepository, MasterPaymentChannelRepository>();
builder.Services.AddScoped<IMasterPaymentChannelService, MasterPaymentChannelService>();

builder.Services.AddScoped<IMasterReferralFromRepository, MasterReferralFromRepository>();
builder.Services.AddScoped<IMasterReferralFromService, MasterReferralFromService>();

builder.Services.AddScoped<ITransConsultRoomRepository, TransConsultRoomRepository>();
builder.Services.AddScoped<ITransConsultRoomService, TransConsultRoomService>();

builder.Services.AddScoped<IFinanceRepository, FinanceRepository>();
builder.Services.AddScoped<IFinanceService, FinanceService>();

builder.Services.AddScoped<IMicrosoftTeamRepository, MicrosoftTeamRepository>();

builder.Services.AddScoped<ITransCaseCancelRepository, TransCaseCancelRepository>();
builder.Services.AddScoped<ITransCaseCancelService, TransCaseCancelService>();

builder.Services.AddScoped<IMasterReasonNewAppointmentRepository, MasterReasonNewAppointmentRepository>();
builder.Services.AddScoped<IMasterReasonNewAppointmentService, MasterReasonNewAppointmentService>();

builder.Services.AddScoped<IMasterChannelInformationRepository, MasterChannelInformationRepository>();
builder.Services.AddScoped<IMasterChannelInformationService, MasterChannelInformationService>();

builder.Services.AddScoped<IMasterReasonUnFollowRepository, MasterReasonUnFollowRepository>();
builder.Services.AddScoped<IMasterReasonUnFollowService, MasterReasonUnFollowService>();

builder.Services.AddScoped<ITransClosePeriodIncomeRepository, TransClosePeriodIncomeRepository>();
builder.Services.AddScoped<ITransClosePeriodIncomeService, TransClosePeriodIncomeService>();

builder.Services.AddScoped<ITransAssignAssistantManagerRepository, TransAssignAssistantManagerRepository>();
builder.Services.AddScoped<ITransAssignAssistantManagerService, TransAssignAssistantManagerService>();

builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<ITransOrderRepository, TransOrderRepository>();

builder.Services.AddScoped<ITransReferralFeeService, TransReferralFeeService>();
builder.Services.AddScoped<ITransReferralFeeRepository, TransReferralFeeRepository>();

builder.Services.AddScoped<ITransAttachmentRepository, TransAttachmentRepository>();
builder.Services.AddScoped<ITransAttachmentService, TransAttachmentService>();

builder.Services.AddScoped<ITransResultPtRepository, TransResultPtRepository>();
builder.Services.AddScoped<ITransResultPtService, TransResultPtService>();

builder.Services.AddScoped<ITransLabRepository, TransLabRepository>();
builder.Services.AddScoped<ITransLabService, TransLabService>();

builder.Services.AddScoped<ISmartCardService, SmartCardService>();

builder.Services.AddScoped<ITransLrRepository, TransLrRepository>();
builder.Services.AddScoped<ITransLrService, TransLrService>();

builder.Services.AddScoped<ITransR8Repository, TransR8Repository>();
builder.Services.AddScoped<ITransR8Service, TransR8Service>();

#endregion

#region | Set Config |
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JWTConfig>();
builder.Services.AddSingleton(jwtSettings);

var connecttionStringSetting = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStringsConfig>();
builder.Services.AddSingleton(connecttionStringSetting);

var urlConfig = builder.Configuration.GetSection("URLConfig").Get<URLConfig>();
builder.Services.AddSingleton(urlConfig);

#endregion

//var optionsJson = new JsonSerializerOptions
//{
//    ReferenceHandler = ReferenceHandler.Preserve,
//    WriteIndented = true // Optional, for easier readability
//};

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<KthContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});


//Configure Authorization
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});



// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = configuration["JWT:Audience"],
            ValidIssuer = configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("finance", policy => policy.RequireRole("finance"));
    options.AddPolicy("manager", policy => policy.RequireRole("manager"));
    options.AddPolicy("ultrasound", policy => policy.RequireRole("ultrasound"));
    options.AddPolicy("executive", policy => policy.RequireRole("executive"));
    options.AddPolicy("user", policy => policy.RequireRole("user"));
    options.AddPolicy("manager_consult", policy => policy.RequireRole("manager_consult"));
    options.AddPolicy("development", policy => policy.RequireRole("development"));
    options.AddPolicy("stat", policy => policy.RequireRole("stat"));
    options.AddPolicy("sale", policy => policy.RequireRole("sale"));
    options.AddPolicy("consult", policy => policy.RequireRole("consult"));
    options.AddPolicy("counter", policy => policy.RequireRole("counter"));
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
});

// Add services to the container.


System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

var location = assembly.Location;
DateTime setFileCreated = DateTime.Now;
DateTime setFileLastModified = DateTime.Now;
var bytes = new byte[2048];

using (var file = new FileStream(location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
{
    file.Read(bytes, 0, bytes.Length);

    FileInfo fi = new FileInfo(location);
    setFileCreated = fi.CreationTime;
    setFileLastModified = fi.LastWriteTime;
}


// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
    c.SwaggerDoc($"{version}", new OpenApiInfo
    {
        Title = $"{title}",
        Version = $"{version}",
        Description = $"{versionDescription} build at {setFileLastModified.ToString("dd/MM/yyyy HH:mm")}",
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter the JWT token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
});




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddTransient<IMessageConfigurationCache, MessageConfigurationCache>();


builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = Convert.ToInt32(configuration["RATE_LIMIT:PERMIT_LIMIT"]),
                QueueLimit = Convert.ToInt32(configuration["RATE_LIMIT:QUEUE_LIMIT"]),
                Window = TimeSpan.FromSeconds(Convert.ToInt32(configuration["RATE_LIMIT:SECONDS"]))
            }));

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;

        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            await context.HttpContext.Response.WriteAsync(
                $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s). " +
                $"Read more about our rate limits at.", cancellationToken: token);
        }
        else
        {
            await context.HttpContext.Response.WriteAsync(
                "Too many requests. Please try again later. " +
                "Read more about our rate limits at.", cancellationToken: token);
        }
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddDbContext<KthContext>(options => options.UseNpgsql(connectionString));


var app = builder.Build();

GlobalConfiguration.Configuration
    .UsePostgreSqlStorage(
        connectionStrHangFire,
        new PostgreSqlStorageOptions
        {
            DistributedLockTimeout = TimeSpan.FromMinutes(10) // Adjust the timeout as needed
        }
    );


var options = new DashboardOptions()
{
    Authorization = new[] { new MyAuthorizationFilter() }
};

app.UseHangfireDashboard("/hangfire", options);

var optionsHangfire = new BackgroundJobServerOptions
{
    WorkerCount = 1 // Only one worker will execute jobs
};

app.UseHangfireServer(optionsHangfire);


using (var server = new BackgroundJobServer())
{
    //var thaiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // "SE Asia Standard Time" is the IANA time zone ID for Thailand

    //RecurringJob.AddOrUpdate<AutoInterfaceController>(
    //    x => x.DisabledCaseAuto(),
    //    Cron.HourInterval(1),
    //    thaiTimeZone);

    RecurringJob.AddOrUpdate<AutoInterfaceController>(
        x => x.DisabledCaseAuto(),
        Cron.HourInterval(1));

    RecurringJob.AddOrUpdate<AutoInterfaceController>(
        x => x.FinishedCaseAuto(),
        Cron.MinuteInterval(30));

    RecurringJob.AddOrUpdate<FinanceController>(
       x => x.AutoCreateSum1663EveryMonth(),
       Cron.Monthly(1));
}

//app.UseMiddleware<SwaggerAuthenticationMiddleware>();


app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseRateLimiter();
app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = configuration["WatchDogLogging:username"];
    opt.WatchPagePassword = configuration["WatchDogLogging:password"];
});

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.Run();


public class MyAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context) => true;
}




public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var resp = new ResponseModel
        {
            Status = ConstantsResponse.StatusError,
            Code = ConstantsResponse.HttpCode500,
            Message = ConstantsResponse.HttpCode500Message,
            InnerException = ex.InnerException == null ? ex.Message : ex.InnerException.ToString()
        };

        var controllerName = context.GetRouteValue("controller")?.ToString();
        var actionName = context.GetRouteValue("action")?.ToString();

        WatchLogger.LogError(resp.InnerException, ex.StackTrace, controllerName + "|" + actionName);

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return context.Response.WriteAsJsonAsync(resp);
    }

    public DateTime GetLinkerTimestampUtc(string filePath)
    {
        DateTime setFileCreated = DateTime.Now;
        DateTime setFileLastModified = DateTime.Now;

        var bytes = new byte[2048];

        using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            file.Read(bytes, 0, bytes.Length);

            FileInfo fi = new FileInfo(filePath);
            setFileCreated = fi.CreationTime;
            setFileLastModified = fi.LastWriteTime;
        }

        return setFileLastModified;
    }
}