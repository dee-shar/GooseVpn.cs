# GooseVpn.cs
Mobile-API for [GOOSE VPN](https://play.google.com/store/apps/details?id=com.goosevpn.gooseandroid) a secure VPN service that lets you browse the internet anonymously while protecting your data

## Example
```cs
using GooseVpnApi;

namespace Application
{
    internal class Program
    {
        static async Task Main()
        {
            var api = new GooseVpn();
            await api.Register("example@gmail.com", "password");
            string servers = await api.GetServers();
            Console.WriteLine(servers);
        }
    }
}
```
