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
            _connections = connections;
        }


        public  static  ITradeTerminal GetInstance(List<ITradableConnection> connections) => new TradeTerminal(connections);


        public Dictionary<string, List<string>> Instruments
        {
            get
            {
                if (_instrumentsByVendors == null)
                    InstrumentInitialize();
                if (_instrumentsByVendors == null || _instrumentsByVendors.Count == 0)
                    return null;
                return new Dictionary<string, List<string>>(_instrByVendorStr);
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
            if (_instrumentsByVendors == null)
                InstrumentInitialize();

            if (!_instrumentsByVendors.TryGetValue(vendorName, out Dictionary<string, IInstrument> instruments))
                return null;           

            return !instruments.TryGetValue(instrName, out IInstrument instrument) ? null : instrument;
        }


        private IVendor GetVendor(ITradeCommand command)
        {
            if (command == null || !string.IsNullOrEmpty(command.VendorName))
                return null;

            _vendors.TryGetValue(command.VendorName, out IVendor vendor);
            return vendor;
        }

        private void InstrumentInitialize()
        {
            _vendors = new Dictionary<string, IVendor>();
            _instrumentsByVendors = new Dictionary<string, Dictionary<string, IInstrument>>();

            foreach (ITradableConnection connection in _connections)
            {
                var vendors = connection.GetVendors();
                foreach (IVendor vendor in vendors)
                {
                    if (_vendors.ContainsKey(vendor.Name))
                        continue;
                    var instruments = vendor.GetInstruments().Result;
                    if (instruments == null || instruments.Count < 1)
                        continue;
                    _vendors.Add(vendor.Name, vendor);
                    _instrumentsByVendors.Add(vendor.Name, instruments);
                }

                _instrByVendorStr = new Dictionary<string, List<string>>();
                foreach (var instrument in _instrumentsByVendors)
                {
                    var instrList = instrument.Value.Select((i) => i.Key).ToList();
                    _instrByVendorStr.Add(instrument.Key, instrList);
                }
            }
        }

        private Dictionary<string, List<string>> _instrByVendorStr;
        private Dictionary<string, Dictionary<string, IInstrument>> _instrumentsByVendors;
        private Dictionary<string, IVendor> _vendors;
        private List<ITradableConnection> _connections;
    }
}
