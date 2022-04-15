using System.Data;

namespace Keepr.Repositories
{
  public class VaultKeepsRepo
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepo(IDbConnection db)
    {
      _db = db;
    }
  }
}