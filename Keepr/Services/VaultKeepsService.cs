using System;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepo _vkr;
    private readonly VaultsService _vs;

    public VaultKeepsService(VaultKeepsRepo kvr, VaultsService vs)
    {
      _vkr = kvr;
      _vs = vs;
    }

    internal VaultKeep CreateKV(VaultKeep data, string userId)
    {
      Vault found = _vs.GetVaultById(data.VaultId, userId);
      if (found.CreatorId != userId)
      {
        throw new Exception("Not your vault");
      }
      return _vkr.CreateKV(data);
    }

    internal ActionResult<string> RemoveKV(string userId, int id)
    {
      VaultKeep vk = _vkr.GetVKById(id);
      if (vk.CreatorId != userId)
      {
        throw new Exception("Not yours to delete");
      }
      return _vkr.RemoveVK(id);
    }

    //TODO kept increments will happen in this layer
  }
}