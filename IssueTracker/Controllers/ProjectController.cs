using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IRepositoryManager _context;
        public ProjectController(IRepositoryManager context)
        {
            _context = context;
        }
    }
}
