using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Components
{
    public class Appointments: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
