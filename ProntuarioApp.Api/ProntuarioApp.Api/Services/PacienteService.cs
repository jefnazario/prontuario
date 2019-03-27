using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProntuarioApp.Api.Infra.Models;
using ProntuarioApp.Api.Infra.Repositories;

namespace ProntuarioApp.Api.Services
{
    public class PacienteService
    {
        private readonly PacienteRepository pacienteRepository;

        public PacienteService(PacienteRepository pacienteRepository)
        {
            this.pacienteRepository = pacienteRepository;
        }

        public List<Paciente> Buscar()
        {
            return pacienteRepository.Buscar();
        }

    }
}
