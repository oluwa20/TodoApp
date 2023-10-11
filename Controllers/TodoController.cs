using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly TodoDbContext _context;

        public TodoController(TodoDbContext context)
        {
            _context = context;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
              return _context.Todos != null ? 
                          View(await _context.Todos.ToListAsync()) :
                          Problem("Entity set 'TodoDbContext.Todos'  is null.");
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todoItem = await _context.Todos
                .FirstOrDefaultAsync(m => m.TodoId == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodoId,Title,Description,DueDate,Status")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.TodoId = Guid.NewGuid();
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todoItem = await _context.Todos.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TodoId,Title,Description,DueDate,Status")] TodoItem todoItem)
        {
            if (id != todoItem.TodoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.TodoId))
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
            return View(todoItem);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todoItem = await _context.Todos
                .FirstOrDefaultAsync(m => m.TodoId == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Todos == null)
            {
                return Problem("Entity set 'TodoDbContext.Todos'  is null.");
            }
            var todoItem = await _context.Todos.FindAsync(id);
            if (todoItem != null)
            {
                _context.Todos.Remove(todoItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(Guid id)
        {
          return (_context.Todos?.Any(e => e.TodoId == id)).GetValueOrDefault();
        }
    }
}
