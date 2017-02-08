using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ctWebApi.Models
{
    public class ProductBrand
    {
        public int protocol_id { get; set; }
        public string submission_no { get; set; }
        public int brand_id { get; set; }
        public int manufacturer_id { get; set; }
        public string manufacturer_name { get; set; }
        public string brand_name { get; set; }
        //Note: In the table there are brand_name_en & brand_name_fr fields.
      
    }
}