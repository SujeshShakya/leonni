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
    
    public partial class Favourite
    {
        // Primitive properties
    
        public long Id { get; set; }
        public int SectionId { get; set; }
        public long EntityId { get; set; }
        public long UserProfileId { get; set; }
    
        // Navigation properties
    
        public virtual Section Section { get; set; }
    
    }
}