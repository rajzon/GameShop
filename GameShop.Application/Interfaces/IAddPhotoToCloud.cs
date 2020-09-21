using CloudinaryDotNet;
using GameShop.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace GameShop.Application.Interfaces
{
    public interface IAddPhotoToCloud
    {
        bool Do(Cloudinary cloudinary, PhotoForCreationDto photoForCreationDto);
    }
}