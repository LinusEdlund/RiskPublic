namespace RiskLibrary.Service.Attack.Interface;

public interface IAttackDictionaryService
{
    Dictionary<string, List<string>> GetAttackDic();
}