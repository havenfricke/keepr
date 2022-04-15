using System;

namespace Keepr.Models
{
  //AKA A COLLECTION OF POSTS
  public class Vault
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
}