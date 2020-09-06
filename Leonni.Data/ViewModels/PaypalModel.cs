using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class PaypalModel : BaseViewModel
    {
        public string Quantity { get; set; }
        public string Amount { get; set; }
    }
}
