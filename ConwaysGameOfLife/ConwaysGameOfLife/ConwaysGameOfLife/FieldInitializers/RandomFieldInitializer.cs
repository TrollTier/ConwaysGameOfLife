using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{
    class RandomFieldInitializer : IFieldInitializer
    {
        void IFieldInitializer.Initialize(Field field, int startX, int startY)
        {
            Random random = new Random();

            int x;
            int y;

            field.SetAll(false); 
            for (int i = 0; i < (field.Columns * field.Rows) * 0.5; i++)
            {
                x = random.Next(0, field.Columns);
                y = random.Next(0, field.Rows);

                field.SetCell(y, x, true);
            }
        }
    }
}
