using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MvcWebApp.Models;

namespace MvcWebApp.ViewModels
{

    public class MessageFull : MessageList
    {

        public MessageFull()
        {

            //

        }

    }

    public class MessageList
    {

        [Key]
        public int MessageId { get; set; }

        [Required, Display(Name = "Standard Message")]
        public string StandardMessage { get; set; }

        [Required, Display(Name = "Custom Message")]
        public string CustomMessage { get; set; }

        [Display(Name = "Faculty")]
        public Faculty Faculty { get; set; }

    }

}