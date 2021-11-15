using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab5.Models;


namespace Lab5.Pages.AnswerImages
{
    public class CreateModel : PageModel
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";
        private string containerName;

        private readonly Lab5.Data.AnswerImageDataContext _context;

        public CreateModel(Lab5.Data.AnswerImageDataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }



        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AnswerImage AnswerImage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Question q, IFormFile file)
        {

            BlobContainerClient containerClient;

            if(q == Question.earth)
            {
                containerName = earthContainerName;
            }
            else
            {
                containerName = computerContainerName;
            }


            try
            {

                containerClient = await _blobServiceClient.CreateBlobContainerAsync(earthContainerName);

                // Give access to public
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            try
            {
                string randomFileName = Path.GetRandomFileName();
                var blockBlob = containerClient.GetBlobClient(randomFileName);

                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                using (var memoryStream = new MemoryStream())
                {
                    // copy the file data into memory
                    await file.CopyToAsync(memoryStream);

                    // navigate back to the beginning of the memory stream
                    memoryStream.Position = 0;

                    // send the file to the cloud
                    await blockBlob.UploadAsync(memoryStream);
                    memoryStream.Close();
                }

                // add the photo to the database if it uploaded successfully
                var image = new AnswerImage
                {
                    Url = blockBlob.Uri.AbsoluteUri,
                    FileName = randomFileName,
                    Question = q
                };

                _context.AnswerImages.Add(image);
                await _context.SaveChangesAsync();


            }
            catch (RequestFailedException)
            {
                return RedirectToPage("Error");
            }

            return RedirectToPage("./Index");
        }
    }
}
