namespace LMS.Views.Shared.PartialViews
{
    public class ControlBtnPartialViewModel
    {
        public bool? Edit { get; set; }
        public bool? Details { get; set; }
        public bool? Delete { get; set; }
        public bool? Approve { get; set; }
        public bool? Return { get; set; }

        public int Id { get; set; }
        public string ControllerName { get; set; }
    }
}
