using FocusMediaMQ.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FocusMediaMQ
{
    public class MQFactory
    {
        private static IMessageQueue _MessageQueue;
        public static IMessageQueue GetMQ()
        {
            if (_MessageQueue == null)
                _MessageQueue = new MessageQueue();
            return _MessageQueue;
        }
    }
}
