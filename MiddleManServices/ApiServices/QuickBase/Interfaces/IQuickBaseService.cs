using MiddleManServices.ApiServices.QuickBase.ServiceModels;

namespace MiddleManServices.ApiServices.QuickBase.Interfaces;

public interface IQuickBaseService
{
    public Task SendGetInTouchMessage(GetInTouchServiceModel formInfo);

    public Task<List<InformationThumbnailServiceModel>> GetStaredInformationPosts();
}