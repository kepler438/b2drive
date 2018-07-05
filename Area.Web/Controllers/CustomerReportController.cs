using System; 
using System.Web.Mvc;
using Area.Data;

namespace Area.Web.Controllers
{
    public class CustomerReportController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();


        public ActionResult SamePlace(InputParameter input)
        {
            input.startdate = input.startdate == null ? GetNullStartDate() : input.startdate;
            input.enddate = input.enddate == null ? GetNullEndDate() : input.enddate;
            input.CategoryID = input.CategoryID == 0 ? null : input.CategoryID;
            input.SubCategoryID = input.SubCategoryID == 0 ? null : input.SubCategoryID;
            var result = db.GetSamePlace(input.startdate, input.enddate,input.CategoryID,input.SubCategoryID);
            return View(result);
        }

        public ActionResult DifferentPlace(InputParameter input)
        {
            input.startdate = input.startdate == null ? GetNullStartDate() : input.startdate;
            input.enddate = input.enddate == null ? GetNullEndDate() : input.enddate;
            input.CategoryID = input.CategoryID == 0 ? null : input.CategoryID;
            input.SubCategoryID = input.SubCategoryID == 0 ? null : input.SubCategoryID;
            var result = db.GetDifferentPlace(input.startdate, input.enddate, input.CategoryID, input.SubCategoryID);
            return View(result);
        }

        public ActionResult ConversionAndUKS(InputParameter input)
        {
            input.startdate = input.startdate == null ? GetNullStartDate() : input.startdate;
            input.enddate = input.enddate == null ? GetNullEndDate() : input.enddate;
            input.place = input.place == 0 ? null : input.place;
            input.CategoryID = input.CategoryID == 0 ? null : input.CategoryID;
            input.SubCategoryID = input.SubCategoryID == 0 ? null : input.SubCategoryID;
            var result = db.GetConversionAndUKS(input.startdate, input.enddate, input.place, input.CategoryID, input.SubCategoryID);
            return View(result);
        }

        public ActionResult PlaceOccupancyRate(InputParameter input)
        {
            input.startdate = input.startdate == null ? GetNullStartDate() : input.startdate;
            input.enddate = input.enddate == null ? GetNullEndDate() : input.enddate;
            input.place = input.place == 0 ? null : input.place;
            input.CategoryID = input.CategoryID == 0 ? null : input.CategoryID;
            input.SubCategoryID = input.SubCategoryID == 0 ? null : input.SubCategoryID;
            var result = db.GetPlaceOccupancyRate(input.startdate, input.enddate, input.place, input.CategoryID, input.SubCategoryID);
            return View(result);
        }

        public ActionResult GeneralEvaluationReport(InputParameter input)
        {
            input.startdate = input.startdate == null ? GetNullStartDate() : input.startdate;
            input.enddate = input.enddate == null ? GetNullEndDate() : input.enddate;
            input.place = input.place == 0 ? null : input.place;
            input.CategoryID = input.CategoryID == 0 ? null : input.CategoryID;
            input.SubCategoryID = input.SubCategoryID == 0 ? null : input.SubCategoryID;
            var result = db.GetGeneralEvaluation(input.startdate, input.enddate, input.place, input.CategoryID, input.SubCategoryID);
            return View(result);
        }

        public ActionResult ConversionPlaceRate(InputParameter input)
        {
            input.startdate = input.startdate == null ? GetNullStartDate() : input.startdate;
            input.enddate = input.enddate == null ? GetNullEndDate() : input.enddate;
            input.place = input.place == 0 ? null : input.place;
            input.CategoryID = input.CategoryID == 0 ? null : input.CategoryID;
            input.SubCategoryID = input.SubCategoryID == 0 ? null : input.SubCategoryID;
            var result = db.GetConversionPlaceRate(input.startdate, input.enddate, input.place, input.CategoryID, input.SubCategoryID);
            return View(result);
        }

        public ActionResult UksRate(InputParameter input)
        {
            input.startdate = input.startdate == null ? GetNullStartDate() : input.startdate;
            input.enddate = input.enddate == null ? GetNullEndDate() : input.enddate;
            input.place = input.place == 0 ? null : input.place;
            input.CategoryID = input.CategoryID == 0 ? null : input.CategoryID;
            input.SubCategoryID = input.SubCategoryID == 0 ? null : input.SubCategoryID;
            var result = db.GetUKSRate(input.startdate, input.enddate, input.CategoryID, input.SubCategoryID);
            return View(result);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}