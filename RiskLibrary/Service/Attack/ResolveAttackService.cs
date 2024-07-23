

using RiskLibrary.Models;
using RiskLibrary.Service.Attack.Interface;

namespace RiskLibrary.Service.Attack;
public class ResolveAttackService : IResolveAttackService
{
    public (List<PlayerModel>, List<string>) ResolveCombat(PlayerModel playerUnderAttack, string countryName, 
        List<PlayerModel> players, PlayerModel playing, string firstClicked)
    {

        bool failAttack = false;
        int startingSolDef = playerUnderAttack.OwnedCountries[countryName];
        int startingSolAttack = playing.OwnedCountries[firstClicked];

        int count = 0;
        List<string> combatChat = new();
        bool firstTime = true;
        
        string statbord = "┌───┬───┬───┐" + Environment.NewLine +
                          "| A | D | W |" + Environment.NewLine +
                          "├───┼───┼───┤" + Environment.NewLine;
        string startingAttack = $"Attack on {countryName}";

        while (playing.OwnedCountries[firstClicked] > playerUnderAttack.OwnedCountries[countryName])
        {
            // Combat resolution logic
            failAttack = true;
            int attackingDice = 0;
            int defendingDice = 1;
            int attackingAmount = playing.OwnedCountries[firstClicked];
            int defAmount = playerUnderAttack.OwnedCountries[countryName];
            count++;


            if (attackingAmount > 3)
            {
                attackingDice = 3;
            }
            if (attackingAmount == 3)
            {
                attackingDice = 2;
            }
            if (attackingAmount == 2)
            {
                attackingDice = 1;
            }

            if (defAmount > 1)
            {
                defendingDice = 2;
            }
            int[] attackerRolls = RollDice(attackingDice);
            int[] defenderRolls = RollDice(defendingDice);
            Array.Sort(attackerRolls);
            Array.Reverse(attackerRolls);
            Array.Sort(defenderRolls);
            Array.Reverse(defenderRolls);

            int attackSolLost = 0;
            int defSolLost = 0;


            // Compare dice results
            for (int i = 0; i < Math.Min(attackingDice, defendingDice); i++)
            {
                if (attackerRolls[i] > defenderRolls[i])
                {
                    // Attacker wins, defender loses one soldier
                    playerUnderAttack.OwnedCountries[countryName]--;
                    defSolLost++;
                    statbord = statbord + $"│ {attackerRolls[i]} │ {defenderRolls[i]} │ A │" + Environment.NewLine +
                        "├───┼───┼───┤" + Environment.NewLine;
                    if (playerUnderAttack.OwnedCountries[countryName] == 0)
                    {

                        var defP = players.First(x => x.ConnectionId == playerUnderAttack.ConnectionId);
                        defP.Soldiers -= startingSolDef;
                        defP.OwnedCountries.Remove(countryName);
                        defP.Land--;
                        var attackP = players.FirstOrDefault(x => x.ConnectionId == playing.ConnectionId);

                        int solMoving = playing.OwnedCountries[firstClicked] - 1;
                        attackP.OwnedCountries.Add(countryName, solMoving);
                        attackP.OwnedCountries[firstClicked] = 1;
                        attackP.Soldiers -= startingSolAttack - (solMoving + 1);
                        attackP.Land++;

                        combatChat.Add(startingAttack);
                        statbord = statbord + $"└───┴───┴───┘";
                        combatChat.Add(statbord);
                        combatChat.Add("Attacker wins");

                        return (players, combatChat);
                    }
                }
                else
                {
                    playing.OwnedCountries[firstClicked]--;
                    attackSolLost++;
                    statbord = statbord + $"│ {attackerRolls[i]} │ {defenderRolls[i]} │ D │" + Environment.NewLine +
                        "├───┼───┼───┤" + Environment.NewLine;
                }
            }


            firstTime = false;


        }



        if (failAttack)
        {
            var defP = players.First(x => x.ConnectionId == playerUnderAttack.ConnectionId);
            defP.OwnedCountries[countryName] = playerUnderAttack.OwnedCountries[countryName];
            defP.Soldiers -= startingSolDef - playerUnderAttack.OwnedCountries[countryName];
            var attackP = players.First(x => x.ConnectionId == playing.ConnectionId);
            attackP.OwnedCountries[firstClicked] = playing.OwnedCountries[firstClicked];
            attackP.Soldiers -= startingSolAttack - playing.OwnedCountries[firstClicked];

            combatChat.Add(startingAttack); 
            statbord = statbord + $"└───┴───┴───┘ \n";
            combatChat.Add(statbord);
            combatChat.Add("Defender wins");
        }

        return (players, combatChat);
    }

    private static int[] RollDice(int count)
    {
        Random random = new Random();
        int[] rolls = new int[count];
        for (int i = 0; i < count; i++)
        {
            rolls[i] = random.Next(1, 7); // Rolling a 6-sided dice
        }
        return rolls;
    }
}
