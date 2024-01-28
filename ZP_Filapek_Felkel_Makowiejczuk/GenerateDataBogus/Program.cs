using Data;
using Data.Model;
using Bogus;

internal class Program
{
    static List<User> users = new List<User>();
    static List<Lesson> lessons = new List<Lesson>();
    static List<Grade> grades = new List<Grade>();
    static List<StudentClass> studentClasses = new List<StudentClass>();

    private static void Main(string[] args)
    {
        DBContext context = new DBContext();

        GenerateUser();

        context.Users.AddRange(users);
        context.SaveChanges();
        users = context.Users.ToList();

        GenerateLesson();
     
        context.Lessons.AddRange(lessons);
        context.SaveChanges();
        lessons = context.Lessons.ToList();

        GenerateGrade();
        
        context.Grades.AddRange(grades);
        context.SaveChanges();
        grades = context.Grades.ToList();

        GenerateStudentClass();
       
        context.StudentClasses.AddRange(studentClasses);
        context.SaveChanges();   
    }

    private static void GenerateUser()
    {
        var userFaker = new Faker<User>("pl")
            .RuleFor(u => u.Id, 0)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.PasswordHash, f => f.Random.Hash())
            .RuleFor(u => u.FirstName, f => f.Person.FirstName)
            .RuleFor(u => u.LastName, f => f.Person.LastName)
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.District, f => f.Address.CitySuffix())
            .RuleFor(u => u.Street, f => f.Address.StreetName())
            .RuleFor(u => u.PostalCode, f => f.Address.ZipCode())
            .RuleFor(u => u.HouseNumber, f => f.Address.BuildingNumber())
            .RuleFor(u => u.UserType, f => f.PickRandom<UserType>())
            .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth);

        users = userFaker.Generate(100);

        //foreach (var user in users)
        //{
        //    Console.WriteLine($"{user.Id} mail: {user.Email} name: {user.FirstName} {user.LastName} Birth: {user.DateOfBirth} Number: {user.HouseNumber} City: {user.City} District: {user.District} PostalCode: {user.PostalCode} UserType: {user.UserType}");
        //}
    }

    private static void GenerateLesson()
    {
        var lessonFaker = new Faker<Lesson>("pl")
            .RuleFor(u => u.Id, 0)
            .RuleFor(l => l.Name, f => f.Lorem.Word())
            .RuleFor(l => l.TeacherID, f => f.PickRandom(users.Where(u => u.UserType == UserType.Teacher).Select(u => u.Id)))
            .RuleFor(l => l.Date, f => f.Date.Future());

        lessons = lessonFaker.Generate(100);
    }

    private static void GenerateGrade()
    {
        var gradeFaker = new Faker<Grade>("pl")
            .RuleFor(u => u.Id, 0)
            .RuleFor(g => g.StudentID, f => f.PickRandom(users.Where(u => u.UserType == UserType.Student).Select(u => u.Id)))
            .RuleFor(g => g.TeacherID, f => f.PickRandom(users.Where(u => u.UserType == UserType.Teacher).Select(u => u.Id)))
            .RuleFor(g => g.LessonID, f => f.PickRandom(lessons.Select(l => l.Id)))
            .RuleFor(g => g.GradeValue, f => f.Random.Int(1, 6))
            .RuleFor(g => g.GradeDate, f => f.Date.Past());

        grades = gradeFaker.Generate(100);
    }

    private static void GenerateStudentClass()
    {
        var studentClassFaker = new Faker<StudentClass>("pl")
            .RuleFor(u => u.Id, 0)
            .RuleFor(sc => sc.StudentID, f => f.PickRandom(users.Where(u => u.UserType == UserType.Student).Select(u => u.Id)))
            .RuleFor(sc => sc.LessonID, f => f.PickRandom(lessons.Select(l => l.Id)));

        studentClasses = studentClassFaker.Generate(100);
    }
}