namespace Beauty.Application.Modules.Appointment.ViewModel
{
    public class AppointmentServiceDetailViewModel
    {
        public int AppointmentServiceDetailId { get; set; }
        public short ServiceDetailId { get; set; }

        public string Title { get; set; }
        public float Count { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
    }
}
