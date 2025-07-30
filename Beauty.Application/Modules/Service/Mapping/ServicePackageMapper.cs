
using Beauty.Application.Modules.Service.ViewModel;
using Beauty.Model.Service;
using Common.Crosscutting.Utility;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Service.Mapping
{
    public static class ServicePackageMapper
    {
        public static ServicePackageModel ToCreateModel(this ServicePackageViewModel servicePackage, IHttpContextAccessor httpContextAccessor)
        {
            return new ServicePackageModel
            {
                Title = servicePackage.Title,
                From = servicePackage.From.PersianDateStringToAllowNullDateTime(),
                To = servicePackage.To.PersianDateStringToAllowNullDateTime(),
                ExtendTo = servicePackage.ExtendTo.PersianDateStringToAllowNullDateTime(),
                DiscountPrice = servicePackage.DiscountPrice.ToFloatPrice(),
                Items = servicePackage.Items.Where(x => x.ServiceId > 0).Select(x => new ServicePackageItemModel
                {
                    ServiceId = x.ServiceId,
                    AfterDiscountPrice = x.AfterDiscountPrice.ToFloatPrice()
                }).ToList(),
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }

        public static ServicePackageModel ToUpdateModel(this ServicePackageModel model, ServicePackageViewModel servicePackage)
        {
            model.Title = servicePackage.Title;
            model.From = servicePackage.From.PersianDateStringToAllowNullDateTime();
            model.To = servicePackage.To.PersianDateStringToAllowNullDateTime();
            model.ExtendTo = servicePackage.ExtendTo.PersianDateStringToAllowNullDateTime();
            model.DiscountPrice = servicePackage.DiscountPrice.ToFloatPrice();

            //Not deleted
            foreach (var item in servicePackage.Items.Where(x => x.ServiceId > 0))
            {
                //New
                if (item.Id == 0)
                {
                    model.Items.Add(new ServicePackageItemModel
                    {
                        ServiceId = item.ServiceId,
                        AfterDiscountPrice = item.AfterDiscountPrice.ToFloatPrice(),
                    });
                }
                //Update
                else
                {
                    model.Items.FirstOrDefault(x => x.Id == item.Id).AfterDiscountPrice = item.AfterDiscountPrice.ToFloatPrice();
                }
            }
            return model;
        }

        public static List<ServicePackageViewModel> ToListViewModel(this IQueryable<ServicePackageModel> servicePackages)
        {
            return servicePackages.Select(x => new ServicePackageViewModel
            {
                Id = x.Id,
                Title = x.Title,
                From = x.From.ToShortPersianDateNullable(""),
                To = x.To.ToShortPersianDateNullable(""),
                ExtendTo = x.ExtendTo.ToShortPersianDateNullable(""),
                DiscountPrice = x.DiscountPrice.ToString(TypeUtility.CommaFormatted),
                Items = x.Items.Select(i => new ServicePackageItemViewModel
                {
                    Id = i.Id,
                    AfterDiscountPrice = i.AfterDiscountPrice.ToString(TypeUtility.CommaFormatted),
                    ServicePrice = i.Service.Prices.FirstOrDefault(x => x.ToDateTime == null) != null ? i.Service.Prices.FirstOrDefault(x => x.ToDateTime == null).Price.ToString(TypeUtility.CommaFormatted) : "0",
                    ServiceId = i.ServiceId,
                    ServiceTitle = i.Service.Title,
                }).ToList(),
            }).ToList();
        }

        public static ServicePackageViewModel ToFindByIdViewModel(this ServicePackageModel servicePackage)
        {
            return new ServicePackageViewModel
            {
                Id = servicePackage.Id,
                Title = servicePackage.Title,
                From = servicePackage.From.ToShortPersianDateNullable(""),
                To = servicePackage.To.ToShortPersianDateNullable(""),
                ExtendTo = servicePackage.ExtendTo.ToShortPersianDateNullable(""),
                DiscountPrice = servicePackage.DiscountPrice.ToString(TypeUtility.CommaFormatted),
                Items = servicePackage.Items.Select(i => new ServicePackageItemViewModel
                {
                    Id = i.Id,
                    AfterDiscountPrice = i.AfterDiscountPrice.ToString(TypeUtility.CommaFormatted),
                    ServicePrice = i.Service.Prices.FirstOrDefault(x => x.ToDateTime == null) != null ? i.Service.Prices.FirstOrDefault(x => x.ToDateTime == null).Price.ToString(TypeUtility.CommaFormatted) : "0",
                    ServiceId = i.ServiceId,
                    ServiceTitle = i.Service.Title,
                }).ToList(),
            };
        }


    }
}
