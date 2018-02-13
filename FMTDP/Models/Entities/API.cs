using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Models.Entities
{
    public class API
    {
        public int APICode { get; set; }
        public string APIName { get; set; }
        public string APIIntranetUrl { get; set; }
        public string APIInternetUrl { get; set; }
        public string APIType { get; set; }
        public int NeedApply { get; set; }
        public int FMSystemCode { get; set; }
        public string Description { get; set; }
        public int DeleteFlag { get; set; }
    }

    public class APILog : API
    {
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
