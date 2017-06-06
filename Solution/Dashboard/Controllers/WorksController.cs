using Newtonsoft.Json;
using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class WorksController : Controller
    {
        //
        // GET: /Works/

        public ActionResult Index()
        {
            try
            {
                #region load data
                List<Work> works = new List<Work>();
                if (Session["works"] == null)
                {
                    using (StreamReader r = new StreamReader(Server.MapPath("~/Data/work.json")))
                    {
                        string json = r.ReadToEnd();
                        works = JsonConvert.DeserializeObject<List<Work>>(json);
                        if (Session["works"] == null)
                        {
                            Session["works"] = works;
                        }
                    }
                }
                else
                {
                    works = (List<Work>)Session["works"];
                }
                #endregion                
            }
            catch (Exception ex)
            {
                ViewBag.MsgOK = "";
                ViewBag.MsgErrore = "Error occurs!";
            }
            return View();
        }

        public JsonResult GetData(Int32 Page = 1, Int32 Per_page = 15, String Sort_by = "submitDateTime", String Order = "asc", String Filter = "")
        {
            GridResult worksGrd = new GridResult();
            
            try
            {
                List<Work> works = (List<Work>)Session["works"];

                #region Filer                  
                if (!String.IsNullOrEmpty(Filter))
                {
                    if (Filter.Split('|').FirstOrDefault(x => x.StartsWith("SUB=")).Substring("SUB=".Length) != "")
                    {
                        string subject = Filter.Split('|').FirstOrDefault(x => x.StartsWith("SUB=")).Substring("SUB=".Length);
                        works = works.Where(x => x.subject.ToLower().Contains(subject)).ToList();
                    }
                    if (Filter.Split('|').FirstOrDefault(x => x.StartsWith("USR=")).Substring("USR=".Length) != "")
                    {
                        string usrID = Filter.Split('|').FirstOrDefault(x => x.StartsWith("USR=")).Substring("USR=".Length);
                        works = works.Where(x => x.userId.ToString().Contains(usrID)).ToList();
                    }                    
                }
                #endregion

                #region Order    
                switch (Sort_by)
                {
                    case "submitDateTime":
                        if (Order== "asc")
                            works = works.OrderBy(x => x.submitDateTime).ToList();
                        else
                            works = works.OrderByDescending(x => x.submitDateTime).ToList();
                        break;
                    case "correct":
                        if (Order == "asc")
                            works = works.OrderBy(x => x.correct).ToList();
                        else
                            works = works.OrderByDescending(x => x.correct).ToList();
                        break;
                    case "userId":
                        if (Order == "asc")
                            works = works.OrderBy(x => x.userId).ToList();
                        else
                            works = works.OrderByDescending(x => x.userId).ToList();
                        break;
                    case "difficulty":
                        if (Order == "asc")
                            works = works.OrderBy(x => x.difficulty).ToList();
                        else
                            works = works.OrderByDescending(x => x.difficulty).ToList();
                        break;                    
                }
                #endregion

                worksGrd.NumTotWorks = works.Count();
                worksGrd.NumTotPages =  (works.Count / Per_page) + (works.Count % Per_page > 0 ? 1 : 0);
                worksGrd.Works = works.Skip((Per_page * (Page == 1 ? 0 : Page - 1))).Take(Per_page).ToList();
            }
            catch (Exception ex)
            {               
                ViewBag.MsgOK = "";
                ViewBag.MsgErrore = "Error occurs!";
            }
            return Json(worksGrd, JsonRequestBehavior.AllowGet);
        }

    }
}
