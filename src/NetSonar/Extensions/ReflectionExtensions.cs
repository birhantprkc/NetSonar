/*
 *                     GNU AFFERO GENERAL PUBLIC LICENSE
 *                       Version 3, 19 November 2007
 *  Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 *  Everyone is permitted to copy and distribute verbatim copies
 *  of this license document, but changing it is not allowed.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using NetSonar.Avalonia.Models;
using Utf8StringInterpolation;

namespace NetSonar.Avalonia.Extensions;

public static class ReflectionExtensions
{
    public static PropertyInfo[] GetProperties(object obj)
    {
	    return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
    }

    public static void BuildTabularData(IDictionary<string, GroupNameValue> collection, object? obj, string group = "")
    {
        if (obj is null) return;

        var type = obj.GetType();

        if (type.IsSimpleType())
        {
            var key = $"{group}.{type.Name}";
            var value = obj.ToString() ?? string.Empty;
            SetOrAdd(key, type.Name, value);
        }
        else if(type.IsArray && obj is Array array)
        {
            var key = $"{group}.{type.Name}";
            var value = $"Count: {array.Length}";
            SetOrAdd(key, type.Name, value);
        }
        else if (type.IsClass)
        {
            var properties = GetProperties(obj);

            foreach (var property in properties)
            {
                try
                {
                    var propertyType = property.GetType();
                    var objValue = property.GetValue(obj);
                    var key = $"({type.Name}){group}.{property.Name}";
                    var value = string.Empty;
                    if (objValue is not null)
                    {
                        if (propertyType.IsSimpleType())
                        {
                            value = objValue.ToString() ?? string.Empty;
                            SetOrAdd(key, property.Name, value);
                        }
                        else if (objValue is IPAddressCollection ipAddressCollection)
                        {
                            using var writer = Utf8String.CreateWriter(out var zsb);
                            for (var i = 0; i < ipAddressCollection.Count; i++)
                            {
                                var address = ipAddressCollection[i];
                                if (i > 0) zsb.AppendLine();
                                zsb.AppendLiteral(address.ToString());
                            }

                            zsb.Flush();
                            SetOrAdd(key, property.Name, writer.ToString());
                        }
                        else if (objValue is GatewayIPAddressInformationCollection gatewayIpAddressInformationCollection)
                        {
                            using var writer = Utf8String.CreateWriter(out var zsb);
                            for (var i = 0; i < gatewayIpAddressInformationCollection.Count; i++)
                            {
                                var address = gatewayIpAddressInformationCollection[i];
                                if (i > 0) zsb.AppendLine();
                                zsb.AppendLiteral(address.Address.ToString());
                            }

                            zsb.Flush();
                            SetOrAdd(key, property.Name, writer.ToString());
                        }
                        else if (objValue is IPAddressInformationCollection ipAddressInformationCollection)
                        {
                            using var writer = Utf8String.CreateWriter(out var zsb);
                            for (var i = 0; i < ipAddressInformationCollection.Count; i++)
                            {
                                var address = ipAddressInformationCollection[i];
                                if (i > 0) zsb.AppendLine();
                                if (OperatingSystem.IsWindows())
                                {
#pragma warning disable CA1416
                                    zsb.AppendLiteral($"{{{address.Address}, {nameof(address.IsDnsEligible)}: {address.IsDnsEligible}, {nameof(address.IsTransient)}: {address.IsTransient}}}");
#pragma warning restore CA1416
                                }
                                else
                                {
                                    zsb.AppendLiteral(address.Address.ToString());
                                }
                            }

                            zsb.Flush();
                            SetOrAdd(key, property.Name, writer.ToString());
                        }
                        else if (objValue is MulticastIPAddressInformationCollection multicastIpAddressInformationCollection)
                        {
                            using var writer = Utf8String.CreateWriter(out var zsb);
                            for (var i = 0; i < multicastIpAddressInformationCollection.Count; i++)
                            {
                                var address = multicastIpAddressInformationCollection[i];
                                if (i > 0) zsb.AppendLine();
                                if (OperatingSystem.IsWindows())
                                {
#pragma warning disable CA1416
                                    zsb.AppendLiteral($"{{{address.Address}, {nameof(address.IsDnsEligible)}: {address.IsDnsEligible}, {nameof(address.IsTransient)}: {address.IsTransient}");
#pragma warning restore CA1416
                                    if (address.DhcpLeaseLifetime > 0)
                                    {
                                        zsb.AppendLiteral($", Lease: {ConverterExtension.ToTimeShortString(address.DhcpLeaseLifetime)}");
                                    }
                                    zsb.AppendLiteral("}");
                                }
                                else
                                {
                                    zsb.AppendLiteral(address.Address.ToString());
                                }
                            }

                            zsb.Flush();
                            SetOrAdd(key, property.Name, writer.ToString());
                        }
                        else if (objValue is UnicastIPAddressInformationCollection unicastIpAddressInformationCollection)
                        {
                            using var writer = Utf8String.CreateWriter(out var zsb);
                            for (var i = 0; i < unicastIpAddressInformationCollection.Count; i++)
                            {
                                var address = unicastIpAddressInformationCollection[i];
                                if (i > 0) zsb.AppendLine();
                                if (OperatingSystem.IsWindows())
                                {
#pragma warning disable CA1416
                                    zsb.AppendLiteral($"{{{address.Address}, {nameof(address.IPv4Mask)}: {address.IPv4Mask} {nameof(address.IsDnsEligible)}: {address.IsDnsEligible}, {nameof(address.IsTransient)}: {address.IsTransient}");
#pragma warning restore CA1416
                                    if (address.DhcpLeaseLifetime > 0)
                                    {
                                        zsb.AppendLiteral($", Lease: {ConverterExtension.ToTimeShortString(address.DhcpLeaseLifetime)}");
                                    }
                                    zsb.AppendLiteral("}");
                                }
                                else
                                {
                                    zsb.AppendLiteral($"{{{address.Address}, {nameof(address.IPv4Mask)}: {address.IPv4Mask}}}");
                                }
                            }

                            zsb.Flush();
                            SetOrAdd(key, property.Name, writer.ToString());
                        }
                        else if (objValue is ICollection iCollection)
                        {
                            value = $"Count: {iCollection.Count}";
                            SetOrAdd(key, property.Name, value);
                        }
                        else if (propertyType.IsArray && objValue is Array array2)
                        {
                            value = $"Count: {array2.Length}";
                            SetOrAdd(key, property.Name, value);
                        }
                        else
                        {
                            value = objValue.ToString() ?? string.Empty;
                            SetOrAdd(key, property.Name, value);
                        }
                    }
                }
                catch (Exception e)
                {
                    App.WriteLine(e);
                }
            }
        }

        void SetOrAdd(string key, string name, string value)
        {
            if (collection.TryGetValue(key, out var item))
            {
                if (string.Equals(item.Value, value)) return;
                item.Value = value;
            }
            else
            {
                collection.Add(key, new GroupNameValue(name, value, group));
            }
        }
    }
}