using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Models.ViewModels
{
    public class ViewConducting : Lab.Models.Conducting
    {
        public IEnumerable<SelectListItem> Tests { get; set; }
        public IEnumerable<SelectListItem> Patients { get; set; }
    }
}