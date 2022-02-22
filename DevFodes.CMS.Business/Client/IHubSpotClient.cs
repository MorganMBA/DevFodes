using DevFodes.CMS.Business.Models;
using System.Threading.Tasks;

namespace DevFodes.CMS.Business.Client
{
    public interface IHubSpotClient
    {
        Task<string> PostContactAsync(Contact serializedRequest);
    }
}
