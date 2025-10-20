using Domain.Common;
using Domain.Entities.SystemManagement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.ViewModels.SystemManagement.Response;
using Repository.UnitWork;
using Service.Constants;
using Service.Helpers;
using Service.Interface.SystemManagement;
using static Service.Helpers.CommonProperties;

namespace Service.Services.SystemManagement;

internal class HomeService<TController> : HandlerWithUnitOfWorkAndHost<TController>, IHomeService<TController>
{
    private readonly string _sharedImagesFolder;
    public HomeService(
        ILogger<TController> logger,
        IUnitOfWork unitOfWork,
        IConfiguration configuration,
        IHostingEnvironment hosting,
        IUserContext userContext
    ) : base(logger, unitOfWork, configuration, hosting, userContext)
    {
        _sharedImagesFolder = $"{ServiceConstants.AssetsPath}\\{FolderPathNameDictionary[FolderPathName.Shared]}";
    }

    public async Task<StandardServiceResponse> GetAboutUs()
    {
        var dbResult = await _unitOfWork.MasterAboutUs.GetById(new MasterAboutUs { MasterAboutUsId = 1 });

        var serviceResult = new AboutUsResponseVM
        {
            Id = dbResult.MasterAboutUsId,
            Title = dbResult.Title,
            Description = dbResult.Description,
            HasAppStoreButton = dbResult.HasAppStoreButton,
            AppStoreButtonText = dbResult.AppStoreButtonText,
            AppStoreButtonIconClass = dbResult.AppStoreButtonIconClass,
            AppStoreButtonLinkUrl = dbResult.AppStoreButtonLinkUrl,
            HasGooglePlayButton = dbResult.HasGooglePlayButton,
            GooglePlayButtonText = dbResult.GooglePlayButtonText,
            GooglePlayButtonIconClass = dbResult.GooglePlayButtonIconClass,
            GooglePlayButtonLinkUrl = dbResult.GooglePlayButtonLinkUrl,
            ImageUrl = dbResult.ImageUrl is not null ? StorageUtility.GetSingleFileUrlFromFolder(_hostEnvironment.WebRootPath, ServiceConstants.BaseUrl, _sharedImagesFolder, dbResult.ImageUrl) : null
        };

        serviceResponse.IsValide = true;
        serviceResponse.Data = serviceResult;
        return serviceResponse;
    }

    public async Task<StandardServiceResponse> GetAchievement()
    {
        var dbResult = await _unitOfWork.MasterAchievement.GetById(new MasterAchievement { MasterAchievementId = 1 });

        var serviceResult = new AchievementResponseVM
        {
            Id = dbResult.MasterAchievementId,
            Title = dbResult.Title,
            Description = dbResult.Description,
            SatisfactionValue = $"{dbResult.SatisfactionValue}%",
            HapyUserValue = $"{dbResult.HapyUserValue}K",
            DownloadValue = $"{dbResult.DownloadValue}+"
        };

        serviceResponse.IsValide = true;
        serviceResponse.Data = serviceResult;
        return serviceResponse;
    }

    public async Task<StandardServiceResponse> GetCallAction()
    {
        var dbResult = await _unitOfWork.MasterCallAction.GetById(new MasterCallAction { MasterCallActionId = 1 });

        var serviceResult = new CallActionResponseVM
        {
            Id = dbResult.MasterCallActionId,
            Title = dbResult.Title,
            Description = dbResult.Description,
            HasButton = dbResult.HasButton,
            ButtonText = dbResult.ButtonText
        };

        serviceResponse.IsValide = true;
        serviceResponse.Data = serviceResult;
        return serviceResponse;
    }

    public async Task<StandardServiceResponse> GetFeatures()
    {
        var featureDbResult = await _unitOfWork.MasterFeature.GetById(new MasterFeature { MasterFeatureId = 2 });

        var featureItemsDbResult = await _unitOfWork.MasterFeatureItem.GetByFeature(new MasterFeatureItem { MasterFeatureId = featureDbResult.MasterFeatureId });

        var serviceResult = new FeatureResponseVM
        {
            Id = featureDbResult.MasterFeatureId,
            Title = featureDbResult.Title,
            Brief = featureDbResult.Brief,
            Description = featureDbResult.Description,
            Items = featureItemsDbResult.Select(s => new FeatureItemVM
            {
                Id = s.MasterFeatureItemId,
                Title = s.Title,
                Description = s.Description,
                IconClass = s.IconClass
            }).ToList()
        };

        serviceResponse.IsValide = true;
        serviceResponse.Data = serviceResult;
        return serviceResponse;
    }

    public async Task<StandardServiceResponse> GetFooterMenus()
    {
            var solutionTask = _unitOfWork.MasterSolution.GetList(new MasterSolution());
            var supportTask = _unitOfWork.MasterSupport.GetList(new MasterSupport());
            var companyTask = _unitOfWork.MasterCompany.GetList(new MasterCompany());
            var legalTask = _unitOfWork.MasterLegal.GetList(new MasterLegal());

            await Task.WhenAll(solutionTask, supportTask, companyTask, legalTask);

            var solutionDbResult = solutionTask.Result;
            var supportDbResult = supportTask.Result;
            var companyDbResult = companyTask.Result;
            var legalDbResult = legalTask.Result;

            var serviceResult = new FooterMenusResponseVM
            {
                Solutions = solutionDbResult.Select(x => new LookupItemVM
                {
                    Id = x.MasterSolutionId,
                    Text = x.Text,
                    LinkUrl = x.LinkUrl
                }),

                Support = supportDbResult.Select(x => new LookupItemVM
                {
                    Id = x.MasterSupportId,
                    Text = x.Text,
                    LinkUrl = x.LinkUrl
                }),

                Company = companyDbResult.Select(x => new LookupItemVM
                {
                    Id = x.MasterCompanyId,
                    Text = x.Text,
                    LinkUrl = x.LinkUrl
                }),

                Legal = legalDbResult.Select(x => new LookupItemVM
                {
                    Id = x.MasterLegalId,
                    Text = x.Text,
                    LinkUrl = x.LinkUrl
                })
            };

        serviceResponse.IsValide = true;
        serviceResponse.Data = serviceResult;
        return serviceResponse;
    }

    public async Task<StandardServiceResponse> GetHeaderMenu()
    {
        var dbResult = await _unitOfWork.MasterHeaderMenu.GetList(new MasterHeaderMenu());

        var serviceResult = dbResult.Select(s => new HeaderMenuResponseVM
        {
            Id = s.MasterHeaderMenuId,
            Title = s.Title,
            Order = s.Order,
            LinkUrl = s.LinkUrl,
            Tooltip = s.Tooltip
        }).ToList();

        serviceResponse.IsValide = true;
        serviceResponse.Data = serviceResult;
        return serviceResponse;
    }

    public async Task<StandardServiceResponse> GetPricingPlans()
    {
        var dbPricingResult = await _unitOfWork.MasterPricing.GetById(new MasterPricing { MasterPricingId = 1 });

        var dbPricingItemsResult = await _unitOfWork.MasterPricingItem
            .GetByPricing(new MasterPricingItem { MasterPricingId = dbPricingResult.MasterPricingId });

        var pricingItemsList = new List<PricingItemVM>();

        foreach (var item in dbPricingItemsResult)
        {
            var dbPricingItemFeaturesResult = await _unitOfWork.MasterPricingItemFeature
                .GetByPricingItem(new MasterPricingItemFeature { MasterPricingItemId = item.MasterPricingItemId });

            var pricingItemFeaturesList = dbPricingItemFeaturesResult.Select(f => new PricingItemFeatureVM
            {
                Id = f.MasterPricingItemFeatureId,
                Title = f.Title
            }).ToList();

            pricingItemsList.Add(new PricingItemVM
            {
                Id = item.MasterPricingItemId,
                Title = item.Title,
                Description = item.Description,
                PricePerMonth = item.PricePerMonth,
                PricePerYear = item.PricePerYear,
                ButtonText = item.ButtonText,
                LabelText = item.LabelText,
                Features = pricingItemFeaturesList
            });
        }

        var serviceResult = new PricingResponseVM
        {
            Id = dbPricingResult.MasterPricingId,
            Title = dbPricingResult.Title ?? string.Empty,
            Brief = dbPricingResult.Brief ?? string.Empty,
            Description = dbPricingResult.Description,
            Items = pricingItemsList
        };

        serviceResponse.IsValide = true;
        serviceResponse.Data = serviceResult;
        return serviceResponse;
    }

    public async Task<StandardServiceResponse> GetSetting()
    {
        var dbResult = await _unitOfWork.MasterSetting.GetById(new MasterSetting { MasterSettingId = 1 });

        var serviceResult = new SettingResponseVM
        {
            Id = dbResult.MasterSettingId,
            HeaderLogoUrl = StorageUtility.GetSingleFileUrlFromFolder(_hostEnvironment.WebRootPath, ServiceConstants.BaseUrl, _sharedImagesFolder, dbResult.HeaderLogoUrl)!,
            FooterLogoUrl = StorageUtility.GetSingleFileUrlFromFolder(_hostEnvironment.WebRootPath, ServiceConstants.BaseUrl, _sharedImagesFolder, dbResult.FooterLogoUrl)!,
            FooterDescription = dbResult.FooterDescription,
            DesignedBy = dbResult.DesignedBy
        };

        serviceResponse.IsValide = true;
        serviceResponse.Data = serviceResult;
        return serviceResponse;
    }
}
