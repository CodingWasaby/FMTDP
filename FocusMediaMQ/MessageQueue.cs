using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using FocusMediaMQ.Interface;
using FocusMediaMQ.Model;

namespace FocusMediaMQ
{
    class MessageQueue : IMessageQueue
    {
        private static List<string> WorkingTypes;
        private static List<WorkBlock> MQ;
        private static readonly object _MQLockObj = new object();

        public MessageQueue()
        {
            MQ = new List<WorkBlock>();
            WorkingTypes = new List<string>();
        }

        public string JoinQueue(string workType, Delegate work, params object[] args)
        {
            var w = new WorkBlock(workType, work, args);
            lock (_MQLockObj)
                MQ.Add(w);
            StartWorking(workType);
            return w.BlockKey;
        }

        private void StartWorking(string workType)
        {
            if (WorkingTypes.Exists(m => m == workType))
            {
                return;
            }
            else
            {
                lock (_MQLockObj)
                    WorkingTypes.Add(workType);

                Task.Factory.StartNew(() =>
                {
                    Working(workType);
                }).ContinueWith(t =>
                {
                    lock (_MQLockObj)
                        WorkingTypes.Remove(workType);
                });
            }
        }

        private void Working(string workType)
        {
            WorkBlock w;
            lock (_MQLockObj)
                w = MQ.FirstOrDefault(m => m.WorkType == workType);
            if (w == null)
                return;
            else
            {
                try
                {
                    w.Work.DynamicInvoke(w.Args);
                }
                catch (Exception ex)
                {                    
                    //TODO log
                }
                lock (_MQLockObj)
                    MQ.Remove(w);
                Working(workType);
            }
        }
    }
}
