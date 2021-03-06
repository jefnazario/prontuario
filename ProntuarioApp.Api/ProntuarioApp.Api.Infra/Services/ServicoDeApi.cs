﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ProntuarioAppAPI.Infra.Helper;
using Newtonsoft.Json;

namespace ProntuarioAppAPI.Infra.Services
{
    public class ServicoDeApi
    {
        public T GetData<T>(string url)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new JsonLowerCaseUnderscoreContractResolver()
            };

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result, settings);
            }
        }

        public T GetDataRaw<T>(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
        }

        public void PostData(string url, string json)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new JsonLowerCaseUnderscoreContractResolver()
            };

            using (var httpClient = new HttpClient())
            {
                httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }

        public void PostDataToken(string url, string json, string token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);

                httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }

        public T PostData<T>(string url, string json)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new JsonLowerCaseUnderscoreContractResolver()
            };

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                return JsonConvert.DeserializeObject<T>(response.Result.Content.ReadAsStringAsync().Result, settings);
            }
        }

        public T PostDataRaw<T>(string url, string json)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                return JsonConvert.DeserializeObject<T>(response.Result.Content.ReadAsStringAsync().Result);
            }
        }

        public T PostDataAuth<T>(string url, string json, string user, string pass)
        {
            using (var httpClient = new HttpClient())
            {
                var byteArray = Encoding.ASCII.GetBytes(user + ":" + pass);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var response = httpClient.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                return JsonConvert.DeserializeObject<T>(response.Result.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
