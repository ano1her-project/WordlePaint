using WordlePaint.Core;

namespace WordlePaint.Spy;

public class PlayersPattern
{
    public string playerName;
    public Pattern pattern;

    public PlayersPattern(string p_playerName, Pattern p_pattern)
    {
        playerName = p_playerName;
        pattern = p_pattern;
    }
}
