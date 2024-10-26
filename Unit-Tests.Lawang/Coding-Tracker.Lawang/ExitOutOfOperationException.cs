using System;

namespace Lawang.Coding_Tracker;

public class ExitOutOfOperationException: Exception
{
    public ExitOutOfOperationException(string message): base(message)
    {
        
    }
}
