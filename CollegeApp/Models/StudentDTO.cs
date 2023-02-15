
using System.ComponentModel.DataAnnotations;
using CollegeApp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CollegeApp.Models;

public class StudentDTO
{
    [ValidateNever]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "student name is requried")]
    [StringLength(10)]
    public string StudentName { get; set; }
    
    [EmailAddress(ErrorMessage = "Please enter valid email address")]
    public string Email { get; set; }

    [Required] 
    public string Address { get; set; }
/*
    [Compare(nameof(Password))]
    public string Password { get; set; }
    
    [Range(10,20)]
    public int Age { get; set; }
    
    [DateCheck]
    public DateTime AdmissionDate { get; set; }      
    
    */
        
    
}