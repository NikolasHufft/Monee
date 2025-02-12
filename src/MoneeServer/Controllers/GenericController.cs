using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace MoneeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T>(IGenericRepository<T> genericRepository) : ControllerBase where T : class
    {
        //private readonly IGenericRepository<T> _genericRepository;

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await genericRepository.GetAll());
        }

        //public GenericController(IGenericRepository<T> genericRepository)
        //{
        //    genericRepository = genericRepository;
        //}
        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            IsValid(id);
            return Ok(await genericRepository.GetById(id));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(T entity)
        {
            IsValid(entity);
            return Ok(await genericRepository.Insert(entity));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(T entity)
        {
            IsValid(entity);
            return Ok(await genericRepository.Update(entity));
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            IsValid(id);
            return Ok(await genericRepository.DeleteById(id));
        }

        private BadRequestObjectResult? IsValid(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid request sent");
            }

            return null;
        }
        private BadRequestObjectResult? IsValid(T entity)
        {
            if (entity is null)
            {
                return BadRequest("Invalid request sent");
            }
            return null;
        }
    }
}
