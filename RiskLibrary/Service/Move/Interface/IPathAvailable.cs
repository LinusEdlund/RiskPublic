using RiskLibrary.Models;

namespace RiskLibrary.Service.Move.Interface;
public interface IPathAvailable
{
    bool IsPathAvailable(string source, string destination, Dictionary<string, List<string>> attackDic, PlayerModel playing);
}