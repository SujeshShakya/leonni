using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leonni.Models.ViewModels
{
    public class MessageModel : BaseViewModel
    {
        public long Id { get; set; }
        public long SentTo { get; set; }
        public System.DateTime SentDate { get; set; }
        public long? SentBy { get; set; }
        public string MessageContent{ get; set; }
    }
}
