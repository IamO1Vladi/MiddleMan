using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using MiddleMan.Common.Constants;
using MiddleMan.Common.Utilities;
using MiddleManServices.ApiServices.QuickBase.Interfaces;
using MiddleManServices.ApiServices.QuickBase.RequestModels;
using MiddleManServices.ApiServices.QuickBase.ResponseModels;
using MiddleManServices.ApiServices.QuickBase.ServiceModels;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase;

public class QuickBaseService:IQuickBaseService
{

    private readonly HttpClient httpClient;
    private readonly string userToken = Environment.GetEnvironmentVariable("UserToken")!;

    public QuickBaseService()
    {
        httpClient=new HttpClient();
    }

    public async Task SendGetInTouchMessage(GetInTouchServiceModel formInfo)
    {

        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{QuickBaseApiConstants.QbRealmHostName}");


        // This will generate a jSON payload in the form the quickbases API requires ->  https://developer.quickbase.com/operation/upsert
        List<Dictionary<string,Dictionary<string,object>>> payloadData = new List<Dictionary<string, Dictionary<string, object>>>
            {
                new Dictionary<string, Dictionary<string, object>>
                {
                    {"6", new Dictionary<string, object>{{"value",formInfo.Name}}},
                    { "7", new Dictionary<string, object> { { "value", formInfo.Email } } },
                    { "8", new Dictionary<string, object> { { "value", formInfo.PhoneNumber } } },
                    { "9", new Dictionary<string, object> { { "value", formInfo.InitialMessage } } },
                    { "10", new Dictionary<string, object> { { "value", formInfo.ServiceType } } },
                    { "11", new Dictionary<string, object> { { "value", string.IsNullOrEmpty(formInfo.Industry) ? "No value" : formInfo.Industry } } }
                    
                }
            };

        //If there is a file attachment this will create a 64bitString for it and add it  
            if (formInfo.FileAttachment != null)
            {
                using MemoryStream memoryStream = new MemoryStream();
                await formInfo.FileAttachment.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                string file64String = Convert.ToBase64String(fileBytes);

            payloadData[0].Add("12", new Dictionary<string, object> 
            {
                { "value", new Dictionary<string, string>
                    {
                        { "fileName", formInfo.FileAttachment.FileName },
                        { "data", file64String }
                    }
                }
            });
            }

            var payLoad = new
            {
                to = $"{QuickBaseApiConstants.CustomersTableId}",
                data = payloadData
            };

            string jsonPayload = JsonConvert.SerializeObject(payLoad);

            StringContent contentPayload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

             

            HttpResponseMessage response = await httpClient.PostAsync(QuickBaseApiConstants.InsertOrUpdateRecordsEndpoint, contentPayload);


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.ReasonPhrase}");
            }
        
    }

    public async Task<List<InformationThumbnailServiceModel>> GetStaredInformationPosts()
    {

        QueryForRecordsRequestModel requestBody= new QueryForRecordsRequestModel
        {
            From = QuickBaseApiConstants.InformationTableId,
            Select = new List<int>{3,6,7,14},
            Where ="{15.EX.1}", //This is QuickBases query language
            Options = new QueryForDataOptionsModel
            {
                Skip = 0,
                Top = 0,
                CompareWithAppLocalTime = false
            }
            
        };

        string jsonRequest= JsonConvert.SerializeObject(requestBody);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{QuickBaseApiConstants.QbRealmHostName}");

        StringContent contentPayload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync("https://api.quickbase.com/v1/records/query", contentPayload);

        if (!response.IsSuccessStatusCode)
        {
            // Handle the failure case
            throw new Exception($"Failed to retrieve records: {response.StatusCode}");
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        QueryForRecordsResponseModel apiResponse = JsonConvert.DeserializeObject<QueryForRecordsResponseModel>(jsonResponse);


       

        List<InformationThumbnailServiceModel> result = apiResponse.Data.Select(post =>
        {

            string link = post.ThumbnailUrl!.Value["url"];
            string recordId = link.Split("/")[3];
            string fieldId= link.Split("/")[4];
            string versionId= link.Split("/")[5];
            string validLink = $"https://{QuickBaseApiConstants.QbRealmHostName}/up/{QuickBaseApiConstants.InformationTableId}/a/r{recordId}/e{fieldId}/v{versionId}";

            return new InformationThumbnailServiceModel
            {

                ThumbnailImageLink = validLink,
                Topic = post.Topic!.Value!,
                Summary = post.Summary!.Value!,
                RecordId = recordId
            };
        }).ToList();


        return result;
    }

    //Not needed currently, keeping in case RestAPi from QuickBase breaks
    public async Task UploadFileToQuickBase(IFormFile file, string recordId)
    {

        using MemoryStream memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream);
        byte[] fileBytes=memoryStream.ToArray();

        string file64String = Convert.ToBase64String(fileBytes);

        UploadAFileRequestModel requestPayload = new UploadAFileRequestModel
        {
            UserToken = userToken,
            RecordId = recordId,
            Field = new Field
            {
                FieldId = "12",
                FileName = file.FileName,
                Value = file64String,
            }
        };
        var emptyNamespaces = new XmlSerializerNamespaces();
        emptyNamespaces.Add("", "");

        StringWriter writer = new StringWriter();
        XmlSerializer serializer = new XmlSerializer(typeof(UploadAFileRequestModel));
        
        serializer.Serialize(writer,requestPayload,emptyNamespaces);

        string xmlRequest= writer.ToString();



        // Prepare the HttpClient and set necessary headers
        httpClient.DefaultRequestHeaders.Add("QUICKBASE-ACTION", "API_UploadFile");

        StringContent content = new StringContent(xmlRequest, Encoding.UTF8, "application/xml");

        // Send the POST request to the QuickBase API with the necessary parameters
        HttpResponseMessage response = await httpClient.PostAsync($"https://{QuickBaseApiConstants.QbRealmHostName}/db/{QuickBaseApiConstants.CustomersTableId}?API_UploadFile", content);

        // Handle the response
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("File uploaded successfully.");
        }
        else
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to upload file: {response.StatusCode}. Response: {responseContent}");
        }
    }


    public async Task<List<InformationThumbnailServiceModel>> GetInformationPostsBasedOnFilters(bool stared, string category, int page,int perPage)
    {

        QueryForRecordsRequestModel requestBody = new QueryForRecordsRequestModel
        {
            From = QuickBaseApiConstants.InformationTableId,
            Select = new List<int> {3, 6, 7, 14 },// These are the id's of the fields in QuickBase
            Where =  stared==true? "{15.EX.1}": !string.IsNullOrEmpty(category)?"{8.EX."+category+"}":"", //This is QuickBases query language}
            Options = new QueryForDataOptionsModel
            {
                Skip = page == 1 ? 0 : (page - 1) * perPage,
                Top = perPage,
                CompareWithAppLocalTime = false
            }
        };

        string jsonRequest = JsonConvert.SerializeObject(requestBody);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{QuickBaseApiConstants.QbRealmHostName}");

        StringContent contentPayload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await ApiUtilities.RetryAsync(()=>httpClient.PostAsync(QuickBaseApiConstants.QueryForRecordsEndpoint, contentPayload),QuickBaseApiConstants.MaxApiRetries,QuickBaseApiConstants.ApiRetryDelayMilliseconds);

        if (!response.IsSuccessStatusCode)
        {
            // Handle the failure case
            throw new Exception(string.Format(QuickBaseApiConstants.QueryForRecordsErrorMessage, response.StatusCode));
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        QueryForRecordsResponseModel? apiResponse = JsonConvert.DeserializeObject<QueryForRecordsResponseModel>(jsonResponse);



        List<InformationThumbnailServiceModel> result = apiResponse!.Data!.Select(post =>
        {

            string link = post.ThumbnailUrl!.Value["url"];
            string recordId = link.Split("/")[3];
            string imageLink =
                GenerateValidQuickBaseImageLink(link, QuickBaseApiConstants.InformationTableId);

            return new InformationThumbnailServiceModel
            {

                ThumbnailImageLink = imageLink,
                Topic = post.Topic!.Value!,
                Summary = post.Summary!.Value!,
                Metadata = apiResponse.Metadata,
                RecordId = recordId
            };
        }).ToList();


        return result;
    }

    public async Task<InformationSinglePostServiceModel> GetInformationSinglePost(string recordId)
    {
        QueryForRecordsRequestModel requestBody = new QueryForRecordsRequestModel
        {
            From = QuickBaseApiConstants.InformationTableId,  
            Select = new List<int> {6, 7, 8, 9, 10, 11, 14, 16,17,22 },
            Where = "{3.EX."+recordId+"}", //This is QuickBases query language}
            Options = new QueryForDataOptionsModel
            {
                Skip = 0,
                Top = 0,
                CompareWithAppLocalTime = false
            }
        };

        string jsonRequest = JsonConvert.SerializeObject(requestBody);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{QuickBaseApiConstants.QbRealmHostName}");

        StringContent contentPayload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await ApiUtilities.RetryAsync(()=> httpClient.PostAsync(QuickBaseApiConstants.QueryForRecordsEndpoint, contentPayload),QuickBaseApiConstants.MaxApiRetries,QuickBaseApiConstants.ApiRetryDelayMilliseconds);

        if (!response.IsSuccessStatusCode)
        {
            // Handle the failure case
            throw new Exception(string.Format(QuickBaseApiConstants.QueryForRecordsErrorMessage, response.StatusCode));
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        QueryForSingleInformationPostResponseModel? apiResponse = JsonConvert.DeserializeObject<QueryForSingleInformationPostResponseModel>(jsonResponse);

        List<QueryForInformationPostImagesModel> postImages = await GetAllSinglePostImages(recordId);

        List<InformationSinglePostServiceModel> result = apiResponse.Data.Select(post => new InformationSinglePostServiceModel
            
        {
            Category = post.Category!.Value,
            FirstParagraph = post.FirstParagraph!.Value!,
            SecondParagraph = post.SecondParagraph!.Value!,
            HeaderImageUrl = GenerateValidQuickBaseImageLink((string)post.HeaderImageUrl!.Value!["url"],QuickBaseApiConstants.InformationTableId),
            SectionImageUrl = GenerateValidQuickBaseImageLink((string)post.SectionImageUrl!.Value!["url"], QuickBaseApiConstants.InformationTableId),
            Topic = post.Topic!.Value!,
            PostViews = (int)post.PostViews!.Value!,
            PostImages = postImages.Select(image=>image.Url).ToList(),
            KeyWordsMetaTag = post.KeyWordsMetaTag!.Value!,
            Summary = post.Summary!.Value!
        }).ToList();

        return result.First();
    }

    public async Task UpdateSinglePostUserViews(string recordId,int currentViews)
    {

        List<Dictionary<string,Dictionary<string,object>>> trueData = new List<Dictionary<string, Dictionary<string, object>>>
            {
                new Dictionary<string, Dictionary<string, object>>
                {
                    {"3", new Dictionary<string, object>{{"value",recordId}}},
                    { "17", new Dictionary<string, object> { { "value", currentViews+1 } } }

                }
            };


        var payLoad = new
        {
            to = $"{QuickBaseApiConstants.InformationTableId}",
            data = trueData
        };

        string jsonPayload = JsonConvert.SerializeObject(payLoad);

        StringContent contentPayload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");



        HttpResponseMessage response = await httpClient.PostAsync(QuickBaseApiConstants.InsertOrUpdateRecordsEndpoint, contentPayload);

    }

    public async Task<List<InformationThumbnailServiceModel>> GetMostViewedInformationPosts(int postToReturn)
    {
        QueryForRecordsRequestModel requestBody = new QueryForRecordsRequestModel
        {
            From = QuickBaseApiConstants.InformationTableId,
            Select = new List<int> { 3, 6, 7, 14 },
            Where = "",
            SortBy = new List<QueryForDataSortBy>
            {
                new QueryForDataSortBy {FieldId = 17,Order = "DESC"}
            },
            Options = new QueryForDataOptionsModel
            {
                Skip = 0,
                Top = postToReturn,
                CompareWithAppLocalTime = false
            }
        };

        string jsonRequest = JsonConvert.SerializeObject(requestBody);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{QuickBaseApiConstants.QbRealmHostName}");

        StringContent contentPayload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await ApiUtilities.RetryAsync( ()=> httpClient.PostAsync(QuickBaseApiConstants.QueryForRecordsEndpoint, contentPayload),QuickBaseApiConstants.MaxApiRetries,QuickBaseApiConstants.ApiRetryDelayMilliseconds);

        if (!response.IsSuccessStatusCode)
        {
            // Handle the failure case
            throw new Exception(string.Format(QuickBaseApiConstants.QueryForRecordsErrorMessage, response.StatusCode));
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        QueryForRecordsResponseModel apiResponse = JsonConvert.DeserializeObject<QueryForRecordsResponseModel>(jsonResponse);



        List<InformationThumbnailServiceModel> result = apiResponse.Data.Select(post =>
        {

            string link = post.ThumbnailUrl!.Value["url"];
            string recordId = link.Split("/")[3];
            string imageUrl = GenerateValidQuickBaseImageLink(link, QuickBaseApiConstants.InformationTableId);

            return new InformationThumbnailServiceModel
            {

                ThumbnailImageLink = imageUrl,
                Topic = post.Topic!.Value!,
                Summary = post.Summary!.Value!,
                Metadata = apiResponse.Metadata,
                RecordId = recordId
            };
        }).ToList();


        return result;
    }

    public async Task<List<InformationThumbnailServiceModel>> GetInformationPostsBasedOnAKeyword(string keyword, int postToReturn)
    {
        QueryForRecordsRequestModel requestBody = new QueryForRecordsRequestModel
        {
            From = QuickBaseApiConstants.InformationTableId,
            Select = new List<int> {1, 3, 6, 7, 14 },
            Where = "{7.CT."+keyword.Substring(1)+"}",
            SortBy = new List<QueryForDataSortBy>
            {
                new QueryForDataSortBy {FieldId = 1,Order = "DESC"}
            },
            Options = new QueryForDataOptionsModel
            {
                Skip = 0,
                Top = postToReturn,
                CompareWithAppLocalTime = false
            }
        };

        string jsonRequest = JsonConvert.SerializeObject(requestBody);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{QuickBaseApiConstants.QbRealmHostName}");

        StringContent contentPayload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await ApiUtilities.RetryAsync(() => httpClient.PostAsync(QuickBaseApiConstants.QueryForRecordsEndpoint, contentPayload), QuickBaseApiConstants.MaxApiRetries, QuickBaseApiConstants.ApiRetryDelayMilliseconds);

        if (!response.IsSuccessStatusCode)
        {
            // Handle the failure case
            throw new Exception(string.Format(QuickBaseApiConstants.QueryForRecordsErrorMessage, response.StatusCode));
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        QueryForRecordsResponseModel apiResponse = JsonConvert.DeserializeObject<QueryForRecordsResponseModel>(jsonResponse);



        List<InformationThumbnailServiceModel> result = apiResponse.Data.Select(post =>
        {

            string link = post.ThumbnailUrl!.Value["url"];
            string recordId = link.Split("/")[3];
            string imageUrl = GenerateValidQuickBaseImageLink(link, QuickBaseApiConstants.InformationTableId);

            return new InformationThumbnailServiceModel
            {

                ThumbnailImageLink = imageUrl,
                Topic = post.Topic!.Value!,
                Summary = post.Summary!.Value!,
                Metadata = apiResponse.Metadata,
                RecordId = recordId
            };
        }).ToList();


        return result;
    }

    public async Task<List<QueryForInformationPostImagesModel>> GetAllSinglePostImages(string recordId)
    {
        QueryForRecordsRequestModel requestBody = new QueryForRecordsRequestModel
        {
            From = QuickBaseApiConstants.InformationImagesTableId,
            Select = new List<int> { 6 },
            Where = "{7.EX." + recordId + "}", //This is QuickBases query language
            Options = new QueryForDataOptionsModel
            {
                Skip = 0,
                Top = 0,
                CompareWithAppLocalTime = false
            }
        };

        string jsonRequest = JsonConvert.SerializeObject(requestBody);

        StringContent contentPayload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await ApiUtilities.RetryAsync(()=> httpClient.PostAsync(QuickBaseApiConstants.QueryForRecordsEndpoint, contentPayload),QuickBaseApiConstants.MaxApiRetries,QuickBaseApiConstants.ApiRetryDelayMilliseconds);

        if (!response.IsSuccessStatusCode)
        {
            // Handle the failure case
            throw new Exception(string.Format(QuickBaseApiConstants.QueryForRecordsErrorMessage, response.StatusCode));
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        QueryForSingleInformationPostResponseModel apiResponse = JsonConvert.DeserializeObject<QueryForSingleInformationPostResponseModel>(jsonResponse);



        List<QueryForInformationPostImagesModel> result = apiResponse.Data.Select(post =>

             new QueryForInformationPostImagesModel
            {
                FileName = post.Thumbnail!.Value!["versions"][0]["fileName"],
                Url = GenerateValidQuickBaseImageLink((string)post.Thumbnail!.Value!["url"],QuickBaseApiConstants.InformationImagesTableId),
            }
        ).ToList();

        return result;
    }

    public async Task SubscribeCustomerToNewsLetter(SubscribeToNewsLetterServiceModel formInfo)
    {
        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{QuickBaseApiConstants.QbRealmHostName}");



        List<Dictionary<string,Dictionary<string,object>>> trueData = new List<Dictionary<string, Dictionary<string, object>>>
            {
                new Dictionary<string, Dictionary<string, object>>
                {
                    {"6", new Dictionary<string, object>{{"value",formInfo.Name}}},
                    { "7", new Dictionary<string, object> { { "value", formInfo.Email } } },
                }
            };

        var payLoad = new
        {
            to = $"{QuickBaseApiConstants.SubscribedUsersTableId}",
            data = trueData
        };

        string jsonPayload = JsonConvert.SerializeObject(payLoad);

        StringContent contentPayload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");



        HttpResponseMessage response = await httpClient.PostAsync(QuickBaseApiConstants.InsertOrUpdateRecordsEndpoint, contentPayload);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(string.Format(QuickBaseApiConstants.ErrorSubscribingMessage, response.ReasonPhrase));
        }
    }

    public async Task<string> GetDynamicSiteMap()
    {
        QueryForRecordsRequestModel requestBody = new QueryForRecordsRequestModel
        {
            From = QuickBaseApiConstants.SiteMapTableId,
            Select = new List<int> { 6}
        };

        string jsonRequest = JsonConvert.SerializeObject(requestBody);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{QuickBaseApiConstants.QbRealmHostName}");

        StringContent contentPayload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await ApiUtilities.RetryAsync(() => httpClient.PostAsync(QuickBaseApiConstants.QueryForRecordsEndpoint, contentPayload), QuickBaseApiConstants.MaxApiRetries, QuickBaseApiConstants.ApiRetryDelayMilliseconds);

        if (!response.IsSuccessStatusCode)
        {
            // Handle the failure case
            throw new Exception(string.Format(QuickBaseApiConstants.QueryForRecordsErrorMessage, response.StatusCode));
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        SiteMapResponseModel apiResponse = JsonConvert.DeserializeObject<SiteMapResponseModel>(jsonResponse);



        string result = apiResponse!.Data[0].SiteMap.Value.ToString();


        return result!;
    }

    private string GenerateValidQuickBaseImageLink(string url,string tableId)
    {
        string recordId = url.Split("/")[3];
        string fieldId = url.Split("/")[4];
        string versionId = url.Split("/")[5];
        string validLink = $"https://{QuickBaseApiConstants.QbRealmHostName}/up/{tableId}/a/r{recordId}/e{fieldId}/v{versionId}";

        return validLink;
    }




}