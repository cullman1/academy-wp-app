using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using O2Academy.Utils;
using System.IO;
using Newtonsoft.Json;


namespace O2Academy.Networking
{
    public abstract class BaseNetworkUtils
    {
       

       

        /// <summary>
        /// This method builds the JSON object depending on the type of request
        ///
        /// @param type of request to the server
        /// @param segment of the authenticated user
        /// @return a JSON string to post to server */
        /// </summary>
        protected static String buildObject(String type, String segment)
        {
            String data = "";
            if (segment == null)
            {
                //TODO: implement the correct handset model and OS in here
                data = "{\"data\":{\"msisdn\":\"" + O2Academy.getInstance().getMSISDN() + "\",\"version\":\"0.1;WindowsPhone7;Nokia Lumia\"},\"guid\":\"9999\",\"type\":\"" + type + "\"}";
            }
            else
            {
                data = "{\"data\":{\"customersegment\":\"" + segment + "\",\"msisdn\":\"" + O2Academy.getInstance().getMSISDN() + "\",\"version\":\"0.1;WindowsPhone7;Nokia Lumia\"},\"guid\":\"9999\",\"type\":\"" + type + "\"}";
            }
            return data;
        }

        public Segment getSegment()
        {
            string json = buildObject("getCustomerSegment", null);
            string response = makeRequest(json);
            Segment segment = JsonConvert.DeserializeObject<Segment>(response);
            return segment;
        }

        public string getPostPayPriceplan()
        {
            string json = buildObject("getpostpaypriceplan", O2Academy.getInstance().getSegment().data[0].segment);
            string response = makeRequest(json);
            return response;
        }

        public string getPostPaySummary()
        {
            string json = buildObject("getpostpayaccountsummary", O2Academy.getInstance().getSegment().data[0].segment);
            string response = makeRequest(json);
            return response;
        }

        public string getPrePaySummary()
        {
            string json = buildObject("getprepaysubinfo", O2Academy.getInstance().getSegment().data[0].segment);
            string response = makeRequest(json);
            return response;
        }

        public abstract string getStrings();

        public abstract string getTutorial();

        public abstract string makeRequest(string json);
    }
}
