using HelmesAssignment.Api.Initializers;
using HelmesAssignment.Api.Properties;
using HelmesAssignment.Data.Context;
using HelmesAssignment.Data.Respositories.Sector;
using HelmesAssignment.Entites.Sectors;
using HelmesAssignment.Entites.Sectors.Model;
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
    public class SectorsController : ApiController
    {
        protected static readonly ILog _logger = LogManager.GetLogger(Settings.ApplicationName);
        private readonly HelmesDbContext _helmesDbContext = null;
        private ISectorsRepository<SectorsTable,Sector> _sectorsRepo = null;

        public SectorsController()
        {
            _helmesDbContext = new HelmesDbContext(HelmesSettings.Instance.HelmesDbConnectionString);
        }

        [HttpPost]
        [Route("api/Sectors/InsertSectors")]
        public async Task<IHttpActionResult> Post([FromBody()]SectorListModel sectorModel)
        {
            using (_sectorsRepo = new SectorsRepository(_helmesDbContext))
            {
                try
                {
                    _logger.Info($"Adding sectors to db. Sector count:");
                    var result = await _sectorsRepo.InsertSectorsAsync(sectorModel.SectorList);
                    _logger.Info("Sectors successfully added.");
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.Error("InsertSectors", ex);
                    StringBuilder message = new StringBuilder();
                    message.AppendLine(ex.Message);
                    if (ex.InnerException != null)
                        message.AppendLine(ex.InnerException.Message);
                    return BadRequest(message.ToString());
                }
            }
        }

        [HttpGet]
        [Route("api/Sectors/GetSectorList")]
        public async Task<List<Sector>> Get()
        {
            using (_sectorsRepo = new SectorsRepository(_helmesDbContext))
            {
                try
                {
                    _logger.Info("Getting sector list from database");
                    var result = await _sectorsRepo.FillSectorList();
                    return result;
                }
                catch (Exception ex)
                {
                    _logger.Error("GetSectorList", ex);
                    StringBuilder message = new StringBuilder();
                    message.AppendLine(ex.Message);
                    if (ex.InnerException != null)
                        message.AppendLine(ex.InnerException.Message);
                    return null;
                }
            }
        }
    }
}
