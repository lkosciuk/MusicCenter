using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MusicCenter.Common.ViewModels.File
{
    public class FileViewModel
    {

        [DataType(DataType.Upload)]
        public HttpPostedFileBase PostedFile { get; set; }

        public string RelativePathToSave { get; set; }

        public string PathToShow { get; set; }
    }
}
