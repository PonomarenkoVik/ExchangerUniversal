using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;
using  System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace CommonLogic.Commons
{
     public class TradeTerminal : ITradeTerminal
    {
        public TradeTerminal(List<ITradableConnection> connections)
        {
            Initialize(connections);
        }

        private void Initialize(List<ITradableConnection> connections)
        {
            _vendors = new Dictionary<string, IVendor>();
            _instrumentsByVendors = new Dictionary<string, Dictionary<string, IInstrument>>();

            foreach (ITradableConnection connection in connections)
            {
                var vendors = connection.GetVendors();
                foreach (IVendor vendor in vendors)
                {
                    if (_vendors.ContainsKey(vendor.Name))
                    continue;
                    var instruments = vendor.GetInstruments();
                    if (instruments == null || instruments.Count < 1)
                    continue;
                    _vendors.Add(vendor.Name, vendor);
                    _instrumentsByVendors.Add(vendor.Name, vendor.GetInstruments());
                }
               
            }
        }

        public  static  ITradeTerminal GetInstance(List<ITradableConnection> connections) => new TradeTerminal(connections);


        public Dictionary<string, List<string>> Instruments
        {
            get
            {
                if (_instrumentsByVendors == null || _instrumentsByVendors.Count == 0)
                    return null;

                Dictionary<string, List<string>> instruments = new Dictionary<string, List<string>>();
                foreach (var instrument in _instrumentsByVendors)
                {
                    var instrList = instrument.Value.Select((i) => i.Key).ToList();
                    instruments.Add(instrument.Key, instrList);
                }
                return instruments;
            }
        }

        public async Task<ITradeResult> SendCommand(ITradeCommand command)
        {
            if (command == null)
                return null;

            var vendor = GetVendor(command);
            return await vendor.Execute(command);
        }

        public Task<List<Order>> GetLevel2(string vendorName, string instrumentName, int sourceType)
        {
            var instrument = GetInstrument(vendorName, instrumentName);
            return instrument?.GetLevel2(sourceType);
        }

        public IInstrument GetInstrument(string vendorName, string instrName)
        {
            if (!_vendors.TryGetValue(vendorName, out IVendor vendor))
                return null;

            var instruments = vendor.GetInstruments();
            if (instruments == null || instruments.Count < 1)
                return null;

            if (!instruments.TryGetValue(instrName, out IInstrument instrument))
                return null;

            return instrument;
        }


        private IVendor GetVendor(ITradeCommand command)
        {
            if (command == null || !string.IsNullOrEmpty(command.VendorName))
                return null;

            _vendors.TryGetValue(command.VendorName, out IVendor vendor);
            return vendor;
        }


        private Dictionary<string, Dictionary<string, IInstrument>> _instrumentsByVendors;
        private Dictionary<string, IVendor> _vendors;
    }
}
