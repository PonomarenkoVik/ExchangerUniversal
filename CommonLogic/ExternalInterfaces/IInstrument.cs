using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLogic.ExternalInterfaces
{
    public interface IInstrument
    {
        int InstrumentId { get; }
        string InstrumentName { get; }
        string Currency1 { get; }
        string Currency2 { get; }
        IVendor Vendor { get; }
    }
}
