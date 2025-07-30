using Beauty.Application.Modules.Service.ViewModel;
using Beauty.Model.Service;
using Common.Crosscutting.Utility;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Service.Mapping
{
    public static class ServiceMapper
    {
        public static ServiceModel ToCreateModel(this ServiceCreateViewModel service, IHttpContextAccessor httpContextAccessor)
        {
            return new ServiceModel
            {
                Title = service.Title,
                DurationMinutes = short.TryParse(service.DurationMinutes, out short _) ? short.Parse(service.DurationMinutes) : short.Parse("0"),
                IsActive = service.IsActive,
                Score = int.TryParse(service.Score, out int _) ? int.Parse(service.Score) : 0,
                TakeItFreeCount = int.TryParse(service.TakeItFreeCount, out int _) ? int.Parse(service.TakeItFreeCount) : 0,
                Prepayment = service.Prepayment.ToFloatPrice(),
                LineId = service.LineId,
                Prices = service.Details.Count > 0 ?
                    new List<ServicePriceModel> { new ServicePriceModel { Price = 0 } } :
                    new List<ServicePriceModel> { new ServicePriceModel { Price = service.Price.ToFloatPrice() } },
                Details = service.Details.Where(x => !string.IsNullOrEmpty(x.Title)).Select(x => new ServiceDetailModel
                {
                    Title = x.Title,
                    Price = x.Price.ToFloatPrice()
                }).ToList(),
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }

        public static ServiceModel ToUpdateModel(this ServiceModel model, ServiceUpdateViewModel service)
        {
            model.Title = service.Title;
            model.Score = int.TryParse(service.Score, out int _) ? int.Parse(service.Score) : model.Score;
            model.TakeItFreeCount = int.TryParse(service.TakeItFreeCount, out int _) ? int.Parse(service.TakeItFreeCount) : model.TakeItFreeCount;
            model.Prepayment = service.Prepayment.ToFloatPrice();
            model.DurationMinutes = short.TryParse(service.DurationMinutes, out short _) ? short.Parse(service.DurationMinutes) : model.DurationMinutes;
            model.LineId = service.LineId;

            foreach (var detail in service.Details)
            {
                if (detail.Id == 0 && !string.IsNullOrEmpty(detail.Title))
                    model.Details.Add(new ServiceDetailModel
                    {
                        Title = detail.Title,
                        Price = detail.Price.ToFloatPrice()
                    });
                else if (detail.Id != 0 && !string.IsNullOrEmpty(detail.Title))
                {
                    model.Details.FirstOrDefault(x => x.Id == detail.Id).Title = detail.Title;
                    model.Details.FirstOrDefault(x => x.Id == detail.Id).Price = detail.Price.ToFloatPrice();
                }
                else if (detail.Id != 0 && string.IsNullOrEmpty(detail.Title))
                    model.Details.FirstOrDefault(x => x.Id == detail.Id).ReverseDelete();

            }

            if (!string.IsNullOrWhiteSpace(service.Price) && model.Prices.Any(x => x.ToDateTime == null) && (service.Details == null || service.Details.Where(x => !string.IsNullOrEmpty(x.Title)).Count() == 0))
            {
                model.Prices.FirstOrDefault(x => x.ToDateTime == null).ToDateTime = DateTime.Now;
                model.Prices.Add(new ServicePriceModel { Price = service.Price.ToFloatPrice() });
            }
            else if (!string.IsNullOrWhiteSpace(service.Price) && !model.Prices.Any(x => x.ToDateTime == null) && (service.Details == null || service.Details.Where(x => !string.IsNullOrEmpty(x.Title)).Count() == 0))
            {
                model.Prices.Add(new ServicePriceModel { Price = service.Price.ToFloatPrice() });
            }
            else if ((string.IsNullOrWhiteSpace(service.Price) || service.Price.ToFloatPrice() == 0) && model.Prices.Any(x => x.ToDateTime == null) && (service.Details != null && service.Details.Count > 0))
            {
                model.Prices.FirstOrDefault(x => x.ToDateTime == null).ToDateTime = DateTime.Now;
            }

            return model;
        }

        public static List<ServiceDataTableViewModel> ToDataTableViewModel(this IQueryable<ServiceModel> services)
        {
            return services.Select(service => new ServiceDataTableViewModel
            {
                Id = service.Id,
                Title = service.Title,
                LineId = service.LineId,
                LineTitle = service.Line.Title,
                CurrentPrice = service.Prices.OrderByDescending(x => x.Id).FirstOrDefault().Price.ToString(TypeUtility.CommaFormatted),
                DurationMinutes = service.DurationMinutes.ToString(),
                Score = service.Score.ToString(),
                Prepayment = service.Prepayment.ToString(TypeUtility.CommaFormatted),
                TakeItFreeCount = service.TakeItFreeCount.ToString(),
                IsActive = service.IsActive,
                CustomerCount = 0,
                Rate = service.ServiceRatings.Sum(x => x.Rate) > 0 ? (service.ServiceRatings.Sum(x => x.Rate) / service.ServiceRatings.Count) : 0,
                Prices = service.Prices.OrderByDescending(x => x.Id).Select(x => x.Price.ToString(TypeUtility.CommaFormatted)).ToList(),
                Details = service.Details.Select(x => new ServiceDetailViewModel { Id = x.Id, Price = x.Price.ToString(TypeUtility.CommaFormatted), Title = x.Title }).ToList(),
            }).ToList();
        }

        public static List<ServiceFindAllViewModel> ToFindAllViewModel(this IQueryable<ServiceModel> services)
        {
            return services.Select(s => new ServiceFindAllViewModel
            {
                Id = s.Id,
                Title = s.Title,
            }).ToList();
        }

        public static ServiceViewModel ToFindByIdViewModel(this ServiceModel service)
        {
            return new ServiceViewModel
            {
                Id = service.Id,
                Title = service.Title,
                LineId = service.LineId,
                LineTitle = service.Line.Title,
                DurationMinutes = service.DurationMinutes.ToString(),
                Score = service.Score.ToString(),
                TakeItFreeCount = service.TakeItFreeCount.ToString(),
                Prepayment = service.Prepayment.ToString(TypeUtility.CommaFormatted),
                Details = service.Details.Select(x => new ServiceDetailViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price=x.Price.ToString(TypeUtility.CommaFormatted),
                    ServiceId = x.ServiceId
                }).ToList(),
                CurrentPrice = service.Prices.FirstOrDefault(x => x.ToDateTime == null)?.Price.ToString(TypeUtility.CommaFormatted),
                IsActive = service.IsActive,
            };
        }

        public static List<ServiceSearchViewModel> ToFindAllBySearchViewModel(this IEnumerable<ServiceModel> services)
        {
            if (services is null || services.Count() == 0) return new List<ServiceSearchViewModel>();

            return services.Select(service => new ServiceSearchViewModel
            {
                Id = service.Id,
                Title = service.Title,
                DurationMinutes = service.DurationMinutes.ToString(),
                Prepayment = service.Prepayment.ToString(TypeUtility.CommaFormatted),
                Price = service.Prices.FirstOrDefault(x => x.ToDateTime == null)?.Price.ToString(TypeUtility.CommaFormatted),
            }).ToList();
        }
        public static List<ServiceSearchViewModel> ToSearchAllByNameViewModel(this IEnumerable<ServiceModel> services)
        {
            if (services is null || services.Count() == 0) return new List<ServiceSearchViewModel>();

            return services.Select(service => new ServiceSearchViewModel
            {
                Id = service.Id,
                Title = service.Title,
                Price = service.Prices.FirstOrDefault(x => x.ToDateTime == null)?.Price.ToString(TypeUtility.CommaFormatted),
            }).ToList();
        }
    }
}
