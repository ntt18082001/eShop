using AutoMapper;
using eShop.Areas.Admin.ViewModels.ProductCategory;
using eShop.Database.Entities;

namespace eShop.WebConfig
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<ProductCategory, AddOrUpdateCategoryVM>().ReverseMap();
		}

		public static MapperConfiguration categoryConf = new(opt =>
		{
			opt.CreateMap<ProductCategory, ListItemCategoryVM>();
		});
	}
}
