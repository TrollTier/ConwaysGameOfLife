using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysWayOfLife
{
    class Field
    {
        #region fields

        /// <summary>
        /// The cells that define this field.
        /// </summary>
        //private Cell[,] cells;
        private bool[] cells; 

        /// <summary>
        /// The number of columns of this field.
        /// </summary>
        private int columns; 

        /// <summary>
        /// The number of rows of this field.
        /// </summary>
        private int rows;        

        #endregion

        #region properties

        /// <summary>
        /// Gets the available cells.
        /// </summary>
        public IEnumerable<bool> Cells
        {
            get { return cells; }
        }

        /// <summary>
        /// Gets the number of loaded cells.
        /// </summary>
        public int Count
        {
            get { return cells.Length; }
        }

        /// <summary>
        /// Gets the number of rows of this field.
        /// </summary>
        public int Columns
        {
            get { return columns; }
        }

        /// <summary>
        /// Gets the number of rows of this field.
        /// </summary>
        public int Rows
        {
            get { return rows; }
        }

        #endregion

        #region ctors

        /// <summary>
        /// Initializes a new instance of the Field class.
        /// </summary>
        /// <param name="rows">The number of rows on the field.</param>
        /// <param name="columns">The number of columns on the field.</param>
        public Field(int rows, int columns)
        {
            if (rows < 3)
                throw new ArgumentException("Number of rows has to be at least 3.");

            if (columns < 3)
                throw new ArgumentException("Nunber of columns has to be at least 3.");

            this.rows = rows;
            this.columns = columns; 
        }

        public void InitializeCells()
        {
            this.cells = new bool[rows * columns];

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = false; 
            }

            //this.cells = new Cell[rows, columns];

            //for (int y = 0; y < rows; y++)
            //{
            //    for (int x = 0; x < columns; x++)
            //    {
            //        cells[y, x] = new Cell(false); 
            //    }
            //}
        }

        #endregion

        #region methods

        /// <summary>
        /// Gets the cell at the specified positition in the 2-dimensional field
        /// or null, if the coordinates are out of bounds.
        /// </summary>
        /// <param name="row">The row to search at.</param>
        /// <param name="column">The column to search at.</param>
        public bool GetCellAt(int row, int column)
        {
            if (row >= 0 && row < this.rows
                && column >= 0 && column < this.columns)
            {
                return cells[(row * columns) + column]; 
            }

            return false; 
        }

        /// <summary>
        /// Sets the is alive state of all cells to the specified value.
        /// </summary>
        /// <param name="isAlive">The is alive state to set the cells to.</param>
        public void SetAll(bool isAlive)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = isAlive;
            }
        }

        /// <summary>
        /// Sets the is alive state of the specified cell.
        /// </summary>
        /// <param name="row">The row of the cell to set.</param>
        /// <param name="column">The column of the cell to set.</param>
        /// <param name="isAlive">The value to set for the cell.</param>
        public void SetCell(int row, int column, bool isAlive)
        {
            if (row >= 0 && row < this.rows
                && column >= 0 && column < this.columns)
            {
                cells[(row * columns) + column] = isAlive; 
            }
        }

        public void SetField(bool[] newField)
        {
            cells = newField; 
        }

        /// <summary>
        /// Updates the is alive state of all cells.
        /// </summary>
        /// <param name="rule">The rule to determine the alive state of the cell with.</param>
        public void UpdateCells(ICellRule rule)
        {
            bool[] newValues = new bool[rows * columns];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    newValues[(y * columns) + x]
                        = GetCellValue(cells[(y * columns) + x], x, y, rule);
                }
            }

            cells = newValues; 
        }

        private bool GetCellValue(bool cell, int currX, int currY, ICellRule rule)
        {
            bool neighbour;
            int livingNeighbours = 0;

            for (int dX = -1; dX <= 1; dX++)
            {
                for (int dY = -1; dY <= 1; dY++)
                {
                    if (!(dX == 0 && dY == 0))
                    {
                        neighbour = GetCellAt(currY + dY, currX + dX);
                        if (neighbour)
                            livingNeighbours++;
                    }
                }
            }

            return rule.GetAliveState(cell, livingNeighbours); 
        }

        #endregion
    }
}
