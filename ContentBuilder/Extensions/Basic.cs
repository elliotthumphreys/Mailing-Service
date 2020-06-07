namespace ContentBuilder.Extensions
{
    public static class Basic
    {
        private static string BasicElement(string content, string styles = "", int marginX = 20, int marginY = 10)
        {
            return $@"<tr style=""margin:0;padding:0;height:{marginY}px;width:100%;"">
                            <td></td>
                            <td></td>
                            <td></td>
                      </tr>
                      <tr style=""margin:0;padding:0;"">
                            <td style=""width:{marginX}px;margin:0;padding:0;""></td>
                            <td style=""margin:0;padding:0;{styles}"">{content}</td>
                            <td style=""width:{marginX}px;margin:0;padding:0;""></td>
                      </tr>
                      <tr style=""margin:0;padding:0;height:{marginY}px;width:100%;"">
                            <td></td>
                            <td></td>
                            <td></td>
                      </tr>";
        }

        public static HTMLBodyBuilder AddP(this HTMLBodyBuilder builder, string content)
        {
            return builder.Add(BasicElement(content, "font-size:1em;font-weight:400;"));
        }

        public static HTMLBodyBuilder AddH1(this HTMLBodyBuilder builder, string content)
        {
            return builder.Add(BasicElement(content, "font-size:2em;font-weight:600;"));
        }

        public static HTMLBodyBuilder AddH2(this HTMLBodyBuilder builder, string content)
        {
            return builder.Add(BasicElement(content, "font-size:1.5em;font-weight:600;"));
        }

        public static HTMLBodyBuilder AddH3(this HTMLBodyBuilder builder, string content)
        {
            return builder.Add(BasicElement(content, "font-size:1.17em;font-weight:600;"));
        }

        public static HTMLBodyBuilder AddA(this HTMLBodyBuilder builder, string content, string url)
        {
            return builder.Add(BasicElement($"<a href=\"{url}\" style=\"margin:0;padding:0;\">{content}</a>"));
        }

        public static HTMLBodyBuilder AddButton(this HTMLBodyBuilder builder, string content, string url)
        {
            var a = $@"<table cellpadding=""0"" cellspacing=""0"" border=""0""><tbody><tr><td valign=""middle"" style=""background: #188479; padding: 10px 20px; border-radius: 4px;border: 1px solid #00b79b;""><a href=""{url}"" style=""text-decoration: none;color: white;""><span style=""font-weight: 500;"">{content}</span></a></td></tr></tbody></table>";

            return builder.Add(BasicElement(a));
        }
    }
}
