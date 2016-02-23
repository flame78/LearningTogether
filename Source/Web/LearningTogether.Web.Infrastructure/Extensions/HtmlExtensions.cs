namespace LearningTogether.Web.Infrastructure.Extensions
{
    using System.Text;
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static MvcHtmlString Ratings(this HtmlHelper helper, RatingModel rating)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(
                "<span class='rating' data-rating='{0}' data-item-id='{1}' title='Click to cast vote'>",
                rating.Rating,
                rating.Id);
            sb.AppendFormat(
                "<span data-toggle='tooltip' data-placement='top' data-original-title='Currently rated {0} by {1} people'>",
                rating.Rating,
                rating.Raters);
            string formatStr = "<img src='/Content/images/{0}' alt='star' class='star' value='{1}' />";

            for (double i = 1; i <= 5; i++)
            {
                if (rating.Rating >= i)
                {
                    sb.AppendFormat(formatStr, "star-on.png", i);
                }
                else
                {
                    sb.AppendFormat(formatStr, "star-off.png", i);
                }
            }

            sb.Append("</span></span>");
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString SubmitButton(
            this HtmlHelper helper,
            string buttonText,
            object htmlAttributes = null)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<input type = 'submit' value = '{0}' ", buttonText);
            var attributes = helper.AttributeEncode(htmlAttributes);
            if (!string.IsNullOrEmpty(attributes))
            {
                attributes = attributes.Trim('{', '}');
                var attrValuePairs = attributes.Split(',');
                foreach (var attrValuePair in attrValuePairs)
                {
                    var equalIndex = attrValuePair.IndexOf('=');
                    var attrValue = attrValuePair.Split('=');
                    html.AppendFormat(
                        "{0}='{1}' ",
                        attrValuePair.Substring(0, equalIndex).Trim(),
                        attrValuePair.Substring(equalIndex + 1).Trim());
                }
            }

            html.Append("/>");
            return new MvcHtmlString(html.ToString());
        }
    }
}