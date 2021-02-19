using System;
using System.Collections.Generic;
using System.ComponentModel;
using Uppgift_ASP.NET_MVC.Data;

#nullable disable

namespace Uppgift_ASP.NET_MVC.Entities
{
    public partial class SchoolClassStudent
    {
        [DisplayName("Student")]
        public string StudentId { get; set; }
        public Guid SchoolClassId { get; set; }

        public virtual SchoolClass SchoolClass { get; set; }
    }
}
