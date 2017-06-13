using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab.Models.ViewModels
{
    public class ViewResult : Lab.Models.Result
    {
        public IEnumerable<SelectListItem> Conductings { get; set; }
        public IEnumerable<SelectListItem> Demands { get; set; }
    }
}