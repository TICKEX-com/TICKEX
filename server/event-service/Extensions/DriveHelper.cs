using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace event_service.Extensions
{
    public class DriveHelper
    {
        private DriveService GetDriveService()
        {
            // Load credentials from JSON file
            GoogleCredential credential = GoogleCredential.FromFile("appsettings.json")
                .CreateScoped(DriveService.Scope.Drive);

            // Create Drive API service.
            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "TICKEX"
            });
        }

        public List<string> GetImageIds(string directoryId)
        {
            List<string> imageIds = new List<string>();

            try
            {
                var service = GetDriveService();

                // Define parameters for the file list request
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.Fields = "files(id, name)";
                listRequest.Q = $"mimeType='image/jpeg' and '{directoryId}' in parents";

                // Retrieve the files
                var files = listRequest.Execute().Files;

                // Add the IDs of the retrieved images to the list
                foreach (var file in files)
                {
                    imageIds.Add(file.Id);
                }

                return imageIds;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
        }

        public string UploadImage(string filePath)
        {
            try
            {
                var service = GetDriveService();

                // Upload file photo.jpg on drive.
                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = "photo.jpeg",
                    Parents = new List<string> { "1wmh0szUT3ghre7fojCl_2Ozh-_ViPcbX" }
                };
                FilesResource.CreateMediaUpload request;
                // Create a new file on drive.
                using (var stream = new FileStream(filePath,
                           FileMode.Open))
                {
                    // Create a new file, with metadata and stream.
                    request = service.Files.Create(
                        fileMetadata, stream, "image/jpeg");
                    request.Fields = "id";
                    request.Upload();
                }

                var file = request.ResponseBody;
                // Prints the uploaded file id.
                Console.WriteLine("File ID: " + file.Id);
                return file.Id;
            }
            catch (Exception e)
            {
                // TODO(developer) - handle error appropriately
                if (e is AggregateException)
                {
                    Console.WriteLine("Credential Not found");
                }
                else if (e is FileNotFoundException)
                {
                    Console.WriteLine("File not found");
                }
                else
                {
                    throw;
                }
                return null;
            }
        }
    }
}
