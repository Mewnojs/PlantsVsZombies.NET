using System;
using System.Collections.Generic;
using System.Text;

namespace IronPyInteractiveDef_Shared
{
    internal class WSEvents
    {
        public class WSEvent
        {
            public string eventtype;
            public long timestamp;
        }

        public class ExecutionEvent : WSEvent
        {
            public ExecutionEvent() {
                eventtype = "execution";
            }
            public ExecutionEventResult statuscode;
            public object result;
            public string errortype;
        }

        public class OutputEvent : WSEvent
        {
            public OutputEvent()
            {
                eventtype = "output";
            }
            public string name;
            public string msg;
        }
        public enum ExecutionEventResult
        {
            error = -1,
            executed = 0
        }
    }
}
