using System.Collections.Generic;

namespace BurstPaparazzi.core
{
    interface ITerminator
    {
        List<string> terminate(bool isIsolate);
    }
}
