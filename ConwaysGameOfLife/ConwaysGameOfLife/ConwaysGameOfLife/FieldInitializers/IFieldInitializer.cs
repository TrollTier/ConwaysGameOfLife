using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{
    interface IFieldInitializer
    {
        void Initialize(Field field, int startX, int startY);
    }
}
