using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;
using ZP_Filapek_Felkel_Makowiejczuk.Interface;


namespace ZP_Filapek_Felkel_Makowiejczuk.Controllers;

[Route("test/grades")]
[ApiController]
[Authorize]
public class GradeController : Controller
{
    private readonly IGradeService gradeService;

    public GradeController(IGradeService gradeService)
    {
        this.gradeService = gradeService;
    }

    [HttpPost("add")]
    public IActionResult AddGrade([FromBody] NewGradeDto gradeDto)
    {
        var teacherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //try
        {
            gradeService.AddGrade(teacherId, gradeDto);
            return Ok("Grade added successfully");
        }
        //catch (Exception ex)
        {
            //return BadRequest($"Error adding grade: {ex.Message}");
        }
    }

    [HttpGet("get/{studentId}")]
    public IActionResult GetGradeListByStudentId(int studentId)
    {
        var teacherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //try
        {
            var grades = gradeService.GetGradeListByStudentId(studentId, teacherId);
            return Ok(grades);
        }
        //catch (Exception ex)
        {
            //return BadRequest($"Error getting grade list: {ex.Message}");
        }
    }

    [HttpGet("get/{studentId}/{count}")]
    public IActionResult GetGradeListByStudentId(int studentId, int count)
    {
        var teacherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //try
        {
            var grades = gradeService.GetGradeListByStudentId(studentId, teacherId, count);
            return Ok(grades);
        }
        //catch (Exception ex)
        {
            //return BadRequest($"Error getting grade list: {ex.Message}");
        }
    }

    [HttpPut("update")]
    public IActionResult UpdateGrade([FromBody] GradeDto gradeDto)
    {
        var teacherId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //try
        {
            gradeService.UpdateGrade(teacherId, gradeDto);
            return Ok("Grade updated successfully");
        }
        //catch (Exception ex)
        {
            //return BadRequest($"Error updating grade: {ex.Message}");
        }
    }


}

