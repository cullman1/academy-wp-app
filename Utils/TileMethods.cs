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
using O2Academy.Data;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;

namespace O2Academy.Utils
{
    public class TileMethods
    {
        public static string getTileText()
        {
            //Code - added C. Ullman - for live tiles
            string tileText = "";

           
            return tileText;
        }

        public static string getTileTitle()
        {

           
            return "";
        }

        public static void createLiveBackTile()
        {

            WriteableBitmap bmp = new WriteableBitmap(173, 173);

            Canvas can = new Canvas();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            // Describes the brush's color using RGB values. 
            // Each value has a range of 0-255.
            mySolidColorBrush.Color = Color.FromArgb(255, 228, 19, 0);

            can.Background = mySolidColorBrush;
            can.Width = 173;
            can.Height = 173;

            bmp.Render(can, null);


            var bl = new TextBlock();
            bl.Foreground = new SolidColorBrush(Colors.White);
            bl.FontSize = 22.0;
            Thickness myThickness = new Thickness();
            myThickness.Top = 10;
            myThickness.Left = 5;
            bl.Padding = myThickness;
            bl.Text = getTileText();

            bmp.Render(bl, null);

            bmp.Invalidate();

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var filename = "/Shared/ShellContent/testtile.jpg";
                using (var st = new IsolatedStorageFileStream(filename, FileMode.Create, FileAccess.Write, store))
                {
                    bmp.SaveJpeg(st, 173, 173, 0, 100);
                }
            }



        }

        public static void createLiveFrontTile()
        {
            var logo2 = new BitmapImage(new Uri("Images/icon.png", UriKind.Relative));


            logo2.CreateOptions = BitmapCreateOptions.None;

            logo2.ImageOpened += (s2, e2) =>
            {

                WriteableBitmap bmp2 = new WriteableBitmap((BitmapImage)s2);

                var img2 = new Image { Source = logo2 };
                // Force the bitmapimage to load it's properties so the transform will work
                logo2.CreateOptions = BitmapCreateOptions.None;

                var tt2 = new TranslateTransform();
                tt2.X = 173 - logo2.PixelWidth;
                tt2.Y = 173 - logo2.PixelHeight;

                bmp2.Render(img2, tt2);

                bmp2.Invalidate();

                using (var store2 = IsolatedStorageFile.GetUserStoreForApplication())
                {

                    var filename2 = "/Shared/ShellContent/icon.jpg";
                    using (var st2 = new IsolatedStorageFileStream(filename2, FileMode.Create, FileAccess.Write, store2))
                    {
                        bmp2.SaveJpeg(st2, 173, 173, 0, 100);
                    }
                }
            };
        }
    }
}
