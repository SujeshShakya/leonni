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
    
    public partial class Advertisement
    {
        // Primitive properties
    
        public long Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int CategoryId { get; set; }
        public int DisciplineId { get; set; }
        public string Sex { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public System.DateTime BeginningDay { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public int UserProfileId { get; set; }
        public string EngineeredTo { get; set; }
    
    }
}
