using Area.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExporterObjects;
using ExportImplementation;
using System.IO;
using System.Diagnostics;

namespace Area.Web.Helper
{
    public static class ExtensionMethods
    {
        public static string GetPersonelForVisit(int? personID)
        {
            if (personID <= 0)
                return "";
            using (B2DriveForPostEntities db = new B2DriveForPostEntities())
            {
                var user = db.Users.Where(p => p.ID == personID).FirstOrDefault();
                return user.FirstName + " " + user.LastName;
            }
        }

        public static void CreatePdfReport()
        {
            try
            {
                B2DriveForPostEntities db = new B2DriveForPostEntities();
                List<GetConversionAndUKS_Result> listWithPerson = db.GetConversionAndUKS(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1), null, null, null).ToList();
                var export = new ExportPdfiTextSharp4<GetConversionAndUKS_Result>();
                var data = export.ExportResult(listWithPerson);
                File.WriteAllBytes("a.pdf", data);
                Process.Start("a.pdf");
            }
            catch (Exception ex)
            {
                 
            }  
        } 
    }
}