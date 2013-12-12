using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O2Academy.AndroidHelperClasses;

namespace O2Academy.Utils
{
    public class O2AcademyPrefs
    {
        private static string TAG = "O2AcademyPrefs";
        private static String PREF_TERMS_ACCEPTED = "terms_accepted";
        private static String PREF_TUTORIAL_VIEWED = "tutorial_viewed";
        private static String PREF_TERMS_VIEWED = "terms_viewed";
        private static String PREF_ACCOUNT_TYPE = "account_type";
        private static String PREF_TUTORIAL_CURRENT_PAGE = "tutorial_current_page";
        private static String PREF_MSISDN = "MSISDN";
        private static String PREF_IMSI = "IMSI";
        private static String PREF_HAS_USER_BEEN_WARN_OF_WIFI_PRICES = "warn_user_first_time";
        private static String PREF_SEGMENT = "SEGMENT";
        private static String PREF_SEGMENT_ERROR = "SEGMENT_ERROR";
        private static String PREF_BUSINESS = "BUSINESS";
        private static String PREF_RETRY_BUTTON_EXPIRY = "reset_button_expiry";
        private static String PREF_DATA_UPDATE = "data_update";
        private static String PREF_DATA_EXPIRY = "data_expiry";

        // App Rating count preferences
        private String PREF_APP_RATING_DATE = "app_rating_date";
        private static String PREF_APP_RATING_COUNT = "app_rating_count";

        // BTOPENZONE preferences
        private static String PREF_BTOPENZONE = "btOpenzone";
        private static String PREF_AUTOCONNECT = "autoconnect";
        private static String PREF_ENABLED_WIFI = "enablewifi";
        private static String PREF_TIME_ENABLED_WIFI = "time_enablewifi";
        private static String PREF_LOGOFF_URL = "logoff_url";
        private static String PREF_SCREEN_TIME_ON = "screen_time_on";
        private static String PREF_LAST_PROMO_SHOWN = "lastPromoShown";

        private static O2AcademyPrefs self;
        private SharedPreferences preferences = null;
        private SharedPreferences.Editor editor = null;

        private bool btOpenzone = true;
        private bool btOpenzoneAutoConnect = false;
        private bool didAppEnableWiFi = false;
        private long wifiEnabledTime = 0l;
        private long screenOnTime = 0l;
        private String logOffUrl = null;
        private bool termsAccepted = false;
        private bool tutorialViewed = false;
        private bool termsViewed = false;
        private string accountType = "";
        private int tutorialCurrentPage = 0;
        private int lastPromoShown = 0;

        private bool userHasBeenWarnedOfFees = false;
        private long retryButtonEnableTime = -1l;
        private long dataUpdateTime = -1l;
        private long dataExpiryTime = -1l;
        private String msisdn = null;
        private Segment segment = null;
        private String imsi = null;
        private bool isBusinessUser = false;
        private String WiFiStartCode;

        /** This is Synchronous, which is bad, but we should initialise in an async task on start up, but need the context passed incase we lose it in the
         * future
         * 
         * @param context */
        public static O2AcademyPrefs getInstance()
        {
            if (self == null)
            {
                self = new O2AcademyPrefs();
            }
            return self;
        }

        private O2AcademyPrefs()
        {
            segment = new Segment();
            preferences = new SharedPreferences();
            editor = preferences.edit();
            loadPreferences();

        }

        public bool getTutorialViewed()
        {
            return tutorialViewed;
        }

        public void setTutorialViewed(bool newTutorialViewed)
        {
            tutorialViewed = newTutorialViewed;
            savePrefsAsync();
        }

        public bool getTermsViewed()
        {
            return termsViewed;
        }

        public void setTermsViewed(bool newTermsViewed)
        {
            termsViewed = newTermsViewed;
            savePrefsAsync();
        }


        /** Notify to say t&c's have been accepted */
        public void setTermsAccepted()
        {
            // Set to false to indicate next time is not the first launch
            termsAccepted = true;
            savePrefsAsync();
        }

        /** Checks if the Terms and Conditions have been accepted */
        public bool getTermsAccepted()
        {
            return termsAccepted;
        }

        public void setRetryButtonEnableTime(long newRetryButtonEnableTime)
        {
            retryButtonEnableTime = newRetryButtonEnableTime;
            savePrefsAsync();
        }

        public long getRetryButtonEnableTime()
        {
            return retryButtonEnableTime;
        }

        /** Set the time at which the data should be updated if possible */
        public void setDataUpdateTime(long newdataExpiryTime)
        {
            dataUpdateTime = newdataExpiryTime;
            savePrefsAsync();
        }

        /** Get the time at which the data should be updated if possible */
        public long getDataUpdateTime()
        {
            return dataUpdateTime;
        }

        /** Set the time at which the data will expire and will no longer be valid to be shown to user */
        public void setDataExpiryTime(long newdataExpiryTime)
        {
            dataExpiryTime = newdataExpiryTime;
            savePrefsAsync();
        }

        /** Get the time at which the data will expire and will no longer be valid to be shown to user */
        public long getDataExpiryTime()
        {
            return dataExpiryTime;
        }

        /** Set MSISDN */
        public void setMSISDN(String newMSISDN)
        {
            msisdn = newMSISDN;
            savePrefsAsync();
        }

       

        /** set SEGMENT */
        public void setSegment(Segment segment)
        {
            this.segment.data[0].segment = segment.data[0].segment;
            this.segment.errorCode = segment.errorCode;
            savePrefsAsync();
        }

        /** get SEGMENT */
        public Segment getSegment()
        {
            return segment;
        }

        /** set IMSI */
        public void setIMSI(String imsi)
        {
            this.imsi = imsi;
            savePrefsAsync();
        }

        /** set IMSI */
        public String getIMSI()
        {
            return imsi;
        }

        public void setBusiness(bool userIsBusiness)
        {
            isBusinessUser = userIsBusiness;
            savePrefsAsync();
        }

        /** Returns true if the user is stored as a business user */
        public bool getBusiness()
        {
            return isBusinessUser;
        }

        /** reset all cached IMSI and MSISDN settings and set the current IMSI */
        public void clearImsiAndMsidn()
        {

            // delete the old data
            Log.d(TAG, "Resetting IMSI and MSISDN");
            setIMSI(null);
            setMSISDN(null);
            Segment segment = new Segment();
            segment.data[0].segment = null;
            setSegment(segment);
        }

        public int getLastPromoShown()
        {
            return lastPromoShown;
        }

        public void setLastPromoShown(int position)
        {
            Log.e(TAG, "setting last promo id =" + position);
            lastPromoShown = position;
            savePrefsAsync();
        }

        private void savePrefsAsync()
        {

            savePreferences();

        }

        private void savePreferences()
        {
            editor.putBool(PREF_BTOPENZONE, btOpenzone);
            editor.putLong(PREF_DATA_EXPIRY, dataExpiryTime);
            editor.putBool(PREF_AUTOCONNECT, btOpenzoneAutoConnect);
            editor.putBool(PREF_ENABLED_WIFI, didAppEnableWiFi);
            editor.putLong(PREF_TIME_ENABLED_WIFI, wifiEnabledTime);
            editor.putLong(PREF_SCREEN_TIME_ON, screenOnTime);
            editor.putString(PREF_LOGOFF_URL, logOffUrl);
            editor.putBool(PREF_TERMS_ACCEPTED, termsAccepted);
            editor.putBool(PREF_HAS_USER_BEEN_WARN_OF_WIFI_PRICES, userHasBeenWarnedOfFees);
            editor.putLong(PREF_RETRY_BUTTON_EXPIRY, retryButtonEnableTime);
            editor.putLong(PREF_DATA_UPDATE, dataUpdateTime);
            editor.putString(PREF_MSISDN, msisdn);
            editor.putString(PREF_SEGMENT, segment.data[0].segment);
            editor.putInt(PREF_SEGMENT_ERROR, segment.errorCode);
            editor.putString(PREF_IMSI, imsi);
            editor.putBool(PREF_BUSINESS, isBusinessUser);
            editor.putBool(PREF_TUTORIAL_VIEWED, false);
            editor.putBool(PREF_TERMS_VIEWED, false);
            editor.putString(PREF_ACCOUNT_TYPE, accountType);
            editor.putInt(PREF_TUTORIAL_CURRENT_PAGE, 1);
            editor.putInt(PREF_LAST_PROMO_SHOWN, lastPromoShown);

        }

        private void loadPreferences()
        {
            segment = new Segment();
            dataExpiryTime = preferences.getLong(PREF_DATA_EXPIRY, 0);
            termsAccepted = preferences.getBool(PREF_TERMS_ACCEPTED, false);
            retryButtonEnableTime = preferences.getLong(PREF_RETRY_BUTTON_EXPIRY, 0l);
            dataUpdateTime = preferences.getLong(PREF_DATA_UPDATE, 0l);
            segment.data[0].segment = preferences.getString(PREF_SEGMENT, "");
            segment.errorCode = preferences.getInt(PREF_SEGMENT_ERROR, 0);
            msisdn = preferences.getString(PREF_MSISDN, null);
            imsi = preferences.getString(PREF_IMSI, null);
            isBusinessUser = preferences.getBool(PREF_BUSINESS, false);
            tutorialViewed = preferences.getBool(PREF_TUTORIAL_VIEWED, false);
            termsViewed = preferences.getBool(PREF_TERMS_VIEWED, false);
            accountType = preferences.getString(PREF_ACCOUNT_TYPE, "");
            tutorialCurrentPage = preferences.getInt(PREF_TUTORIAL_CURRENT_PAGE, 1);
            lastPromoShown = preferences.getInt(PREF_LAST_PROMO_SHOWN, 0);
        }

        public void clearPreferences()
        {

            editor.putString(PREF_SEGMENT, null);
            editor.putInt(PREF_SEGMENT_ERROR, 0);
            editor.putBool(PREF_BTOPENZONE, true);
            editor.putString(PREF_MSISDN, null);
            editor.putBool(PREF_BUSINESS, false);
            editor.putBool(PREF_TUTORIAL_VIEWED, false);
            editor.putBool(PREF_TERMS_VIEWED, false);
            editor.putBool(PREF_AUTOCONNECT, false);
            editor.putBool(PREF_TERMS_ACCEPTED, false);
            editor.putBool(PREF_HAS_USER_BEEN_WARN_OF_WIFI_PRICES, false);
            editor.putLong(PREF_RETRY_BUTTON_EXPIRY, 0);
            editor.putLong(PREF_DATA_UPDATE, 0);
            editor.putLong(PREF_DATA_EXPIRY, 0);
            editor.putBool(PREF_ENABLED_WIFI, false);
            editor.putString(PREF_IMSI, null);
            editor.putInt(PREF_LAST_PROMO_SHOWN, 0);
            editor.putString(PREF_ACCOUNT_TYPE, "");

            dataUpdateTime = 0;
            dataExpiryTime = 0;
            loadPreferences();
        }
    }
}
