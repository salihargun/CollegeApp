namespace CollegeApp.Models;

public class CollegeRepository
{
    public static List<Student> Students { get; set; } = new List<Student>
    {
        new Student {
            Id = 1,
            StudentName = "Salih Argun",
            Email = "salih@gmail.com",
            Address = "Istanbul"
        },
        new Student {
            Id = 2,
            StudentName = "Elif",
            Email = "elif@gmail.com",
            Address = "Istanbul"  
        }
            
    };
}