using HeliumApi.SDK.Responses;
using Xunit;

namespace HeliumCat.Test;

public class HotspotTest
{
    [Fact]
    public void LastPocChallenge_IsNullable()
    {
        var hotspot = new Hotspot { LastPocChallenge = null };
        Assert.Null(hotspot.LastPocChallenge);
    }
}