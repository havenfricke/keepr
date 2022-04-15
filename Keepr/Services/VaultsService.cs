using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepo _vr;

    public VaultsService(VaultsRepo vr)
    {
      _vr = vr;
    }
  }
}