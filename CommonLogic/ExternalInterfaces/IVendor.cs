using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;

namespace CommonLogic.ExternalInterfaces
{
    public interface IVendor
    {
        string Name { get; }
        int Id { get; }
        Dictionary<string, IInstrument> GetInstrumentsAsync();
        ITradeResult ExecuteAsync(ITradeCommand tradeCommand);
        List<Order> GetLevel2Async(IInstrument instrument, int sourceType);

        /// <summary>
        /// Getting own orders by definitive instrument or all instrument (instrument = null)
        /// </summary>
        /// <param name="account"></param>
        /// <param name="instrument"></param>
        /// <returns></returns>
        List<Order> GetOwnOrdersAsync(IAccount account, IInstrument instrument = null);

        List<IAsset> GetAssets(IAccount account);

        List<ITrade> GetAssetHistory(IAccount account, IAsset asset);

    }
}
