using Microsoft.AspNetCore.Mvc;
using SPCManagementSystemAPI.Models;
using SPCManagementSystemAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Controllers
{
    [Route("api/tenders")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly ITenderRepository _tenderRepository;

        public TenderController(ITenderRepository tenderRepository)
        {
            _tenderRepository = tenderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tender>>> GetAllTenders()
        {
            return Ok(await _tenderRepository.GetAllTenders());
        }

        [HttpPost]
        public async Task<ActionResult<Tender>> SubmitTender(Tender tender)
        {
            var createdTender = await _tenderRepository.SubmitTender(tender);
            return CreatedAtAction(nameof(GetAllTenders), createdTender);
        }
    }
}
