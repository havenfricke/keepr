using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepo _kr;

    public KeepsService(KeepsRepo kr)
    {
      _kr = kr;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      return _kr.CreateKeep(keepData);
    }

    internal List<Keep> GetAllKeeps()
    {
      return _kr.GetAllKeeps();
    }

    internal Keep GetKeepById(int id)
    {
      Keep found = _kr.GetKeepById(id);
      if (found == null)
      {
        throw new Exception("No keep by that id");
      }
      return found;
    }

    internal Keep UpdateKeep(Keep updateData)
    {
      Keep original = GetKeepById(updateData.Id);
      if (updateData.CreatorId != original.CreatorId)
      {
        throw new Exception("Not your keep to edit");
      }

      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      return _kr.UpdateKeep(original);
    }

    internal Keep IncreaseKeepViews(Keep updateData, int id)
    {
      Keep original = GetKeepById(id);
      original.Views = (updateData.Views++);
      return _kr.IncreaseKeepViews(original);
    }
    internal ActionResult<string> RemoveKeep(int id, string userId)
    {
      Keep keep = _kr.GetKeepById(id);
      if (userId != keep.CreatorId)
      {
        throw new Exception("Not yours to delete");
      }
      return _kr.RemoveKeep(id);
    }

  }
}