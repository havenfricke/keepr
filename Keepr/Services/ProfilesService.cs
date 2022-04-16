using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepo _pr;

    public ProfilesService(ProfilesRepo pr)
    {
      _pr = pr;
    }

    internal Profile GetProfileById(string id)
    {
      return _pr.GetProfileById(id);
    }

    internal List<Keep> GetKeepsByAccId(string id)
    {
      return _pr.GetKeepsByAccId(id);
    }

    internal List<Vault> GetVaultsByAccId(string id)
    {
      return _pr.GetVaultsByAccId(id);
    }
  }
}