

namespace RiskLibrary.Models;
public class JoinGameModel
{
    public string UrlName { get; set; }
    public int PlayersCount { get; set; }
    public List<string> Colors { get; set; } = new List<string>();  
}
