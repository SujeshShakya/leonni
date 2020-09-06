//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Leonni.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Publication
    {
        // Primitive properties
    
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long ProjectId { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int DisciplineId { get; set; }
        public string Resume { get; set; }
        public string Description { get; set; }
        public Nullable<long> PhotoId { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<long> DisabledBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public long CreatedBy { get; set; }
        public int Status { get; set; }
    
        // Navigation properties
    
        public virtual Category Category { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual Project Project { get; set; }
    
    }
}