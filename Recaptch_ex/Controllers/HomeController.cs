using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Recaptch_ex.Models;

namespace Recaptch_ex.Controllers
{
    public class HomeController : Controller
    {
        string strmessage = "";
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            
            if (RecaptchaWork(ref strmessage) == false)
            {
                ModelState.AddModelError("", strmessage);
                return View(model);
            }

            return View(model);
        }
        private bool RecaptchaWork(ref string strmessage)
        {
            bool t = false;

            var response = Request["g-recaptcha-response"];
            //secret that was generated in key value pair

            const string secret = "";
            var client = new WebClient();

            var reply =
        client.DownloadString(
            string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
        secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0)
                {
                    t = true;
                    return t;
                }

                //agar responser == false check for the erorr mesasge
                var erorr = captchaResponse.ErrorCodes[0].ToLower();
                switch (erorr)
                {
                    case ("missing-input-secret"):
                        //پارامتر مخفی گم شده است
                        strmessage = "خطایی رخ داده است، دوباره تلاش  کنید.";
                        break;
                    case ("invalid-input-secret"):
                        //پارامتر مخفی نامعتبر است یا ناقص است
                        strmessage = "خطایی رخ داده است، دوباره تلاش  کنید.";
                        break;
                    case ("missing-input-response"):
                        //پارامتر پاسخ گم شده است
                        strmessage = "خطایی رخ داده است، دوباره تلاش  کنید.";
                        break;
                    case ("invalid-input-response"):
                        //پارامتر پاسخ نامعتبر است یا ناقص است
                        strmessage = "خطایی رخ داده است، دوباره تلاش  کنید.";
                        break;
                    case ("bad-request"):
                        //درخواست نامعتبر است یا ناقص است
                        strmessage = "خطایی رخ داده است، دوباره تلاش  کنید.";
                        break;

                    default:

                        strmessage = "خطایی رخ داده، اینترنت خود را چک کنید و دوباره امتحان کنید.";
                        break;
                }
            }
            else
            {
                t = true;
                return t;
            }
            return false;

        }
    }
}