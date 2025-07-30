using Beauty.Application.Modules.Customer.ViewModel;
using Beauty.Model.Customer;
using Common.Crosscutting.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beauty.Application.Modules.Customer.Mapping
{
    public static class CustomerChequeMapper
    {
        public static CustomerChequeModel ToCreateModel(this CustomerChequeViewModel cheque)
        {
            return new CustomerChequeModel
            {
                BankId = cheque.BankId,
                CreateDateTime = !string.IsNullOrEmpty(cheque.CreateDateTime) ? cheque.CreateDateTime.PersianDateStringToDateTime() : DateTime.Now,
                CustomerProfileId = cheque.CustomerProfileId,
                Date = cheque.Date.PersianDateStringToDateTime(),
                Details = cheque.Details,
                Fee = cheque.Fee.ToFloatPrice(),
                Number = cheque.Number,
            };
        }

        public static List<CustomerChequeViewModel> ToFindViewModel(this IQueryable<CustomerChequeModel> cheques)
        {
            return cheques.Select(cheque => new CustomerChequeViewModel
            {
                Id = cheque.Id,
                Number = cheque.Number,
                BankId = cheque.BankId,
                BankName = cheque.Bank.Title,
                CreateDateTime = cheque.CreateDateTime.ToPersianDate(),
                CustomerProfileId = cheque.CustomerProfileId,
                Date = cheque.Date.ToPersianDate(),
                Details = cheque.Details,
                Fee = cheque.Fee.ToString(TypeUtility.CommaFormatted),
            }).ToList();
        }

    }
}
