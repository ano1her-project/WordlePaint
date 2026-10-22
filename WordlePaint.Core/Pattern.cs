namespace WordlePaint.Core;

public class Pattern
{
    public Row[] rows = new Row[6];

    public Pattern(Row[] p_rows)
    {
        rows = p_rows;
    }

    public readonly static Dictionary<string, Pattern> presets = new() {
        { "green67", new([Row.FromString("XXGGG"), Row.FromString("GGGXG"), Row.FromString("GXXXG"), Row.FromString("GGGXG"), Row.FromString("GXGXG"), Row.FromString("GGGXX")])},
        { "yellow67", new([Row.FromString("XXYYY"), Row.FromString("YYYXY"), Row.FromString("YXXXY"), Row.FromString("YYYXY"), Row.FromString("YXYXY"), Row.FromString("YYYXX")])},
        { "green6yellow7", new([Row.FromString("XXYYY"), Row.FromString("GGGXY"), Row.FromString("GXXXY"), Row.FromString("GGGXY"), Row.FromString("GXGXY"), Row.FromString("GGGXX")]) },
        { "yellow6green7", new([Row.FromString("XXGGG"), Row.FromString("YYYXG"), Row.FromString("YXXXG"), Row.FromString("YYYXG"), Row.FromString("YXYXG"), Row.FromString("YYYXX")]) }
    };

    public override bool Equals(object? obj)
    {
        var other = obj as Pattern;
        for (int i = 0; i < 6; i++)
        {
            if (!rows[i].Equals(other.rows[i]))
                return false;
        }
        return true;
    }
}