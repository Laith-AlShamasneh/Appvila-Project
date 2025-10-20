using Domain.Common;

namespace Service.Interface.SystemManagement;

public interface IHomeService<TController>
{
    Task<StandardServiceResponse> GetHeaderMenu();
    Task<StandardServiceResponse> GetAboutUs();
    Task<StandardServiceResponse> GetFeatures();
    Task<StandardServiceResponse> GetAchievement();
    Task<StandardServiceResponse> GetPricingPlans();
    Task<StandardServiceResponse> GetCallAction();
    Task<StandardServiceResponse> GetFooterMenus();
    Task<StandardServiceResponse> GetSetting();
}
