namespace ZP_Filapek_Felkel_Makowiejczuk.Dto;

public class GradeDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int? LessonId { get; set; }
    public int Value { get; set; }
    public DateTime GradeDate { get; set; }
}