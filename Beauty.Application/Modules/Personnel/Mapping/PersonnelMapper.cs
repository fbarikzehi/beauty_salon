using Beauty.Application.Modules.Account.User.Mapping;
using Beauty.Application.Modules.Personnel.ViewModel;
using Beauty.Model.Personnel;
using Beauty.Model.Salon;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Extensions;
using Common.Crosscutting.Utility;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Personnel.Mapping
{
    public static class PersonnelMapper
    {
        public static PersonnelProfileModel ToCreateModel(this PersonnelProfileModifyViewModel vm, IHttpContextAccessor httpContextAccessor, ushort code)
        {
            return new PersonnelProfileModel
            {
                Address = vm.Address,
                Birthdate = vm.Birthdate.PersianDateStringToDateTime(),
                Code = code,
                //CooperationType = vm.CooperationType,
                JobStart = vm.JobStart.PersianDateStringToDateTime(),
                LastName = vm.LastName,
                Name = vm.Name,
                Avatar = vm.Avatar,
                //Salary = float.Parse(vm.Salary),
                //Services = vm.Services.ToCreateModel(),
                Contacts = vm.Contacts.Where(c => !string.IsNullOrEmpty(c.Value)).ToList().ToCreateModel(),
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }

        public static PersonnelProfileModel ToUpdateModel(this PersonnelProfileModifyViewModel vm, PersonnelProfileModel model, IHttpContextAccessor httpContextAccessor)
        {
            model.Address = vm.Address;
            model.Birthdate = vm.Birthdate.PersianDateStringToDateTime();
            model.JobStart = vm.JobStart.PersianDateStringToDateTime();
            model.JobEnd = vm.JobEnd.PersianDateStringToAllowNullDateTime();
            model.LastName = vm.LastName;
            model.Name = vm.Name;
            model.Avatar = vm.FileAvatar is null ? model.Avatar : vm.Avatar;
            foreach (var c in vm.Contacts.Where(c => !string.IsNullOrEmpty(c.Value)))
            {
                if (model.Contacts.Any(x => x.Type == c.Type))
                {
                    model.Contacts.FirstOrDefault(x => x.Type == c.Type).Value = c.Value;
                    model.Contacts.FirstOrDefault(x => x.Type == c.Type).IsActive = c.IsActive;
                }
                else
                    model.Contacts.Add(new PersonnelContactModel { Type = c.Type, Value = c.Value });
            }
            return model;
        }

        public static PersonnelProfileModifyViewModel ToFindByIdModifyViewModel(this PersonnelProfileModel m)
        {
            return new PersonnelProfileModifyViewModel
            {
                Id = m.Id,
                Avatar = m.Avatar,
                Name = m.Name,
                LastName = m.LastName,
                Birthdate = m.Birthdate.ToPersianDate(),
                JobStart = m.JobStart.ToPersianDate(),
                JobEnd = m.JobEnd.ToShortPersianDateTimeNullable(null),
                Address = m.Address,
                Contacts = m.Contacts.ToFindViewModel(),
                Code = m.Code.ToString(),
            };
        }

        public static PersonnelProfileViewModel ToFindByIdModel(this PersonnelProfileModel m)
        {
            var totalIncome = 0;

            ////


            return new PersonnelProfileViewModel
            {
                Id = m.Id,
                Code = m.Code,
                Avatar = m.Avatar,
                Name = m.Name,
                LastName = m.LastName,
                Birthdate = m.Birthdate.ToPersianDate(),
                JobStart = m.JobStart.ToPersianDate(),
                JobEnd = m.JobEnd.ToShortPersianDateTimeNullable(null),
                Address = m.Address,
                CooperationType = m.CooperationType,
                CooperationTitle = EnumExtensions<CooperationTypeEnum>.GetPersianName(m.CooperationType),
                Salary = m.Salary.ToString(TypeUtility.CommaFormatted),
                Income = totalIncome.ToString(TypeUtility.CommaFormatted),
                Contacts = m.Contacts.ToFindViewModel(),
                Lines = m.LinePersonnels?.Select(x => new PersonnelLineViewModel { Id = x.Id, Title = x.Line.Title, LineId = x.Line.Id }).ToList(),
                User = m.User.ToFindViewModel(),
            };
        }

        public static List<PersonnelPercentageServiceItemsViewModel> ToFindAllPercentageServiceModel(this ICollection<PersonnelServiceModel> m)
        {
            return m.Select(x => new PersonnelPercentageServiceItemsViewModel
            {
                PersonnelServiceId = x.Id,
                Percentage = x.Percentage,
                ServiceTitle = x.Service.Title
            }).ToList();
        }

        public static List<PersonnelDataTableViewModel> ToFindAllPercentageServiceModel(this IQueryable<PersonnelProfileModel> personnels)
        {
            var list = personnels.Select(x => new PersonnelDataTableViewModel
            {
                Id = x.Id,
                Code = x.Code,
                Avatar = x.Avatar,
                //Name = x.Name,
                //LastName = x.LastName,
                Username = x.User == null ? string.Empty : x.User.Username,
                CooperationType = EnumExtensions<CooperationTypeEnum>.GetPersianName(x.CooperationType),
                Mobile = x.Contacts.Count > 0 &&
                          x.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile) != null ?
                          x.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile).Value : "",
            }).ToList();

            return list;
        }

        public static List<PersonnelWorkingTimeViewModel> ToFindAllUpdateWorkingViewModel(this ICollection<PersonnelWorkingTimeModel> workingTimes)
        {
            if (workingTimes is null) return new List<PersonnelWorkingTimeViewModel>();

            return workingTimes.Select(x => new PersonnelWorkingTimeViewModel
            {
                Id = x.Id,
                PersonnelProfileId = x.PersonnelProfileId,
                Date = x.Date.ToPersianDate(),
                FromTime = x.FromTime.ToString(TypeUtility.HoursAndMinutes),
                ToTime = x.ToTime.ToString(TypeUtility.HoursAndMinutes),
                Selected = true
            }).ToList();
        }

        public static PersonnelWorkingTimeModel ToCreateModel(this PersonnelWorkingTimeViewModel vm, SalonModel salon)
        {
            return new PersonnelWorkingTimeModel
            {
                Date = vm.Date.PersianDateStringToDateTime(),
                FromTime = salon.OpeningTime,
                ToTime = salon.ClosingTime,
                PersonnelProfileId = vm.PersonnelProfileId,
            };
        }

        public static List<PersonnelDataTableViewModel> ToDataTableViewModel(this IQueryable<PersonnelProfileModel> personnelProfiles)
        {
            return personnelProfiles.Select(personnelProfile => new PersonnelDataTableViewModel
            {
                Id = personnelProfile.Id,
                FullName = $"{personnelProfile.Name} {personnelProfile.LastName}",
                Code = personnelProfile.Code,
                Username = personnelProfile.User == null ? string.Empty : personnelProfile.User.Username,
                CooperationType = EnumExtensions<CooperationTypeEnum>.GetPersianName(personnelProfile.CooperationType),
                Mobile = personnelProfile.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile && x.IsActive) == null ? string.Empty :
                                               personnelProfile.Contacts.FirstOrDefault(x => x.Type == ContactTypeEnum.Mobile && x.IsActive).Value,
            }).ToList();
        }
    }
}
