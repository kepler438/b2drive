//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Area.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class AvailableUserVisit
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int VisitID { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    
        public virtual User User { get; set; }
        public virtual VisitPlace VisitPlace { get; set; }
    }
}
