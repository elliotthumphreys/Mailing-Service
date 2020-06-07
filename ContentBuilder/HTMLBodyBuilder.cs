namespace ContentBuilder
{
    public class HTMLBodyBuilder
    {
        private readonly string _start = @"
                <!DOCTYPE html>
                <html>
                    <head>
                        <meta http-equiv=""Content-Type"" content=""text/html charset=UTF-8"" />
                        <!-- Outlook only styles -->
                        <style type=""text/css"">
                          table {border-collapse:separate;}
                          a, a:link, a:visited {text-decoration: none;}
                          a:hover {text-decoration: underline;}
                          .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td {line-height: 100%;}
                          .ExternalClass {width: 100%;}
                        </style>
                    </head>
                    <body style=""font-family: sans-serif;margin:0;padding:0;box-sizing: border-box;"">";
        private readonly string _end = @"
                    </body>
                </html>";

        private readonly string _tableStart = @"<table style=""max-width:600px;margin:auto;width:100%;padding:0;color:black;border-collapse: collapse;"" align=""center""><tbody>";
        private readonly string _tableEnd = "</tbody></table>";

        private string _body = string.Empty;

        public bool IncludeTable = true;

        public HTMLBodyBuilder Add(string content)
        {
            if (!IncludeTable)
            {
                _body += content;

                return this;
            }

            _body += _tableStart + content + _tableEnd;

            return this;
        }

        public string Build() => _start.Trim() + _body.Trim() + _end.Trim();
    }
}
