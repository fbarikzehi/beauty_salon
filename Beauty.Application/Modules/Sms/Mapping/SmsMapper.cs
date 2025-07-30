using Beauty.Application.Modules.Sms.ViewModel;
using Beauty.Model.Sms;
using Common.Crosscutting.Enum;
using Common.Crosscutting.Extensions;
using Common.Crosscutting.Utility;
using Common.Security.Claim;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Sms.Mapping
{
    public static class SmsMapper
    {
        public static SmsMessageModel ToCreateModel(this SmsMessageViewModel smsMessage, IHttpContextAccessor httpContextAccessor)
        {
            return new SmsMessageModel
            {
                AllowSend = smsMessage.AllowSend,
                CreateUser = ClaimManager.GetUserId(httpContextAccessor),
            };
        }

        public static SmsMessageModel ToUpdateModel(this SmsMessageModel model, SmsMessageViewModel smsMessage)
        {
            model.Title = smsMessage.Title;
            model.Text = smsMessage.Text;
            model.AllowSend = smsMessage.AllowSend;
            model.AfterHours = smsMessage.AfterHours;
            model.BeforeHours = smsMessage.BeforeHours;
            model.ReceiverType = smsMessage.ReceiverType;
            
            return model;
        }

        public static SmsMessageViewModel ToUpdateViewModel(this SmsMessageModel smsMessage)
        {
            return new SmsMessageViewModel
            {
                Id = smsMessage.Id,
                AllowSend = smsMessage.AllowSend,
                Text = smsMessage.Text,
                Title = smsMessage.Title,
                AfterHours = smsMessage.AfterHours,
                BeforeHours = smsMessage.BeforeHours,
                IsSystemMessage = smsMessage.IsSystemMessage,
                ReceiverType = smsMessage.ReceiverType,
                SystemMessageType = smsMessage.SystemMessageType,
                Parameters = smsMessage.Parameters.Select(p => new SmsMessageParameterViewModel
                {
                    Index = p.Index,
                    Title = p.Title
                }).ToList(),
                SendSchedules = smsMessage.SendSchedules.Select(s => new SmsMessageSendScheduleViewModel
                {
                    Id = s.Id,
                    Time = s.Time.TimeSpanToString(TypeUtility.HoursAndMinutes),
                    CalendarDateId = s.CalendarDateId,
                }).ToList(),
            };
        }

        public static List<SmsMessageViewModel> ToFindAllViewModel(this IQueryable<SmsMessageModel> smsMessages)
        {
            return smsMessages.Select(x => new SmsMessageViewModel
            {
                Id = x.Id,
                AllowSend = x.AllowSend,
                Text = x.Text,
                Title = x.Title,
                AfterHours = x.AfterHours,
                BeforeHours = x.BeforeHours,
                IsSystemMessage = x.IsSystemMessage,
                ReceiverType = x.ReceiverType,
                SystemMessageType = x.SystemMessageType,
            }).ToList();
        }
    }
}
