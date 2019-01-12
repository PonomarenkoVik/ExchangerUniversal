using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TradeConnection.Webmoney
{
    public class WebmoneyDataVendor : IVendor
    {
        public string Name => "wm.echanger.ru";
        public int Id => 1;

        private static Connection _connection => Connection.Instance;
        public static IVendor Instance { get; } = new WebmoneyDataVendor();

        public List<IInstrument> GetInstrumentList() => WebmoneyInstrument.Instruments;

        public async Task<List<Order>> GetLevel2List(IInstrument instrument) =>await _connection.GetLevel2(instrument);

        public async Task<ITradeResult> Execute(ITradeCommand tradeCommand) => await _connection.Execute(tradeCommand);
    }
}
