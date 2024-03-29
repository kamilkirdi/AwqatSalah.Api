﻿namespace DiyanetNamazVakti.Api.Core;

public interface ICacheSettings
{
    string Prefix { get; set; }
    string MainKey { get; set; }
    int ExpiryTime { get; set; }
}