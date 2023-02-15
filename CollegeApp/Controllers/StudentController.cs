using CollegeApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    [HttpGet]
    [Route("All", Name = "GetAllStudents")]
    public ActionResult<IEnumerable<Student>> GetStudents()
    {
        var students = CollegeRepository.Students.Select(s => new StudentDTO()
        {
            Id = s.Id,
            StudentName = s.StudentName,
            Email = s.Email,
            Address = s.Address
        });
        
        //OK - 200 - success
        return Ok(CollegeRepository.Students);
    }
    
    [HttpGet]
    [Route("{id:int}", Name = "GetStudentById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    
    public ActionResult<StudentDTO> GetStudentById(int id)
    {
        //OK - 400 - Badrequest-client error
        if (id <= 0)
            return BadRequest();

        var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        if (student == null)
            return NotFound($"The student with id {id} not found");

        var studentDTO = new StudentDTO()
        {
            Id = student.Id,
            StudentName = student.StudentName,
            Email = student.Email,
            Address = student.Address
        };
        //OK - 200 - success
        return Ok(student);

    }

    [HttpGet("{name:alpha}", Name = "GetStudentByName")]
    public ActionResult<StudentDTO> GetStudentByName(string name)
    {
        //OK - 400 - Badrequest-client error
        if (string.IsNullOrEmpty(name))
            return BadRequest();

        var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
        if (student == null)
            return NotFound($"The student with name {name} not found");
        var studentDTO = new StudentDTO()
        {
            Id = student.Id,
            StudentName = student.StudentName,
            Email = student.Email,
            Address = student.Address
        };
        //OK - 200 - success
        return Ok(studentDTO);

    }

    [HttpPut]
    [Route("Update")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    
    public ActionResult UpdateStudent([FromBody] StudentDTO model)
    {
        if (model == null || model.Id <= 0)
            BadRequest();

        var exitingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();

        if (exitingStudent == null)
            return NotFound();

        exitingStudent.StudentName = model.StudentName;
        exitingStudent.Address = model.Address;
        exitingStudent.Id = model.Id;
        exitingStudent.Email = model.Email;
        return NoContent();
    }
        
    
    
    [HttpPost]
    [Route("Create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
    {
        //if (ModelState.IsValid)
          //  return BadRequest(ModelState);
        
        if (model == null)
            return BadRequest();
        int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
        Student student = new Student
        {
            Id = newId,
            StudentName = model.StudentName,
            Email = model.Email,
            Address = model.Address
        };
        CollegeRepository.Students.Add(student);

        model.Id = student.Id;
        return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
        //return Ok(model);
    }

    [HttpDelete("{id}", Name ="DeleteStudentById")]
    public ActionResult<bool> DeleteStudent(int id)
    {
        //OK - 400 - Badrequest-client error
        if (id <= 0)
            return BadRequest();

        var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
        if (student == null)
            return NotFound($"The student with id {id} not found");
        
        
        CollegeRepository.Students.Remove(student);
        
        //OK - 200 - success
        return (true);

    }
    
}