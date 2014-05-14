using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysWayOfLife
{
    interface ICellRule
    {
        bool GetAliveState(bool isCellAlive, int livingNeighbours);
    }
}
