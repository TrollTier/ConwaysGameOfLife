using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{ 
    class MagnetsFieldInitializer : IFieldInitializer
    {
        public void Initialize(Field field, int startX, int startY)
        {
            field.SetCell(startX, startY, true);
            field.SetCell(startX + 1, startY, true);
            field.SetCell(startX + 2, startY, true);

            field.SetCell(startX, startY + 1, true);
            field.SetCell(startX + 2, startY + 1, true);

            field.SetCell(startX, startY + 2, true);
            field.SetCell(startX + 2, startY + 2, true);

            field.SetCell(startX, startY + 4, true);
            field.SetCell(startX + 2, startY + 4, true);

            field.SetCell(startX, startY + 5, true);
            field.SetCell(startX + 2, startY + 5, true);

            field.SetCell(startX, startY + 6, true);
            field.SetCell(startX + 1, startY + 6, true);
            field.SetCell(startX + 2, startY + 6, true); 
        }
    }
}
