using System.Data;

namespace Keepr.Repositories
{
  public class KeepVaultsRepo
  {
    private readonly IDbConnection _db;

    public KeepVaultsRepo(IDbConnection db)
    {
      _db = db;
    }
  }
}