using Microsoft.AspNetCore.Mvc;
using NETCore3WebApp.Business;


namespace NETCore3WebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyEntityController : ControllerBase
    {
        private readonly IMyEntityManager _entityManager;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MyEntityController));

        public MyEntityController(IMyEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        [HttpGet]
        public Domain.Entity Get()
        {
            log.Debug("MyController Get Method");
            return _entityManager.GetMyEntity();
        }

        [HttpPost]
        public bool Post(Domain.Entity myEntity)
        {
            log.Debug("MyController Post Method");
            return _entityManager.SaveMyEntity(myEntity);
        }
    }
}
