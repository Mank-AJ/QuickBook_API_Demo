
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

using static QBOApiCallDemo.Models.InvoiceModel;
//using Document = System.Reflection.Metadata.Document;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;
//using System.IO;


namespace QBOApiCallDemo.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: InvoiceController
        public static int minorversion=65;
        public static long CompanyId= 4620816365303325910;
        public async Task<ActionResult> Index()
        {
            string AccessToken = Request.Cookies["AccessTokenResponse"];
            if (AccessToken != null)
            {

                //    //string accessToken, string refreshToken, int expiresIn
                var options = new RestClientOptions("https://sandbox-quickbooks.api.intuit.com")
            {
                MaxTimeout = -1,
            };
                var client = new RestClient(options);
                var request = new RestRequest("/v3/company/" + CompanyId + "/query?minorversion="+minorversion, Method.Post);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/text");
                request.AddHeader("Authorization", "Bearer " + AccessToken);
                var body = @"select * from invoice startposition 1 maxresults 5";
                request.AddParameter("application/text", body, ParameterType.RequestBody);
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var responseData = response.Content;
                    Root InvoiceResponse = JsonConvert.DeserializeObject<Root>(responseData);
                  
                    return View(InvoiceResponse);
                }
                else
                {
                    Response.Cookies.Append("AccessTokenResponse", "");
                    return RedirectToAction("InitiateAuth", "Home", new { submitButton = "Connect to QuickBooks" });
                    // Handle the case when the API call is not successful
                    // Log or display the response error message
                    string errorMessage = response.ErrorMessage;
                    // ...
                }
                //Console.WriteLine(response.Content);
                //Invoice invoiceResponse = new ResponseCacheAttribute();

            }
        
            return View(null);
        }

        public async Task<ActionResult> Details(int? rowcount)
        {
            string AccessToken = Request.Cookies["AccessTokenResponse"];
            if (AccessToken != null)
            {

                //    //string accessToken, string refreshToken, int expiresIn
                var options = new RestClientOptions("https://sandbox-quickbooks.api.intuit.com")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/v3/company/" + CompanyId + "/query?minorversion=" + minorversion, Method.Post);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/text");
                request.AddHeader("Authorization", "Bearer " + AccessToken);
                var body = @"select * from invoice startposition 1 maxresults 5";
                request.AddParameter("application/text", body, ParameterType.RequestBody);
                RestResponse response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var responseData = response.Content;
                    Root InvoiceResponse = JsonConvert.DeserializeObject<Root>(responseData);
                    InvoiceResponse.rowcount = rowcount;
                    return View(InvoiceResponse);
                }
                else
                {
                    Response.Cookies.Append("AccessTokenResponse", "");
                    return RedirectToAction("InitiateAuth", "Home", new { submitButton = "Connect to QuickBooks" });
                    // Handle the case when the API call is not successful
                    // Log or display the response error message
                    string errorMessage = response.ErrorMessage;
                    // ...
                }
                //Console.WriteLine(response.Content);
                //Invoice invoiceResponse = new ResponseCacheAttribute();

            }

            return View(null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            Response.Cookies.Append("AccessTokenResponse", "");
            return RedirectToAction("Index", "Home");
           
        }

        [HttpPost]
      
        public FileResult GenratePDF(HtmlModel GridHtml)
        {
            using (MemoryStream stream = new MemoryStream())
            {


                return File(stream.ToArray(), "application/pdf", "SignOffSheet.pdf");
            }
        }

}
}
