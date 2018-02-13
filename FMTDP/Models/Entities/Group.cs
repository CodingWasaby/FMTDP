using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Models.Entities
{
    public class Group
    {
        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        public string OwnerCode { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string GroupType { get; set; }
        public int BelongCode { get; set; }
        public int DeleteFlag { get; set; }
    }

    public class GroupLog
    {
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int GroupMembers { get; set; }
    }
}
