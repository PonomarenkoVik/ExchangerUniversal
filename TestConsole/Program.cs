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
        
            var orders = tradeTerminal.GetLevel2Async("wm.exchanger.ru", "WMZ/WMU", 2);
           
        }

         private static List<Order> GetLevel2(IInstrument instrument, ITradeTerminal terminal)
        {
            return terminal.GetLevel2Async("wm.exchanger.ru", "WMZ/WMU", 2);
        }
    }
} 
