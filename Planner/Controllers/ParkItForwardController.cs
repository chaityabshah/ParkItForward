using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Planner.Models.Database;
using Planner.Models.DTO;

namespace Planner.Controllers
{
    [DataContract]
    public class AccessTokenReponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "expires-in")]
        public string ExpiresIn { get; set; }
        [DataMember(Name = "token-type")]
        public string TokenType { get; set; }
    }

    public class VehiclesResponse
    {
        public Vehicles vehicles { get; set; }
    }

    public class Vehicles
    {
        public string size { get; set; }     // casing matters. If you use Size use DataMember(Name="size")] left as exercise (below in Vehicle class as well)
        public List<Vehicle> vehicle { get; set; }
    }

    public class VehicleResponse
    {
        public Models.DTO.Vehicle vehicle { get; set; } // casing matters
    }

    public class Vehicle
    {
        public string vin { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string manufacturer { get; set; }
        public string phone { get; set; }
        public string unitType { get; set; }
        public string onstarStatus { get; set; }
        public string primaryDriverId { get; set; }
        public string primaryDriverURL { get; set; }
        public string url { get; set; }
        public string isInPreActivation { get; set; }
    }


    [RoutePrefix("api")]
    public class ParkItForwardController : ApiController
    {
        [Route("register")]
        [HttpGet]
        public HttpResponseMessage Register(Register form)
        {
            var accessToken = GetAuthTokenAsync();

            if (!string.IsNullOrEmpty(accessToken))
            {
                //var vehiclesResponse = GetVehicles(accessToken);
                var vehicleResponse = GetVehicle("1G1PJ5S95B7000009", accessToken);
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(vehicleResponse)), StatusCode = HttpStatusCode.OK };

            }
            return new HttpResponseMessage { Content = new StringContent("Failure"), StatusCode = HttpStatusCode.BadRequest };
        }

        [Route("vehicles")]
        [HttpGet]
        public string GetVs()
        {
            var accessToken = GetAuthTokenAsync();
            if (!string.IsNullOrEmpty(accessToken))
            {
                var vehiclesResponse = GetVehicles(accessToken);
                //var vehicleResponse = GetVehicle(form.VIN, accessToken);
                return JsonConvert.SerializeObject(vehiclesResponse);

            }
            return "false";
            //return new HttpResponseMessage { Content = new StringContent("Failure"), StatusCode = HttpStatusCode.BadRequest };
        }

        [Route("leave")]
        [HttpPost]
        public HttpResponseMessage NeedToLeave(Register form)
        {
            var accessToken = GetAuthTokenAsync();
            if (!string.IsNullOrEmpty(accessToken))
            {
                //var vehiclesResponse = GetVehicles(accessToken);
                var vehicleResponse = GetVehicle(form.VIN, accessToken);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(vehicleResponse)), StatusCode = HttpStatusCode.OK };
            }
            return new HttpResponseMessage { Content = new StringContent("Failure"), StatusCode = HttpStatusCode.BadRequest };
        }

        [Route("park")]
        [HttpGet]
        public string NeedToPark()
        {
            var accessToken = GetAuthTokenAsync();
            if (!string.IsNullOrEmpty(accessToken))
            {
                //var vehiclesResponse = GetVehicles(accessToken);
                //var vehicleResponse = GetVehicle(form.VIN, accessToken);
                return JsonConvert.SerializeObject(GetAll());
                //return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(vehicleResponse)), StatusCode = HttpStatusCode.OK };
            }

            return JsonConvert.SerializeObject(GetAll());


            //return new HttpResponseMessage { Content = new StringContent("Failure"), StatusCode = HttpStatusCode.BadRequest };
        }

        public static IEnumerable<Location> GetAll()
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<Location>().Where(_ => _.Status == "ACTIVE").ToList();
            }
        }

        private VehicleResponse GetVehicle(string vin, string accessToken)
        {
            return Call<VehicleResponse>(string.Format("account/vehicles/{0}", vin), HttpMethod.Get, accessToken);
        }

        private VehicleResponse GetVehicleLocation(string vin, string accessToken)
        {
            return Call<VehicleResponse>("account/vehicles/" + vin + "/commands/location", HttpMethod.Post, accessToken);
        }

        private VehiclesResponse GetVehicles(string accessToken)
        {
            return Call<VehiclesResponse>("account/vehicles", HttpMethod.Get, accessToken);
        }

        private T Call<T>(string relativeUri, HttpMethod method, string accessToken)
        {
            var baseUrl = "https://developer.gm.com/api/v1/"; // trailing / is important!

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = Task.Run(async () => await client.GetAsync(relativeUri).ConfigureAwait(false)).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return default(T);
                }

                var result = Task.Run(async () => await response.Content.ReadAsAsync<T>()).Result;

                return result;
            }
        }


        private string GetAuthTokenAsync()
        {
            var authUrl = "https://developer.gm.com/api/v1/oauth/access_token";
            var clientId = "l7xxb1ace3b4c4454f1a9ec7a30b69c850f6"; // change it to your client id
            var clientSecret = "5d41035a5619413d8cebe6eb1c4bd9eb"; // change it to your client secret

            var auth = clientId + ":" + clientSecret;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(authUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes(auth)));

                HttpResponseMessage response = Task.Run(async () => await client.GetAsync("?grant_type=client_credentials").ConfigureAwait(false)).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                
                var result = Task.Run(async () => await response.Content.ReadAsAsync<AccessTokenReponse>()).Result;
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result.AccessToken;
                }
                //.AccessToken; // return accesstoken if not null (or else return null if result == null)
            }
        }
    }
}
