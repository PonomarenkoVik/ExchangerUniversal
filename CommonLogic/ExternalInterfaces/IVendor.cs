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
        Task<Dictionary<string, IInstrument>> GetInstruments();
        Task<ITradeResult> Execute(ITradeCommand tradeCommand);
        Task<List<Order>> GetLevel2List(IInstrument instrument, int sourceType);
    }
}
