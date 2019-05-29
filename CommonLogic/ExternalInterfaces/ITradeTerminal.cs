using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.Commons;
using CommonLogic.ModelEntities;

namespace CommonLogic.ExternalInterfaces
{
    public interface ITradeTerminal
    {
        /// <summary>
        /// Returns instruments are sorted by vendors
        /// </summary>
        /// <returns>vendor, instruments</returns>
        Dictionary<string, List<string>> Instruments { get; }
        ITradeResult SendCommandAsync(ITradeCommand command);
        List<Order> GetLevel2Async(string vendorName, string instrumentName, int sourceType);

        List<IAsset> GetAssets(string vendorName, string account);

    }
}
