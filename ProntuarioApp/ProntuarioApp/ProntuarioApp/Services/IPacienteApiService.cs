using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProntuarioApp.Client;
using ProntuarioApp.Client.Models;
using ProntuarioApp.Models;
using ProntuarioApp.Views;

namespace ProntuarioApp.Services
{
    public interface IPacienteApiService : IApiClient
    {
        Task<BaseApiResult<List<Paciente>>> BuscarTodosPacientes();
    }
}
