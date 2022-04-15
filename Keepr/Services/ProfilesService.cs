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
  }
}