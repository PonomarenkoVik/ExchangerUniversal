using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLogic.ExternalInterfaces;
using CommonLogic.ModelEntities;

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


        public Dictionary<string, Dictionary<string, IInstrument>> Instruments => _instrumentsByVendors;

        public async Task<ITradeResult> SendCommand(ITradeCommand command)
        {
            if (command == null)
                return null;

            var vendor = GetVendor(command);
            return await vendor.Execute(command);
        }

        public async Task<List<Order>> GetLevel2(IInstrument instrument)
        {
            if (instrument == null)
                return null;
            var vendor = GetVendor(instrument);
            return vendor != null ?  await vendor.GetLevel2List(instrument) : null;
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

        private IVendor GetVendor(IInstrument instrument)
        {
            if (instrument == null || instrument.Vendor == null || string.IsNullOrEmpty(instrument.Vendor.Name))
                return null;

            _vendors.TryGetValue(instrument.Vendor.Name, out IVendor vendor);
            return vendor;
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
