using System.Collections.Generic;

namespace Extra_Exercise_3
{
    public interface IAuditLogger
    {
        void AddMessage(string message);
        List<string> GetLog();
    }
}