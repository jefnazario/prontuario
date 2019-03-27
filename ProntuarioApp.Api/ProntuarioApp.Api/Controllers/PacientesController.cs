using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProntuarioApp.Api.Controllers;
using ProntuarioApp.Api.Helper;
using ProntuarioApp.Api.Models.Request;
using ProntuarioApp.Api.Services;

namespace ProntuarioApp.Api.Controllers
{
    [ApiController]
    public class PacientesController : BaseController
    {
        private readonly PacienteService pacienteService;

        public PacientesController(PacienteService pacienteService)
        {
            this.pacienteService = pacienteService;
        }

        [HttpGet("api/pacientes/buscar")]
        public async Task<JsonResult> Buscar()
        {
            var pacientes = pacienteService.Buscar();
            return Json(new ObjectReturnSuccess(1, pacientes));
        }

    }
}
