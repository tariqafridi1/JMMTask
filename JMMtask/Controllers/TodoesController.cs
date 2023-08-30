using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JMMtask.DatabaseContext;
using JMMtask.Models;
using Microsoft.AspNetCore.Authorization;

namespace JMMtask.Controllers
{
    [Authorize]

    public class TodoesController : Controller
    {

        private readonly TodoDBContext _context;

        public TodoesController(TodoDBContext context)
        {
            _context = context;
        }

        // GET: Todoes
        public  IActionResult Index()
        {
            var result=  _context.Todos.ToList();
            var model = new List<Todo>();
            foreach (var item in result)
            {
                model.Add(new Todo()
                {
                    ID = item.ID,
                    Description = item.Description, 
                     DueDate = item.DueDate,
                     Priority = item.Priority,  
                     Status = item.Status,  
                     Title= item.Title,
                });
            }
            return View(model);
                        
        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: Todoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todo =  _context.Todos.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit( Todo todo)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.ID))
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
            return View(todo);
        }

        // GET: Todoes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Todos == null)
            {
                return NotFound();
            }

            var todo =  _context.Todos
                .FirstOrDefault(m => m.ID == id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            if (_context.Todos == null)
            {
                return Problem("Entity set 'TodoDBContext.Todos'  is null.");
            }
            var todo =  _context.Todos.Find(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
            }
            
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
          return (_context.Todos?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
