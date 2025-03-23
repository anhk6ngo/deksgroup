namespace DTour.Controllers.Identity;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/v1/[controller]")]
[Produces("application/json")]
public class TokenController(ITokenService identityService) : ControllerBase
{
    [HttpPost("token")]
    public async Task<IActionResult> GetToken(TokenRequest model)
    {
        var response = await identityService.LocalLoginAsync(model);
        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
    {
        var response = await identityService.GetLocalRefreshTokenAsync(model);
        return Ok(response);
    }
}