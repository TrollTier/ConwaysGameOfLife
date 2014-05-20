using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{
    class GliderFieldInitializer : IFieldInitializer
    {
        /// <summary>
        /// Initializes a glider, starting at coordinates (0,0).
        /// </summary>
        /// <param name="field">The field to set the start cells for.</param>
        public void Initialize(Field field)
        {
            field.SetCell(0, 1, true);
            field.SetCell(1, 2, true);
            field.SetCell(2, 0, true);
            field.SetCell(2, 1, true);
            field.SetCell(2, 2, true); 
        }

        public void Initialize(Field field, int startX, int startY)
        {
            field.SetCell(startY, startX + 1, true);
            field.SetCell(startY + 1, startX + 2, true);
            field.SetCell(startY + 2, startX, true);
            field.SetCell(startY + 2, startX + 1, true);
            field.SetCell(startY + 2, startX + 2, true); 
        }
    }
}
