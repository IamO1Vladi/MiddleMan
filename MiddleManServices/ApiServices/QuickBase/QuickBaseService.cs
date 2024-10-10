using System.Text;
using MiddleManServices.ApiServices.QuickBase.Interfaces;
using MiddleManServices.ApiServices.QuickBase.ServiceModels;
using Newtonsoft.Json;

namespace MiddleManServices.ApiServices.QuickBase;

public class QuickBaseService:IQuickBaseService
{

    private readonly HttpClient httpClient;

    public QuickBaseService()
    {
        httpClient=new HttpClient();
    }

    public async Task SendGetInTouchMessage(GetInTouchServiceModel formInfo)
    {
       string qbRealmHostName= "vladimirbuilder.quickbase.com";
       string tableId = "buj25wk4r";
       string userToken = "b79g7m_qaib_0_bi8nk42bjcudk7btngw5dbw6uigd";

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
}