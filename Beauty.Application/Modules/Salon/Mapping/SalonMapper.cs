using Beauty.Application.Modules.Salon.ViewModel;
using Beauty.Model.Salon;
using Common.Crosscutting.Utility;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Beauty.Application.Modules.Salon.Mapping
{
    public static class SalonMapper
    {
        public static SalonViewModel ToViewModel(this SalonModel salon)
        {
            return new SalonViewModel
            {
                Id = salon.Id,
                Logo = salon.Logo,
                Title = salon.Title,
                About = salon.About,
                Address = salon.Address,
                EstablishYear = salon.EstablishYear.ToString(),
                AppointmentPrePayment = salon.AppointmentPrePayment,
                AppointmentRemindingSmsSendTime = salon.AppointmentRemindingSmsSendTime.ToString(),
                OvertimePay = salon.OvertimePay.ToString(TypeUtility.CommaFormatted),
                AppointmentPrePaymentPerecnt = salon.AppointmentPrePaymentPerecnt.ToString(),
                DefaultPersonnelServicePerecnt = salon.DefaultPersonnelServicePerecnt.ToString(),
                PersonnelToPersonnelSalePerecnt = salon.PersonnelToPersonnelSalePerecnt.ToString(),
                OpeningTime = salon.OpeningTime.ToString(TypeUtility.HoursAndMinutes),
                ClosingTime = salon.ClosingTime.ToString(TypeUtility.HoursAndMinutes),
                Contacts = salon.Contacts.ToFindViewModel()
            };
        }
        public static SalonModel ToCreateModel(this SalonViewModel salon, IHttpContextAccessor httpContextAccessor)
        {
            return new SalonModel
            {
                Id = salon.Id,
                Logo = salon.Logo,
                Title = salon.Title,
                About = salon.About,
                Address = salon.Address,
                ClosingTime = salon.ClosingTime.StringToTimeSpan(),
                OpeningTime = salon.OpeningTime.StringToTimeSpan(),
                EstablishYear = int.TryParse(salon.EstablishYear, out int _) ? int.Parse(salon.EstablishYear) : 0,
                Contacts = salon.Contacts.ToCreateModel(),
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }

        public static SalonModel ToUpdateModel(this SalonViewModel salon, SalonModel model)
        {
            model.ClosingTime = salon.ClosingTime.StringToTimeSpan();
            model.OpeningTime = salon.OpeningTime.StringToTimeSpan();
            model.About = salon.About;
            model.Address = salon.Address;
            model.Title = salon.Title;
            model.EstablishYear = int.TryParse(salon.EstablishYear, out int _) ? int.Parse(salon.EstablishYear) : 0;
            model.Logo = salon.Logo is null ? model.Logo : salon.Logo;

            foreach (var c in salon.Contacts.Where(c => !string.IsNullOrEmpty(c.Value)))
            {
                if (model.Contacts.Any(x => x.Type == c.Type))
                {
                    model.Contacts.FirstOrDefault(x => x.Type == c.Type).Value = c.Value;
                    model.Contacts.FirstOrDefault(x => x.Type == c.Type).IsActive = c.IsActive;
                }
                else
                    model.Contacts.Add(new SalonContactModel { Type = c.Type, Value = c.Value });
            }

            return model;
        }
    }
}
