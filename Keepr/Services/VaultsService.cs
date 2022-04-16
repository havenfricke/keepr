using System;
using Keepr.Models;
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

    internal Vault CreateVault(Vault vaultData)
    {
      throw new NotImplementedException();
    }

    internal Vault GetVaultById(int id)
    {
      throw new NotImplementedException();
    }
  }
}