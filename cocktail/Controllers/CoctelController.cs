using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;

namespace cocktail.Controllers
{
    public class CoctelController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CoctelController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        //    public ActionResult GetAll()
        //    {
        //        ML.Coctel resultCoctel = new ML.Coctel();
        //        resultCoctel.strDrinks = new List<Object>();
        //        string letra = "Coctel";
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(_configuration["WebApi"]);

        //            var responseTask = client.GetAsync("json/v1/1/search.php?s=margarita");
        //            responseTask.Wait();

        //            var result = responseTask.Result;

        //            if (result.IsSuccessStatusCode)
        //            {
        //                var readTask = result.Content.ReadAsAsync<ML.Result>();
        //                readTask.Wait();

        //                foreach (var resultItem in readTask.Result.Objects)
        //                {
        //                    ML.Coctel resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Coctel>(resultItem.ToString());
        //                    resultCoctel.strDrinks.Add(resultItemList);
        //                }
        //            }
        //        }
        //        return View(resultCoctel);
        //    }
        public IActionResult Getall()
        {
            ML.Result resultcoctel = new ML.Result();
            resultcoctel.Objects = new List<object>();
            
            using (var client = new HttpClient())

            {
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/");
                var responseTask = client.GetAsync("json/v1/1/search.php?s=margarita");
                responseTask.Wait();
                var resultAPI = responseTask.Result;
                if (resultAPI.IsSuccessStatusCode)
                {
                    var readTask = resultAPI.Content.ReadAsAsync<ML.Coctel>();
                    readTask.Wait();
                    foreach (var resultItem in readTask.Result.drinks)
                    {
                   ML.Coctel resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Coctel>(resultItem.ToString());
                        resultItemList.extension = ".png";
                        resultcoctel.Objects.Add(resultItemList);
                    }
                    
                }
                ML.Coctel coctel = new ML.Coctel();
                coctel.drinks = resultcoctel.Objects;

                return View(coctel);
            }
        }
        public IActionResult GetallDinamic(ML.Coctel coctele)
        {
            ML.Result resultcoctel = new ML.Result();
            resultcoctel.Objects = new List<object>();
            string letra = "Coctel";
            using (var client = new HttpClient())

            {
                client.BaseAddress = new Uri("https://www.thecocktaildb.com/api/");
                var responseTask = client.GetAsync("json/v1/1/search.php?s=a");
                responseTask.Wait();
                var resultAPI = responseTask.Result;
                if (resultAPI.IsSuccessStatusCode)
                {
                    var readTask = resultAPI.Content.ReadAsAsync<dynamic>();
                    readTask.Wait();
                    foreach (dynamic resultItem in readTask.Result.drinks)
                    {
                        //ML.Coctel resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Coctel>(resultItem);
                        ML.Coctel resultItemList = new ML.Coctel();
                        resultItemList.strDrink = resultItem.strDrink;
                        resultcoctel.Objects.Add(resultItemList);
                    }
                }
                return View(coctele);
            }
        }



    }
}
