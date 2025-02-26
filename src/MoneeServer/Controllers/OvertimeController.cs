using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace MoneeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController(IGenericRepository<Overtime> genericRepository) : 
        GenericController<Overtime>(genericRepository)
    {
    }
}
