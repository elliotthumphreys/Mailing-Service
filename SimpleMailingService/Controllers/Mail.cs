using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMailingService.Models;
using SimpleMailingService.Services;
using StockportGovUK.AspNetCore.Attributes.TokenAuthentication;

namespace SimpleMailingService.Controllers
{
    [TokenAuthentication]
    public class Mail : ControllerBase
    {
        private readonly ISendService _sendService;

        public Mail(ISendService sendService)
        {
            _sendService = sendService;
        }

        /// <summary>Sends an email to multiple recipients.</summary>
        /// <remarks>
        /// Recipients requires at least one Recipient.
        /// 
        /// Recipient.Email is a string in 'email' format.
        /// 
        /// Client is of type 'ClientEnum' and is not required.
        /// 
        /// Attachments are not required.
        /// 
        /// Attachments[].Base64Content is a base64 encoded byte array.
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Mail.Send: Failed to send email.");
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}