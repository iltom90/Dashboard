using Newtonsoft.Json;
using Dashboard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeModel home = new HomeModel();
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

                home.lstUserID = works.Select(x => x.userId).Distinct().ToList();
            }
            catch (Exception ex)
            {
                ViewBag.MsgOK = "";
                ViewBag.MsgErrore = "Error occurs!";
            }
            return View(home);
        }

        public JsonResult GetAnswerStateChartData(string Subject)
        {
            JsonResult jsonResult = new JsonResult();           
            List<ChartResult> worksGraf = new List<ChartResult>();
            List<Work> works = (List<Work>)Session["works"];

            // load correct answer
            worksGraf.Add(new ChartResult()
            {
                dataora = DateTime.Now.ToString(),
                name = "Correct",
                tipo = "correct",
                y = works.Where(x => x.correct == 1 && x.subject == Subject).ToList().Count()
            });
            // load wrong answer
            worksGraf.Add(new ChartResult()
            {
                dataora = DateTime.Now.ToString(),
                name = "Wrong",
                tipo = "Wrong",
                y = works.Where(x => x.correct == 0 && x.subject == Subject).ToList().Count()
            });
            jsonResult = Json(worksGraf, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetUserIdChartData(string UserID)
        {
            JsonResult jsonResult = new JsonResult();
            if (UserID != "*" && !string.IsNullOrEmpty(UserID))
            {
                List<List<ChartResult>> worksGraf = new List<List<ChartResult>>();
                List<ChartResult> lstAnswer = new List<ChartResult>();
                List <Work> works = (List<Work>)Session["works"];
                // load correct answer for user
                foreach (string subject in works.Select(x => x.subject).Distinct().ToList())
                {
                    lstAnswer.Add(new ChartResult()
                    {
                        dataora = DateTime.Now.ToString(),
                        name = subject.ToString(),
                        tipo = subject.ToString(),
                        y = works.Where(x => x.subject == subject && x.correct==1 && x.userId == long.Parse(UserID)).Count()
                    });
                }
                worksGraf.Add(lstAnswer);
                // load wrong answer for user
                lstAnswer = new List<ChartResult>();
                foreach (string subject in works.Select(x => x.subject).Distinct().ToList())
                {
                    lstAnswer.Add(new ChartResult()
                    {
                        dataora = DateTime.Now.ToString(),
                        name = subject.ToString(),
                        tipo = subject.ToString(),
                        y = works.Where(x => x.subject == subject && x.correct == 0 && x.userId == long.Parse(UserID)).Count()
                    });
                }
                worksGraf.Add(lstAnswer);

                jsonResult = Json(worksGraf, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
            }
            return jsonResult;
        }

    }
}
