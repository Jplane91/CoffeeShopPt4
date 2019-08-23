using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoffeeShop.Models;

namespace CoffeeShop.Controllers
{
    public class CoffeeUsersController : Controller
    {
        private readonly CoffeeShopDBContext _context;

        public CoffeeUsersController(CoffeeShopDBContext context)
        {
            _context = context;
        }

        // GET: CoffeeUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoffeeUser.ToListAsync());
        }

        // GET: CoffeeUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffeeUser = await _context.CoffeeUser
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (coffeeUser == null)
            {
                return NotFound();
            }

            return View(coffeeUser);
        }

        // GET: CoffeeUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CoffeeUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Age,UserPassword,UserId,Funds")] CoffeeUser coffeeUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coffeeUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coffeeUser);
        }

        // GET: CoffeeUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffeeUser = await _context.CoffeeUser.FindAsync(id);
            if (coffeeUser == null)
            {
                return NotFound();
            }
            return View(coffeeUser);
        }

        // POST: CoffeeUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Email,Age,UserPassword,UserId,Funds")] CoffeeUser coffeeUser)
        {
            if (id != coffeeUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coffeeUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoffeeUserExists(coffeeUser.UserId))
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
            return View(coffeeUser);
        }

        // GET: CoffeeUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coffeeUser = await _context.CoffeeUser
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (coffeeUser == null)
            {
                return NotFound();
            }

            return View(coffeeUser);
        }

        // POST: CoffeeUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coffeeUser = await _context.CoffeeUser.FindAsync(id);
            _context.CoffeeUser.Remove(coffeeUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoffeeUserExists(int id)
        {
            return _context.CoffeeUser.Any(e => e.UserId == id);
        }
    }
}
