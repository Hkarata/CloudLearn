using CloudLearn.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CloudLearn.Shared.Services
{
    public class ApiService(HttpClient httpClient)
    {
        public async Task<HttpResponseMessage> DoSth(StudentDto message)
        {
            return await httpClient.PostAsJsonAsync<StudentDto>("https://apiservice", message);
        }
    }
}
