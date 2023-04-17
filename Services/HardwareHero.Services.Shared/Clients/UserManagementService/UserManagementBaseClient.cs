﻿using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Options;
using HardwareHero.Services.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace HardwareHero.Services.Shared.Clients.UserManagementService
{
    public class UserManagementBaseClient : IDisposable
    {
        public HttpClient HttpClient { get; init; }

        public UserManagementBaseClient(HttpClient client, IOptions<ServiceAddressOptions> options)
        {
            HttpClient = client;
            HttpClient.BaseAddress = new Uri(options.Value.UserManagementService);
        }

        protected async Task<IdentityResult> SendPostRequest<TRequest>(TRequest request, string path)
        {
            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var requestResult = await HttpClient.PostAsync(path, httpContent);

            IdentityResult result;

            if (requestResult.IsSuccessStatusCode)
            {
                var responseJson = await requestResult.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<IdentityResultDto>(responseJson);
                result = HandleResponse(response);
            }
            else
            {
                result = IdentityResult.Failed(
                    new IdentityError()
                    {
                        Code = requestResult.StatusCode.ToString(),
                        Description = requestResult.ReasonPhrase
                    }
                );
            }

            return result;
        }

        protected async Task<UserManagementServiceResponse<TResult>> SendGetRequest<TResult>(string request)
        {
            var requestResult = await HttpClient.GetAsync(request);

            UserManagementServiceResponse<TResult> result;

            if (requestResult.IsSuccessStatusCode)
            {
                var response = await requestResult.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(response))
                {
                    result = new UserManagementServiceResponse<TResult>()
                    {
                        Code = requestResult.StatusCode.ToString(),
                        Description = requestResult.ReasonPhrase
                    };
                }
                else
                {
                    var payload = JsonConvert.DeserializeObject<TResult>(response);
                    result = new UserManagementServiceResponse<TResult>()
                    {
                        Code = requestResult.StatusCode.ToString(),
                        Description = requestResult.ReasonPhrase,
                        Payload = payload
                    };
                }
            }
            else
            {
                result = new UserManagementServiceResponse<TResult>()
                {
                    Code = requestResult.StatusCode.ToString(),
                    Description = requestResult.ReasonPhrase
                };
            }

            return result;
        }

        protected async Task<IdentityResult> SendDeleteRequest(string property, string path)
        {
            var requestResult = await HttpClient.DeleteAsync(path + property);

            IdentityResult result;

            if (requestResult.IsSuccessStatusCode)
            {
                var response = await requestResult.Content.ReadAsStringAsync();
                result = IdentityResult.Success;
            }
            else
            {
                result = IdentityResult.Failed(
                    new IdentityError()
                    {
                        Code = requestResult.StatusCode.ToString(),
                        Description = requestResult.ReasonPhrase
                    }
                );
            }

            return result;
        }

        protected async Task<IdentityResult> SendPutRequest<TRequest>(TRequest request, string path)
        {
            var jsonContent = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var requestResult = await HttpClient.PutAsync(path, httpContent);

            IdentityResult result;

            if (requestResult.IsSuccessStatusCode)
            {
                var response = await requestResult.Content.ReadAsStringAsync();
                result = IdentityResult.Success;
            }
            else
            {
                result = IdentityResult.Failed(
                    new IdentityError()
                    {
                        Code = requestResult.StatusCode.ToString(),
                        Description = requestResult.ReasonPhrase
                    }
                );
            }

            return result;
        }
        public void Dispose()
        {
            HttpClient.Dispose();
        }

        private IdentityResult HandleResponse(IdentityResultDto response)
        {
            if (response.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(response.Errors.ToArray());
            }
        }
    }
}
