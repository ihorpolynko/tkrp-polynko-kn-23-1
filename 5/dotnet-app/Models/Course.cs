using System.ComponentModel.DataAnnotations;

namespace CoursesApi.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(150)]
    public string Teacher { get; set; } = "";

    [Range(1, 1000)]
    public int Hours { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
}