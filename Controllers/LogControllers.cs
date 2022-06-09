
using logapp.DTOs;
using logapp.Models;
using logapp.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace logapp.Controllers;


[ApiController]
[Route("api/Log")]


public class LogController : ControllerBase
{

    private readonly ILogger<LogController> _logger;
    private readonly ILogRepository _Log;

    public LogController(ILogger<LogController> logger,
    ILogRepository Log)
    {
        _logger = logger;
        _Log = Log;
    }



    [HttpGet]

    public async Task<ActionResult<List<LogDTO>>> GetAllLogs()
    {
        var LogsList = await _Log.GetAllLogs();

        // Log -> LogDTO
        var dtoList = LogsList.Select(x => x.asDto);

        return Ok(dtoList);
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<Log>> GetLogById([FromRoute] int id)
    {
        var res = await _Log.GetLogById(id);

        if (res is null)
            return NotFound("No Log found with given id");

        var dto = res.asDto;
        dto.ListOfTags = (await _Log.GetLogTagsById(id)).Select(x => x.asDto).ToList();



        return Ok(dto);
    }
    [HttpPost]
    public async Task<ActionResult<LogDTO>> CreateLog([FromBody] CreateLogDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        //     return BadRequest("Gender value is not recognized");

        //    / var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        //     if (subtractDate.TotalDays / 365 < 18.0)
        //         return BadRequest("Log must be at least 18 years old");

        var toCreateLog = new Log
        {
            Title = Data.Title.Trim(),
            Description = Data.Description.Trim().ToLower(),
            StackTrace = Data.StackTrace
        };

        var createdLog = await _Log.CreateLog(toCreateLog);

        return StatusCode(StatusCodes.Status201Created, createdLog.asDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLog([FromRoute] int id,
    [FromBody] UpdateLogDTO Data)
    {
        var existing = await _Log.GetLogById(id);
        if (existing is null)
            return NotFound("No Log found with given id");

        var toUpdateLog = existing with
        {
            Description = Data.Description?.Trim() ?? existing.Description,

        };

        var didUpdate = await _Log.UpdateLog(toUpdateLog);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Log");

        return NoContent();
    }



    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLog([FromRoute] int id)
    {
        var existing = await _Log.GetLogById(id);
        if (existing is null)
            return NotFound("No Log found with given Log name");

        var didDelete = _Log.DeleteLog(id);

        return NoContent();
    }
}