using CoursesApi.Data;
using CoursesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Controllers;

[ApiController]
[Route("courses")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _db;

    public CoursesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetAll()
    {
        return await _db.Courses
            .AsNoTracking()
            .OrderBy(course => course.Id)
            .ToListAsync();
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var courses = _db.Courses.AsNoTracking();

        var count = await courses.CountAsync();
        var totalHours = await courses.SumAsync(
            course => (int?)course.Hours) ?? 0;
        var averageHours = await courses.AverageAsync(
            course => (double?)course.Hours);
        var minHours = await courses.MinAsync(
            course => (int?)course.Hours);
        var maxHours = await courses.MaxAsync(
            course => (int?)course.Hours);

        return Ok(new
        {
            count,
            totalHours,
            averageHours = averageHours ?? 0,
            minHours,
            maxHours
        });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Course>> GetById(int id)
    {
        var course = await _db.Courses.FindAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        return course;
    }

    [HttpPost]
    public async Task<ActionResult<Course>> Create(Course course)
    {
        _db.Courses.Add(course);
        await _db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = course.Id },
            course);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Course updatedCourse)
    {
        var course = await _db.Courses.FindAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        course.Title = updatedCourse.Title;
        course.Teacher = updatedCourse.Teacher;
        course.Hours = updatedCourse.Hours;
        course.Description = updatedCourse.Description;

        await _db.SaveChangesAsync();
        return Ok(course);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _db.Courses.FindAsync(id);

        if (course is null)
        {
            return NotFound();
        }

        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}