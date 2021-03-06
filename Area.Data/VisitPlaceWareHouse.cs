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
    
    public partial class VisitPlaceWareHouse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VisitPlaceWareHouse()
        {
            this.ProductRecivedDelivereds = new HashSet<ProductRecivedDelivered>();
            this.WareHouseProducts = new HashSet<WareHouseProduct>();
        }
    
        public int ID { get; set; }
        public int VisitPlaceID { get; set; }
        public int WareHouseID { get; set; }
        public Nullable<System.DateTime> CheckinDate { get; set; }
        public string CheckinLatitude { get; set; }
        public string CheckinLongitude { get; set; }
        public Nullable<System.DateTime> CheckoutDate { get; set; }
        public string CheckoutLatitude { get; set; }
        public string CheckoutLongitude { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductRecivedDelivered> ProductRecivedDelivereds { get; set; }
        public virtual VisitPlace VisitPlace { get; set; }
        public virtual WareHouse WareHouse { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WareHouseProduct> WareHouseProducts { get; set; }
    }
}
