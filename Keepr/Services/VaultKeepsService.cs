using System;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepo _vkr;

    public VaultKeepsService(VaultKeepsRepo kvr)
    {
      _vkr = kvr;
    }

    internal VaultKeep CreateKV(VaultKeep data)
    {
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