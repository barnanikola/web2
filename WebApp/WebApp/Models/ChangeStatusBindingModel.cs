using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ChangeStatusBindingModel
    {
        public string Email { get; set; }
        public string Status { get; set; }
        
        public ChangeStatusBindingModel() { }
    }
}