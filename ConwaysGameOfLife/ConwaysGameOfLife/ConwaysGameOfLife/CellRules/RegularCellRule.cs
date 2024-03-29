﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{
    class RegularCellRule : ICellRule
    {
        public bool GetAliveState(bool isCellAlive, int livingNeighbours)
        {
            return ((isCellAlive && (livingNeighbours == 2)) || livingNeighbours == 3);
        }
    }
}
