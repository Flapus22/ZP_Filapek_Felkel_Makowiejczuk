namespace Data.Model;

public class Grade
{
    public int Id { get; set; }
    public int GradeValue { get; set; }
    public DateTime GradeDate { get; set; }

    public int StudentID { get; set; }
    public User Student { get; set; }
    public int TeacherID { get; set; }
    public User Teacher { get; set; }
    public int LessonID { get; set; }
    public Lesson Lesson { get; set; }
}
