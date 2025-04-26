namespace DTour.Controllers.Catalog
{
    public class UpDownController(IWebHostEnvironment host) : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post(UploadCommandRequest request)
        {
            long maxFileSize = 1024* 1024 * 15;
            List<UploadResult> uploadResults = [];
            var storeFolder = host.WebRootPath;
            foreach (var file in request.Files!)
            {
                var uploadResult = new UploadResult();
                var fileName = $"{Path.GetExtension(file.FileName)}";
                if (file.Data?.Length <= maxFileSize)
                {
                    try
                    {
                        fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}{fileName}"; 
                        var path = Path.Combine(storeFolder, "img", "content", fileName);
                        await System.IO.File.WriteAllBytesAsync(path, file.Data);
                        uploadResult.Uploaded = true;
                        uploadResult.FileName = fileName;
                    }
                    catch (IOException ex)
                    {
                        uploadResult.Uploaded = false;
                        uploadResult.ErrorCode = 3;
                        uploadResult.FileName = ex.Message;
                    }
                }

                uploadResults.Add(uploadResult);
            }

            return Ok(uploadResults);
        }
    }
}