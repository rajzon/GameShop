
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GameShop.Application.Helpers;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GameShop.UI.Controllers
{
    [Route("api/admin/product/{productId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IGameShopRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly IUnitOfWork _unitOfWork;
        private Cloudinary _cloudinary;

        public PhotosController(IGameShopRepository repo, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;

            Account acc = new Account (_cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);

        }


        [HttpGet("id", Name = "GetPhoto")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _unitOfWork.Photo.GetAsync(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        [Authorize(Policy = "ModerateProductRole")]
        public async Task<IActionResult> AddPhotoForProduct(int productId, 
            [FromForm]PhotoForCreationDto photoForCreationDto)
        {
           var productFromRepo = await _repo.GetProductWithPhotos(productId);
           

           var file = photoForCreationDto.File;

           var uploadResult = new ImageUploadResult();

           if (file.Length > 0) 
           {
               using (var stream = file.OpenReadStream())
               {
                   var uploadParams = new ImageUploadParams() 
                   {
                       File = new FileDescription(file.Name, stream),
                       Transformation = new Transformation().Width(500).Height(500)
                            
                   };

                   uploadResult =  _cloudinary.Upload(uploadParams);
               }
           }

           photoForCreationDto.Url = uploadResult.Url.ToString();
           photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!productFromRepo.Photos.Any(p => p.isMain))
            {
                photo.isMain = true;
            }

            productFromRepo.Photos.Add(photo);

            

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { productId = productId, id = photo.Id}, photoToReturn);            
            }

            return BadRequest($"Could not add photo for product: {productId}");
            
        }


        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int productId,int id) 
        {
            var product = await _repo.GetProductWithPhotos(productId);

            if (!product.Photos.Any(p => p.Id == id))
            {
                return Unauthorized();
            }

            var photoFromRepo = await _unitOfWork.Photo.GetAsync(id);

            if (photoFromRepo.isMain)
            {
                return BadRequest("This is already main photo");
            }

            var currrentMainPhoto = await _unitOfWork.Photo.FindAsync(p => p.ProductId == productId);

            if (currrentMainPhoto != null)
            {
                currrentMainPhoto.isMain = false;
            } 


            photoFromRepo.isMain = true;

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not save photo as main photo");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int productId,int id)
        {
            var product = await _repo.GetProductWithPhotos(productId);

            if (!product.Photos.Any(p => p.Id == id))
            {
                return Unauthorized();
            }      

            var photoFromRepo = await _unitOfWork.Photo.GetAsync(id);        

            if (photoFromRepo.PublicId !=null) 
            {
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if (result.Result == "ok")
                {
                    _repo.Delete(photoFromRepo);
                }
            } 
            else 
            {
                _repo.Delete(photoFromRepo);
            }


            if (await _repo.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to delete the photo");
        }
    }
}