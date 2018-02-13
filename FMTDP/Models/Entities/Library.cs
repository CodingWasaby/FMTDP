using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Models.Entities
{
    public class Library
    {
        public int LibraryCode { get; set; }
        public string LibraryName { get; set; }
        public string LibraryType { get; set; }
        public string Description { get; set; }
        public int DeleteFlag { get; set; }
    }

    public class LibraryLog : Library
    {
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
