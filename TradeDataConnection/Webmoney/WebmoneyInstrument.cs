using System;
using System.Collections.Generic;
using System.Text;
using CommonLogic.ExternalInterfaces;

namespace TradeConnection.Webmoney
{
    class WebmoneyInstrument : IInstrument
    {
        public WebmoneyInstrument(int instrumentId, IVendor vendor, string currency1, string currency2)
        {
            InstrumentId = instrumentId;
            Vendor = vendor;
            Currency1 = currency1;
            Currency2 = currency2;
        }

        public int InstrumentId { get; }
        public string Currency1 { get; }
        public string Currency2 { get; }
        public string InstrumentName => Currency1 + "/" + Currency2;
        public IVendor Vendor { get; }


        internal static List<IInstrument> Instruments { get; } = new List<IInstrument>()
        {
            new WebmoneyInstrument(1, WebmoneyDataVendor.Instance, "WMZ", "WMR"),
            new WebmoneyInstrument(2,  WebmoneyDataVendor.Instance, "WMR", "WMZ"),
            new WebmoneyInstrument(3,  WebmoneyDataVendor.Instance, "WMZ", "WME"),
            new WebmoneyInstrument(4,  WebmoneyDataVendor.Instance, "WME", "WMZ"),
            new WebmoneyInstrument(5,  WebmoneyDataVendor.Instance, "WME", "WMR"),
            new WebmoneyInstrument(6,  WebmoneyDataVendor.Instance, "WMR", "WME"),
            new WebmoneyInstrument(7,  WebmoneyDataVendor.Instance, "WMZ", "WMU"),
            new WebmoneyInstrument(8,  WebmoneyDataVendor.Instance, "WMU", "WMZ"),
            new WebmoneyInstrument(9,  WebmoneyDataVendor.Instance, "WMR", "WMU"),
            new WebmoneyInstrument(10,  WebmoneyDataVendor.Instance, "WMU", "WMR"),
            new WebmoneyInstrument(11,  WebmoneyDataVendor.Instance, "WMU", "WME"),
            new WebmoneyInstrument(12,  WebmoneyDataVendor.Instance, "WME", "WMU"),
            new WebmoneyInstrument(25,  WebmoneyDataVendor.Instance, "WMZ", "WMG"),
            new WebmoneyInstrument(26,  WebmoneyDataVendor.Instance, "WMG", "WMZ"),
            new WebmoneyInstrument(27,  WebmoneyDataVendor.Instance, "WME", "WMG"),
            new WebmoneyInstrument(28,  WebmoneyDataVendor.Instance, "WMG", "WME"),
            new WebmoneyInstrument(29,  WebmoneyDataVendor.Instance, "WMR", "WMG"),
            new WebmoneyInstrument(30,  WebmoneyDataVendor.Instance, "WMG", "WMR"),
            new WebmoneyInstrument(31,  WebmoneyDataVendor.Instance, "WMU", "WMG"),
            new WebmoneyInstrument(32,  WebmoneyDataVendor.Instance, "WMG", "WMU"),
        };
    }
}
