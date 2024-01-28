using Data;
using Data.Model;
using Microsoft.AspNetCore.Identity;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;
using ZP_Filapek_Felkel_Makowiejczuk.Interface;
using ZP_Filapek_Felkel_Makowiejczuk.Model.Authentication;

namespace ZP_Filapek_Felkel_Makowiejczuk.Services;

public class GradeService : IGradeService
{
    private readonly DBContext context;

    public GradeService(DBContext context)
    {
        this.context = context;
    }

    public void AddGrade(int teacherId, NewGradeDto gradeDto)
    {
        var grade = new Grade()
        {
            StudentID = gradeDto.StudentId,
            TeacherID = teacherId,
            LessonID = gradeDto.LessonId,
            GradeValue = gradeDto.Value,
            GradeDate = DateTime.Now
        };

        context.Grades.Add(grade);
        context.SaveChanges();
    }

    public IEnumerable<GradeDto> GetGradeListByStudentId(int studentId, int teacherId)
    {
        var grades = context.Grades.Where(x => x.TeacherID == teacherId && x.StudentID == studentId).OrderByDescending(x => x.GradeDate).ToList();
        var result = new List<GradeDto>();
        foreach (var grade in grades)
        {
            result.Add(new GradeDto()
            {
                Id = grade.Id,
                StudentId = grade.StudentID,
                LessonId = grade.LessonID,
                Value = grade.GradeValue,
                GradeDate = grade.GradeDate
            });
        }
        return result;
    }
    public IEnumerable<GradeDto> GetGradeListByStudentId(int studentId, int teacherId, int count)
    {
        var grades = context.Grades.Where(x => x.TeacherID == teacherId && x.StudentID == studentId).OrderByDescending(x => x.GradeDate).Take(count).ToList();
        var result = new List<GradeDto>();
        foreach (var grade in grades)
        {
            result.Add(new GradeDto()
            {
                Id = grade.Id,
                StudentId = grade.StudentID,
                LessonId = grade.LessonID,
                Value = grade.GradeValue,
                GradeDate = grade.GradeDate
            });
        }
        return result;
    }

    public void UpdateGrade(int teacherId, GradeDto gradeDto)
    {
        var grade = context.Grades.FirstOrDefault(x => x.Id == gradeDto.Id);
        if (grade is null)
        {
            throw new Exception("Grade not found");
        }

        if (grade.TeacherID != teacherId)
        {
            throw new Exception("You are not allowed to edit this grade");
        }

        grade.GradeValue = gradeDto.Value;
        grade.GradeDate = DateTime.Now;

        context.Grades.Update(grade);
        context.SaveChanges();
    }
}