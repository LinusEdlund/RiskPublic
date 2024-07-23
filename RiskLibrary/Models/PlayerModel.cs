

namespace RiskLibrary.Models;
public class PlayerModel
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }
    public int Land { get; set; }
    public int Soldiers { get; set; }
    public string Color { get; set; }
    public bool Host { get; set; } = false;
    public int AddedSoldiers { get; set; }
    public bool Bot { get; set; } = false;
    public Dictionary<string, int> OwnedCountries { get; set; } = new();

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (PlayerModel)obj;

        return ConnectionId == other.ConnectionId &&
               Name == other.Name &&
               Land == other.Land &&
               Soldiers == other.Soldiers &&
               Color == other.Color &&
               Host == other.Host &&
               AddedSoldiers == other.AddedSoldiers &&
               Bot == other.Bot &&
               OwnedCountries.Count == other.OwnedCountries.Count &&
               OwnedCountries.All(kv => other.OwnedCountries.TryGetValue(kv.Key, out int value) && kv.Value == value);
    }

    public override int GetHashCode()
    {
        int hashCode = new { ConnectionId, Name, Land, Soldiers, Color, Host, AddedSoldiers, Bot }.GetHashCode();
        foreach (var kv in OwnedCountries)
        {
            hashCode ^= kv.Key.GetHashCode();
            hashCode ^= kv.Value.GetHashCode();
        }
        return hashCode;
    }
}
