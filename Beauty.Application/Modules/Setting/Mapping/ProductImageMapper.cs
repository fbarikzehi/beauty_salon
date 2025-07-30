using Beauty.Application.Modules.Setting.ViewModel;
using Beauty.Model.Product;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Setting.Mapping
{
    public static class ProductImageMapper
    {
        public static List<ProductImageModel> ToCreateModel(this List<ProductImageViewModel> productImages)
        {
            return productImages.Select(x => new ProductImageModel
            {
                ServerPath = x.ServerPath,
            }).ToList();
        }

        public static List<ProductImageModel> ToUpdateModel(this List<ProductImageViewModel> productImages)
        {
            return productImages.Select(x => new ProductImageModel
            {
                ServerPath = x.ServerPath,
            }).ToList();
        }

        public static List<ProductImageViewModel> ToFindAllViewModel(this ICollection<ProductImageModel> model)
        {
            return model.Select(x => new ProductImageViewModel
            {
                Id = x.Id,
                ServerPath = x.ServerPath,
            }).ToList();
        }
    }
}
