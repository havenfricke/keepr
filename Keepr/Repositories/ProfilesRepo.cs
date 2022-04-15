using System.Data;

namespace Keepr.Repositories
{
  public class ProfilesRepo
  {
    private readonly IDbConnection _db;

    public ProfilesRepo(IDbConnection db)
    {
      _db = db;
    }
  }
}