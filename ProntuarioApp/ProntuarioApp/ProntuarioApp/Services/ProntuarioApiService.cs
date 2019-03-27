using System;
using System.Collections.Generic;
using System.Text;
using ProntuarioApp.Client;

namespace ProntuarioApp.Services
{
    public sealed class ProntuarioApiService 
    {
        private static volatile IPacienteApiService _instance;
        private static object syncRoot = new object();

        public static IPacienteApiService Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        _instance = new PacienteApiService(); // You can use Dependency Injection
                    }
                }

                return _instance;
            }
        }
    }
}
