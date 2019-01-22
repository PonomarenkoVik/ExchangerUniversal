using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;

namespace TradeConnection.Webmoney
{
    class WebmoneyClient
    {
        public WebmoneyClient()
        {
            ProxyPort = 61785;
            ProxyURL = "191.100.25.15";
        }

        public string ProxyURL { get; set; }

        public int ProxyPort
        {
            get => _proxyPort;
            set
            {
                if (value > 1023 && value < 65535)
                    _proxyPort = value;
            }
        }


        public static WebmoneyClient Instance { get; } = new WebmoneyClient();

        internal async Task<string> GetPage(IInstrument instrument, PageType pageType)
        {
            WebClient webClient = new WebClient();
           
            if (!string.IsNullOrEmpty(ProxyURL) && ProxyPort > 0)
                webClient.Proxy = new WebProxy(ProxyURL, _proxyPort);
            
            try
            {
                return  await webClient.DownloadStringTaskAsync(GetTradeUrlByInstrument(instrument, pageType));
            }
            catch(Exception)
            {
                return null;
            }
            finally
            {
                webClient.Dispose();
            }
        }


        internal Task<ITradeResult> Execute(ITradeCommand tradeCommand)
        {
            throw new NotImplementedException();
        }

        private string GetTradeUrlByInstrument(IInstrument instrument, PageType pageType)
        {
            string page = (pageType == PageType.Web) ? _tradeUrl : _tradeXMLUrl;
            return page + instrument.InstrumentId;
        }

        private static readonly string _tradeUrl = "https://wm.exchanger.ru/asp/wmlist.asp?exchtype=";
        private static readonly string _tradeXMLUrl = "https://wm.exchanger.ru/asp/XMLwmlist.asp?exchtype=";
        private int _proxyPort;
    }
}
