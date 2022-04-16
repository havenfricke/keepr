using System;
using System.Collections.Generic;
using System.Security.Authentication;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

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
      return _vr.CreateVault(vaultData);
    }

    internal Vault GetVaultById(int id)
    {
      Vault found = _vr.GetVaultById(id);
      if (found.IsPrivate != false)
      {
        throw new AuthenticationException("This Vault is private");
      }
      else if (found == null)
      {
        throw new Exception("No Vault by that id");
      }
      return found;
    }

    internal Vault UpdateVault(Vault updateData)
    {
      Vault original = GetVaultById(updateData.Id);
      if (updateData.CreatorId != original.CreatorId)
      {
        throw new Exception("Not your vault to edit");
      }

      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.IsPrivate = updateData.IsPrivate;
      return _vr.UpdateVault(original);
    }

    internal ActionResult<string> RemoveVault(string userId, int id)
    {
      Vault vault = _vr.GetVaultById(id);
      if (userId != vault.CreatorId)
      {
        throw new Exception("Not yours to delete");
      }
      return _vr.RemoveVault(id);
    }

    internal List<KeepVM> GetAllKV(int id)
    {
      return _vr.GetKeepsByVaultId(id);
    }
  }
}