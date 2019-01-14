using System;
using System.Collections.Generic;
using System.Text;
using CommonLogic.ExternalInterfaces;

namespace TradeConnection.Webmoney
{
    class WebmoneyInstrument : IInstrument
    {
        public WebmoneyInstrument(int instrumentId, IVendor vendor, string currency1, string currency2, string instrumentName)
        {
            InstrumentId = instrumentId;
            Vendor = vendor;
            Currency1 = currency1;
            Currency2 = currency2;
            InstrumentName = instrumentName;
        }

        public int InstrumentId { get; }
        public string Currency1 { get; }
        public string Currency2 { get; }
        public string InstrumentName { get; }
        public IVendor Vendor { get; }
    }
}
