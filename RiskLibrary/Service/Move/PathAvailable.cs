

using RiskLibrary.Models;
using RiskLibrary.Service.Move.Interface;

namespace RiskLibrary.Service.Move;
public class PathAvailable : IPathAvailable
{
    public bool IsPathAvailable(string source, string destination,
        Dictionary<string, List<string>> attackDic, PlayerModel playing)
    {
        if (attackDic.ContainsKey(source))
        {
            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                visited.Add(current);

                if (current == destination)
                    return true;

                foreach (var neighbor in attackDic[current])
                {
                    bool owns = playing.OwnedCountries.ContainsKey(neighbor);
                    if (owns && !visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return false;
    }
}
