using RiskLibrary.Service.Attack.Interface;

namespace RiskLibrary.Service.Attack;
public class AttackDictionaryService : IAttackDictionaryService
{
    public Dictionary<string, List<string>> GetAttackDic()
    {
        Dictionary<string, List<string>> d = new();

        d["eastern_australia"] = new List<string> { "western_australia", "new_guinea" };
        d["indonesia"] = new List<string> { "siam", "western_australia", "new_guinea" };
        d["new_guinea"] = new List<string> { "eastern_australia", "western_australia", "indonesia" };
        d["alaska"] = new List<string> { "northwest_territory", "alberta", "kamchatka" };
        d["ontario"] = new List<string> { "alberta", "quebec", "northwest_territory", "western_united_states", "eastern_united_states", "greenland" };
        d["northwest_territory"] = new List<string> { "alaska", "ontario", "greenland", "alberta" };
        d["venezuela"] = new List<string> { "central_america", "brazil", "peru" };
        d["madagascar"] = new List<string> { "east_africa", "south_africa" };
        d["north_africa"] = new List<string> { "western_europe", "southern_europe", "egypt", "east_africa", "congo", "brazil" };
        d["greenland"] = new List<string> { "northwest_territory", "ontario", "quebec", "iceland" };
        d["iceland"] = new List<string> { "greenland", "great_britain", "scandinavia" };
        d["great_britain"] = new List<string> { "iceland", "western_europe", "scandinavia", "northern_europe" };
        d["scandinavia"] = new List<string> { "iceland", "great_britain", "northern_europe", "ukraine" };
        d["japan"] = new List<string> { "kamchatka", "mongolia" };
        d["yakursk"] = new List<string> { "kamchatka", "irkutsk", "siberia" };
        d["kamchatka"] = new List<string> { "alaska", "yakursk", "irkutsk", "mongolia", "japan" };
        d["siberia"] = new List<string> { "ural", "china", "mongolia", "irkutsk", "yakursk" };
        d["ural"] = new List<string> { "ukraine", "siberia", "china", "afghanistan" };
        d["afghanistan"] = new List<string> { "ukraine", "ural", "china", "middle_east", "india" };
        d["middle_east"] = new List<string> { "ukraine", "afghanistan", "india", "east_africa", "egypt", "southern_europe" };
        d["india"] = new List<string> { "middle_east", "afghanistan", "china", "siam" };
        d["siam"] = new List<string> { "indonesia", "india", "china" };
        d["china"] = new List<string> { "siberia", "ural", "afghanistan", "india", "siam", "mongolia" };
        d["mongolia"] = new List<string> { "siberia", "irkutsk", "kamchatka", "japan", "china" };
        d["irkutsk"] = new List<string> { "kamchatka", "yakursk", "siberia", "mongolia" };
        d["ukraine"] = new List<string> { "scandinavia", "ural", "afghanistan", "middle_east", "southern_europe", "northern_europe" };
        d["southern_europe"] = new List<string> { "western_europe", "northern_europe", "ukraine", "middle_east", "egypt", "north_africa" };
        d["western_europe"] = new List<string> { "great_britain", "northern_europe", "southern_europe", "north_africa" };
        d["northern_europe"] = new List<string> { "scandinavia", "great_britain", "ukraine", "southern_europe", "western_europe" };
        d["egypt"] = new List<string> { "north_africa", "east_africa", "middle_east", "southern_europe" };
        d["east_africa"] = new List<string> { "egypt", "congo", "south_africa", "madagascar", "north_africa", "middle_east" };
        d["congo"] = new List<string> { "east_africa", "south_africa", "north_africa" };
        d["south_africa"] = new List<string> { "congo", "east_africa", "madagascar" };
        d["brazil"] = new List<string> { "venezuela", "peru", "argentina", "north_africa" };
        d["argentina"] = new List<string> { "brazil", "peru" };
        d["eastern_united_states"] = new List<string> { "quebec", "central_america", "western_united_states", "ontario" };
        d["western_united_states"] = new List<string> { "eastern_united_states", "alberta", "ontario", "central_america" };
        d["quebec"] = new List<string> { "greenland", "ontario", "eastern_united_states" };
        d["central_america"] = new List<string> { "western_united_states", "eastern_united_states", "venezuela" };
        d["peru"] = new List<string> { "venezuela", "brazil", "argentina" };
        d["western_australia"] = new List<string> { "eastern_australia", "indonesia", "new_guinea" };
        d["alberta"] = new List<string> { "western_united_states", "northwest_territory", "alaska", "ontario" };
        return d;
    }

}
