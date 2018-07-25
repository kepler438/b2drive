using System;
using System.Linq;
using System.Web.Mvc;
using Area.Data;
namespace Area.Web.Controllers
{
    public class SupervisorReportController : Controller
    {

        private B2DriveForPostEntities db = new B2DriveForPostEntities();
        // GET: SupervisorReport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisitPlaceCarInfo(InputParameter input)
        {
            input.startdate = input.startdate == null ? GetNullStartDate() : input.startdate;
            input.enddate = input.enddate == null ? GetNullEndDate() : input.enddate;
            input.place = input.place == 0 ? null : input.place;
            var result = db.VisitPlaceCarInfoes.Where(p => p.IsActive == true && p.VisitPlace.StartDate > input.startdate && p.VisitPlace.StartDate < input.enddate).ToList();
            return View(result); 
        }

        public ActionResult PenetrationReport(InputParameter input)
        { 
            input.place = input.place == 0 ? null : input.place;
            if (input.place != null )
            {
                var result = db.SupervisorVisitPlacePenetrations.Where(p => p.IsActive == true && p.PenetrationPlaceID > 0 && p.PenetrationPlaceID == input.place).ToList();
                return View(result);
            }
            else
            {
                var result = db.SupervisorVisitPlacePenetrations.Where(p => p.IsActive == true && p.PenetrationPlaceID > 0).ToList();
                return View(result);
            } 
        }

        public class InputParameter
        {
            public DateTime? startdate { get; set; }
            public DateTime? enddate { get; set; }
            public int? place { get; set; }
            public int? CategoryID { get; set; }
            public int? SubCategoryID { get; set; }
        }

        private DateTime GetNullStartDate()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            return start;
        }

        private DateTime GetNullEndDate()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            int days = day - DayOfWeek.Monday;
            DateTime start = DateTime.Now.AddDays(-days);
            DateTime end = start.AddDays(6);
            return end;
        }
    }
}