using Keepr.Models;
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

    internal KeepVault CreateKV(KeepVault data)
    {
      return _kvr.CreateKV(data);
    }

    //TODO kept increments will happen in this layer
  }
}