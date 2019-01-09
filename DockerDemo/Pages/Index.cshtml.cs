using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DockerDemo.Models;
using Microsoft.Extensions.Configuration;

namespace DockerDemo.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(IRepository repo, IConfiguration config)
        {
            Repository = repo;
            Message = config["MESSAGE"] ?? ("Essential Docker " + config["HOSTNAME"] ?? "").Trim();
        }

        public string Message { get; }
        public IRepository Repository { get; }

        public void OnGet()
        {

        }
    }
}