using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.Commons;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;
using TradeConnection;

namespace TestConsole
{
    class Program
    {
        static  void Main(string[] args)
        {
            ITradeTerminal tradeTerminal = new TradeTerminal(new List<ITradableConnection>(){new TradableConnection()});

            var instrument = tradeTerminal.GetInstrument("wm.exchanger.ru", "WMZ/WMU");
            var orders =  GetLevel2(instrument, tradeTerminal).Result;
        }

         private static async Task<List<Order>> GetLevel2(IInstrument instrument, ITradeTerminal terminal)
        {
            return await terminal.GetLevel2(instrument);
        }
    }
} 
