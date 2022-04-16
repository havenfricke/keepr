using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public ProfilesController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet("{id}")]

    public ActionResult<Profile> GetProfileById(string id)
    {
      try
      {
        Profile profile = _ps.GetProfileById(id);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]

    public ActionResult<List<Keep>> GetKeepsByAccId(string id)
    {
      try
      {
        List<Keep> keeps = _ps.GetKeepsByAccId(id);
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]

    public ActionResult<List<Vault>> GetVaultsByAccId(string id)
    {
      try
      {
        List<Vault> vaults = _ps.GetVaultsByAccId(id);
        return Ok(vaults);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}