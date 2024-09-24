using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;

namespace NetAzureBlob.Services;

public class AzureBlobService
{
    private BlobServiceClient _blobClient;
    private BlobContainerClient _containerClient;
    private string azureConnectionString = "";

    public AzureBlobService()
    {
        _blobClient = new BlobServiceClient(azureConnectionString);
        _containerClient = _blobClient.GetBlobContainerClient("purchase-documents");
    }

    public async Task<List<Azure.Response<BlobContentInfo>>> UploadFiles(List<IFormFile> files)
    {
        var azureResponse = new List<Azure.Response<BlobContentInfo>>();
        foreach (var file in files)
        {
            string fileName = file.FileName;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                var client = await _containerClient.UploadBlobAsync(fileName, memoryStream, default);
                azureResponse.Add(client);
            }
        };

        return azureResponse;
    }

    public async Task<List<BlobItem>> GetUploadedBlobs()
    {
        var items = new List<BlobItem>();
        var uploadedFiles = _containerClient.GetBlobsAsync();
        await foreach (BlobItem file in uploadedFiles)
        {
            items.Add(file);
        }

        return items;
    }
}