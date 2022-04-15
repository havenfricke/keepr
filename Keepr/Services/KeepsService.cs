using System;
using Keepr.Models;
using Keepr.Repositories;

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
      throw new NotImplementedException();
    }
  }
}