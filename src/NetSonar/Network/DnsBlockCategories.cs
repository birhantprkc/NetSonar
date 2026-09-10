using System;

namespace NetSonar.Avalonia.Network;

[Flags]
public enum DnsBlockCategories
{
    Uncensored     = -1,
    Unfiltered     = 0,
    Malware        = 1 << 0,
    Phishing       = 1 << 1,
    Ads            = 1 << 2,
    Tracking       = 1 << 3,
    SocialMedia    = 1 << 4,
    Adult          = 1 << 5,
    Drugs          = 1 << 6,
    Gambling       = 1 << 7,
    FakeNews       = 1 << 8,
    Cryptocurrency = 1 << 9,
    Proxies        = 1 << 10,
    VPN            = 1 << 11,
    Tor            = 1 << 12,
    Anonymizer     = 1 << 13,
}