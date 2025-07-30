using Beauty.Application.Modules.Appointment.Messaging;
using System.Threading.Tasks;

namespace Beauty.Application.Modules.Appointment.Implementation
{
    public interface IAppointmentService
    {
        Task<CreateResponse> Create(CreateRequest request);
        Task<UpdateResponse> Update(UpdateRequest request);
        Task<FindByIdResponse> FindById(FindByIdRequest request);
        Task<FindAllByDateResponse> FindAllByDate(FindAllByDateRequest request);
        Task<FindAllByStartEndDateResponse> FindAllByStartEndDate(FindAllByStartEndDateRequest request);
        Task<CancelResponse> Cancel(CancelRequest request);
        Task<DoneResponse> Done(DoneRequest request);
        Task<DeleteResponse> Delete(DeleteRequest request);
        Task<DeleteServiceResponse> DeleteService(DeleteServiceRequest request);
        Task<DoneServiceResponse> DoneService(DoneServiceRequest request);
        Task<ChangeServicePersonnelResponse> ChangeServicePersonnel(ChangeServicePersonnelRequest request);
        Task<AddPaymentResponse> AddPayment(AddPaymentRequest request);
        Task<AddDiscountResponse> AddDiscount(AddDiscountRequest request);
        Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request);
        Task<DeletePaymentResponse> DeletePayment(DeletePaymentRequest request);
        Task<FindAllByPersonnelResponse> FindAllServicesByPersonnelAndDate(FindAllByPersonnelRequest request);
        Task<FindAllByPersonnelResponse> FindAllNotDoneServicesByPersonnelAndDate(FindAllByPersonnelRequest request);
        Task<AppointmentFindAllSchedulerResponse> FindAllScheduler(AppointmentFindAllSchedulerRequest request);
        Task<AppointmentFindDetailResponse> FindDetailByCustomer(AppointmentFindDetailRequest request);
        Task<AppointmentServiceDetailModifyResponse> AppointmentServiceDetailModify(AppointmentServiceDetailModifyRequest request);
        Task<AppointmentServiceDetailFindAllResponse> AppointmentServiceDetailFindAll(AppointmentServiceDetailFindAllRequest request);
        Task<AppointmentDiscountFindAllResponse> AppointmentDiscountFindAll(AppointmentDiscountFindAllRequest request);

    }
}