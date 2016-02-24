namespace LearningTogether.Web.Infrastructure.Extensions
{
    using System.Text;
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static MvcHtmlString Ratings(this HtmlHelper helper, RatingModel rating)
        {
            var html = new StringBuilder();
            html.AppendFormat(
                "<span class='rating' data-rating='{0}' data-item-id='{1}' title='Click to cast vote'>",
                rating.Rating,
                rating.Id);
            html.AppendFormat(
                "<span data-toggle='tooltip' data-placement='top' data-original-title='Currently rated {0} by {1} people'>",
                rating.Rating,
                rating.Raters);
            string formatStr = "<img src='/Content/images/{0}' alt='star' class='star' value='{1}' />";

            for (double i = 1; i <= 5; i++)
            {
                if (rating.Rating >= i)
                {
                    html.AppendFormat(formatStr, "star-on.png", i);
                }
                else
                {
                    html.AppendFormat(formatStr, "star-off.png", i);
                }
            }

            html.Append("</span></span>");
            return new MvcHtmlString(html.ToString());
        }

        public static MvcHtmlString SubmitButton(
            this HtmlHelper helper,
            string buttonText,
            object htmlAttributes = null)
        {
            var html = new StringBuilder();
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

        public static MvcHtmlString Pager(this HtmlHelper helper, string formId, int page, int totalPages)
        {
            var html = new StringBuilder();

            html.Append("<nav class='text-center'><ul class='pagination'>");
            html.AppendFormat("<input type='hidden' id='pageIndex' name='pageIndex' value='{0}'/>", page);

            if (page > 1)
            {
                html.Append("<li><a href='#' aria-label='Previous' onclick = 'DecreasePage()'>");
                html.Append("<span aria-hidden='true'>&laquo;</span></a></li>");
            }

            for (int i = 1; i < totalPages; i++)
            {
                var className = string.Empty;
                if (page == i)
                {
                    className = "active";
                }
                html.AppendFormat("<li class='{0}'><a href='#' onclick='GotoPage({1})'>{1}</a></li>", className, i);
            }

            if (page < totalPages)
            {
                html.Append(
                    "<li><a href='#' aria-label='Next' onclick='IncreasePage()'><span aria-hidden='true'>&raquo;</span></a></li>");
            }
            html.Append(
                "</ul></nav><script>function DecreasePage() { var pageIndex = $('#pageIndex').attr('value');");
            html.AppendFormat("pageIndex--; $('#pageIndex').attr('value', pageIndex); $('#{0}').submit(); }}", formId);
            html.AppendFormat("function GotoPage(page) {{ $('#pageIndex').attr('value', page); $('#{0}').submit(); }}", formId);
            html.Append("function IncreasePage() { var pageIndex = $('#pageIndex').attr('value'); pageIndex++;");
            html.AppendFormat("$('#pageIndex').attr('value', pageIndex);$('#{0}').submit();}}", formId);
            html.AppendFormat("function SubmitData() {{ $('#{0}').submit(); }} </script>", formId);

            return new MvcHtmlString(html.ToString());
        }
    }
}