using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLogic.ExternalInterfaces
{
    public interface ITradeResult
    {
         bool Success { get; }
         string Message { get;}
    }
}
