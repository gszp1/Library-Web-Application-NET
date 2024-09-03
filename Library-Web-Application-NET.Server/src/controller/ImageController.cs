using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/images")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImageController : ControllerBase
    {
        private readonly string imagePath = Path.Combine("wwwroot", "images");
        private readonly string userImagePath = Path.Combine("wwwroot", "userImages");

        private readonly IUserService userService;
        private readonly IResourceService resourceService;

        public ImageController(IUserService userService, IResourceService resourceService)
        {
            this.userService = userService;
            this.resourceService = resourceService;
        }

        [AllowAnonymous]
        [HttpGet("{filename}")]
        public IActionResult GetImage(string? filename)
        {
            try
            {
                return ReadImage(filename, imagePath);
            }
            catch (Exception)
            {
                 return NotFound();
            }
        }

        [AllowAnonymous]
        [HttpGet("userImage/{filename}")]
        public IActionResult GetUserImage(string? filename)
        {
            try
            {
                return ReadImage(filename, userImagePath);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Policy = "UserUpdate")]
        [HttpPut("user/{email}/image")]
        public async Task<IActionResult> UpdateImage(string email, IFormFile image)
        {
            try
            {
                if (image == null || !ValidateFileType(image))
                {
                    return BadRequest("Invalid data provided.");
                }
                var extension = Path.GetExtension(image.FileName);
                var targetLocation = Path.Combine(userImagePath, email + extension);

                using (var stream = new FileStream(targetLocation, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                var url = $"{Request.Scheme}://{Request.Host}/api/images/userImage/{email + extension}";
                await userService.UpdateUserImageUrlAsync(email, url);
                return Ok("Image updated.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Updating image failed.");
            }
        }

        [Authorize(Policy = "AdminUpdate")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateResourceImage(int id, IFormFile image)
        {
            try
            {
                if (image == null || !ValidateFileType(image) || !(await resourceService.ResourceExistsAsync(id)))
                {
                    return BadRequest("Invalid data provided.");
                }
                var originalFilename = image.FileName;
                var targetLocation = Path.Combine(imagePath, originalFilename);
                using (var stream = new FileStream(targetLocation, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                var url = $"{Request.Scheme}://{Request.Host}/api/images/{originalFilename}";
                await resourceService.UpdateResourceImageAsync(id, url);
                return Ok("Image updated.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Updating image failed.");
            }
        }

        [Authorize(Policy = "AdminCreate")]
        [HttpPost("create/{id}")]
        public async Task<IActionResult> AddResourceImage(int id, IFormFile image)
        {
            try
            {
                if (image == null || !ValidateFileType(image) || !(await resourceService.ResourceExistsAsync(id))) {
                    return BadRequest("Invalid data provided.");
                }
                var originalFilename = image.FileName;
                var targetLocation = Path.Combine(imagePath, originalFilename);

                using (var stream = new FileStream(targetLocation, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var url = $"{Request.Scheme}://{Request.Host}/api/images/{originalFilename}";
                await resourceService.UpdateResourceImageAsync(id, url);

                return Ok("Image saved.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Saving image failed.");
            }
        }

        private IActionResult ReadImage(string? filename, string imagePath)
        {
            var filePath = Path.Combine(imagePath, filename);
            if (System.IO.File.Exists(filePath))
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var mimeType)) 
                {
                    mimeType = "application/octet-stream";
                }
                return PhysicalFile(filePath, mimeType);
            } 
            else
            {
                return NotFound();
            }
        }

        private bool ValidateFileType(IFormFile file)
        {
            var mimeType = file.ContentType;
            return !(mimeType == "image/jpeg" ||
                     mimeType == "image/png" ||
                     mimeType == "image/webp");
        }
    }
}
