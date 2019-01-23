using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.ModelEntities;

namespace CommonLogic.ExternalInterfaces
{
    public interface IInstrument
    {
        string InstrumentId { get; }
        double BankRate { get; set; }
        string InstrumentName { get; }
        string Currency1 { get; }
        string Currency2 { get; }
        IVendor Vendor { get; }
        Task<List<Order>> GetLevel2(int sourceType);
    }
}
