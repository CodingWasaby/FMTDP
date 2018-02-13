using System;
using System.Collections.Generic;
using System.Text;

namespace FocusMediaMQ.Model
{
    class WorkBlock
    {
        public WorkBlock(string workType, Delegate work, params object[] args)
        {
            BlockKey = Guid.NewGuid().ToString();
            Timestamp = DateTime.Now;
            WorkType = workType;
            Work = work;
            Args = args;
            State = WorkState.Wait;
        }

        public string BlockKey { get; }

        public string WorkType { get; set; }

        public DateTime Timestamp { get; }

        public Delegate Work { get; set; }

        public object[] Args { get; set; }

        public WorkState State { get; set; }
    }


}
