using AutoMapper;
using ItServiceApp.Data;
using ItServiceApp.Models.Entities;
using ItServiceApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ItServiceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _dbContext;
        private readonly IMapper _mapper;

        public HomeController(MyContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = _dbContext.SubscriptionTypes.OrderBy(x => x.Price).ToList()
                        .Select(x => _mapper.Map<SubscriptionTypeViewModel>(x)).ToList();
            return View(model);
        }
    }
}
