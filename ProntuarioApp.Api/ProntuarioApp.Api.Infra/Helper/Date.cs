using System;


namespace ProntuarioApp.Api.Helper
{
    public static class DateConverter
    { 
        public static int toTimestamp(DateTime convertingDate){
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0); // to convert to timestamp
            return (int)Math.Floor((convertingDate - origin).TotalSeconds);
        }
    }
}