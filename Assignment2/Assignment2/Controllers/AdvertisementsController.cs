using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;
using Assignment2.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Azure;
using System.IO;

namespace Assignment2.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly SchoolCommunityContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string containerName = "communityad";

        public AdvertisementsController(SchoolCommunityContext context, BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _context = context;
            
        }

        // GET: Advertisements
        public async Task<IActionResult> Index(string Id)
        {
            var viewModel = new AdsViewModel();
            viewModel.Community = await _context.Communities.FindAsync(Id);
            viewModel.Advertisements = await _context.Advertisements.ToListAsync();
            viewModel.Advertisements = viewModel.Advertisements.Where(x => x.CommunityId == Id);
            return View(viewModel);
        }

        // GET: Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.Community)
                .FirstOrDefaultAsync(m => m.id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        public IActionResult Create(string ID, string Title)
        {
            var viewModel = new FileInputViewModel();
            viewModel.CommunityId = ID;
            viewModel.CommunityTitle = Title;

            return View(viewModel);

            /* ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Id");
             return View();*/
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]FileInputViewModel myFile)
        {
      
            BlobContainerClient containerClient;

            try
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            try
            {
                var blockBlob = containerClient.GetBlobClient(myFile.File.FileName);

                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                using (var memoryStream = new MemoryStream())
                {
                    // copy the file data into memory
                    await myFile.File.CopyToAsync(memoryStream);

                    // navigate back to the beginning of the memory stream
                    memoryStream.Position = 0;

                    // send the file to the cloud
                    await blockBlob.UploadAsync(memoryStream);
                    memoryStream.Close();
                }

                // add the photo to the database if it uploaded successfully
                var image = new Advertisement
                {
                    Url = blockBlob.Uri.AbsoluteUri,
                    FileName = myFile.File.FileName,
                    CommunityId = myFile.CommunityId
                };

                _context.Advertisements.Add(image);
                _context.SaveChanges();

            }
            catch (RequestFailedException)
            {
                return RedirectToPage("Error");
            }

            return RedirectToAction("Index", new { id = myFile.CommunityId });

        }


       

        // GET: Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.Community)
                .FirstOrDefaultAsync(m => m.id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.id == id);
        }
    }
}
