﻿using System;
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
            ProxyPort = 34986;
            ProxyURL = "213.33.164.158";
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

        internal async Task<string> GetLevel2Page(IInstrument instrument, PageType pageType)
        {
            string page = (pageType == PageType.Web) ? _tradeUrl : _tradeXMLUrl;
            return await GetPage(page + instrument.InstrumentId);
        }

        private async Task<string> GetPage(string url)
        {
            WebClient webClient = new WebClient();

            if (!string.IsNullOrEmpty(ProxyURL) && ProxyPort > 0)
                webClient.Proxy = new WebProxy(ProxyURL, _proxyPort);

            try
            {
                return await webClient.DownloadStringTaskAsync(url);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                webClient.Dispose();
            }
        }

        public async Task<Dictionary<string, IInstrument>> GetInstruments(IVendor vendor)
        {
            string page = await GetPage(_bestRates);
            return WebmoneyHelper.CreateInstruments(page, vendor);
        }

        internal Task<ITradeResult> Execute(ITradeCommand tradeCommand)
        {
            throw new NotImplementedException();
        }

      

        private static readonly string _tradeUrl = "https://wm.exchanger.ru/asp/wmlist.asp?exchtype=";
        private static readonly string _tradeXMLUrl = "https://wm.exchanger.ru/asp/XMLwmlist.asp?exchtype=";
        private static readonly string _bestRates = "https://wm.exchanger.ru/asp/XMLbestRates.asp";
        private int _proxyPort;
    }
}
