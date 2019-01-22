using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TradeConnection.Webmoney
{
    public class WmExchangerVendor : IVendor
    {
        public string Name => "wm.exchanger.ru";
        public int Id => 1;

        private static WebmoneyClient _webmoneyClient => WebmoneyClient.Instance;
        public static IVendor Instance { get; } = new WmExchangerVendor();

        public Dictionary<string, IInstrument> GetInstruments() => Instruments;

        public async Task<List<Order>> GetLevel2List(IInstrument instrument, int sourceType = 0)
        {
            if (sourceType < 0 || sourceType > 2)
                return null;

            string webPage;
            string XMLPage;
            switch ((SourceType)sourceType)
            {
                case SourceType.Web:
                    webPage = await _webmoneyClient.GetPage(instrument, PageType.Web);
                    return WebmoneyHelper.CreateOrdersByPage(instrument, webPage, PageType.Web);
                case SourceType.XML:
                    XMLPage = await _webmoneyClient.GetPage(instrument, PageType.XML);
                    return WebmoneyHelper.CreateOrdersByPage(instrument, XMLPage, PageType.XML);
                case SourceType.Mix:
                    webPage = await _webmoneyClient.GetPage(instrument, PageType.Web);
                    XMLPage = await _webmoneyClient.GetPage(instrument, PageType.XML);
                    var webOrders = WebmoneyHelper.CreateOrdersByPage(instrument, webPage, PageType.Web);
                    var XMLOrders = WebmoneyHelper.CreateOrdersByPage(instrument, XMLPage, PageType.XML);
                    return WebmoneyHelper.CombineOrders(instrument, webOrders, XMLOrders);
            }
            return null;
        }

        public async Task<ITradeResult> Execute(ITradeCommand tradeCommand) => await _webmoneyClient.Execute(tradeCommand);


        internal static Dictionary<string, IInstrument> Instruments { get; } = new Dictionary<string, IInstrument>()
        {
            {"WMZ/WMR", new WebmoneyInstrument(1, WmExchangerVendor.Instance, "WMZ", "WMR", "WMZ/WMR")},
            {"WMR/WMZ", new WebmoneyInstrument(2, WmExchangerVendor.Instance, "WMR", "WMZ", "WMR/WMZ")},
            {"WMZ/WME", new WebmoneyInstrument(3, WmExchangerVendor.Instance, "WMZ", "WME", "WMZ/WME")},
            {"WME/WMZ", new WebmoneyInstrument(4, WmExchangerVendor.Instance, "WME", "WMZ", "WME/WMZ")},
            {"WME/WMR", new WebmoneyInstrument(5, WmExchangerVendor.Instance, "WME", "WMR", "WME/WMR")},
            {"WMR/WME", new WebmoneyInstrument(6, WmExchangerVendor.Instance, "WMR", "WME", "WMR/WME")},
            {"WMZ/WMU", new WebmoneyInstrument(7, WmExchangerVendor.Instance, "WMZ", "WMU", "WMZ/WMU")},
            {"WMU/WMZ", new WebmoneyInstrument(8, WmExchangerVendor.Instance, "WMU", "WMZ", "WMU/WMZ")},
            {"WMR/WMU", new WebmoneyInstrument(9, WmExchangerVendor.Instance, "WMR", "WMU", "WMR/WMU")},
            {"WMU/WMR", new WebmoneyInstrument(10, WmExchangerVendor.Instance, "WMU", "WMR", "WMU/WMR")},
            {"WMU/WME", new WebmoneyInstrument(11, WmExchangerVendor.Instance, "WMU", "WME", "WMU/WME")},
            {"WME/WMU", new WebmoneyInstrument(12, WmExchangerVendor.Instance, "WME", "WMU", "WME/WMU")},
            {"WMZ/WMG", new WebmoneyInstrument(25, WmExchangerVendor.Instance, "WMZ", "WMG", "WMZ/WMG")},
            {"WMG/WMZ", new WebmoneyInstrument(26, WmExchangerVendor.Instance, "WMG", "WMZ", "WMG/WMZ")},
            {"WME/WMG", new WebmoneyInstrument(27, WmExchangerVendor.Instance, "WME", "WMG", "WME/WMG")},
            {"WMG/WME", new WebmoneyInstrument(28, WmExchangerVendor.Instance, "WMG", "WME", "WMG/WME")},
            {"WMR/WMG", new WebmoneyInstrument(29, WmExchangerVendor.Instance, "WMR", "WMG", "WMR/WMG")},
            {"WMG/WMR", new WebmoneyInstrument(30, WmExchangerVendor.Instance, "WMG", "WMR", "WMG/WMR")},
            {"WMU/WMG", new WebmoneyInstrument(31, WmExchangerVendor.Instance, "WMU", "WMG", "WMU/WMG")},
            {"WMG/WMU", new WebmoneyInstrument(32, WmExchangerVendor.Instance, "WMG", "WMU", "WMG/WMU")},
        };
    }
}
