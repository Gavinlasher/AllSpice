using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly RecipesService _rs;

    public AccountController(AccountService accountService, RecipesService rs)
    {
      _accountService = accountService;
      _rs = rs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("favorites")]
    [Authorize]
    public async Task<ActionResult<List<RecipeFavoriteModel>>> GetFavoritesByAccId()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<RecipeFavoriteModel> recipes = _rs.GetFavoritesByAccId(userInfo.Id);
        return Ok(recipes);

      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}