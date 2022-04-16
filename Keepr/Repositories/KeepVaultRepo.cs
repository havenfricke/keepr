using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepVaultsRepo
  {
    private readonly IDbConnection _db;

    public KeepVaultsRepo(IDbConnection db)
    {
      _db = db;
    }

    internal KeepVault CreateKV(KeepVault data)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (keepId, vaultId, creatorId)
      VALUES
      (@KeepId, @VaultId, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }
  }
}