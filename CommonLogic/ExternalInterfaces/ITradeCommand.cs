using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CommandType = CommonLogic.Commons.CommandType;

namespace CommonLogic.ExternalInterfaces
{
    public interface ITradeCommand
    {
        string VendorName { get; }
        string InstrumentName { get; }
        CommandType Type { get; }
    }
}
