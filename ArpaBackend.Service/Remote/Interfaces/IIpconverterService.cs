

using ArpaBackend.Domain.DTOs;

namespace ArpaBackend.Service.Remote.Interfaces
{
    public interface IIpconverterService
    {
        GetCountryByIPDTO IpToCountryCode (string ip);
    }
}
