using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Icon;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

namespace SmartEnergyAPI.Controllers
{
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {

        private readonly ISettingsService _settingService;
        private readonly IIconService _iconService;

        public SettingsController(ISettingsService settingService, IIconService iconService)
        {
            _settingService = settingService;
            _iconService = iconService;
        }

        [HttpGet("last")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SettingsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLastSettings()
        {
            SettingsDto returnValue = _settingService.GetLastSettings();
            if(returnValue == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(returnValue);
            }
           
        }


        [HttpGet("default")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SettingsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDefaultSettings()
        {
            SettingsDto returnValue = _settingService.GetDefaultSettings();
            if (returnValue == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(returnValue);
            }

        }


        [HttpGet("icons")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<IconDto>))]
        public IActionResult GetAllIcons()
        {
            return Ok(_iconService.GetAllIcons());

        }

        [HttpGet("icons/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IconDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetIconById(int id)
        {
            IconDto returnVal = _iconService.GetIconById(id);

            if (returnVal == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(returnVal);
            }

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult ResetSettings()
        {
            _settingService.ResetToDefault();

            return Ok();

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SettingsDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateSettings(int id, [FromBody] SettingsDto settings)
        {
            if (id != settings.ID)
                return BadRequest();

            try
            {
                SettingsDto modified = _settingService.UpdateSettings(settings);
                return Ok(modified);
            }catch(SettingsNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{settingsId}/icons/{iconId}/add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AddIconToSettings(int settingsId, int iconId)
        {
            try
            {
                _iconService.AddIconToSettings(iconId, settingsId);
                return Ok();
            }
            catch (SettingsNotFoundException)
            {
                return BadRequest();
            }
            catch (IconNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{settingsId}/icons/{iconId}/remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveIconFromSettings(int settingsId, int iconId)
        {
            try
            {
                _iconService.RemoveIconFromSettings(iconId, settingsId);
                return Ok();
            }
            catch (SettingsNotFoundException)
            {
                return BadRequest();
            }
            catch (IconNotFoundException)
            {
                return NotFound();
            }
        }




    }
}
