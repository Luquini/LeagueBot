namespace League.SDK;

public static class StoreAPI
{
  public static async Task<string> GetTransactionHistory(string Token)
  {
    var response = await Request.GetStore("/storefront/v3/history/purchase?language=en_US", Token);
    return response;
  }

  public static async Task<bool> ChangeNickName(string Nickname, string AccountID, string Token)
  {
    string Data = "{\"summonerName\":\"" + Nickname + "\", \"accountId\":\"" + AccountID + "\", \"items\":[{\"inventoryType\":\"SUMMONER_CUSTOMIZATION\", \"itemId\":" + 1 + ", \"ipCost\":" + 13900 + ", \"rpCost\":\"null\"," + "\"quantity\":" + 1 + "}]}";

    var response = await Request.PostStore(Data, "/storefront/v3/summonerNameChange/purchase?language=en_US", Token);

    return true;
  }
}
