using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TradeConnection
{
    public class WebmoneyDataVendor : IVendor
    {
        public string Name => "wm.echanger.ru";
        public int Id => 1;

        public static IVendor Instance =>  _wmExchangerVendor ?? (_wmExchangerVendor = new WebmoneyDataVendor());

        public List<IInstrument> GetInstrumentList() => WebmoneyInstrument.Instruments;

        public List<Order> GetLevel2List(IInstrument instrument)
        {
            throw new NotImplementedException();
        }

        public ITradeResult Execute(ITradeCommand tradeCommand)
        {
            throw new NotImplementedException();
        }

        private static IVendor _wmExchangerVendor;
    }
}
