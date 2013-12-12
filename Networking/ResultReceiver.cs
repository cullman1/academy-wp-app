using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2Academy.Networking
{
    public interface ResultReceiver
    {
        void send(int result, String value);
    }
}
