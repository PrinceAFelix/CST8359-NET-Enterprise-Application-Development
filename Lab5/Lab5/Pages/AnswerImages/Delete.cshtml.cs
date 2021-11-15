using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab5.Models;

namespace Lab5.Pages.AnswerImages
{
    public class DeleteModel : PageModel
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";

        private readonly Lab5.Data.AnswerImageDataContext _context;

        public DeleteModel(Lab5.Data.AnswerImageDataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }


        [BindProperty]
        public AnswerImage AnswerImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerImage = await _context.AnswerImages.FirstOrDefaultAsync(m => m.AnswerImageId == id);

            if (AnswerImage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Question q, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerImage = await _context.AnswerImages.FindAsync(id);

            if (AnswerImage != null)
            {
                BlobContainerClient containerClient;

                try
                {
                    if (q == Question.earth)
                    {
                        containerClient = _blobServiceClient.GetBlobContainerClient(earthContainerName);
                    }
                    else
                    {
                        containerClient = _blobServiceClient.GetBlobContainerClient(computerContainerName);

                    }

                }
                catch (RequestFailedException)
                {
                    return RedirectToPage("Error");
                }

                try
                {
                    var blockBlob = containerClient.GetBlobClient(AnswerImage.FileName);
                    if (await blockBlob.ExistsAsync())
                    {
                        await blockBlob.DeleteAsync();
                    }
                    _context.AnswerImages.Remove(AnswerImage);
                    await _context.SaveChangesAsync();
                }
                catch (RequestFailedException)
                {
                    return RedirectToPage("Error");
                }

               
            }

            return RedirectToPage("./Index");
        }
    }
}
