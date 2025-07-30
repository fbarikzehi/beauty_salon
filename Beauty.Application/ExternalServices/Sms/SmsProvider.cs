using Kavenegar;
using Kavenegar.Core.Exceptions;
using Kavenegar.Core.Models;
using System;
using System.Collections.Generic;

namespace Beauty.Application.ExternalServices.Sms.Kavenegar
{
    public static class SmsProvider
    {
        private static KavenegarApi _api = new KavenegarApi(SmsSetting.ApiKey);
        private static string _sender = SmsSetting.SenderNumber;

        public static SendResult Send(string message, string receptor, string localid)
        {
            var sendResult = new SendResult();
            try
            {
                sendResult = _api.Send(_sender, receptor, message, localid).Result;
                //Console.Write("Messageid : " + result.Messageid + "\r\n");
                //Console.Write("Message : " + result.Message + "\r\n");
                //Console.Write("Status : " + result.Status + "\r\n");
                //Console.Write("Statustext : " + result.StatusText + "\r\n");
                //Console.Write("Receptor : " + result.Receptor + "\r\n");
                //Console.Write("Sender : " + result.Sender + "\r\n");
                //Console.Write("Date : " + result.Date + "\r\n");
                //Console.Write("Cost : " + result.Cost + "\r\n");
                //Console.Write("\r\n");

                return sendResult;
            }
            catch (ApiException ex)
            {
                sendResult.Status = 500;
                sendResult.Message = ex.Message.ToString();

                return sendResult;
            }
            catch (HttpException ex)
            {
                sendResult.Status = 404;
                sendResult.Message = ex.Message.ToString();

                return sendResult;
            }
        }

        public static SendResult Send(string message, string receptor)
        {
            var sendResult = new SendResult();
            try
            {
                sendResult = _api.Send(_sender, receptor, message).Result;
                return sendResult;
            }
            catch (ApiException ex)
            {
                sendResult.Status = 500;
                sendResult.Message = ex.Message.ToString();

                return sendResult;
            }
            catch (HttpException ex)
            {
                sendResult.Status = 404;
                sendResult.Message = ex.Message.ToString();

                return sendResult;
            }
        }

        static void VerifyLookup()
        {
            try
            {
                const string receptor = "";
                const string token = "";
                const string token2 = "";
                const string token3 = "";
                const string template = "";
                var result = _api.VerifyLookup(receptor, token, token2, token3, template).Result;
                if (result != null)
                {
                    Console.WriteLine(result.Cost + "\n");
                    Console.WriteLine(result.Date + "\n");
                    Console.WriteLine(result.Message + "\n");
                    Console.WriteLine(result.Receptor + "\n");
                    Console.WriteLine(result.Sender + "\n");
                    Console.WriteLine(result.Status + "\n");
                    Console.WriteLine(result.StatusText + "\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void AccountConfig()
        {
            try
            {
                var apilogs = "enabled";
                var dailyreport = "enabled";
                var debugmode = "enabled";
                var defaultsender = "30006703323323";
                int? mincreditalarm = 1000;
                var resendfailed = "enabled";
                var result = _api.AccountConfig(apilogs, dailyreport, debugmode, defaultsender, mincreditalarm, resendfailed).Result;
                Console.WriteLine(result.ApiLogs + "\n");
                Console.WriteLine(result.DailyReport + "\n");
                Console.WriteLine(result.DebugMode + "\n");
                Console.WriteLine(result.DefaultSender + "\n");
                Console.WriteLine(result.MinCreditAlarm + "\n");
                Console.WriteLine(result.ResendFailed + "\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Send_2()
        {
            try
            {
                var sender = "30006703323323";
                var receptors = new List<string>() { "09362985956", "09362985956" };
                const string message = "Test CSharp SDK : Send";
                const string localid = "94751643";
                var results = _api.Send(sender, receptors, message, localid).Result;
                foreach (var result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Message : " + result.Message + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.StatusText + "\r\n");
                    Console.Write("Receptor : " + result.Receptor + "\r\n");
                    Console.Write("Sender : " + result.Sender + "\r\n");
                    Console.Write("Date : " + result.Date + "\r\n");
                    Console.Write("Cost : " + result.Cost + "\r\n");
                    Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void SendArray()
        {
            try
            {
                var senders = new List<string>() { "10008284" };
                var receptors = new List<string>() { "09125089676" };
                var messages = new List<string>() { "خدمات پیام کوتاه کاوه نگار" };
                var results = _api.SendArray(senders, receptors, messages).Result;
                foreach (SendResult result in results)
                {
                    var messageids = new List<string>() { result.Messageid.ToString() };
                    Status(messageids);
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void SendArray_2()
        {
            try
            {
                var senders = new List<string> { "10008284" };
                var receptors = new List<string> { "09125089676" };
                var messages = new List<string> { "خدمات پیام کوتاه کاوه نگار" };
                string localmessageids = "94761458";
                var results = _api.SendArray(senders, receptors, messages, localmessageids).Result;
                foreach (var result in results)
                {
                    var messageids = new List<string>() { result.Messageid.ToString() };
                    Status(messageids);
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Status()
        {
            try
            {
                const string messageid = "56571749";
                var result = _api.Status(messageid).Result;
                Console.Write("Messageid : " + result.Messageid + "\r\n");
                Console.Write("Status : " + result.Status + "\r\n");
                Console.Write("Statustext : " + result.Statustext + "\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Status(List<string> messageids)
        {
            try
            {
                var results = _api.Status(messageids).Result;
                foreach (StatusResult result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.Statustext + "\r\n");
                    Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void StatusLocalMessageId()
        {
            try
            {
                var localid = "5956";
                var result = _api.StatusLocalMessageId(localid).Result;
                Console.Write("Messageid : " + result.Messageid + "\r\n");
                Console.Write("Status : " + result.Status + "\r\n");
                Console.Write("Statustext : " + result.Statustext + "\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void StatusLocalMessageId(List<string> localids)
        {
            try
            {
                var results = _api.StatusLocalMessageId(localids).Result;
                foreach (var result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.Statustext + "\r\n");
                    Console.Write("\r\n");

                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Select()
        {
            try
            {
                string messageid = "56571749";
                var result = _api.Select(messageid).Result;
                Console.Write("Messageid : " + result.Messageid + "\r\n");
                Console.Write("Message : " + result.Message + "\r\n");
                Console.Write("Status : " + result.Status + "\r\n");
                Console.Write("Statustext : " + result.StatusText + "\r\n");
                Console.Write("Receptor : " + result.Receptor + "\r\n");
                Console.Write("Sender : " + result.Sender + "\r\n");
                Console.Write("Date : " + result.Date + "\r\n");
                Console.Write("Cost : " + result.Cost + "\r\n");
                Console.Write("\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Select(List<string> messageids)
        {
            try
            {

                var results = _api.Select(messageids).Result;
                foreach (var result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Message : " + result.Message + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.StatusText + "\r\n");
                    Console.Write("Receptor : " + result.Receptor + "\r\n");
                    Console.Write("Sender : " + result.Sender + "\r\n");
                    Console.Write("Date : " + result.Date + "\r\n");
                    Console.Write("Cost : " + result.Cost + "\r\n");
                    Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void SelectOutbox()
        {
            try
            {
                var stardate = new DateTime(2015, 09, 21, 10, 11, 12);
                var enddate = DateTime.Now;
                string sender = "30006703323323";
                var results = _api.SelectOutbox(stardate, enddate, sender).Result;
                foreach (SendResult result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Message : " + result.Message + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.StatusText + "\r\n");
                    Console.Write("Receptor : " + result.Receptor + "\r\n");
                    Console.Write("Sender : " + result.Sender + "\r\n");
                    Console.Write("Date : " + result.Date + "\r\n");
                    Console.Write("Cost : " + result.Cost + "\r\n");
                    Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void LatestOutbox()
        {
            try
            {
                long pagesize = 50;
                string sender = "30006703323323";
                var results = _api.LatestOutbox(pagesize, sender).Result;
                foreach (SendResult result in results)
                {
                    Console.Write("Messageid : " + result.Messageid + "\r\n");
                    Console.Write("Message : " + result.Message + "\r\n");
                    Console.Write("Status : " + result.Status + "\r\n");
                    Console.Write("Statustext : " + result.StatusText + "\r\n");
                    Console.Write("Receptor : " + result.Receptor + "\r\n");
                    Console.Write("Sender : " + result.Sender + "\r\n");
                    Console.Write("Date : " + result.Date + "\r\n");
                    Console.Write("Cost : " + result.Cost + "\r\n");
                    Console.Write("\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void CountOutbox()
        {
            try
            {
                var stardate = new DateTime(2015, 09, 21, 10, 11, 12);
                var enddate = DateTime.Now;
                int status = 10;
                var re = _api.CountOutbox(stardate, enddate, status).Result;

                Console.Write("StartDate : " + re.StartDate);
                Console.Write("EndDate : " + re.EndDate);
                Console.Write("SumPart : " + re.SumPart);
                Console.Write("SumCount : " + re.SumCount);
                Console.Write("Cost : " + re.Cost);
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Cancel()
        {
            try
            {
                string messageid = "5956";
                StatusResult result = _api.Cancel(messageid).Result;
                Console.Write("Messageid : " + result.Messageid);
                Console.Write("Status : " + result.Status);
                Console.Write("Statustext : " + result.Statustext);
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Cancel(List<string> messageids)
        {
            try
            {
                List<StatusResult> results = _api.Cancel(messageids).Result;
                foreach (StatusResult result in results)
                {
                    Console.Write("Messageid : " + result.Messageid);
                    Console.Write("Status : " + result.Status);
                    Console.Write("Statustext : " + result.Statustext);
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void Receive()
        {
            try
            {
                string linenumber = "30006703323323";
                int isread = 1;
                List<ReceiveResult> res = _api.Receive(linenumber, isread).Result;
                foreach (ReceiveResult re in res)
                {
                    Console.Write("Messageid : " + re.MessageId);
                    Console.Write("message : " + re.Message);
                    Console.Write("sender : " + re.Sender);
                    Console.Write("receptor : " + re.Receptor);
                    Console.Write("date : " + re.Date);
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void CountInbox()
        {
            try
            {
                DateTime stardate = new DateTime(2015, 09, 21, 10, 11, 12);
                DateTime enddate = DateTime.Now;
                string linenumber = "30006703323323";
                int isread = 10;
                var result = _api.CountInbox(stardate, enddate, linenumber, isread).Result;
                Console.Write("StartDate : " + result.StartDate);
                Console.Write("EndDate : " + result.EndDate);
                Console.Write("SumCount : " + result.SumCount);

            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void CountPostalCode()
        {
            try
            {
                long postalcode = 441585;
                var results = _api.CountPostalCode(postalcode).Result;
                foreach (var result in results)
                {
                    Console.Write("StartDate : " + result.Section + "\r\n");
                    Console.Write("EndDate : " + result.Value + "\r\n");
                }
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void AccountInfo()
        {
            try
            {
                var result = _api.AccountInfo().Result;
                Console.Write("RemainCredit : " + result.RemainCredit + "\r\n");
                Console.Write("Expiredate : " + result.Expiredate + "\r\n");
                Console.Write("Type : " + result.Type + "\r\n");
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
        }
        static void CallMakeTTS()
        {
            try
            {

                var receptors = new List<string> { "", "" };
                var message = "test CallMakeTTS";
                var results = _api.CallMakeTTS(message, receptors);
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
            }
            Console.ReadKey();
        }

        public static string ReturnCodeToMessage(int code) => code switch
        {
            400 => "پارامترها ناقص هستند",
            402 => "عملیات ناموفق بود",
            404 => "متدی با این نام پیدا نشده است",
            405 => "متد فراخوانی Get یا Post اشتباه است",
            407 => "دسترسی به اطلاعات مورد نظر برای شما امکان پذیر نیست",
            409 => "سرور قادر به پاسخگوئی نیست بعدا تلاش کنید",
            414 => "حجم درخواست بیشتر از حد مجاز است.در ارسال پیامک هر فراخوانی 200 رکورد و کنترل وضعیت هر فراخوانی 500 رکورد",
            411 => "شماره گیرنده پیام معتبر نمی باشد",
            412 => "شماره فرستنده معتبر نمی‌باشد",
            413 => "پیام خالی است و یا طول پیام بیش از حد مجاز می‌باشد.لاتین 612 ﻛﺎراﻛﺘﺮ و ﻓﺎرسی 268 ﻛﺎراﻛﺘﺮ",
            417 => "تاریخ ارسال اشتباه است یا تاریخ آن گذشته و یا به فرمت درست ارسال نشده است",
            418 => "اعتبار حساب شما کافی نیست",
            419 => "تعداد اعضای آرایه متن و گیرنده و ارسال کننده هم اندازه نیست ",
            422 => "داده ها به دلیل وجود کاراکتر نامناسب قابل پردازش نیست",
            424 => "الگوی مورد نظر پیدا نشد ، زمانی که نام الگو نادرست باشد یا طرح آن هنوز تائید نشده باشد رخ می‌دهد",
            426 => "استفاده از این متد نیازمند سرویس پیشرفته می‌باشد",
            428 => "ارسال کد از طریق تماس تلفنی امکان پذیر نیست، درصورتی که توکن فقط حاوی عدد نباشد این خطا رخ می‌دهد",
            431 => "ساختار کد صحیح نمی‌باشد ، اگر توکن حاوی خط جدید،فاصله، UnderLine یا جداکننده باشد این خطا رخ می‌دهد",
            432 => "پارامتر کد در متن پیام پیدا نشد ، اگر در هنگام تعریف الگو پارامتر token% را تعریف نکرده باشید این خطا رخ می‌دهد",
            _ => string.Empty,
        };

        public static string StatusCodeToMessage(int code) => code switch
        {
            1 => "در صف ارسال قرار دارد",
            2 => "زمان بندی شده (ارسال در تاریخ معین)",
            4 => "ارسال شده به مخابرات",
            5 => "ارسال شده به مخابرات",
            6 => "خطا در ارسال پیام که توسط سر شماره پیش می آید و به معنی عدم رسیدن پیامک می‌باشد",
            10 => "رسیده به گیرنده",
            11 => "نرسیده به گیرنده ، این وضعیت به دلایلی از جمله خاموش یا خارج از دسترس بودن گیرنده اتفاق می افتد",
            13 => "ارسال پیام از سمت کاربر لغو شده یا در ارسال آن مشکلی پیش آمده که هزینه آن به حساب برگشت داده می‌شود",
            14 => "بلاک شده است، عدم تمایل گیرنده به دریافت پیامک از خطوط تبلیغاتی که هزینه آن به حساب برگشت داده می‌شود",
            100 => "شناسه پیامک نامعتبر است ( به این معنی که شناسه پیام در پایگاه داده کاوه نگار ثبت نشده است یا متعلق به شما نمی‌باشد)",
            _ => string.Empty,
        };
    }
}
