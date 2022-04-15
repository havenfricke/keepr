using System.Data;

namespace Keepr.Repositories
{
  public class VaultsRepo
  {
    private readonly IDbConnection _db;

    public VaultsRepo(IDbConnection db)
    {
      _db = db;
    }
  }
}