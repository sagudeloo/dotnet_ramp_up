﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace developer_todo;

/*
 * Comments:
 *  - Naming Convention
 *  - Take a look and practice SOLID (https://www.c-sharpcorner.com/UploadFile/damubetha/solid-principles-in-C-Sharp/)
 *      https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/may/csharp-best-practices-dangers-of-violating-solid-principles-in-csharp
 *      https://dotnetcoretutorials.com/2019/10/17/solid-in-c-single-responsibility-principle/
 */

class Program
{   
    static readonly HttpClient client = new HttpClient();
    static async Task Main(string[] args)
    {
        try
        {
            string? email = "";
            email = Console.ReadLine();
            JObject userData = await getUserDataByEmail(email);
            var userId = userData["id"].ToString();
            JArray todoData = await getTodoDataByUserId(userId);
            userData.Remove("company");
            userData.Add("todos", todoData);
            using (JsonWriter writer = new JsonTextWriter(new StreamWriter($"{email}.json")))  
            {  
                await userData.WriteToAsync(writer);
            }
        }
        catch (System.Exception)
        {   
            Console.WriteLine("Papi algo fallo.");
            throw;
        }

    }
    
    public static async Task<JObject> getUserDataByEmail(string email){
        using HttpResponseMessage userDataResponse = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/?email={email}");
        userDataResponse.EnsureSuccessStatusCode();
        string userDataJsonString = await userDataResponse.Content.ReadAsStringAsync();
        return (JObject)JToken.Parse(userDataJsonString)[0];
    }

    public static async Task<JArray> getTodoDataByUserId(string userId){
        using HttpResponseMessage todoDataResponse = await client.GetAsync($"https://jsonplaceholder.typicode.com/todos/?userId={userId}");
        todoDataResponse.EnsureSuccessStatusCode();
        string todoDataJsonString = await todoDataResponse.Content.ReadAsStringAsync();
        return (JArray)JToken.Parse(todoDataJsonString);
    }
}
