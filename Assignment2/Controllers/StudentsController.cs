﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;
using Assignment2.Models.ViewModels;

namespace Assignment2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public StudentsController(SchoolCommunityContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(int? id)
        {
            CommunityViewModel viewModel = new CommunityViewModel();

            viewModel.Students = await _context.Students
                .Include(i => i.CommunityMembership)
                    .ThenInclude(i => i.Community)
                .AsNoTracking()
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                ViewData["studentId"] = id;
                viewModel.CommunityMemberships = viewModel.Students.Where(
                    x => x.Id == id).Single().CommunityMembership;
            }
            return View(viewModel);

        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LastName,FirstName,EnrollmentDate")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> EditMemberships(int id)
        {
            CommunityViewModel communityViewModel = new CommunityViewModel();

            communityViewModel.CommunityMemberships = await _context.CommunityMemberships.Where(i => i.StudentId == id).ToListAsync();
            communityViewModel.Students = await _context.Students.Where(i => i.Id == id).ToListAsync();
            communityViewModel.Communities = await _context.Communities.OrderBy(i => i.Title)
                .ToListAsync();

            return View(communityViewModel);
        }

        public async Task<IActionResult> AddMemberships(int studentId, string communityId)
        {
            CommunityMembership addMember = new CommunityMembership();

            addMember.CommunityId = communityId;
            addMember.StudentId = studentId;
            _context.CommunityMemberships.Add(addMember);

            await _context.SaveChangesAsync();

            return RedirectToAction("EditMemberships", new { id = studentId });
        }

        public async Task<IActionResult> RemoveMemberships(int studentId, string communityId)
        {
            CommunityMembership removeMember = new CommunityMembership();

            removeMember.CommunityId = communityId;
            removeMember.StudentId = studentId;
            _context.CommunityMemberships.Remove(removeMember);

            await _context.SaveChangesAsync();

            return RedirectToAction("EditMemberships", new { id = studentId });

        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
