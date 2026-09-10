using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.NetworkInformation;

namespace NetSonar.Avalonia.Network;

public record PingableServiceReply : BasePingReply
{
    public required int BufferLength { get; init; }
    public required int Ttl { get; init; }

    public PingableServiceReply() { }

    [SetsRequiredMembers]
    public PingableServiceReply(bool isSucceed, object status, IPEndPoint ipEndPoint, DateTime sentDateTime, double time, int bufferLength = 0, int ttl = 0, string? errorMessage = null) : base(isSucceed, status, ipEndPoint, sentDateTime, time, errorMessage)
    {
        BufferLength = bufferLength;
        Ttl = ttl;
    }

    [SetsRequiredMembers]
    public PingableServiceReply(PingReply pingReply)
    {
        IsSucceeded = pingReply.Status == IPStatus.Success;
        Status = pingReply.Status;
        SentDateTime = DateTime.Now;
        IpEndPoint = new IPEndPoint(pingReply.Address, 0);
        if (pingReply.Status == IPStatus.Success)
        {
            Time = pingReply.RoundtripTime;
        }
        else
        {
            Time = pingReply.RoundtripTime > 0 ? pingReply.RoundtripTime : double.PositiveInfinity;
        }

        BufferLength = pingReply.Buffer.Length;
        Ttl = pingReply.Options?.Ttl ?? 128;
    }

    public static PingableServiceReply CreateTimeOutReply(Exception e, DateTime sentDateTime, double time = double.PositiveInfinity)
    {
        return new PingableServiceReply(false, IPStatus.TimedOut, new IPEndPoint(IPAddress.Any, 0), sentDateTime, time, 0, 0, e.InnerException?.Message ?? e.Message);
    }

    public static PingableServiceReply CreateErrorReply(object status, Exception e, DateTime sentDateTime, double time = double.PositiveInfinity)
    {
        return new PingableServiceReply(false, status, new IPEndPoint(IPAddress.Any, 0), sentDateTime, time, 0, 0, e.InnerException?.Message ?? e.Message);
    }

    public static PingableServiceReply CreateErrorReply(object status, string errorMessage, DateTime sentDateTime, double time = double.PositiveInfinity)
    {
        return new PingableServiceReply(false, status, new IPEndPoint(IPAddress.Any, 0), sentDateTime, time, 0, 0, errorMessage);
    }

    public static PingableServiceReply CreateErrorReply(Exception e, DateTime sentDateTime, double time = double.PositiveInfinity)
    {
        return CreateErrorReply(IPStatus.Unknown, e, sentDateTime, time);
    }

    public static PingableServiceReply CreateErrorReply(string errorMessage, DateTime sentDateTime, double time = double.PositiveInfinity)
    {
        return CreateErrorReply(IPStatus.Unknown, errorMessage, sentDateTime, time);
    }
}