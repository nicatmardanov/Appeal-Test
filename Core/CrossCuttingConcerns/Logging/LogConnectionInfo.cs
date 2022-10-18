using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogConnectionInfo
    {
        public string? RemoteIp { get; set; }
        public string? RemotePort { get; set; }
        public string? LocalIp { get; set; }
        public string? LocalPort { get; set; }

        public LogConnectionInfo(ConnectionInfo? connectionInfo)
        {
            RemoteIp = connectionInfo?.RemoteIpAddress?.ToString();
            RemotePort = connectionInfo?.RemotePort.ToString();
            LocalIp = connectionInfo?.LocalIpAddress?.ToString();
            LocalPort = connectionInfo?.LocalPort.ToString();
        }
    }
}
