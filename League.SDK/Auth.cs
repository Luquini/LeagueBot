using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace League.SDK;

public static class Auth
{
  public static async Task<string> GetRsoAuth(string Token, string Port)
  {
    var response = await Request.GetLocal("/lol-rso-auth/v1/authorization/access-token", Token, Port);
    var obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());
    var token = (string)(obj["token"]);

    return token;
  }

  public static async Task<List<string>> GetStoreAuth(string Token, string Port)
  {
    var response = await Request.GetLocal("/lol-login/v1/session", Token, Port);
    var obj = JObject.Parse(JsonConvert.DeserializeObject(response).ToString());

    var accountID = (string)(obj["accountId"]);
    var idToken = (string)(obj["idToken"]);

    var ret = new List<string> { idToken, accountID };

    return ret;
  }
}
