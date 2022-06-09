

using Microsoft.AspNetCore.Mvc;
using logapp.Models;
using logapp.Repositories;

namespace logapp.Controllers;


[ApiController]
[Route("api/TagType")]


public class TagTypeController : ControllerBase
{ 

    private readonly ILogger<TagTypeController> _TagTypeger;
    private readonly ITagTypeRepository _TagType;

    public TagTypeController(ILogger<TagTypeController> TagTypeger,
    ITagTypeRepository TagType)
    {
        _TagTypeger = TagTypeger;
        _TagType = TagType;
    }



    [HttpGet]

    public async Task<ActionResult<List<TagTypeDTO>>> GetAllTagTypes()
    {
        var TagTypesList = await _TagType.GetAllTagTypes();

        // TagType -> TagTypeDTO
        var dtoList = TagTypesList.Select(x => x.asDto);

        return Ok(dtoList);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<TagType>> GetTagTypeById([FromRoute] int id)
    {
        var res = await _TagType.GetTagTypeById(id);

        if (res is null)
            return NotFound("No TagType found with given id");

        var dto = res.asDto;
        // dto.ListOfLogs = (await _TagType.GetTagTypeLogsById(id)).Select(x => x.asDto).ToList();

        

        return Ok(dto);
    }
    [HttpPost]
    public async Task<ActionResult<TagTypeDTO>> CreateTagType([FromBody] CreateTagTypeDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        //     return BadRequest("Gender value is not recognized");

        //    / var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        //     if (subtractDate.TotalDays / 365 < 18.0)
        //         return BadRequest("TagType must be at least 18 years old");

        var toCreateTagType = new TagType
        {
            TypeName = Data.TypeName.Trim(),
            
        };

        var createdTagType = await _TagType.CreateTagType(toCreateTagType);

        return StatusCode(StatusCodes.Status201Created, createdTagType.asDto);
    }

    // [HttpPut("{id}")]
    // public async Task<ActionResult> UpdateTagType([FromRoute] int id,
    // [FromBody] UpdateTagTypeDTO Data)
    // {
    //     var existing = await _TagType.GetTagTypeById(id);
    //     if (existing is null)
    //         return NotFound("No TagType found with given id");

    //     var toUpdateTagType = existing with
    //     {
    //         Name = Data.Name?.Trim() ?? existing.Name,

    //     };

    //     var didUpdate = await _TagType.UpdateTagType(toUpdateTagType);

    //     if (!didUpdate)
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Could not update TagType");

        // return NoContent();
    // }



    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTagType([FromRoute] int id)
    {
        var existing = await _TagType.GetTagTypeById(id);
        if (existing is null)
            return NotFound("No TagType found with given TagType name");

        var didDelete = _TagType.DeleteTagType(id);

        return NoContent();
    }
}