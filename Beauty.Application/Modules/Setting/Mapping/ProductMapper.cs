using Beauty.Application.Modules.Setting.ViewModel;
using Beauty.Model.Product;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Setting.Mapping
{
    public static class ProductMapper
    {
        public static ProductModel ToCreateModel(this ProductModifyViewModel product, IHttpContextAccessor httpContextAccessor)
        {
            return new ProductModel
            {
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                UnitId = product.UnitId,
                Images = product.Images?.ToCreateModel(),
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }

        public static ProductModel ToUpdateModel(this ProductModifyViewModel product, ProductModel model, IHttpContextAccessor httpContextAccessor)
        {
            model.Name = product.Name;
            model.Description = product.Description;
            model.UnitId = product.UnitId;

            return model;
        }

        public static ProductViewModel ToFindByIdModel(this ProductModel model)
        {
            return new ProductViewModel
            {
                Id = model.Id,
                Code = model.Code,
                Description = model.Description,
                Name = model.Name,
                UnitId = model.UnitId,
                Images = model.Images?.ToFindAllViewModel(),
            };
        }

        public static List<ProductDataTableViewModel> ToDataTableViewModel(this IQueryable<ProductModel> products)
        {
            return products.Select(product => new ProductDataTableViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                Unit = product.Unit.Title,
                ImageCount = product.Images.Count
            }).ToList();
        }
    }
}
