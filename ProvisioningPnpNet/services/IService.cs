using System.Security;
using ProvisioningPnpNet.models;

namespace ProvisioningPnpNet.services
{
    public interface IService
    {
        void RunAction(DefinedParamsModel options, SecureString passwordSecore);
    }
}
