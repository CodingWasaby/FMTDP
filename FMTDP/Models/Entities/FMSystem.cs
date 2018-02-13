using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Models.Entities
{
    public class FMSystem
    {
        public int FMSystemCode { get; set; }

        public string FMSystemName { get; set; }

        public string FMSystemEName { get; set; }

        public string Abbreviation { get; set; }

        public int DeleteFlag { get; set; }

        public string LogoURL { get; set; }

        public string Instructions { get; set; }
    }

    public class FMSystemLog : FMSystem
    {
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
