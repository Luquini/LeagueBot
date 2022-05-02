using League.SDK;
class Program
{
  private static async void Initialize()
  {
    var Launcher = await Utils.FindLeagueLauncher(3);

    if (Launcher == null)
    {
      Console.WriteLine("[ERROR] Launcher não foi encontrado");
      return;
    }

    Console.WriteLine("[INFO] Launcher encontrado com sucesso");

    var AuthInfo = Utils.GetAuthInfo(Launcher);

    if (AuthInfo == null)
    {
      Console.WriteLine("[ERROR] Não foi possível extrair as informações de autorização");
      return;
    }

    Console.WriteLine("[INFO] Autorização obtida com sucesso");

    var RsoToken = await Auth.GetRsoAuth(AuthInfo[0], AuthInfo[1]);

    if (RsoToken.Length <= 0)
    {
      Console.WriteLine("[ERROR] Falha ao obter o RSO Token");
      return;
    }

    Console.WriteLine("[INFO] RSO Token obtido com sucesso");

    while (true)
    {
      var FlowStatus = await Lobby.GetFlowPhase(AuthInfo[0], AuthInfo[1]);

      switch (FlowStatus)
      {
        case "\"Lobby\"":
          await Lobby.SearchMatchmaking(AuthInfo[0], AuthInfo[1]);
          break;
        case "\"ReadyCheck\"":
          await Lobby.AcceptMatchmaking(AuthInfo[0], AuthInfo[1]);
          break;
        case "\"ChampSelect\"":
          int Ashe = 22;
          await Lobby.PickChampion(Ashe, true, AuthInfo[0], AuthInfo[1]);
          break;
        case "\"EndOfGame\"":
          await Lobby.RestartMatchmaking(AuthInfo[0], AuthInfo[1]);
          break;
        default:
          break;
      }

      Thread.Sleep(1000);
    }
  }
  static void Main(string[] args)
  {
    Console.WriteLine("[INFO] Iniciando aplicação");
    Initialize();
    while (true) Thread.Sleep(5000);
  }
}
