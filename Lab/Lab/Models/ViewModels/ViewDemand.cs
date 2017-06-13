using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Models.ViewModels
{
    public class ViewDemand: Lab.Models.Demand
    {
        public IEnumerable<SelectListItem> Tests { get; set; }
        public string TestId { get; set; }
    }
}