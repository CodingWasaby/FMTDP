using System;
using System.Collections.Generic;
using System.Text;

namespace FocusMediaMQ.Interface
{
    public interface IMessageQueue
    {
        string JoinQueue(string workType, Delegate work, params object[] args);
    }
}
