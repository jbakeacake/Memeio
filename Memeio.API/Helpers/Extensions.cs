using System;
using Microsoft.AspNetCore.Http;

namespace Memeio.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string msg) //use 'this HttpResponse' to overwrite our method into HttpResponse
        {
            response.Headers.Add("Application-Error", msg);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static string DetermineDate(this DateTime theDateTime)
        {
            return theDateTime.ToShortDateString();
        }
    }
}