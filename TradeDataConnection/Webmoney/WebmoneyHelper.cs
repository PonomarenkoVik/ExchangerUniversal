using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;

namespace TradeConnection.Webmoney
{
    internal class WebmoneyHelper
    {
        private static readonly string[] OrderSeparator = new string[]{"title=\"#"};
        private static readonly string[] OrderPointSeparator = new string[]{"align='right'>" , "</td>",  "<span>", "</span>" };
        private static readonly char[] FirstStringSeparator = new[] {' ', ';', ':'};
        private static readonly string[] OrderExceptString = new string[]{"<td", "<tr", "</tr", "&", "%", "div", "class" };
        private static readonly byte OrderSymbolNumber = 8;
        private static readonly byte OrderPointNumber = 8;
        private  static readonly int[] OrderIndexes = new int[]{0, 1, 3, 7};
        internal static List<Order> CreateOrdersByWebPage(string page)
        {
            List<Order> orders = new List<Order>();
            string orderId = String.Empty;
            string instrumentName = String.Empty;
            double straightCrossRate = double.NaN;
            double reverseCrossRate = double.NaN;
            DateTime applicationDate = DateTime.MinValue;
            double sum1 = double.NaN;
            double sum2 = double.NaN;


            string[] orderlines = page.Split(OrderSeparator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in orderlines)
            {
                List<string> orderPointlines = GetWebPageOrderPoints(line);
                if (orderPointlines == null || orderPointlines.Count < OrderPointNumber)
                    continue;
                orderId = orderPointlines[0];
               
                instrumentName = orderPointlines[1];
                if (!double.TryParse(orderPointlines[2], out reverseCrossRate))
                    continue;
              
                bool normalFormat = orderPointlines[5].Length == 19;
                string date = orderPointlines[normalFormat ? 5 : 8].Substring(0, 19);
                if (!DateTime.TryParseExact(date, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out applicationDate))
                    continue;
                if (!double.TryParse(orderPointlines[6], out sum1))
                    continue;
                if (!double.TryParse(orderPointlines[7], out sum2))
                    continue;
                if (!double.TryParse(orderPointlines[normalFormat ? 8 : 5], out straightCrossRate))
                    continue;

                if (!WebmoneyDataVendor.Instruments.TryGetValue(instrumentName, out IInstrument instrument))
                    continue;

                Order order = new Order(orderId, applicationDate, instrument, sum1, sum2, straightCrossRate, reverseCrossRate, false );
                orders.Add(order);
            }
            
            return orders;
        }

        private static List<string> GetWebPageOrderPoints(string line)
        {
            List<string> orderPointlines = new List<string>();
            if (line.Length < OrderSymbolNumber)
                return null;

            var num = line.Substring(0, OrderSymbolNumber);
            if (!int.TryParse(num, out int number))
                return null;

            orderPointlines.Add(num);

            string orderline = line.Substring(OrderSymbolNumber, line.Length - OrderSymbolNumber);
            var tempStrings = orderline.Split(OrderPointSeparator, StringSplitOptions.RemoveEmptyEntries);

            string[] firstStringSplit = tempStrings[0].Split(FirstStringSeparator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var orderIndex in OrderIndexes)
            {
                if (orderIndex < firstStringSplit.Length)
                    orderPointlines.Add(firstStringSplit[orderIndex]);
            }

            foreach (var tempString in tempStrings)
            {
                bool isPointOrder = true;
                foreach (var exceptString in OrderExceptString)
                {
                    if (tempString.Contains(exceptString))
                    {
                        isPointOrder = false;
                        break;
                    }
                }

                if (isPointOrder)
                    orderPointlines.Add(tempString);
            }

            return orderPointlines;
        }

        public static List<Order> CreateOrdersByXMLPage(string xmlPage)
        {
            throw new NotImplementedException();
        }

        public static List<Order> CreateOrdersByMixPage(string xmlPage)
        {
            throw new NotImplementedException();
        }
    }
}
