using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MachineaCafe.Models;
using Newtonsoft.Json;

namespace MachineaCafe.Controllers
{
    public class Machine_CafeController : Controller
    {
        // GET: Machine_Cafe
        string Baseurl = "http://localhost:51649/";
        public ActionResult Index()
        {

            List<Boisson> BoissonInfo = new List<Boisson>();
            Boisson bn = new Boisson();
           
                    bn.ChoixList = GetBoissonListItemsFromHttpClient();
            
                MachineCafe mc = new MachineCafe();
                mc.Boisson = bn;
                ViewBag.QteSucre = GetQtesucreListItems();
                ViewBag.Mug = GetMugListItems();

                return View(mc);
            
            //using (var BoissonHttpClient = new HttpClient())
            //{
            //    //Passing service base url  
            //    BoissonHttpClient.BaseAddress = new Uri(Baseurl);

            //    BoissonHttpClient.DefaultRequestHeaders.Clear();
            //    //Define request data format  
            //    BoissonHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
            //    HttpResponseMessage Res = await BoissonHttpClient.GetAsync("api/Boisson");

            //    //Checking the response is successful or not which is sent using HttpClient  
            //    if (Res.IsSuccessStatusCode)
            //    {
            //        //Storing the response details recieved from web api   
            //        var APIResponse = Res.Content.ReadAsStringAsync().Result;

            //        //Deserializing the response recieved from web api and storing into the Employee list  
            //        BoissonInfo = JsonConvert.DeserializeObject<List<Boisson>>(APIResponse);

            //        bn.ChoixList = from boissoninfo in BoissonInfo
            //                       select new SelectListItem
            //                       {
            //                           Text = boissoninfo.lib_Boisson.ToString(),
            //                           Value = ((int)boissoninfo.id).ToString()
            //                       };

            //    }
            //    //*****************************************
            //    MachineCafe mc = new MachineCafe();
            //    mc.Boisson = bn;
            //    // mc.Mug = 0;
            //    //mc.Qtesucre = 0;

            //    ViewBag.QteSucre = GetQtesucreListItems();
            //    ViewBag.Mug = GetMugListItems();

            //    return View(mc);
            //}
        }
        public IEnumerable<SelectListItem> GetBoissonListItemsFromHttpClient()
            {
            List<Boisson> BoissonInfo = new List<Boisson>();
            Boisson bn = new Boisson();
            using (var BoissonHttpClient = new HttpClient())
            {
                //Passing service base url  
                BoissonHttpClient.BaseAddress = new Uri(Baseurl);

        BoissonHttpClient.DefaultRequestHeaders.Clear(); 
                //Define request data format  
                BoissonHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource  using HttpClient  
                HttpResponseMessage Res =  BoissonHttpClient.GetAsync("api/Boisson").Result;

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode) 
                {
                    //Storing the response details recieved from web api   
                    var APIResponse = Res.Content.ReadAsStringAsync().Result;

        //Deserializing the response recieved from web api and storing into the boisson list  
        BoissonInfo = JsonConvert.DeserializeObject<List<Boisson>>(APIResponse);

                    bn.ChoixList = from boissoninfo in BoissonInfo     
                                   select new SelectListItem
                                   {
                                       Text = boissoninfo.lib_Boisson.ToString(),
                                       Value = ((int) boissoninfo.id).ToString()
                                   };

}
                return bn.ChoixList; 
            }
}
         [HttpPost]
        public ActionResult Index(MachineCafe machinecafe)
        {
             machinecafe.Boisson.ChoixList = GetBoissonListItemsFromHttpClient();
            Boisson boisson = machinecafe.Boisson;
            int mug = machinecafe.Mug;
            int qtesucre = machinecafe.Qtesucre;
            string nom_boisson="";
            ViewBag.QteSucre = GetQtesucreListItems();
            ViewBag.Mug = GetMugListItems(); 
            if (boisson.id==1) { nom_boisson = "Cafe"; }else if (boisson.id == 2) { nom_boisson = "The"; } else { nom_boisson = "Chocolat"; }
            ViewBag.commande = "Vous avez commander un "+ nom_boisson + " avec " + qtesucre.ToString()+" Morceaux de sucre et "+(mug == 0 ? "Vous n'avez pas utilisé votre propre mug" : "Vous avez utilisé votre propre mug");
            return View(machinecafe);
        }
        public IEnumerable<SelectListItem> GetMugListItems()
        {
            return new List<SelectListItem>{
                    new SelectListItem{
                Text="Oui",
                Value = "1"
            },
                     new SelectListItem{
                Text="Non",
                Value = "0"
            } };
        }
        public IEnumerable<SelectListItem> GetQtesucreListItems()
        {
            return new List<SelectListItem>{
                    new SelectListItem{
                Text="0",
                Value = "0"
            },
                     new SelectListItem{
                Text="1",
                Value = "1"
            },
                       new SelectListItem{
                Text="2",
                Value = "2"
            },
            new SelectListItem{
                Text="3",
                Value = "3"
            }};
        }


    }
}
