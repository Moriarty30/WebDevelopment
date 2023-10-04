﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyBankMVC.Context;
using MoneyBankMVC.Models;

namespace MoneyBankMVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly MoneybankdbContext _context;

        public AccountsController(MoneybankdbContext context)
        {
            _context = context;
        }

        // Métodos de acción revisados

        // Index: Listar todas las cuentas
        public async Task<IActionResult> Index()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return View(accounts);
        }

        // Create: Crear una nueva cuenta
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            var account = await GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account account)
        {
            if (ModelState.IsValid)
            {
                account.CreationDate = DateTime.Now;

                if (account.AccountType == "C")
                {
                    account.BalanceAmount += Account.MAX_OVERDRAFT;
                }

                _context.Add(account);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "La creación fue exitosa.";
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // Edit: Editar una cuenta existente
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "La edición fue exitosa.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            return View(account);
        }

        // Delete: Eliminar una cuenta
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "La eliminación fue exitosa.";
            }
            return RedirectToAction(nameof(Index));
        }

        // Depositar: Realizar un depósito en una cuenta
        public async Task<IActionResult> Depositar(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Depositar(int id, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            if (amount <= 0)
            {
                ModelState.AddModelError("amount", "El monto del depósito debe ser mayor a cero.");
                return View("Depositar", account);
            }

            account.BalanceAmount += amount;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "El depósito fue exitoso.";
            return RedirectToAction(nameof(Index));
        }

        // Retirar: Realizar un retiro desde una cuenta
        public async Task<IActionResult> Retirar(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        private async Task<Account?> GetAccountByIdAsync(int? id)
        {
            if (!id.HasValue) return null;
            return await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id.Value);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Retirar(int id, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            if (amount <= 0)
            {
                ModelState.AddModelError("amount", "El monto del retiro debe ser mayor a cero.");
                return View("Retirar", account);
            }

            if (amount > account.BalanceAmount)
            {
                ModelState.AddModelError("amount", "Fondos insuficientes.");
                return View("Retirar", account);
            }

            account.BalanceAmount -= amount;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "El retiro fue exitoso.";
            return RedirectToAction(nameof(Index));
        }

        // Métodos auxiliares

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
