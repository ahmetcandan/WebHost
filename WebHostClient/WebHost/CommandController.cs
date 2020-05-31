using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebHostClient
{
    public class CmdController : ApiController
    {
        [HttpPost]
        public string Execute(CommandRequest request)
        {
            return run(request);
        }

        [HttpGet]
        public string Get(string id)
        {
            if (id.ToLower() == "lock")
                return Execute(new CommandRequest { Command = "rundll32.exe", Param = "user32.dll,LockWorkStation" });
            else if (id.ToLower() == "sleep")
                return Execute(new CommandRequest { Command = "rundll32.exe", Param = "powrprof.dll,SetSuspendState 0,1,0" });
            else if (id.ToLower() == "shutdown")
                return Execute(new CommandRequest { Command = "shutdown", Param = "/s /t 0" });
            else if (id.ToLower() == "restart")
                return Execute(new CommandRequest { Command = "shutdown", Param = "/r /t 0" });
            return Execute(new CommandRequest { Command = id.ToLower() });
        }

        private string run(CommandRequest request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.Command) && !string.IsNullOrEmpty(request.Param))
                    Process.Start(request.Command, request.Param);

                else if (!string.IsNullOrEmpty(request.Command))
                    Process.Start(request.Command);
                else
                    return "Empty command";
                return "Successfull command";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public class CommandRequest
    {
        public string Command { get; set; }
        public string Param { get; set; }
    }
}
