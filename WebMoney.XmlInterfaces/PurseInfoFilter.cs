﻿using System;
using System.Globalization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class PurseInfoFilter : WmRequest<PurseInfoRegister>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLPurses.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLPursesCert.asp";

        public WmId WmId { get; set; }

        protected internal PurseInfoFilter()
        {
        }

        public PurseInfoFilter(WmId wmId)
        {
            WmId = wmId;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}", WmId, requestNumber);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("getpurses"); // <getpurses>

            xmlRequestBuilder.WriteElement("wmid", WmId.ToString());

            xmlRequestBuilder.WriteEndElement(); // </getpurses>
        }
    }
}