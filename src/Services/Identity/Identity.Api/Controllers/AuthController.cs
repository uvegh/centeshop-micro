using Identity.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController:ControllerBase
{

    private readonly IMapper _mapper;
    public AuthController(IMapper mapper)
    {
        _mapper = mapper;

    }
    //[HttpPost("login")]

    //public async Task<IActionResult<SignupRequestDto>Signup(Signu)
    //{

    //}

    //public async  Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto user)
    //{



    //}
}
