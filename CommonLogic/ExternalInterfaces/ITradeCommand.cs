using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLogic.ExternalInterfaces
{
    public interface ITradeCommand
    {
        string VendorName { get; }
        string InstrumentName { get; }
    }
}
