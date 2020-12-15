using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorSample.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(ILogger<IndexModel> logger)
        {
        }
        public List<SelectListItem> WeekDay { get; set; }
        
        [BindProperty]
        public string WeekDaySelected { get; set; }
        public void OnGet()
        {
            this.WeekDay = new List<SelectListItem>();
            this.WeekDay.Add(new SelectListItem 
                                  {
                                      Value = "Monday",Text =  "Monday"
                                  });
            this.WeekDay.Add(new SelectListItem 
                                  {
                                      Value = "Tuesday",Text =  "Tuesday"
                                  });                                  
                                  
        }

        public void OnPost()
        {
            Console.WriteLine(this.WeekDaySelected);
        }
    }
}
