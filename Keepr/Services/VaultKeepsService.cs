using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepo _vkr;

    public VaultKeepsService(VaultKeepsRepo vkr)
    {
      _vkr = vkr;
    }

    //TODO kept increments will happen in this layer
  }
}