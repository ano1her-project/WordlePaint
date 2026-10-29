using WordlePaint.Spy;

namespace WordlePaint.Tests;

public class SpyTests
{
    [Fact]
    public void CombineDictionaries()
    {
        Dictionary<string, string[]> a = [], b = [];
        a.Add("A", ["0", "1"]);
        a.Add("AB1", ["2", "3"]);
        a.Add("AB2", ["8"]);
        b.Add("AB1", ["4", "5"]);
        b.Add("AB2", ["9"]);
        b.Add("B", ["6", "7"]);
        var ab = Spy.Spy.Combine(a, b);
        //
        Assert.Equal(["0", "1"], ab["A"]);
        Assert.Equal(["2", "3", "4", "5"], ab["AB1"]);
        Assert.Equal(["8", "9"], ab["AB2"]);
        Assert.Equal(["6", "7"], ab["B"]);
        //
        Assert.Equal(2, ab["A"].Length);
        Assert.Equal("0", ab["A"][0]);
        Assert.Equal("1", ab["A"][1]);
        Assert.Equal(4, ab["AB1"].Length);
        Assert.Equal("2", ab["AB1"][0]);
        Assert.Equal("3", ab["AB1"][1]);
        Assert.Equal("4", ab["AB1"][2]);
        Assert.Equal("5", ab["AB1"][3]);
        Assert.Equal(2, ab["AB2"].Length);
        Assert.Equal("8", ab["AB2"][0]);
        Assert.Equal("9", ab["AB2"][1]);
        Assert.Equal(2, ab["B"].Length);
        Assert.Equal("6", ab["B"][0]);
        Assert.Equal("7", ab["B"][1]);
    }
}
