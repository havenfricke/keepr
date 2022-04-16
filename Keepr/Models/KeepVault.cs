using System;

namespace Keepr.Models
{
  public class KeepVault
  {
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int KeepId { get; set; }
    public int VaultId { get; set; }
  }
}