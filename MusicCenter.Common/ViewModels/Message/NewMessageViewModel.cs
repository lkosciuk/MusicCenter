using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Message
{
    public class NewMessageViewModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        public string Recipients { get; set; }
        public string RecipientsErrorMsg { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Global), ErrorMessageResourceName = "FieldRequired")]
        public string Title { get; set; }

        public string Content { get; set; }
        public string AuthorEmail { get; set; }
    }
}
