﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
   public class ActivityModel : BaseViewModel
    {
        // Primitive properties

        public long Id { get; set; }
        public long GroupId { get; set; }
        public long ProjectId { get; set; }
        public System.DateTime Date { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int DisciplineId { get; set; }
        public string Resume { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string Locality { get; set; }
        public string Price { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsDisabled { get; set; }
        public Nullable<long> DisabledBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }

        // Navigation properties

        public virtual Category Category { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual Group Group { get; set; }
        public virtual Project Project { get; set; }
    }
}
