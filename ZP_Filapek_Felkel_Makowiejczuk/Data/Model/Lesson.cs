namespace Data.Model;

public class Lesson
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }

    public int TeacherID { get; set; }
    public User Teacher { get; set; }
    //public ICollection<Grade> Grades { get; set; }
}
