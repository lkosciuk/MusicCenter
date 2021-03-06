﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Common.ViewModels.Message
{
    public class MessageLisItemViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Recipients { get; set; }

        public DateTime SentDate { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool IsReaded { get; set; }
    }
}
