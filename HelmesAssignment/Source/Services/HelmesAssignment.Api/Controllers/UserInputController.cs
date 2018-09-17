using HelmesAssignment.Api.Initializers;
using HelmesAssignment.Api.Properties;
using HelmesAssignment.Data.Context;
using HelmesAssignment.Data.Respositories.UserInput;
using HelmesAssignment.Entites.Input;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HelmesAssignment.Api.Controllers
{
    public class UserInputController : ApiController
    {
        protected static readonly ILog _logger = LogManager.GetLogger(Settings.ApplicationName);
        private readonly HelmesDbContext _helmesDbContext = null;
        private IUserInputRepository<UserInputTable> _userInputRepo = null;

        public UserInputController()
        {
            _helmesDbContext = new HelmesDbContext(HelmesSettings.Instance.HelmesDbConnectionString);
        }

        [HttpPost]
        [Route("api/UserInput/AddUserInput")]
        public async Task<IHttpActionResult> Post([FromBody()] UserInputTable input)
        {
            using (_userInputRepo = new UserInputRepository(_helmesDbContext))
            {
                try
                {
                    _logger.Info($"Adding user input to database for user:{input.UserName}");
                    var result = await _userInputRepo.InsertUserInputAsync(input);
                    _logger.Info("User input successfully added.");
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.Error("AddUserInput", ex);
                    StringBuilder message = new StringBuilder();
                    message.AppendLine(ex.Message);
                    if (ex.InnerException != null)
                        message.AppendLine(ex.InnerException.Message);
                    return BadRequest(message.ToString());
                }
            }
        } 

    }
}
