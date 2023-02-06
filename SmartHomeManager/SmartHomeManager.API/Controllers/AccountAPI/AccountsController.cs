using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.AccountDataSource;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;


namespace SmartHomeManager.API.Controllers.AccountController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        // dependency injection is just saying this class can use this thing
        // in this context - accounts controller can use account service
        private readonly AccountService _accountService;
        private readonly EmailService _emailService;

        public AccountsController(AccountService accountService, EmailService emailService)
        {
            _accountService = accountService;
            _emailService = emailService;
        }

        /*private readonly AccountRepository _accountRepository;
        
        public AccountsController(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }*/

        // GET: api/Accounts
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            if (_accountRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            *//*return await _accountRepository.GetAllAsync();*//*
            return await _accountRepository.GetAllAsync();
        }*/

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountByAccountId(Guid id)
        {
            var account = await _accountService.GetAccountByAccountId(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        


        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(Guid id, Account account)
        {
           /* if (id != account.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/

            return NoContent();
        }

        // this is an API endpoint
        
        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostAccount([FromBody]AccountWebRequest account)
        {
            
            // controller will invoke a service function
            string response = await _accountService.CreateAccount(account);

            if (response == "account created")
            {
                // email service
                bool emailResponse = _emailService.SendRegistrationEmail(account.Username, account.Email);

                if (emailResponse)
                {
                    return Ok("Account created");
                }

                return BadRequest("Account created but email not sent");
                
            }

            else
            {
                return BadRequest(response);
            }
        }

        // POST: api/Accounts/login

        [HttpPost("login")]
        public async Task<ActionResult> VerifyLogin([FromBody]LoginWebRequest login)
        {
            bool response = await _accountService.VerifyLogin(login);

            if (response)
            {
                return Ok("Login successful");
            }

            return Unauthorized("Incorrect email or password");
        }




        // DELETE: api/Accounts/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            if (_accountRepository.GetAllAsync() == null)
            {
                return NotFound();
            }
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            await _accountRepository.DeleteAsync(account);
            await _accountRepository.SaveAsync();

            *//*_context.Accounts.Remove(account);
            await _context.SaveChangesAsync();*//*

            return NoContent();
        }*/

        /*private bool AccountExists(Guid id)
        {
            return (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }*/
    }
}
