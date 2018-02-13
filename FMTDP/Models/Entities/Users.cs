using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Models.Entities
{
    public class Users
    {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string HeadPic { get; set; }
        public string Email { get; set; }
        public string DeptName { get; set; }
        public string CompanyName { get; set; }
        public string WorkCity { get; set; }
        public string Sex { get; set; }
        public string WorkNo { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
