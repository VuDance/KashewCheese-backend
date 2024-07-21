using AutoMapper;
using KashewCheese.API.Common;
using KashewCheese.Application.DTO;
using KashewCheese.Application.Services.Files.Commands.UploadImage;
using KashewCheese.Application.Services.Files.Commands.UploadImages;
using KashewCheese.Contracts.Images;
using KashewCheese.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace KashewCheese.API.Controllers
{
    public class FileController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public FileController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("upload-file")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var fileUploadDto = new FileUploadDto
            {
                FileStream = file.OpenReadStream(),
                FileName = file.FileName,
                ContentType = file.ContentType
            };

            var command = new UploadImageCommand(fileUploadDto);
            var fileUrl = await _mediator.Send(command);

            var response=_mapper.Map<ImageResponse>(fileUrl);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImages(IFormFileCollection files)
        {
            if(files == null || files.Count == 0)
            {
                return BadRequest("No files upload.");
            }
            var filesUploadDto=new Collection<FileUploadDto>();
            foreach (var file in files)
            {
                if(file.Length > 0)
                {
                    var f = new FileUploadDto()
                    {
                        FileName = file.FileName,
                        ContentType = file.ContentType,
                        FileStream = file.OpenReadStream(),
                    };
                    filesUploadDto.Add(f);
                }
            }
            var command=new UploadImagesCommand(filesUploadDto);
            var uploadFileRes=await _mediator.Send(command);
            return Ok(uploadFileRes);
        }
    }
}
