namespace WordlePaint.Core;

public class Pattern
{
    public Row[] rows = new Row[6];

    public Pattern(Row[] p_rows)
    {
        rows = p_rows;
    }
}