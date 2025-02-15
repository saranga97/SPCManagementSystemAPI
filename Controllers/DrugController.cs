using Microsoft.AspNetCore.Mvc;
using SPCManagementSystemAPI.Models;
using SPCManagementSystemAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Controllers
{
    [Route("api/drugs")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly IDrugRepository _drugRepository;

        public DrugController(IDrugRepository drugRepository)
        {
            _drugRepository = drugRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drug>>> GetAllDrugs()
        {
            return Ok(await _drugRepository.GetAllDrugs());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Drug>> GetDrugById(string id)
        {
            var drug = await _drugRepository.GetDrugById(id);
            if (drug == null)
                return NotFound("Drug not found.");
            return Ok(drug);
        }

        [HttpPost]
        public async Task<ActionResult<Drug>> AddDrug(Drug drug)
        {
            var createdDrug = await _drugRepository.AddDrug(drug);
            return CreatedAtAction(nameof(GetDrugById), new { id = createdDrug.Id }, createdDrug);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrug(string id, Drug drug)
        {
            if (id != drug.Id)
                return BadRequest("ID mismatch.");

            var success = await _drugRepository.UpdateDrug(drug);
            if (!success)
                return NotFound("Drug not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrug(string id)
        {
            var success = await _drugRepository.DeleteDrug(id);
            if (!success)
                return NotFound("Drug not found.");

            return NoContent();
        }
    }
}
