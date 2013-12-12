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
using System.IO;
using Newtonsoft.Json;

using O2Academy.Utils;
using O2Academy.Data;

namespace O2Academy.Networking
{
    /// <summary>
    /// This class is the static file implementation of the network utils, used for development and testing
    /// </summary>
    public class NetworkUtilsStatic : BaseNetworkUtils
    {
        private static String TAG = "NetworkUtilsStatic";



        public override string getStrings()
        {
            Uri staticFiles = new Uri(BuildSettings.getStaticServerBaseUrl() + "strings.JSON");

            WebRequest req = WebRequest.Create(staticFiles);
            WebResponse resp = WebRequestExtensions.GetResponse(req);
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, System.Text.Encoding.GetEncoding("iso-8859-1"));
            string summaryResponse = reader.ReadToEnd();

            return summaryResponse;
        }

        public override string getTutorial()
        {
            Uri staticFiles = new Uri(BuildSettings.getStaticServerBaseUrl() + "tutorial_feed.JSON");

            WebRequest req = WebRequest.Create(staticFiles);
            WebResponse resp = WebRequestExtensions.GetResponse(req);
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string summaryResponse = reader.ReadToEnd();

            return summaryResponse;
        }


        public override String makeRequest(String jsonPostString)
        {

            String debugURL = null;

            if (jsonPostString.ToLower().Contains("getcustomersegment"))
            {
                debugURL = BuildSettings.getStaticServerBaseUrl() + "getcustomersegment.JSON";
            }
            else if (jsonPostString.ToLower().Contains("getpostpayaccountsummary"))
            {
                debugURL = BuildSettings.getStaticServerBaseUrl() + "getpostpayaccountsummary.JSON";
            }
            else if (jsonPostString.ToLower().Contains("getpostpaybillbalance"))
            {
                throw new Exception("Should not use this anymore!! getpostpaybillbalance");
            }
            else if (jsonPostString.ToLower().Contains("getprepaysubinfo"))
            {
                debugURL = BuildSettings.getStaticServerBaseUrl() + "getprepaysubinfo.JSON";
            }
            else if (jsonPostString.Contains("getpostpaypriceplan"))
            {
                debugURL = BuildSettings.getStaticServerBaseUrl() + "getpostpaypriceplan.JSON";
            }
            else if (jsonPostString.ToLower().Contains("getconfigdetails"))
            {
                debugURL = BuildSettings.getStaticServerBaseUrl() + "getconfigdetails.JSON";
            }
            // else tips
            else
            {
                debugURL = BuildSettings.getStaticServerBaseUrl() + "gethelp.JSON";
            }
            Random rnd = new Random();
            debugURL = debugURL + "?param=" + rnd.Next(10000);
            Log.d(TAG, "loading url - " + debugURL);

            Uri staticFiles = new Uri(debugURL);

            WebRequest req = WebRequest.Create(staticFiles);
            WebResponse resp = WebRequestExtensions.GetResponse(req);
            Stream dataStream = resp.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, System.Text.Encoding.GetEncoding("iso-8859-1"));
            string prepayResponse = reader.ReadToEnd();

            return prepayResponse;

        }

    }
}
