using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    internal List<Keep> GetKeepsByAccId(string id)
    {
      string sql = @"
      SELECT
      *
      FROM
      keeps
      WHERE
      creatorId = @id;
      ";
      return _db.Query<Keep>(sql, new { id }).ToList();
    }

    internal List<Vault> GetVaultsByAccId(string id)
    {
      string sql = @"
      SELECT
      *
      FROM
      vaults
      WHERE
      creatorId = @id;
      ";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }
  }
}