using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EducationalPlatform.Data;
using EducationalPlatform.Models;

namespace EducationalPlatform.Controllers
{
    public class StudentCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentCourses
        public async Task<IActionResult> Index(string searchStudent, string searchCourse)
        {
            var studentcourses = _context.StudentCourse.Include(s => s.Course).Include(s => s.Student).ToList();
            if (!string.IsNullOrEmpty(searchStudent) || !string.IsNullOrEmpty(searchCourse))
            {
                studentcourses = studentcourses.FindAll(m => m.Student.Name!.Contains(searchStudent) && m.Course.Name!.Contains(searchCourse));
            }
            return View(studentcourses);
        }

        // GET: StudentCourses/Details/5
        public async Task<IActionResult> Details(int? studentId, int? courseId)
        {
            if (studentId == null || courseId == null || _context.StudentCourse == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.StudentCourse
                .Include(s => s.Course)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.StudentId == studentId && m.CourseId == courseId);
            if (studentCourse == null)
            {
                return NotFound();
            }
            return View(studentCourse);
        }

        // GET: StudentCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name");
            return View();
        }

        // POST: StudentCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CourseId,Grade,Status,TotalPayment")] StudentCourse studentCourse)
        {
            if (true)
            {
                _context.Add(studentCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", studentCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", studentCourse.StudentId);
            return View(studentCourse);
        }

        // GET: StudentCourses/Edit/5
        public async Task<IActionResult> Edit(int? studentId, int? courseId)
        {
            if (studentId == null || courseId == null || _context.StudentCourse == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.StudentCourse.FindAsync(studentId, courseId);
            if (studentCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", studentCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", studentCourse.StudentId);
            return View(studentCourse);
        }

        // POST: StudentCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int studentId, int courseId, [Bind("StudentId,CourseId,Grade,Status,TotalPayment")] StudentCourse studentCourse)
        {
            if (studentId == null || courseId == null || _context.StudentCourse == null)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(studentCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCourseExists(studentCourse.StudentId, studentCourse.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Name", studentCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", studentCourse.StudentId);
            return View(studentCourse);
        }

        // GET: StudentCourses/Delete/5
        public async Task<IActionResult> Delete(int? studentId, int? courseId)
        {
            if (studentId == null || courseId == null || _context.StudentCourse == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.StudentCourse
                .Include(s => s.Course)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.StudentId == studentId && m.CourseId == courseId);
            if (studentCourse == null)
            {
                return NotFound();
            }

            return View(studentCourse);
        }

        // POST: StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int studentId, int courseId)
        {
            if (_context.StudentCourse == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentCourse'  is null.");
            }
            var studentCourse = await _context.StudentCourse.FindAsync(studentId, courseId);
            if (studentCourse != null)
            {
                _context.StudentCourse.Remove(studentCourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentCourseExists(int studentId, int courseId)
        {
          return (_context.StudentCourse?.Any(e => e.StudentId == studentId && e.CourseId == courseId)).GetValueOrDefault();
        }
    }
}
