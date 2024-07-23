using Microsoft.AspNetCore.SignalR;
using RiskLibrary.Models;
using System;
using System.Runtime.InteropServices.Marshalling;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Risk.Enums;


namespace Risk.Hubs;

public class ChatHub : Hub
{
    private static Dictionary<string, List<PlayerModel>> onlinePlayers = new();
    private static List<LobbysModel> urls = new List<LobbysModel>();

    public async Task GetList(string url)
    {
        if (urls.Any(x => x.UrlName == url) == false)
        {
            return;
        }
        if (!onlinePlayers.ContainsKey(url))
        {
            onlinePlayers.Add(url, new());
        }
        await Clients.Caller.SendAsync("GetPlayersList", onlinePlayers[url]);
    }

    public Task Update(string url, List<PlayerModel> players)
    {
        onlinePlayers[url] = players;
        return Clients.Group(url).SendAsync("UpdateList", onlinePlayers[url]);
    }

    public Task SendMessageKick(string id, string message)
    {
        return Clients.Client(id).SendAsync("ReceiveKick", message);
    }

    private string GenerateShortId()
    {
        Random random = new Random();
        byte[] buffer = new byte[6];
        random.NextBytes(buffer);

        string shortId = Convert.ToBase64String(buffer)
                            .Replace("/", "_")
                            .Replace("+", "-")
                            .Substring(0, 5);

        return shortId;
    }

    public async Task Add(string url, PlayerModel player)
    {
        if (urls.Any(x => x.UrlName == url) == false)
        {
            return;
        }

        if (player.Bot == true)
        {
            player.ConnectionId = GenerateShortId();
            onlinePlayers[url].Add(player);
            await Clients.Group(url).SendAsync("AddPlayer", player);
            return;
        }

        string id = Context.ConnectionId;
        if (!onlinePlayers.ContainsKey(url))
        {
            onlinePlayers.Add(url, new());
        }
        player.ConnectionId = id;


        onlinePlayers[url].Add(player);

        if (onlinePlayers[url].Count == 1)
        {
            onlinePlayers[url][0].Host = true;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, url);
        await Clients.Group(url).SendAsync("AddPlayer", player);
    }

    // TODO: fix so it remove it all cuz i think it can be problems later and put it to not active in db
    // TODO: fix so only the people in the same room gets the call insted of all getting it
    public async Task Remove(string url)
    {
        if (onlinePlayers.ContainsKey(url))
        {
            var player = onlinePlayers[url].FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (player is not null)
            {
                onlinePlayers[url].Remove(player);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, url);
            }

            if (onlinePlayers[url].Count <= 0)
            {
                onlinePlayers.Remove(url);
            }
        }
        await Clients.Group(url).SendAsync("RemovePlayer", Context.ConnectionId);
    }

    public Task SendMessage(string url, string color, string message)
    {
        return Clients.Group(url).SendAsync("ReceiveMessage", color, message);
    }

    public Task GameChat(string url, List<string> messages)
    {
        return Clients.Group(url).SendAsync("GameChatMessage", messages);
    }

    public Task Bord(string url, List<PlayerModel> placings)
    {
        return Clients.Group(url).SendAsync("UpdateBord", placings);
    }

    public Task Turn(string url, int number)
    {
        return Clients.Group(url).SendAsync("UpdateTurn", number);
    }

    public Task Refresh(string url, List<PlayerModel> players)
    {
        onlinePlayers[url] = players;
        return Clients.Group(url).SendAsync("RefreshAll", players);
    }

    public Task Kick(string url, PlayerModel player)
    {
        return Clients.Group(url).SendAsync("KickPlayer", player);
    }

    public void CreateGame(string url)
    {
        LobbysModel lobby = new();
        lobby.UrlName = url;
        urls.Add(lobby);
    }

    public Task LobbyExists(string url)
    {
        bool output = urls.Any(x => url == x.UrlName);
        return Clients.Caller.SendAsync("GameLobbyExists", output);
    }

    public Task GetUrlList()
    {
        List<JoinGameModel> list = new List<JoinGameModel>();
        foreach (var url in onlinePlayers.Keys)
        {
            // i dont want to see games that have already started in join game component
            if (urls.First(x => url == x.UrlName).Started == false)
            {
                JoinGameModel game = new();
                game.UrlName = url;
                game.PlayersCount = onlinePlayers[url].Count;

                foreach (var person in onlinePlayers[url])
                {
                    game.Colors.Add(person.Color);
                }

                list.Add(game);
            }
        }
        return Clients.Caller.SendAsync("GetUrlListGame", list);
    }

    public Task Phase(string url, Phase phase)
    {
        return Clients.Group(url).SendAsync("GetPhase", phase);
    }

    public Task PlayerCount(string url)
    {
        return Clients.Caller.SendAsync("GetPlayerCount", onlinePlayers[url].Count);
    }

    public void Start(string url)
    {
        LobbysModel? game = urls.FirstOrDefault(x => x.UrlName == url);
        if (game is not null)
        {
            game.Started = true;
        }
    }

    public Task GameState(string url)
    {
        var game = urls.FirstOrDefault(x => x.UrlName == url);
        if (game is not null)
        {
            return Clients.Caller.SendAsync("GetGameState", game.Started);
        }
        return Clients.Caller.SendAsync("GetGameState", false);
    }

    public Task StartGame(string url)
    {
        return Clients.Group(url).SendAsync("GameStarted", true);
    }

    public void RemoveGame(string url)
    {
        onlinePlayers.Remove(url);
    }

    public void RemoveLobby(string url)
    {
        var obj = urls.FirstOrDefault(x => x.UrlName.Equals(url));
        if (obj is not null)
        {
            urls.Remove(obj);
        }
        RemoveGame(url);
    }
    
    public Task StartClock(string url, int time)
    {
        return Clients.Group(url).SendAsync("GetStartClock", time);
    }

    public Task StopClock(string url)
    {
        return Clients.Group(url).SendAsync("GetStopClock");
    }


}
