using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using WordPredictionEngine.Domain;

namespace WordPredictionEngine.Controllers
{
    public class HomeController : Controller
    {
        private DomainServices domainServices ;
        public ActionResult Index( )
        {
            
            return View( );
        }

        public ActionResult Query( )
        {
            return PartialView( );
        }

        
        private DomainServices DomainServicesInstance( )
        {
            if(domainServices == null)
                domainServices = new DomainServices( );
            return domainServices;
        }

        public ActionResult LearnFromFiles( string path )
        {
            if ( !string.IsNullOrEmpty( path ) )
            {
                if(path.Contains(".txt"))
                    this.DomainServicesInstance().LearnFromFile(path);
                else
                    this.DomainServicesInstance().LearnFromFiles(path);
            }
            return RedirectToAction("Query", "Home");
        }
        public ActionResult LearnFromASentence(string sentence)
        {
            if ( !string.IsNullOrEmpty( sentence ) )
            {
                this.DomainServicesInstance().LearnFromSentence(sentence);
            }
            return RedirectToAction("Query","Home");
        }

        public JsonResult predict( string word )
        {
            if (!string.IsNullOrEmpty(word))
            {
               string nextword =  this.DomainServicesInstance().PredictNextWord(word);
                return Json( nextword, JsonRequestBehavior.AllowGet );
            }
            return Json( string.Empty, JsonRequestBehavior.AllowGet );
        }
        public JsonResult NextWordList(string word)
        {
            var listOfNextWords = new List< string >();
            
            if ( !string.IsNullOrEmpty( word ) )
            {
                listOfNextWords = this.DomainServicesInstance( ).NextWordList( word );
                
            }
            return Json(listOfNextWords, JsonRequestBehavior.AllowGet);
            //return Json( string.Empty, JsonRequestBehavior.AllowGet );
        }
    }
}
