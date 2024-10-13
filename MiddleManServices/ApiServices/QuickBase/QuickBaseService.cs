using System.Text;
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

    public async Task SendGetInTouchMessage(GetInTouchServiceModel formInfo)
    {

       httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
       httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{qbRealmHostName}");


       List<Dictionary<string,Dictionary<string,string>>> trueData = new List<Dictionary<string, Dictionary<string, string>>>
       {
           new Dictionary<string, Dictionary<string, string>>()
           {
               {
                   "6", new Dictionary<string, string>()
                   {
                       { "value", formInfo.Name }
                   }

               },

               {
                   "7", new Dictionary<string, string>()
                   {
                       { "value", formInfo.Email }
                   }
               },

               {
                   "8", new Dictionary<string, string>()
                   {
                       { "value", formInfo.PhoneNumber }
                   }
               },

               {
                   "9", new Dictionary<string, string>()
                   {
                       { "value", formInfo.InitialMessage }
                   }
               },

               {
                   "10", new Dictionary<string, string>()
                   {
                       { "value", formInfo.ServiceType }
                   }
               }
               ,

               {
                   "11", new Dictionary<string, string>()
                   {
                       { "value", string.IsNullOrEmpty(formInfo.Industry)?"No value":formInfo.Industry  }
                   }
               }



           }


       };

       var payLoad = new
       {
           to = $"{tableId}",
           data = trueData
       };


        string jsonPayload = JsonConvert.SerializeObject(payLoad);

       StringContent contentPayload = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

       HttpResponseMessage response = await httpClient.PostAsync("https://api.quickbase.com/v1/records", contentPayload);
    }

    public async Task<List<InformationThumbnailServiceModel>> GetStaredInformationPosts()
    {

        QueryForRecordsRequestModel requestBody= new QueryForRecordsRequestModel
        {
            From = informationTableId,
            Select = new List<int>{6,7,14},
            Where ="{15.EX.1}", //This is QuickBases query language
            Skip = 0,
            Top = 0,
            CompareWithAppLocalTime = false
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
                Summary = post.Field14!.Value!
            };
        }).ToList();


        return result;
    }
}