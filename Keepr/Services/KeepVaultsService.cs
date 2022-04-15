using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepVaultsService
  {
    private readonly KeepVaultsRepo _kvr;

    public KeepVaultsService(KeepVaultsRepo kvr)
    {
      _kvr = kvr;
    }

    //TODO kept increments will happen in this layer
  }
}