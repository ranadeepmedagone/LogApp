
using logapp.DTOs;
using logapp.Models;
using logapp.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace logapp.Controllers;


[ApiController]
[Route("api/user")]


public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _user;

    public UserController(ILogger<UserController> logger,
    IUserRepository user)
    {
        _logger = logger;
        _user = user;
    }



    [HttpGet]

    public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
    {
        var usersList = await _user.GetAllUsers();

        // User -> UserDTO
        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUserById([FromRoute] int id)
    {
        var res = await _user.GetUserById(id);

        if (res is null)
            return NotFound("No user found with given id");

        var dto = res.asDto;
        dto.ListOfTags = (await _user.GetUserTagsById(id)).Select(x => x.asDto).ToList();


        return Ok(dto);
    }
    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserCreateDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        //     return BadRequest("Gender value is not recognized");

        //    / var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        //     if (subtractDate.TotalDays / 365 < 18.0)
        //         return BadRequest("User must be at least 18 years old");

        var toCreateUser = new User
        {
            Name = Data.Name.Trim(),
            Email = Data.Email.Trim().ToLower(),
            HashPassword = Data.HashPassword
        };

        var createdUser = await _user.CreateUser(toCreateUser);

        return StatusCode(StatusCodes.Status201Created, createdUser.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] int id,
    [FromBody] UserUpdateDTO Data)
    {
        var existing = await _user.GetUserById(id);
        if (existing is null)
            return NotFound("No user found with given id");

        var toUpdateUser = existing with
        {
            Name = Data.Name?.Trim() ?? existing.Name,
            HashPassword = Data.HashPassword

        };

        var didUpdate = await _user.UpdateUser(toUpdateUser);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();
    }



    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteUser([FromRoute] int id)
    // {
    //     var existing = await _user.GetUserById(id);
    //     if (existing is null)
    //         return NotFound("No user found with given user name");

    //     var didDelete = _user.DeleteUser(id);

    //     return NoContent();
    // }
}