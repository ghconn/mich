﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl.Mail
{
    public class ConfigMail
    {
        public string From { get; set; }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] Attachments { get; set; }
        public string[] Resources { get; set; }
    }
}
