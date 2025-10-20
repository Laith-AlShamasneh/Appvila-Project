using Microsoft.AspNetCore.Mvc;
using Service.CommonCall;

namespace AppvilaAPI.Helpers;

public class ControllerBaseHelper<TController, TCaller>(TCaller caller, IServiceBroker<TController> serviceCaller) : ControllerBase
{
    protected readonly TCaller _Caller = caller;
    protected readonly IServiceBroker<TController> _ServiceCaller = serviceCaller;
}
