using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;

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
        public double BankRate { get; set; } = double.NaN;
        public string Currency1 { get; }
        public string Currency2 { get; }
        public string InstrumentName { get; }
        public IVendor Vendor { get; }
        public async Task<List<Order>> GetLevel2(int sourceType) => Vendor != null ? await Vendor.GetLevel2List(this, sourceType) : null;
       
    }
}
