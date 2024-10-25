using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using MiddleManServices.ApiServices.QuickBase.Interfaces;
using MiddleManServices.ApiServices.QuickBase.RequestModels;
using MiddleManServices.ApiServices.QuickBase.ResponseModels;
using MiddleManServices.ApiServices.QuickBase.ServiceModels;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase;

public class QuickBaseService:IQuickBaseService
{

    private readonly HttpClient httpClient;
    private readonly string qbRealmHostName = "vladimirbuilder.quickbase.com";
    private readonly string tableId = "buj25wk4r";
    private readonly string userToken = "b79g7m_qaib_0_bi8nk42bjcudk7btngw5dbw6uigd";
    private readonly string informationTableId = "bukcr8zp3";

    public QuickBaseService()
    {
        httpClient=new HttpClient();
    }

    //public async Task SendGetInTouchMessage(GetInTouchServiceModel formInfo)
    //{

    //   httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
    //   httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{qbRealmHostName}");


    //   List<Dictionary<string,Dictionary<string,string>>> trueData = new List<Dictionary<string, Dictionary<string, string>>>
    //   {
    //       new Dictionary<string, Dictionary<string, string>>()
    //       {
    //           {
    //               "6", new Dictionary<string, string>()
    //               {
    //                   { "value", formInfo.Name }
    //               }

    //           },

    //           {
    //               "7", new Dictionary<string, string>()
    //               {
    //                   { "value", formInfo.Email }
    //               }
    //           },

    //           {
    //               "8", new Dictionary<string, string>()
    //               {
    //                   { "value", formInfo.PhoneNumber }
    //               }
    //           },

    //           {
    //               "9", new Dictionary<string, string>()
    //               {
    //                   { "value", formInfo.InitialMessage }
    //               }
    //           },

    //           {
    //               "10", new Dictionary<string, string>()
    //               {
    //                   { "value", formInfo.ServiceType }
    //               }
    //           }
    //           ,

    //           {
    //               "11", new Dictionary<string, string>()
    //               {
    //                   { "value", string.IsNullOrEmpty(formInfo.Industry)?"No value":formInfo.Industry  }
    //               }
    //           }



    //       }


    //   };

    //   var payLoad = new
    //   {
    //       to = $"{tableId}",
    //       data = trueData
    //   };


    //    string jsonPayload = JsonConvert.SerializeObject(payLoad);

    //   StringContent contentPayload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //   HttpResponseMessage response = await httpClient.PostAsync("https://api.quickbase.com/v1/records", contentPayload);
    //}

    public async Task SendGetInTouchMessage(GetInTouchServiceModel formInfo)
    {

        httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{qbRealmHostName}");

   
        
            var trueData = new List<Dictionary<string, Dictionary<string, object>>>
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

            if (formInfo.FileAttachment != null)
            {
                using MemoryStream memoryStream = new MemoryStream();
                await formInfo.FileAttachment.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                string file64String = Convert.ToBase64String(fileBytes);

            trueData[0].Add("12", new Dictionary<string, object> 
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
                to = $"{tableId}",
                data = trueData
            };

            string jsonPayload = JsonConvert.SerializeObject(payLoad);

            StringContent contentPayload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

             

            HttpResponseMessage response = await httpClient.PostAsync("https://api.quickbase.com/v1/records", contentPayload);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error uploading file: {response.ReasonPhrase}");
            }
        
    }

    public async Task<List<InformationThumbnailServiceModel>> GetStaredInformationPosts()
    {

        QueryForRecordsRequestModel requestBody= new QueryForRecordsRequestModel
        {
            From = informationTableId,
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
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{qbRealmHostName}");

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

            string link = post.Field6!.Value["url"];
            string recordId = link.Split("/")[3];
            string fieldId= link.Split("/")[4];
            string versionId= link.Split("/")[5];
            string validLink = $"https://{qbRealmHostName}/up/{informationTableId}/a/r{recordId}/e{fieldId}/v{versionId}";

            return new InformationThumbnailServiceModel
            {

                ThumbnailImageLink = validLink,
                Topic = post.Field7!.Value!,
                Summary = post.Field14!.Value!,
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
        HttpResponseMessage response = await httpClient.PostAsync($"https://{qbRealmHostName}/db/{tableId}?API_UploadFile", content);

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


    public async Task<List<InformationThumbnailServiceModel>> GetInformationPostsBasedOnFilters(bool stared, string category, string recordId, int page,int perPage)
    {

        QueryForRecordsRequestModel requestBody = new QueryForRecordsRequestModel
        {
            From = informationTableId,
            Select = new List<int> {3, 6, 7, 14 },
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
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{qbRealmHostName}");

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

            string link = post.Field6!.Value["url"];
            string recordId = link.Split("/")[3];
            string fieldId = link.Split("/")[4];
            string versionId = link.Split("/")[5];
            string validLink = $"https://{qbRealmHostName}/up/{informationTableId}/a/r{recordId}/e{fieldId}/v{versionId}";

            return new InformationThumbnailServiceModel
            {

                ThumbnailImageLink = validLink,
                Topic = post.Field7!.Value!,
                Summary = post.Field14!.Value!,
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
            From = informationTableId,
            Select = new List<int> {6, 7, 8, 9, 10, 11, 14, 16,17 },
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
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{qbRealmHostName}");

        StringContent contentPayload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await httpClient.PostAsync("https://api.quickbase.com/v1/records/query", contentPayload);

        if (!response.IsSuccessStatusCode)
        {
            // Handle the failure case
            throw new Exception($"Failed to retrieve records: {response.StatusCode}");
        }

        string jsonResponse = await response.Content.ReadAsStringAsync();
        QueryForSingleInformationPostResponseModel apiResponse = JsonConvert.DeserializeObject<QueryForSingleInformationPostResponseModel>(jsonResponse);

        List<InformationSinglePostServiceModel> result = apiResponse.Data.Select(post => new InformationSinglePostServiceModel
            
        {
            Category = post.Field8!.Value,
            FirstParagraph = post.Field9!.Value!,
            SecondParagraph = post.Field10!.Value!,
            HeaderImageUrl = GenerateValidQuickBaseImageLink((string)post.Field16!.Value!["url"]),
            SectionImageUrl = GenerateValidQuickBaseImageLink((string)post.Field11!.Value!["url"]),
            Topic = post.Field7!.Value!,
            PostViews = (int)post.Field17!.Value!
        }).ToList();

        return result.First();
    }

    public async Task UpdateSinglePostUserViews(string recordId,int currentViews)
    {

        var trueData = new List<Dictionary<string, Dictionary<string, object>>>
            {
                new Dictionary<string, Dictionary<string, object>>
                {
                    {"3", new Dictionary<string, object>{{"value",recordId}}},
                    { "17", new Dictionary<string, object> { { "value", currentViews+1 } } }

                }
            };


        var payLoad = new
        {
            to = $"{informationTableId}",
            data = trueData
        };

        string jsonPayload = JsonConvert.SerializeObject(payLoad);

        StringContent contentPayload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");



        HttpResponseMessage response = await httpClient.PostAsync("https://api.quickbase.com/v1/records", contentPayload);

    }

    public async Task<List<InformationThumbnailServiceModel>> GetMostViewedInformationPosts(int postToReturn)
    {
        QueryForRecordsRequestModel requestBody = new QueryForRecordsRequestModel
        {
            From = informationTableId,
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
        httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{qbRealmHostName}");

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

            string link = post.Field6!.Value["url"];
            string recordId = link.Split("/")[3];
            string fieldId = link.Split("/")[4];
            string versionId = link.Split("/")[5];
            string validLink = $"https://{qbRealmHostName}/up/{informationTableId}/a/r{recordId}/e{fieldId}/v{versionId}";

            return new InformationThumbnailServiceModel
            {

                ThumbnailImageLink = validLink,
                Topic = post.Field7!.Value!,
                Summary = post.Field14!.Value!,
                Metadata = apiResponse.Metadata,
                RecordId = recordId
            };
        }).ToList();


        return result;
    }

    private string GenerateValidQuickBaseImageLink(string url)
    {
        string recordId = url.Split("/")[3];
        string fieldId = url.Split("/")[4];
        string versionId = url.Split("/")[5];
        string validLink = $"https://{qbRealmHostName}/up/{informationTableId}/a/r{recordId}/e{fieldId}/v{versionId}";

        return validLink;
    }
}