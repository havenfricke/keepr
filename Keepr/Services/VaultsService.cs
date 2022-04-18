using System;
using System.Collections.Generic;
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

    internal Vault GetVaultById(int id, string? userId)
    {
      Vault found = _vr.GetVaultById(id);
      if (found == null)
      {
        throw new Exception("No Vault by that id");
      }
      if (found.IsPrivate == true && userId != found.CreatorId)
      {
        throw new Exception("This vault is private");
      }
      return found;
    }

    internal Vault UpdateVault(Vault updateData, string userId)
    {
      Vault original = GetVaultById(updateData.Id, userId);
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

    internal List<KeepVM> GetAllKV(int id, string? userId)
    {
      Vault found = this.GetVaultById(id, userId);
      if (found.IsPrivate == true && userId != found.CreatorId)
      {
        throw new Exception("This vault is private");
      }
      return _vr.GetKeepsByVaultId(id);
    }
  }
}