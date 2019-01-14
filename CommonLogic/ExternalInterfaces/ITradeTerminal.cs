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
        Dictionary<string, Dictionary<string, IInstrument>> Instruments { get; }


        Task<ITradeResult> SendCommand(ITradeCommand command);
        Task<List<Order>> GetLevel2(IInstrument instrument);

        IInstrument GetInstrument(string vendorName, string instrumentName);
    }
}
