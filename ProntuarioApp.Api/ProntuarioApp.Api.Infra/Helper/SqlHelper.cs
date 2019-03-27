using System;
using System.Collections.Generic;

namespace ProntuarioAppAPI.Infra.Helper
{
    public class SqlHelper
    {
        public FormattableString Where { get; private set; }
        public FormattableString OrderBy { get; private set; }
        public IDictionary<string, object> Parameters { get; }

        public SqlHelper()
        {
            Where = $"";
            OrderBy = $"";
            Parameters = new Dictionary<string, object>();
        }

        public void WhereAndLike(string valor, string campo)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                if (Where.ArgumentCount > 0)
                {
                    Where = $"{Where} AND ";
                }

                Where = $"{Where} {campo:C} Like @{campo}";
                Parameters.Add(campo, $"%{valor}%");
            }
        }

        public void WhereOrLike(string valor, string campo)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                if (Where.ArgumentCount > 0)
                {
                    Where = $"{Where} OR ";
                }

                Where = $"{Where} {campo:C} Like @{campo}";
                Parameters.Add(campo, $"%{valor}%");
            }
        }

        public void WhereAnd(string valor, string campo)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                if (Where.ArgumentCount > 0)
                {
                    Where = $"{Where} And ";
                }

                Where = $"{Where} {campo:C} = @{campo}";
                Parameters.Add(campo, valor);
            }
        }

        public void WhereAnd(int? valor, string campo)
        {
            if (valor.HasValue)
            {
                if (Where.ArgumentCount > 0)
                {
                    Where = $"{Where} And ";
                }

                Where = $"{Where} {campo:C} = @{campo}";
                Parameters.Add(campo, valor);
            }
        }

        public void OrderByDesc(string campo)
        {
            OrderBy = $"{campo:C} DESC";
        }

        public void OrderByAsc(string campo)
        {
            OrderBy = $"{campo:C}";
        }
    }
}