using System;
using System.Collections.Generic;
using System.Text;
using CommonLogic.ExternalInterfaces;
using TradeConnection.Webmoney;

namespace TradeConnection
{
    public class TradableConnection : ITradableConnection
    {
        public List<IVendor> GetVendors { get; } = new List<IVendor>() { new WebmoneyDataVendor() };
    }
}
