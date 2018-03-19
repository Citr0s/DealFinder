using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DealFinder.Api.Controllers.Deploy
{
    [Route("api/[controller]")]
    public class DeployController : Controller
    {
        [HttpPost("")]
        public ActionResult Get()
        {
            return Ok("cd .. && sudo bash deploy.sh > stdout.txt 2> stderr.txt &".Bash());
        }
    }
    
    public static class ShellHelper
    {
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}