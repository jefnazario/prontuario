using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using ProntuarioApp.Api.Infra.Models;
using ProntuarioAppAPI.Infra.Configurations;
using ProntuarioAppAPI.Infra.Repositories;

namespace ProntuarioApp.Api.Infra.Repositories
{
    public class PacienteRepository : BaseRepository
    {
        public PacienteRepository() 
        {
        }

        public List<Paciente> Buscar(string nome)
        {
            var sql = $@"SELECT CD_PACIENTE as ID,
	                           NM_PACIENTE as Name,
                               SEXO as Sexo,
                               DT_NASCIMENTO as DataNascimento,
                               Idade
                               
                        FROM prontuario_unificado.PACIENTE";

            return ExecuteQueryList<Paciente>(sql).ToList();
        }
    }
}
