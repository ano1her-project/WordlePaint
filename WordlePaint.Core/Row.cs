using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordlePaint.Core;

public class Row
{
    public LetterColor[] cells = new LetterColor[5];

    public Row(LetterColor[] p_cells)
    {
        cells = p_cells;
    }

    public Row (LetterColor cell1, LetterColor cell2, LetterColor cell3, LetterColor cell4, LetterColor cell5) : this([cell1, cell2, cell3, cell4, cell5]) { }
}
