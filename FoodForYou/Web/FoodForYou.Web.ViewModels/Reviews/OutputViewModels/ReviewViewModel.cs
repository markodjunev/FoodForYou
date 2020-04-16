namespace FoodForYou.Web.ViewModels.Reviews
{
    using System;

    using Ganss.XSS;

    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Text);

        public int ProductId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUserName { get; set; }
    }
}
