using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMTDP.Models.Entities
{
    public class CreateLog
    {
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int DataID { get; set; }
        public string DataType { get; set; }
        public string OperateType { get; set; }
    }
}
