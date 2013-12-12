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
using System.IO.IsolatedStorage;

namespace O2Academy.AndroidHelperClasses
{
    /// <summary>
    ///This is an android replicated shared preferences class, so that android code can be copied and pasted into this project
    ///Author: Marc Liddell
    /// </summary>
    public class SharedPreferences
    {
        private Editor editor;

        public Editor edit()
        {
            if (editor == null)
            {
                editor = new Editor();
            }
            return editor;
        }

        public String getString(String key, String defaultValue)
        {
            String name;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out name))
            {
                return name;
            }
            else
            {
                return defaultValue;
            }
        }

        public bool getBool(String key, bool defaultValue)
        {
            bool name;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out name))
            {
                return name;
            }
            else
            {
                return defaultValue;
            }
        }

        public long getLong(String key, long defaultValue)
        {
            long name;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out name))
            {
                return name;
            }
            else
            {
                return defaultValue;
            }
        }

        public int getInt(String key, int defaultValue)
        {
            int name;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out name))
            {
                return name;
            }
            else
            {
                return defaultValue;
            }
        }

        public class Editor
        {

            public void putString(string key, string newValue)
            {

                IsolatedStorageSettings.ApplicationSettings[key] = newValue;
            }

            public void putBool(string key, bool newValue)
            {
                IsolatedStorageSettings.ApplicationSettings[key] = newValue;
            }

            public void putLong(string key, long newValue)
            {
                IsolatedStorageSettings.ApplicationSettings[key] = newValue;
            }

            public void putInt(string key, int newValue)
            {
                IsolatedStorageSettings.ApplicationSettings[key] = newValue;
            }
        }
    }
}

