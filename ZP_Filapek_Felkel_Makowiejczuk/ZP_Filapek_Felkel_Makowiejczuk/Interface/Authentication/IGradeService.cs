using Data.Model;
using ZP_Filapek_Felkel_Makowiejczuk.Dto;

namespace ZP_Filapek_Felkel_Makowiejczuk.Interface
{
    public interface IGradeService
    {
        public IEnumerable<GradeDto> GetGradeListByStudentId(int studentId, int teacherId);
        public IEnumerable<GradeDto> GetGradeListByStudentId(int studentId, int teacherId, int count);
        public void AddGrade(int userId, NewGradeDto gradeDto);
        public void UpdateGrade(int teacherId, GradeDto gradeDto);
    }
}