﻿using MiddleManServices.ApiServices.QuickBase.ResponseModels;
using MiddleManServices.ApiServices.QuickBase.ServiceModels;

namespace MiddleManServices.ApiServices.QuickBase.Interfaces;

public interface IQuickBaseService
{
    public Task SendGetInTouchMessage(GetInTouchServiceModel formInfo);

    public Task<List<InformationThumbnailServiceModel>> GetStaredInformationPosts();

    public Task<List<InformationThumbnailServiceModel>> GetInformationPostsBasedOnFilters(bool stared, string category,int page,int perPage);

    public Task<InformationSinglePostServiceModel> GetInformationSinglePost(string recordId);

    public Task UpdateSinglePostUserViews(string recordId,int currentViews);

    public Task<List<InformationThumbnailServiceModel>> GetMostViewedInformationPosts(int postToReturn);

    public Task<List<InformationThumbnailServiceModel>> GetInformationPostsBasedOnAKeyword(string keyword,int postToReturn);

    public Task<List<QueryForInformationPostImagesModel>> GetAllSinglePostImages(string recordId);

    public Task SubscribeCustomerToNewsLetter(SubscribeToNewsLetterServiceModel formInfo);

    public Task<string> GetDynamicSiteMap();
}