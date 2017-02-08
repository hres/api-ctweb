﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ctWebApi.Models
{
    public class Protocol
    {
        public int protocol_id { get; set; }
        public string protocol_no { get; set; }
        public string submission_no { get; set; }
        public int status_id { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public DateTime? nol_date { get; set; }
        public string protocol_title { get; set; }
        //Note: In the table there are protocol_title_en & protocol_title_fr fields.
    }
}