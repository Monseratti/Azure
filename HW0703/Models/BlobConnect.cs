using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.ComponentModel;

namespace HW0703.Models
{
	public static class BlobConnect
	{
		static string conn = "DefaultEndpointsProtocol=https;AccountName=monseratti03;AccountKey=oB/V9O+kERNdX3rsOh8pvgSkyb1LrfBYCzvEncsxNXEcJA0wK1Cl648ZSyUzA2JNcvJAEngH1OB7+AStrrHB+w==;EndpointSuffix=core.windows.net";

        static BlobServiceClient blobServiceClient = new BlobServiceClient(conn);
		
		static BlobContainerClient GetContainerClient() {
			try
			{
				return blobServiceClient.GetBlobContainerClient("home");
			}
			catch (Exception)
			{
				return blobServiceClient.CreateBlobContainer("home");
			}
		}

		public static BlobContainerClient CreateContainer(string containerName)
		{
			try
			{
				return blobServiceClient.CreateBlobContainer(containerName, PublicAccessType.Blob);
			}
			catch (Exception)
			{
				return blobServiceClient.GetBlobContainerClient(containerName);
			}
		}

		public static async Task<Image?> UploadFile(string filePath)
		{
			var fileName = Path.GetFileName(filePath);
			var client = CreateContainer("home").GetBlobClient(fileName);
			try
			{
				await client.UploadAsync(filePath);
				return new Image()
				{
					Id = 0,
					URL = client.Uri.ToString()
				};
            }
			catch (Exception)
			{
				return null;
			}
		}

		public static async Task DownloadFile(string fileName)
		{
			try
			{
				var tmpPath = $@"{Directory.GetCurrentDirectory()}\wwwroot\tmp";
                var client = CreateContainer("home").GetBlobClient(fileName);
				Directory.CreateDirectory(tmpPath);
				using (FileStream fs = File.OpenWrite(Path.Combine(tmpPath, client.Name)))
				{
					await client.DownloadToAsync(fs);
				}
            }
			catch (Exception)
			{
			}
		}

        public static async Task<Image?> DeleteFile(string fileName)
        {
            try
            {
                var client = CreateContainer("home").GetBlobClient(fileName);
				var image = new Image()
                {
                    Id = 0,
                    URL = client.Uri.ToString()
                };
                await client.DeleteAsync();
				return image;
            }
            catch (Exception)
            {
				return null;
            }
        }


        public static async Task<List<object>> ContainerInfo()
		{
			List<object> tmp = new List<object>();
			await foreach (var blob in CreateContainer("home").GetBlobsAsync())
			{
				tmp.Add(new { 
					name = blob.Name, 
					createdAt = blob.Properties.CreatedOn.Value.ToLocalTime(), 
					absolutePath = CreateContainer("home").GetBlobClient(blob.Name).Uri.AbsolutePath });
			}
			return tmp;
		}
	}
}
