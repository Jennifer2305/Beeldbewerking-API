using System.Net;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Numerics;
using System.Net.Http.Headers;
using System.Text.Json;
using DogMatch_web_api.Dto;
using DogMatch_web_api.Service;

namespace DogMatch_web_api.Controllers
{

    [Route("api/beeldbewerking")]
    [ApiController]
    public class BeeldbewerkingController : ControllerBase
    {
        //Dependency injection Service
        private IBeeldbewerkingService _service;

        public BeeldbewerkingController(IBeeldbewerkingService service){
            _service = service;
        }

        [Route("{id}")]
        [HttpGet]
        //De foto wordt bewerkt met alle filters die zijn toegepast
        public IActionResult editPicture(
            [FromRoute] long id,
            [FromQuery] int[] options, 
            [FromQuery] int sizeWidth=0,
            [FromQuery] int sizeHeight=0, 
            [FromQuery] double amountOfAdjustment=0.0){
            try{
                if (_service.dogExists(id)){
                    //amountOfAdjustment wordt gebruikt voor het lichter of donkerder maken van een foto en moet een waarde tussen 0.0 en 1.0 hebben om te werken
                    if (amountOfAdjustment < 0.0 || amountOfAdjustment > 1.0)
                    {
                        return BadRequest("Je hebt geen waarde tussen 0.0 en 1.0 opgegeven voor het lichter of donkerder maken van de foto. Geef een juiste waarde op");
                    }
                    string pictureName = _service.editPicture(id, options, sizeWidth, sizeHeight, amountOfAdjustment);
                    if (pictureName == ""){
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                    string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
                    var response = new BeeldbewerkingResponseDto 
                    {
                        message = baseUrl + "/api/Beeldbewerking/return/" + pictureName, 
                        status = "Succes"
                    };
                    var jsonString = JsonSerializer.Serialize(response);
                    return Content(jsonString, "application/json");
                }
                return NotFound("Deze hond kan niet gevonden worden. Geef een andere id op.");                
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return BadRequest("Er is iets fout gegaan...");
            }
        }   

        [Route("return/{filename}")]
        [HttpGet]
        //Stuurt de bewerkte foto terug
        public IActionResult returnEditedPicture([FromRoute] string filename){
            try{
                var editedImage = _service.getEditedPicture(filename);
                if (filename.Substring(filename.Length - 3) == "jpg"){
                    return File(editedImage, "image/jpeg");
                }
                return File(editedImage, "image/png");
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return BadRequest("Er is iets fout gegaan...");
            }
        }

        [Route("DownloadAll")]
        [HttpGet]
        //Downloadt alle foto's en slaat het (lokaal) op
        public IActionResult downloadPictures(){
            try{
                _service.addPicturesToFolder();
                return Ok("Alle foto's van de database worden gedownload!"); 
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return BadRequest("Er is iets fout gegaan...");
            }
  
        }

        [Route("DeleteEditedPictures")]
        [HttpGet]
        //Verwijdert alle bewerkte foto's in de Edited Pictures folder (voor test doeleinden)
        public IActionResult deleteEditedPictures(){
            try{
                _service.deleteEditedPictures();
                return Ok("Alle bewerkte foto's zijn verwijderd");
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
                return BadRequest("Er is iets fout gegaan...");
            }
        }
    }
}