#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial2.Models;
using ParcialTickets.Data;
using ParcialTickets.Data.Entities;

namespace Parcial2.Controllers
{
    public class EntrancesController : Controller
    {
        private readonly DataContext _context;

        public EntrancesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Entradas
                .Include(e => e.Tickets)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entrance entrance = await _context.Entradas
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrance == null)
            {
                return NotFound();
            }

            return View(entrance);
        }

        public async Task<IActionResult> DetailsTicket(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Ticket ticket = await _context.Tickets
                .Include(t => t.Entrance)
                .FirstOrDefaultAsync(m =>m.Id == id);
            if(ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        public IActionResult Create()
        {
            Entrance entrance = new() {Tickets = new List<Ticket>()};
            return View(entrance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Entrance entrance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(entrance);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Entrada con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(entrance);
        }

        public async Task<IActionResult> AddTicket(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Entrance entrance = await _context.Entradas.FindAsync(id);
            if(entrance == null)
            {
                return NotFound();
            }
            TicketViewModel model = new()
            {
                EntranceId = entrance.Id,
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTicket(TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Ticket ticket = new();
                    _context.Add(ticket);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.EntranceId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un Ticket con estos datos.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);

        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entrance entrance = await _context.Entradas
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (entrance == null)
            {
                return NotFound();
            }
            return View(entrance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Entrance entrance)
        {
            if (id != entrance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrance);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Entrada con los mismos datos.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(entrance);
        }
        public async Task<IActionResult> EditTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.Tickets
                .Include(s => s.Entrance)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            TicketViewModel model = new()
            {
                EntranceId = ticket.Entrance.Id,
                Id = ticket.Id,
                Name = ticket.Name,
                Document = ticket.Document,
                WasUsed = ticket.WasUsed,
                Entrance = ticket.Entrance,
                Date = ticket.Date,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTicket(int id, TicketViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Ticket state = new()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Document = model.Document,
                        WasUsed = model.WasUsed,
                        Entrance = model.Entrance,
                        Date = model.Date,

                    };
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = model.EntranceId });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un un ticket con los mismos datos.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }


        // GET: Entrances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entrance entrance = await _context.Entradas
                .Include(e => e.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entrance == null)
            {
                return NotFound();
            }

            return View(entrance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Entrance entrance = await _context.Entradas.FindAsync(id);
            _context.Entradas.Remove(entrance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket ticket = await _context.Tickets
                .Include(s => s.Entrance)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost, ActionName("DeleteState")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTicketConfirmed(int id)
        {
            Ticket ticket = await _context.Tickets
                .Include(s => s.Entrance)
                .FirstOrDefaultAsync(s => s.Id == id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = ticket.Entrance.Id });
        }       
    }
}
