using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace League.SDK;
public static class Lobby
{
  public static async Task<string> GetFlowPhase(string Token, string Port)
  {
    var response = await Request.GetLocal("lol-gameflow/v1/gameflow-phase", Token, Port);
    return response;
  }
  public static async Task SearchMatchmaking(string Token, string Port)
  {
    await Request.PostLocal("", "lol-lobby/v2/lobby/matchmaking/search", Token, Port);
  }
  public static async Task AcceptMatchmaking(string Token, string Port)
  {
    await Request.PostLocal("", "lol-matchmaking/v1/ready-check/accept", Token, Port);
  }
  public static async Task RestartMatchmaking(string Token, string Port)
  {
    await Request.PostLocal("", "/lol-lobby/v2/play-again", Token, Port);
  }

  public static async Task PickChampion(int ChampionID, bool LockChampion, string Token, string Port)
  {
    var data = await Request.GetLocal("lol-champ-select/v1/session", Token, Port);

    var obj = JObject.Parse(JsonConvert.DeserializeObject(data).ToString());
    var localcell = (int)obj["localPlayerCellId"];

    var actionid = 0;

    foreach (var action in obj["actions"][0])
    {
      var currentcell = (int)action["actorCellId"];
      var currentid = (int)action["id"];

      if (currentcell == localcell)
        actionid = currentid;
    }

    var champinfo = new JObject();
    champinfo.Add("championId", ChampionID);
    champinfo.Add("completed", LockChampion);

    await Request.PatchLocal(champinfo.ToString(), $"lol-champ-select/v1/session/actions/{actionid}", Token, Port);
  }
}
