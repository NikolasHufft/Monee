using BaseLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace MoneeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(IGenericRepository<City> genericRepository) :
        GenericController<City>(genericRepository)
    {
    }
}
