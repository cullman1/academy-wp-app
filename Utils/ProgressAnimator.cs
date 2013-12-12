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


namespace O2Academy.Utils
{
    public class ProgressAnimator
    {
        ProgressBar mPb;
        int mTargetValue;
        int currentPostion;

        public ProgressAnimator(ProgressBar pb, int maxValue, int targetValue)
        {
            mPb = pb;
            mTargetValue = targetValue;
            currentPostion = maxValue;
        }

        public void animate()
        {
            while (currentPostion > mTargetValue)
            {

                currentPostion = currentPostion - 1;
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    mPb.Value = currentPostion;
                });
                Thread.Sleep(7);
            }
        }
    }
}
