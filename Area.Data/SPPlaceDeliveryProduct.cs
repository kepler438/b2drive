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
    
    public partial class SPPlaceDeliveryProduct
    {
        public int ID { get; set; }
        public int SupervisorVisitPlaceID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual SupervisorVisitPlace SupervisorVisitPlace { get; set; }
    }
}
