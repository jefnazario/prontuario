using System;
using System.Collections.Generic;
using System.Text;

namespace ProntuarioApp.Client.Models
{
    public sealed class ApiResult<TModel>
    {
        public bool Success { get; set; }
        public TModel Data { get; set; }
    }
}
