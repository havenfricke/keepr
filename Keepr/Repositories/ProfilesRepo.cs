using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class ProfilesRepo
  {
    private readonly IDbConnection _db;

    public ProfilesRepo(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetProfileById(string id)
    {
      string sql = @"
      SELECT
      *
      FROM
      accounts
      WHERE
      id = @id;
      ";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }
  }
}