using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using static System.Net.WebRequestMethods;

namespace E_CookBook.OCR
{
    public class RecipeOCR
    {
        static readonly HttpClient client = new HttpClient();

        public async Task<string> ProcessImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty.", nameof(file));
            }

            string serverUrl = "http://localhost:5000/extract-text";

            using (var form = new MultipartFormDataContent())
            {
                using (var fileStream = file.OpenReadStream())
                {
                    using (var streamContent = new StreamContent(fileStream))
                    {
                        using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                        {
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                            form.Add(fileContent, "photo", file.FileName);
                            HttpResponseMessage response = await client.PostAsync(serverUrl, form);

                            if (response.IsSuccessStatusCode)
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();
                                return responseBody;
                            }
                            else
                            {
                                // Log the error or throw an exception as per your error handling
                                throw new HttpRequestException($"Error from OCR service: {(int)response.StatusCode} {response.ReasonPhrase}");
                            }
                        }
                    }
                }
            }

        }
    }
}
