using AutoMapper;
using Finatech.Infrastructure.Model;
using Finatech.Infrastructure.Service;
using Finatech.Security.Business;
using Finatech.Security.Model;
using Finatech.Security.Service;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Finatech.Web.Filters;


public class ServiceContextFilter : IActionFilter
{
    private readonly ILogger<ServiceContextFilter> _logger;
    private readonly ServiceContext _serviceContext;
    private readonly IConfigurationParameterService _configurationParameterService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ServiceContextFilter(
        ILogger<ServiceContextFilter> logger, 
        ServiceContext serviceContext, 
        IUserService userService,
        IConfigurationParameterService configurationParameterService,
        IMapper mapper)
    {
        _logger = logger;
        _serviceContext = serviceContext;
        _userService = userService;
        _configurationParameterService = configurationParameterService;
        _mapper = mapper;
        
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("ServiceContextFilter - OnActionExecuting called.");
        
        // Initialize ServiceContext info from session, cache etc. using the JWT, request parameters etc.
        User? user = _userService.GetUser("1");
       _serviceContext.UserInfo = _mapper.Map<UserInfo>(user);
       _serviceContext.ConfigurationParameters = _configurationParameterService.GetAllConfigurationParameters();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No implementation needed for this example
    }
}