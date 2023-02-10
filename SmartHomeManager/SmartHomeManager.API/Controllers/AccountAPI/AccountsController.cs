using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        /* 
         * GET: api/Accounts
         * Returns: 
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            IEnumerable<Account> accounts = await _accountService.GetAccounts();

            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

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

        /* 
         * POST: api/Accounts
         * Return:
         * Ok(1) - Account Created & Email Sent
         * Ok(2) - Account Created but Email Not Sent
         * BadRequest(1) - Account Not Created
         * BadRequest(2) - Email already exists
         * 
        */
        [HttpPost]
        public async Task<ActionResult> PostAccount([FromBody] AccountWebRequest accountWebRequest)
        {

            // controller will invoke a service function
            int response = await _accountService.CreateAccount(accountWebRequest);

            // if create account is successful
            if (response == 1)
            {
                // Email service
                bool emailResponse = _emailService.SendRegistrationEmail(accountWebRequest.Username, accountWebRequest.Email);
                
                if (emailResponse)
                {
                    // if everything is ok
                    return Ok(1);
                }

                // if account created, but email not sent
                return Ok(2);
            }

            // if create account is unsuccessful
            else if (response == 2)
            {
                return BadRequest(1);
            }

            // email already exists
            return BadRequest(2);
            
        }

        /*
         * POST: api/Accounts/login
         * Return:
         * Ok(1) - Login Successful
         * BadRequest(1) - Login Unsuccessful, wrong password
         * BadRequest(2) - Login Unsuccessful, account does not exist
         */

        [HttpPost("login")]
        public async Task<ActionResult> VerifyLogin([FromBody]LoginWebRequest login)
        {
            int response = await _accountService.VerifyLogin(login);

            // login successful
            if (response == 1)
            {            
                return Ok(1);
            }

            // account exists, password wrong
            else if (response == 2)
            {
                return BadRequest(1);
            }

            // account does not exist
            return BadRequest(2);
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
