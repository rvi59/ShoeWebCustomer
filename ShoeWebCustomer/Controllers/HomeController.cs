using ShoeWebCustomer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeWebCustomer.Controllers
{
  
    public class HomeController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public HomeController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        
        // GET: Home
        public ActionResult Index()
        {
            TempData["Sportsdata"]  = _dataRepository.GetTop4Sports();
            TempData["Casualdata"] = _dataRepository.GetTop4Casual();
            TempData["Formaldata"] = _dataRepository.GetTop4Formal();

            return View();
           
        }
    }
}