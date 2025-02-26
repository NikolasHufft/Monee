using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace MoneeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeTypeController(IGenericRepository<OvertimeType> genericRepository) :
        GenericController<OvertimeType>(genericRepository)
    {
    }
}
