using System;
using System.Collections.Generic;
using System.Text;

namespace ProntuarioApp.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public double Idade { get; set; }
    }
}
