using System;

namespace Keepr.Models
{
  //AKA POSTS
  public class Keep
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; }
    public int Views { get; set; }
    public int Kept { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }

  }

  public class KeepVM : Keep
  {
    public int? VaultKeepId { get; set; }
    public int? VaultId { get; set; }
  }
}