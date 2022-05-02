using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Management;

namespace League.SDK;

public static class Utils
{
  public static async Task<Process> FindLeagueLauncher(int NumberOfRetrys)
  {
    Process ret = null;
    int counter = 0;

    await Task.Run(() =>
    {
      while (counter <= NumberOfRetrys)
      {
        var processes = Process.GetProcessesByName("LeagueClientUx");
        if (processes.Length > 0)
        {
          ret = processes[0];
          break;
        }
        counter++;
        Thread.Sleep(1000);
      }
    });

    return ret;
  }

  public static List<string> GetAuthInfo(Process LeagueProcess)
  {

    string? commandline = "";
    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + LeagueProcess.Id))
    using (ManagementObjectCollection objects = searcher.Get())
    {
      commandline = objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
    }

    string[] args = commandline.Split('"');

    int port = 0;
    string token = "";

    foreach (String s in args)
    {
      var portRegex = Regex.Match(s, @"(?<=--app-port=)\d+");

      if (portRegex.Success)
        port = int.Parse(portRegex.Value);

      var tokenRegex = Regex.Match(s, "(?<=--remoting-auth-token=).*");

      if (tokenRegex.Success)
        token = tokenRegex.Value;
    }

    var ret = new List<string> { token, port.ToString() };
    return ret;
  }
}
