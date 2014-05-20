using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{
    class CrossFieldInitializer : IFieldInitializer
    {
        public void Initialize(Field field, int startX, int startY)
        {
            field.SetCell(startY, startX, true);
            field.SetCell(startY - 1, startX, true);
            field.SetCell(startY - 2, startX, true);
            field.SetCell(startY + 1, startX, true);
            field.SetCell(startY + 2, startX, true);
            field.SetCell(startY, startX - 1, true);
            field.SetCell(startY, startX - 2, true);
            field.SetCell(startY, startX + 1, true);
            field.SetCell(startY, startX + 2, true); 
        }
    }
}
