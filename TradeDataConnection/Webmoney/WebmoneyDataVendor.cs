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
        public string Name => "wm.exchanger.ru";
        public int Id => 1;

        private static Connection _connection => Connection.Instance;
        public static IVendor Instance { get; } = new WebmoneyDataVendor();

        public Dictionary<string, IInstrument> GetInstruments() => Instruments;

        public async Task<List<Order>> GetLevel2List(IInstrument instrument) =>await _connection.GetLevel2(instrument);

        public async Task<ITradeResult> Execute(ITradeCommand tradeCommand) => await _connection.Execute(tradeCommand);


        internal static Dictionary<string, IInstrument> Instruments { get; } = new Dictionary<string, IInstrument>()
        {
            {"WMZ/WMR", new WebmoneyInstrument(1, WebmoneyDataVendor.Instance, "WMZ", "WMR", "WMZ/WMR")},
            {"WMR/WMZ", new WebmoneyInstrument(2, WebmoneyDataVendor.Instance, "WMR", "WMZ", "WMR/WMZ")},
            {"WMZ/WME", new WebmoneyInstrument(3, WebmoneyDataVendor.Instance, "WMZ", "WME", "WMZ/WME")},
            {"WME/WMZ", new WebmoneyInstrument(4, WebmoneyDataVendor.Instance, "WME", "WMZ", "WME/WMZ")},
            {"WME/WMR", new WebmoneyInstrument(5, WebmoneyDataVendor.Instance, "WME", "WMR", "WME/WMR")},
            {"WMR/WME", new WebmoneyInstrument(6, WebmoneyDataVendor.Instance, "WMR", "WME", "WMR/WME")},
            {"WMZ/WMU", new WebmoneyInstrument(7, WebmoneyDataVendor.Instance, "WMZ", "WMU", "WMZ/WMU")},
            {"WMU/WMZ", new WebmoneyInstrument(8, WebmoneyDataVendor.Instance, "WMU", "WMZ", "WMU/WMZ")},
            {"WMR/WMU", new WebmoneyInstrument(9, WebmoneyDataVendor.Instance, "WMR", "WMU", "WMR/WMU")},
            {"WMU/WMR", new WebmoneyInstrument(10, WebmoneyDataVendor.Instance, "WMU", "WMR", "WMU/WMR")},
            {"WMU/WME", new WebmoneyInstrument(11, WebmoneyDataVendor.Instance, "WMU", "WME", "WMU/WME")},
            {"WME/WMU", new WebmoneyInstrument(12, WebmoneyDataVendor.Instance, "WME", "WMU", "WME/WMU")},
            {"WMZ/WMG", new WebmoneyInstrument(25, WebmoneyDataVendor.Instance, "WMZ", "WMG", "WMZ/WMG")},
            {"WMG/WMZ", new WebmoneyInstrument(26, WebmoneyDataVendor.Instance, "WMG", "WMZ", "WMG/WMZ")},
            {"WME/WMG", new WebmoneyInstrument(27, WebmoneyDataVendor.Instance, "WME", "WMG", "WME/WMG")},
            {"WMG/WME", new WebmoneyInstrument(28, WebmoneyDataVendor.Instance, "WMG", "WME", "WMG/WME")},
            {"WMR/WMG", new WebmoneyInstrument(29, WebmoneyDataVendor.Instance, "WMR", "WMG", "WMR/WMG")},
            {"WMG/WMR", new WebmoneyInstrument(30, WebmoneyDataVendor.Instance, "WMG", "WMR", "WMG/WMR")},
            {"WMU/WMG", new WebmoneyInstrument(31, WebmoneyDataVendor.Instance, "WMU", "WMG", "WMU/WMG")},
            {"WMG/WMU", new WebmoneyInstrument(32, WebmoneyDataVendor.Instance, "WMG", "WMU", "WMG/WMU")},
        };
    }
}
