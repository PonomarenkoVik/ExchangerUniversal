using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TradeConnection.Webmoney
{
    public class WebmoneyDataVendor : IVendor
    {
        public string Name => "wm.echanger.ru";
        public int Id => 1;

        private static Connection _connection => Connection.Instance;
        public static IVendor Instance { get; } = new WebmoneyDataVendor();

        public List<IInstrument> GetInstrumentList() => WebmoneyInstrument.Instruments;

        public List<Order> GetLevel2List(IInstrument instrument) => _connection.GetLevel2(instrument);

        public ITradeResult Execute(ITradeCommand tradeCommand) => _connection.Execute(tradeCommand);
    }
}
