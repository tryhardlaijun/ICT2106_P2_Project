using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities.DTOs;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.ScenariosAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenariosController : ControllerBase
    {
        private readonly ScenarioServices _scenarioServices;
        //private readonly GetScenarioService _getScenarioService;

        public ScenariosController(IScenarioRepository<Scenario> scenarioRepository, IInformDirectorServices informDirectorServices)
        {
            _scenarioServices = new(scenarioRepository,informDirectorServices);
            //_getScenarioService = new(scenarioRepository);
        }

        // GET: api/Scenarios/GetAllScenarios
        [HttpGet("GetAllScenarios")]
        public async Task<IEnumerable<ScenarioRequest>> GetAllScenarios()
        {
            var resp = await _scenarioServices.GetAllScenariosAsync();
            return resp;
        }

        // GET api/Scenarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scenario>> GetScenario(Guid id)
        {
            var scenario = await _scenarioServices.GetScenarioByIdAsync(id);
            if(scenario !=null)
            {
                return StatusCode(200,scenario);
            }
            return StatusCode(404, "scenario not exist");
        }

        // POST api/Scenarios
        [HttpPost("CreateScenario")]
        public async Task<ActionResult<Scenario>> CreateScenario([FromBody] ScenarioRequest scenarioRequest)
        { 
            await _scenarioServices.CreateScenarioAsync(scenarioRequest);
            return StatusCode(200, scenarioRequest);
        }

        // PUT api/Scenarios/5
        [HttpPut("EditScenario")]
        public async Task<ActionResult> EditScenario(ScenarioRequest scenarioRequest)
        {
            await _scenarioServices.EditScenarioAsync(scenarioRequest);
            return StatusCode(200, scenarioRequest);
        }


        // DELETE api/Scenarios/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScenario(Guid id)
        {
           var scenario = await _scenarioServices.GetScenarioByIdAsync(id);
           if (scenario != null)
            {
                await _scenarioServices.DeleteScenarioeByIdAsync(id);
                return StatusCode(200, scenario);
            }
            return StatusCode(404, "scenario not exist");
        }
        
        [HttpPost("VoiceInput")]
        public async Task<ActionResult<Scenario>> VoiceInput([FromBody] string input)
        {
            await _scenarioServices.CheckVoiceInput(input);
            return Ok();
        }

        [HttpGet("GetScenarioByName")]
        public async Task<ActionResult<Scenario>> GetScenarioByName(string name)
        {
            var scenario = await _scenarioServices.GetScenarioByName(name);
            if(scenario !=null)
            {
                return StatusCode(200,scenario);
            }
            return StatusCode(404, "scenario not exist");
        }
    }
}

