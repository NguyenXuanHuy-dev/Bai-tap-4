using Microsoft.AspNetCore.Mvc;
using baitap.Models;
using System.Collections.Generic;
using System.Linq;

namespace baitap.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoItem> _todoItems = new List<TodoItem>
        {
            new TodoItem { Id = 1, Name = "Đi chợ", IsCompleted = true },
            new TodoItem { Id = 2, Name = "Chơi thể thao", IsCompleted = false },
            new TodoItem { Id = 3, Name = "Chơi game", IsCompleted = false },
            new TodoItem { Id = 4, Name = "Học bài", IsCompleted = true }
        };

        public IActionResult Index()
        {
            return View(_todoItems);
        }

        public IActionResult Details(int id)
        {
            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                item.Id = _todoItems.Max(t => t.Id) + 1;
                _todoItems.Add(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                var existingItem = _todoItems.FirstOrDefault(t => t.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.Name = item.Name;
                    existingItem.IsCompleted = item.IsCompleted;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public IActionResult Delete(int id)
        {
            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item != null)
            {
                _todoItems.Remove(item);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
