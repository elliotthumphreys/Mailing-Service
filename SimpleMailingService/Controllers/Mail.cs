using System;
using ContentBuilder;
using ContentBuilder.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMailingService.Models;
using SimpleMailingService.Services;

namespace SimpleMailingService.Controllers
{
    public class Mail : ControllerBase
    {
        private readonly ISendService _sendService;

        public Mail(ISendService sendService)
        {
            _sendService = sendService;
        }

        /// <summary>Sends an email to the recipient defined</summary>
        /// <remarks>
        /// Note that the Recipients requires at least one Recipient.
        /// 
        /// Note that the Recipient.Email is a string in 'email' format.
        /// 
        /// Note that the Client is of type 'ClientEnum' not 'int'.
        /// 
        /// Note that the Body is 'html/text'.
        /// 
        /// Note that the Attachments are not required.
        /// 
        /// Note that the Attachments[].Base64Content is a base64 encoded byte array.
        ///  
        ///     POST /api/v1/mail
        ///     {
        ///        "Recipients": [{
        ///             "Name": "Example Name",
        ///             "Email": "example@example-host.com"
        ///         }],
        ///        "Subject": "example email subject",
        ///        "Body": "<h1>Example body content</h1>",
        ///        "Client": ClientEnum.Default,
        ///        "Attachments":
        ///             [{
        ///                 "Name": "example attachment name",
        ///                 "Base64Content": "",
        ///                 "MimeType": {
        ///                     "Type": "image",
        ///                     "SubType": "png"
        ///             }]
        ///         }
        ///     }
        /// 
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Request model is invalid</response>
        /// <response code="500">Email failed to send</response>
        [HttpPost]
        [Route("api/v1/mail")]
        public IActionResult Send([FromBody]SendMailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _sendService.Send(request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>Shows an example of the html content produced when using HTMLBodyBuilder class library</summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("test-builder")]
        public IActionResult Test()
        {
            var content = new HTMLBodyBuilder()
                .StartHeader()
                    .AddH1("Example Heading")
                    .AddH2("Thank you for filling out a form")
                    .AddP("Some sub heading to do with the form")
                    .AddH3("Here is a random heading")
                    .AddP("Some more random p text")
                    .EndHeader()
                .AddP($"Your reference number: <strong>Example-Ref</strong>")
                .AddH2("What happens next?")
                .AddP("Someone will be in contact with you in the next 10 working days.")
                .AddH3("Here is a random heading")
                .AddP("Some more random p text")
                .AddButton("Go to Homepage", "http://www.google.com")
                .Build();

            //var request = new SendMailRequest
            //{
            //    Body = content,
            //    Subject = "Test email subject",
            //    Client = ClientEnum.Personal,
            //    Recipients = new List<Recipient>
            //    {
            //        new Recipient
            //        {
            //            Email = "",
            //            Name = ""
            //        },
            //        new Recipient
            //        {
            //            Email = "",
            //            Name = ""
            //        }
            //    }
            //};

            //Send(request);

            return new ContentResult
            {
                ContentType = "text/html",
                Content = content
            };
        }
    }
}
