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
    
    public partial class ELMAH_Error
    {
        // Primitive properties
    
        public System.Guid ErrorId { get; set; }
        public string Application { get; set; }
        public string Host { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public int StatusCode { get; set; }
        public System.DateTime TimeUtc { get; set; }
        public int Sequence { get; set; }
        public string AllXml { get; set; }
    
    }
}