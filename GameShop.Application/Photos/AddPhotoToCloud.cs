using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;

namespace GameShop.Application.Photos
{
    public class AddPhotoToCloud : IAddPhotoToCloud
    {
        public bool Do(Cloudinary cloudinary, PhotoForCreationDto photoForCreationDto)
        {

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

                   uploadResult =  cloudinary.Upload(uploadParams);
               }
           }

           if (uploadResult.Url == null)
           {
               return false;
           }

           photoForCreationDto.Url = uploadResult.Url?.ToString();
           photoForCreationDto.PublicId = uploadResult.PublicId;

           return true;
        }
    }
}