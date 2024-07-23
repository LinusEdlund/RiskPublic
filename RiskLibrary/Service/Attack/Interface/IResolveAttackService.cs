using RiskLibrary.Models;

namespace RiskLibrary.Service.Attack.Interface;
public interface IResolveAttackService
{
    (List<PlayerModel>, List<string>) ResolveCombat(PlayerModel playerUnderAttack, string countryName, List<PlayerModel> players, PlayerModel playing, string firstClicked);
}