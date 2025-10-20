using AppvilaAPI.Helpers;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels.SystemManagement.Response;
using Service.CommonCall;
using Service.Interface.SystemManagement;
using System.Net.Mime;

namespace AppvilaAPI.Controllers.SchoolManagement;

[Route("api/home")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class HomeController(IHomeService<HomeController> caller, IServiceBroker<HomeController> serviceCaller) : ControllerBaseHelper<HomeController, IHomeService<HomeController>>(caller, serviceCaller)
{
    [HttpPost("get/header-menu")]
    public async Task<StandardApiResponse> GetHeaderMenu() => await _ServiceCaller.ReturnListMethodsWithoutEntity<HeaderMenuResponseVM>(_Caller.GetHeaderMenu, ModelState.IsValid, MessageType.RetrieveSuccessfully, MessageType.NoDataFound, MessageType.SystemProblem);

    [HttpPost("get/about-us")]
    public async Task<StandardApiResponse> GetAboutUs() => await _ServiceCaller.ReturnObjectMethodsWithoutEntity<AboutUsResponseVM>(_Caller.GetAboutUs, ModelState.IsValid, MessageType.RetrieveSuccessfully, MessageType.NoDataFound, MessageType.SystemProblem);

    [HttpPost("get/features")]
    public async Task<StandardApiResponse> GetFeatures() => await _ServiceCaller.ReturnObjectMethodsWithoutEntity<FeatureResponseVM>(_Caller.GetFeatures, ModelState.IsValid, MessageType.RetrieveSuccessfully, MessageType.NoDataFound, MessageType.SystemProblem);

    [HttpPost("get/achievement")]
    public async Task<StandardApiResponse> GetAchievement() => await _ServiceCaller.ReturnObjectMethodsWithoutEntity<AchievementResponseVM>(_Caller.GetAchievement, ModelState.IsValid, MessageType.RetrieveSuccessfully, MessageType.NoDataFound, MessageType.SystemProblem);

    [HttpPost("get/pricing-plans")]
    public async Task<StandardApiResponse> GetPricingPlans() => await _ServiceCaller.ReturnObjectMethodsWithoutEntity<PricingResponseVM>(_Caller.GetPricingPlans, ModelState.IsValid, MessageType.RetrieveSuccessfully, MessageType.NoDataFound, MessageType.SystemProblem);

    [HttpPost("get/call-action")]
    public async Task<StandardApiResponse> GetCallAction() => await _ServiceCaller.ReturnObjectMethodsWithoutEntity<CallActionResponseVM>(_Caller.GetCallAction, ModelState.IsValid, MessageType.RetrieveSuccessfully, MessageType.NoDataFound, MessageType.SystemProblem);

    [HttpPost("get/footer-menus")]
    public async Task<StandardApiResponse> GetFooterMenus() => await _ServiceCaller.ReturnObjectMethodsWithoutEntity<FooterMenusResponseVM>(_Caller.GetFooterMenus, ModelState.IsValid, MessageType.RetrieveSuccessfully, MessageType.NoDataFound, MessageType.SystemProblem);

    [HttpPost("get/setting")]
    public async Task<StandardApiResponse> GetSetting() => await _ServiceCaller.ReturnObjectMethodsWithoutEntity<SettingResponseVM>(_Caller.GetSetting, ModelState.IsValid, MessageType.RetrieveSuccessfully, MessageType.NoDataFound, MessageType.SystemProblem);
}
