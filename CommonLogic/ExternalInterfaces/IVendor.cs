﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;

namespace CommonLogic.ExternalInterfaces
{
    public interface IVendor
    {
        string Name { get; }
        int Id { get; }
        List<IInstrument> GetInstrumentList();
        List<Order> GetLevel2List(IInstrument instrument);
        ITradeResult Execute(ITradeCommand tradeCommand);
    }
}