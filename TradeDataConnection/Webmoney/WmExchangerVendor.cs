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

        public async Task<Dictionary<string, IInstrument>> GetInstruments() => instruments ?? (instruments = await _webmoneyClient.GetInstruments(Instance));
        

        public async Task<List<Order>> GetLevel2List(IInstrument instrument, int sourceType = 0)
        {
            if (sourceType < 0 || sourceType > 2)
                return null;

            string webPage;
            string XMLPage;
            switch ((SourceType)sourceType)
            {
                case SourceType.Web:
                    webPage = await _webmoneyClient.GetLevel2Page(instrument, PageType.Web);
                    return WebmoneyHelper.CreateOrdersByPage(instrument, webPage, PageType.Web);
                case SourceType.XML:
                    XMLPage = await _webmoneyClient.GetLevel2Page(instrument, PageType.XML);
                    return WebmoneyHelper.CreateOrdersByPage(instrument, XMLPage, PageType.XML);
                case SourceType.Mix:
                    webPage = await _webmoneyClient.GetLevel2Page(instrument, PageType.Web);
                    XMLPage = await _webmoneyClient.GetLevel2Page(instrument, PageType.XML);
                    var webOrders = WebmoneyHelper.CreateOrdersByPage(instrument, webPage, PageType.Web);
                    var XMLOrders = WebmoneyHelper.CreateOrdersByPage(instrument, XMLPage, PageType.XML);
                    return WebmoneyHelper.CombineOrders(instrument, webOrders, XMLOrders);
            }
            return null;
        }

        public async Task<ITradeResult> Execute(ITradeCommand tradeCommand) => await _webmoneyClient.Execute(tradeCommand);




        private static Dictionary<string, IInstrument> instruments;

    }
}
