using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;

namespace TradeConnection.Webmoney
{
    class Connection
    {
        public static Connection Instance { get; } = new Connection();

        internal async Task<List<Order>> GetLevel2(IInstrument instrument)
        {
            StringBuilder page = await GetPage(instrument);
            return WebmoneyHelper.CreateOrdersByPage(page);
        }

        private async Task<StringBuilder> GetPage(IInstrument instrument)
        {
            StringBuilder page = new StringBuilder();
            WebClient webClient = new WebClient();
            try
            {
                page.Append(await webClient.DownloadStringTaskAsync(GetTradeUrlByInstrument(instrument)));
            }
            catch(Exception)
            {
                return null;
            }
            finally
            {
                webClient.Dispose();
            }

            return page;
        }

        internal Task<ITradeResult> Execute(ITradeCommand tradeCommand)
        {
            throw new NotImplementedException();
        }

        private string GetTradeUrlByInstrument(IInstrument instrument) => _tradeUrl + instrument.InstrumentId;

        private static readonly string _tradeUrl = "https://wm.exchanger.ru/asp/wmlist.asp?exchtype=";
    }
}
