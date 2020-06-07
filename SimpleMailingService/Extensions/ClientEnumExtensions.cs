using System;
using SimpleMailingService.Enums;
using SimpleMailingService.Models;

namespace SimpleMailingService.Extensions
{
    public static class ClientEnumExtensions
    {
        public static Client GetClient(this ClientEnum clientEnum, ClientOptions options)
        {
            if(options.Values.TryGetValue(clientEnum.ToString(), out var client))
                return client;

            throw new Exception($"ClientEnumExtensions.GetClient: Client '{clientEnum.ToString()}' does not exist.");
        }
    }
}
