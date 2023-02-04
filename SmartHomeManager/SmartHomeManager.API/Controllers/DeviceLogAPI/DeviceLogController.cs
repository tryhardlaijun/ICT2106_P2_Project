using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Services;
using SmartHomeManager.Domain.RoomDomain.Entities;



namespace SmartHomeManager.API.Controllers.DeviceLogAPI
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DeviceLogController : Controller
    {

        private readonly ApplicationDbContext _context;
        

        // Dependency Injection of repos to services...
        public DeviceLogController(ApplicationDbContext context)
        {
            _context = context;
           
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceLog>>> GetAll()
        {
            if (_context.DeviceLogs == null)
            {
                return NotFound();
            }

            var result = await _context.DeviceLogs.ToListAsync();

            return Ok(result);
        }

        // API routes....
        // GET /api/devicelog/{deviceId}
        // POST /api/devicelog
        // Request body...

    }
}

/*namespace SmartHomeManager.API.Controllers.DeviceLogAPI
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}*/
