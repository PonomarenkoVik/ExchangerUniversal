using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using WebMoney.Cryptography;
using WebMoney.XmlInterfaces;
using WebMoney.XmlInterfaces.BasicObjects;

namespace TradeConnection.Webmoney
{
    public class WmExchangerVendor : IVendor
    {

        public WmExchangerVendor()
        {
            wmid = WmId.Parse("320508520783");
            key = new KeeperKey("<RSAKeyValue><Modulus>pV4KSuF3Tb7KrHeB+Mng4tRp14nw1HjuM/pBqa/YikNM7HBtwJaL9hUE5nZrcge8qjVU60jJyzPTPxEaenverjUM</Modulus><D>5bBcUTgYAzswW48F4eV6QpmscTKTBYvxasFem+NM+mlR2de+G5BO387ziYab09BtUypQKVYbJL9bewyqDqNufd8E</D></RSAKeyValue>");
            initializer = new Initializer(wmid, key);
        }
        public string Name => "wm.exchanger.ru";
        public int Id => 1;

        private static WebmoneyClient _webmoneyClient => WebmoneyClient.Instance;
        public static IVendor Instance { get; } = new WmExchangerVendor();

        public Dictionary<string, IInstrument> GetInstrumentsAsync() => instruments ?? (instruments = _webmoneyClient.GetInstruments(Instance).Result);
        

        public List<Order> GetLevel2Async(IInstrument instrument, int sourceType = 0)
        {
            if (sourceType < 0 || sourceType > 2)
                return null;

            string webPage;
            string XMLPage;
            switch ((SourceType)sourceType)
            {
                case SourceType.Web:
                    webPage =  _webmoneyClient.GetLevel2Page(instrument, PageType.Web).Result;
                    return WebmoneyHelper.CreateOrdersByPage(instrument, webPage, PageType.Web);
                case SourceType.XML:
                    XMLPage = _webmoneyClient.GetLevel2Page(instrument, PageType.XML).Result;
                    return WebmoneyHelper.CreateOrdersByPage(instrument, XMLPage, PageType.XML);
                case SourceType.Mix:
                    webPage = _webmoneyClient.GetLevel2Page(instrument, PageType.Web).Result;
                    XMLPage = _webmoneyClient.GetLevel2Page(instrument, PageType.XML).Result;
                    var webOrders = WebmoneyHelper.CreateOrdersByPage(instrument, webPage, PageType.Web);
                    var XMLOrders = WebmoneyHelper.CreateOrdersByPage(instrument, XMLPage, PageType.XML);
                    return WebmoneyHelper.CombineOrders(instrument, webOrders, XMLOrders);
            }
            return null;
        }

        public ITradeResult ExecuteAsync(ITradeCommand tradeCommand) => _webmoneyClient.Execute(tradeCommand).Result;

        public List<Order> GetOwnOrdersAsync(IAccount account, IInstrument instrument = null) => _webmoneyClient.GetOwnOrdersAsync(account, instrument).Result;



        public List<IAsset> GetAssets(IAccount account)
        {
            _webmoneyClient.GetAssetsAsync(initializer);
            return null;
        }

        public List<ITrade> GetAssetHistory(IAccount account, IAsset asset)
        {
            throw new NotImplementedException();
        }

       




        private static Dictionary<string, IInstrument> instruments;


        private static Initializer initializer;
        private static WmId wmid;
        private static KeeperKey key;

    }
}
