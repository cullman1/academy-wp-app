using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2Academy
{
    public class BuildSettings
    {

        public enum SERVERS { PROD, DEBUG, STATIC_SERVER };

        public enum PAGES { HOME, EVENTS };

        public static SERVERS getServer()
        {
            return SERVERS.STATIC_SERVER;
        }

        public static PAGES getPage(int pageid)
        {
            switch (pageid)
            {
                case 1:
                    return PAGES.HOME;
                case 2:
                    return PAGES.EVENTS;
               
                default:
                    return PAGES.HOME;
            }
        }

        public static string getStaticServerBaseUrl()
        {
            /** This is jeffs files */
            //return "https://s3-eu-west-1.amazonaws.com/myvodafonetesting/account_files/";
            /** This is marcs files */
            return "http://com.jigsaw.apps.s3.amazonaws.com/android/Vodafone/testfeeds/";
            /** This is chris files */
            //return "http://localhost/testfeeds/";
        }

    }
}
