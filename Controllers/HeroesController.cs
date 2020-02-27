using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HeroesAPI.Models;


namespace HeroesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroesController : ControllerBase
    {
        private readonly ILogger<HeroesController> _logger;
        private readonly TestDAASContext _context;

        public HeroesController(ILogger<HeroesController> logger, TestDAASContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Heroes> Get()
        {
            return _context.Heroes.ToList();
        }

        public Heroes Get(int id)
        {
            return _context.Heroes.Find(id);
        }
    }
}
