using eShop.Database;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BaseController : Controller
	{
		protected readonly AppDbContext _db;
		public BaseController(AppDbContext db)
		{
			_db = db;
		}
	}
}
