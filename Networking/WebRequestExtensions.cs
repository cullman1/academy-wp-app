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

using System.Threading;
using System.IO;

namespace O2Academy.Networking
{
    /// <summary>
    ///This class is a wrapper on WebRequest to allow syncrhonous web calls
    ///Useful if networking on a seperate thread already
    ///Author: Marc Liddell 
    /// </summary>
    public static class WebRequestExtensions
    {

        /// <summary>
        /// Completes the passed in request synchrounously
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The response from the WebRequest</returns>
        public static WebResponse GetResponse(this WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult asyncResult = request.BeginGetResponse(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetResponse(asyncResult);
        }

        /// <summary>
        /// Completes the passed in request synchrounously
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The completed stream from the WebRequest</returns>
        public static Stream GetRequestStream(this WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult asyncResult = request.BeginGetRequestStream(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetRequestStream(asyncResult);
        }



    }
}
