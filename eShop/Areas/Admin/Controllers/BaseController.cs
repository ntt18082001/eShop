using AutoMapper;
using eShop.Database;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BaseController : Controller
	{
		protected readonly AppDbContext _db;
		protected readonly IMapper _mapper;
		public BaseController(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}
	}
}
