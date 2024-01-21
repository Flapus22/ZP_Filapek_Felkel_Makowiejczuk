namespace Data.Model;

public class StudentClass
{
    public int Id { get; set; }
    
    public int StudentID { get; set; }
    public User Student { get; set; }
    public int LessonID { get; set; }
    public Lesson Lesson { get; set; }
}
