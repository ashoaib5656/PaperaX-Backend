using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PaperaX.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ImagesController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("signature")]
        public IActionResult GetSignature()
        {
            var apiSecret = _config["Cloudinary:ApiSecret"];
            var apiKey = _config["Cloudinary:ApiKey"];
            var cloudName = _config["Cloudinary:CloudName"];

            if (string.IsNullOrEmpty(apiSecret) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(cloudName))
            {
                return StatusCode(500, new { message = "Cloudinary configuration is missing." });
            }

            // Generate UNIX timestamp
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            // Cloudinary requires the signature to be exactly this format before hashing:
            // "timestamp=<timestamp><api_secret>"
            var stringToSign = $"timestamp={timestamp}{apiSecret}";

            // Generate SHA-1 hash
            using var sha1 = SHA1.Create();
            var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
            var signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return Ok(new
            {
                signature,
                timestamp,
                apiKey,
                cloudName
            });
        }
    }
}
