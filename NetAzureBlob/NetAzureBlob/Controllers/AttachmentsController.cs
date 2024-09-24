using Microsoft.AspNetCore.Mvc;
using NetAzureBlob.Services;

namespace NetAzureBlob.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttachmentsController : ControllerBase
{
    AzureBlobService _service;
    public AttachmentsController(AzureBlobService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> UploadBlobs(List<IFormFile> files)
    {
        var response = await _service.UploadFiles(files);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBlobs()
    {
        var response = await _service.GetUploadedBlobs();
        return Ok(response);
    }
}
