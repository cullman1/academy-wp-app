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
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Collections;
using O2Academy.Data;

namespace O2Academy.Networking
{
    /// <summary>
    /// This class contains static methods to parse prepay JSON responses
    /// </summary>
    public class StringDictionaryParser
    {

        private static String TAG = "StringDictionaryParser";

        /// <summary>
        /// Parses postpay account info strings and creates a single postpayAccountInfo object
        /// </summary>
        /// <param name="prePayJsonString"> The JSON response from the server for getprepaysubinfo</param>
        /// <returns>A complete PrepayAccontInfo object or null if null or an empty string is passed in</returns>
        public static ResourceDictionary parseStringDictionary(string stringDictJson)
        {

            StringDictionaryInfo stringDict = new StringDictionaryInfo();

            if (stringDictJson == null || stringDictJson.Equals("{}")) { return null; }

            XDocument rdRawXml = JsonConvert.DeserializeXNode(stringDictJson, "ResourceDictionary");


            XNamespace xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            XNamespace sys = "clr-namespace:System;assembly=mscorlib";
            XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";
            rdRawXml.Root.Name = xmlns + rdRawXml.Root.Name.LocalName;
            rdRawXml.Root.Add(new XAttribute(XNamespace.Xmlns + "sys", sys));
            rdRawXml.Root.Add(new XAttribute(XNamespace.Xmlns + "x", x));

            XDocument newRD = new XDocument(rdRawXml);
            newRD.Root.RemoveNodes();

            foreach (XElement xe in rdRawXml.Elements().Elements())
            {

                RenameElement(xe, "String", sys, x, newRD);
                string test = "test";
            }

            ResourceDictionary rd = (ResourceDictionary)System.Windows.Markup.XamlReader.Load(newRD.ToString());



            return rd;

        }

        public static XElement RenameElement(XElement e, string newName, XNamespace ns, XNamespace ns2, XDocument newRD)
        {
            XElement newElement = new XElement(ns + newName);
            newElement.SetValue(e.Value);
            XName xn = e.Name;
            newElement.Add(new XAttribute(ns2 + "Key", xn.LocalName));


            newElement.Add(e.Elements());
            //e.ReplaceWith(newElement);
            newRD.Root.Add(newElement);
            return newElement;
        }
    }


    public class StringDictionaryJsonObject
    {
        public int errorCode { get; set; }
        public string message { get; set; }
        public string success { get; set; }
        public string requestType { get; set; }
    }





}


