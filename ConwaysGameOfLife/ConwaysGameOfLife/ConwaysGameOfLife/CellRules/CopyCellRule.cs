using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{
    class CopyCellRule : ICellRule
    {
        public bool GetAliveState(bool isCellAlive, int livingNeighbours)
        {
            return livingNeighbours % 2 == 1; 
        }
    }
}
