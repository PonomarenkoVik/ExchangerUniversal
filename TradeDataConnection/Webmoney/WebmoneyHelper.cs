using System;
using System.Collections.Generic;
using System.Text;
using CommonLogic.ModelEntities;

namespace TradeConnection.Webmoney
{
    internal class WebmoneyHelper
    {
        private static string OrderSeparator = "title=\"#";
        private static string OrderPointSeparator = ";";

        internal static List<Order> CreateOrdersByPage(StringBuilder page)
        {
            List<Order> orders = new List<Order>();
            string[] lines = page.ToString().Split(OrderSeparator.ToCharArray());
            foreach (var line in lines)
            {
                if (!line.Contains(OrderSeparator))
                continue;
                string[] orderLines = line.Replace(" ", string.Empty).Split(OrderPointSeparator.ToCharArray());
                int id = int.Parse(orderLines[0]);

            }
        }



    }
}
