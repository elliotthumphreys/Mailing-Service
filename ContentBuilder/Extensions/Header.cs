namespace ContentBuilder.Extensions
{
    public static class Header
    {
        public static HTMLBodyBuilder StartHeader(this HTMLBodyBuilder builder)
        {
            builder.IncludeTable = false;

            builder
                .Add("<table style=\"max-width:600px;margin:auto auto 10px;width:100%;padding:0;background-color:#188479;color:white;border:none;border-left:solid 10px #055C58;border-collapse: collapse;\" align=\"center\">")
                .Add("<tbody>");

            return builder;
        }

        public static HTMLBodyBuilder EndHeader(this HTMLBodyBuilder builder)
        {
            builder
                .Add("</tbody>")
                .Add("</table>");

            builder.IncludeTable = true;

            return builder;
        }
    }
}
