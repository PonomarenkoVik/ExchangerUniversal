using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLogic.ExternalInterfaces
{
    public interface ITradableConnection
    {
        List<IVendor> GetVendors();
    }
}
