using System;
using System.Collections.Generic;
using System.Text;

namespace ProntuarioApp.Client.Models
{
    public class InvalidApiResult<T> : BaseApiResult<T>
    {
        public InvalidApiResult()
            : base()
        {
            Success = false;
        }

        public InvalidApiResult(Exception ex)
            : base()
        {
            Success = false;
            Message = ex.Message;
        }
    }
}
