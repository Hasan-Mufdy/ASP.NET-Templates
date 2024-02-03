using API_CRUD_Template.Data.Interfaces;
using API_CRUD_Template.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_CRUD_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstEntityController : ControllerBase
    {
        private readonly IFirstEntity _firstEntityService;

        public FirstEntityController(IFirstEntity firstEntityService)
        {
            _firstEntityService = firstEntityService ?? throw new ArgumentNullException(nameof(firstEntityService));
        }

        // GET: api/FirstEntity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FirstEntity>>> GetAll()
        {
            var entities = await _firstEntityService.GetAll();
            return Ok(entities);
        }

        // GET: api/FirstEntity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FirstEntity>> GetById(int id)
        {
            var entity = await _firstEntityService.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        // POST: api/FirstEntity
        [HttpPost]
        public async Task<ActionResult<FirstEntity>> Post(FirstEntity firstEntity)
        {
            var createdEntity = await _firstEntityService.Post(firstEntity);
            return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
        }

        // PUT: api/FirstEntity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FirstEntity firstEntity)
        {
            var updatedEntity = await _firstEntityService.Update(id, firstEntity);

            if (updatedEntity == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/FirstEntity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _firstEntityService.Delete(id);
            return NoContent();
        }
    }
}
