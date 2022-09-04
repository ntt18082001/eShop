using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Areas.Admin.ViewModels.ProductCategory;
using eShop.Database;
using eShop.Database.Entities;
using eShop.WebConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eShop.Areas.Admin.Controllers
{
	public class ProductCategoryController : BaseController
	{
		public ProductCategoryController(AppDbContext db, IMapper mapper) : base(db, mapper)
		{
		}

		public async Task<IActionResult> Index()
		{
			var data = await _db.ProductCategories
				.ProjectTo<ListItemCategoryVM>(AutoMapperProfile.categoryConf)
				.OrderByDescending(x => x.Id)
				.ToListAsync();
			return View(data);
		}

		public async Task<IActionResult> CreateOrUpdate(int? id = 0)
		{
			var title = "Thêm";
			if (id != 0)
			{
				title = "Sửa";
				var category = await _db.ProductCategories.FindAsync(id);
				if (category == null)
				{
					return RedirectToAction(nameof(Index));
				}
				var data = _mapper.Map<AddOrUpdateCategoryVM>(category);
				ViewBag.Title = title;
				return View(data);
			}
			ViewBag.Title = title;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateOrUpdate(AddOrUpdateCategoryVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			if (model.Id > 0)
			{
				var data = await _db.ProductCategories.FindAsync(model.Id);
				if (data == null)
				{
					return RedirectToAction(nameof(Index));
				}

				_mapper.Map<AddOrUpdateCategoryVM, ProductCategory>(model, data);
				data.UpdatedAt = DateTime.Now;
				_db.ProductCategories.Update(data);
				await _db.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			var category = _mapper.Map<ProductCategory>(model);
			category.UpdatedAt = DateTime.Now;
			category.CreatedAt = DateTime.Now;
			await _db.ProductCategories.AddAsync(category);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == 0)
			{
				return RedirectToAction(nameof(Index));
			}
			var data = await _db.ProductCategories.FindAsync(id);
			if (data == null)
			{
				return RedirectToAction(nameof(Index));
			}
			if (await _db.Products.AnyAsync(x => x.CategoryId == id))
			{
				TempData["Error"] = "Danh mục có chứa sản phẩm, không thể xóa!";
				return RedirectToAction(nameof(Index));
			}

			_db.ProductCategories.Remove(data);
			await _db.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
