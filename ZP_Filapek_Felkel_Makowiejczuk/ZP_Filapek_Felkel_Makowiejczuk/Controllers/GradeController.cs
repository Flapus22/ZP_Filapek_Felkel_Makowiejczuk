using Microsoft.AspNetCore.Mvc;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;
using ZP_Filapek_Felkel_Makowiejczuk.Interface;


namespace ZP_Filapek_Felkel_Makowiejczuk.Controllers;

[Route("test/grades")]
[ApiController]
public class GradeController : Controller
{
    private readonly IGradeService gradeService;

    public GradeController(IGradeService gradeService)
    {
        this.gradeService = gradeService;
    }

    [HttpPost("add")]
    public IActionResult AddGrade(int userId, [FromBody] GradeDto gradeDto)
    {
        try
        {
            gradeService.AddGrade(userId, gradeDto);
            return Ok("Grade added successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error adding grade: {ex.Message}");
        }
    }
}

