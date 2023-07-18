using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Services;

public interface IChatBotService
{
    public Task<string> GetResponse(string message, string model = "gpt-3.5-turbo");
}
