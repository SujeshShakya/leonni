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
    
    public partial class Discipline
    {
        public Discipline()
        {
            this.Activities = new HashSet<Activity>();
            this.Galleries = new HashSet<Gallery>();
            this.Links = new HashSet<Link>();
            this.Categories = new HashSet<Category>();
            this.UserProfiles = new HashSet<UserProfile>();
            this.Projects = new HashSet<Project>();
            this.Publications = new HashSet<Publication>();
            this.Groups = new HashSet<Group>();
        }
    
        // Primitive properties
    
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public int CategoryId { get; set; }
        public string DisciplineName { get; set; }
    
        // Navigation properties
    
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }
        public virtual ICollection<Link> Links { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    
    }
}
