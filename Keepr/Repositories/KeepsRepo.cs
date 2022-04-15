using System.Data;

namespace Keepr.Repositories
{
  public class KeepsRepo
  {
    private readonly IDbConnection _db;

    public KeepsRepo(IDbConnection db)
    {
      _db = db;
    }
  }
}