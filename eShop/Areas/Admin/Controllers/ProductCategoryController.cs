using eShop.Database;
using eShop.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Areas.Admin.Controllers
{
	public class ProductCategoryController : BaseController
	{
		public ProductCategoryController(AppDbContext db) : base(db)
		{
		}

		public async Task<IActionResult> Index()
		{
			var data = await _db.ProductCategories.ToListAsync();
			return View(data);
		}
		
		public async Task<IActionResult> CreateOrUpdate(int? id = 0)
		{
			var title = "Thêm";

			if(id != 0)
			{
				title = "Sửa";
				var data = await _db.ProductCategories.FindAsync(id);
				ViewBag.Title = title;
				return View(data);
			}
			ViewBag.Title = title;

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(ProductCategory model)
		{
			if(model == null)
			{
				return View();
			}
			if(model.Id > 0)
			{
				var data = await _db.ProductCategories.FindAsync(model.Id);
				if(data == null)
				{
					return RedirectToAction(nameof(Index));
				}

				data.UpdatedAt = DateTime.Now;
				data.Name = model.Name;
				_db.ProductCategories.Update(data);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			model.CreatedAt = DateTime.Now;
			await _db.ProductCategories.AddAsync(model);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if(id > 0)
			{
				var data = await _db.ProductCategories.FindAsync(id);
				if(data != null)
				{
					_db.ProductCategories.Remove(data);
					await _db.SaveChangesAsync();
				}
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
