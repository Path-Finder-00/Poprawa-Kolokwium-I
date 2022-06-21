using KolPowt.Models.DTOs;
using KolPowt.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KolPowt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentsController : ControllerBase
    {

        private readonly IDbService _IDbService;

        public MedicamentsController(IDbService dbService)
        {
            _IDbService = dbService;
        }

        [HttpGet("{id}")]
        public IActionResult GetMedicament(int id)
        {
            var Medicament = _IDbService.GetMedicament(id);
            return Ok(Medicament);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            try
            {
                _IDbService.DeletePatient(id);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
