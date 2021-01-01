using System;
using System.Collections.Generic;
using System.Linq;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    /// <summary>
    ///     Controller for <see cref="ApplicantController" />s.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService service;
        private readonly IStringLocalizer<ApplicantController> localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicantController"/> class.
        /// </summary>
        /// <param name="service">The configuration for the class</param>

        public ApplicantController(IApplicantService service, IStringLocalizer<ApplicantController> localizer)
        {
            this.service = service;
            this.localizer = localizer;
        } 

        /// <summary>
        ///    Gets a list of applicants.
        /// </summary>
        /// <returns><see cref="ActionResult"/></returns>
        /// <response code="200">
        /// A list of available applicants
        /// </response>
        /// <response code="404">
        /// The applicant does not exist
        /// </response>
        [HttpGet("applicants")]
        [ProducesResponseType(typeof(IList<ApplicantDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var res = service.GetApplicants();
            return res.Count() != 0 ? Ok(res) : NotFound();
        }

        /// <summary>
        ///    Gets an existing applicant.
        /// </summary>
        /// <param name="id" example="1">The id of the applicant to get.</param>
        /// <returns><see cref="ActionResult"/></returns>
        /// <response code="200">
        /// The existing applicant
        /// </response>
        /// <response code="404">
        /// The applicant does not exist
        /// </response>

        [HttpGet("applicants/{id}", Name = "GetApplicant")]
        [ProducesResponseType(typeof(ApplicantDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var res = service.GetApplicantById(id);
            return res != null ? Ok(res) : NotFound();
        }

        /// <summary>
        ///    Creates a new applicant.
        /// </summary>
        /// <param name="model">The applicant model to create</param>
        /// <returns><see cref="ActionResult"/></returns>
        /// <response code="201">
        /// Applicant successfully created
        /// </response>
        /// <response code="400">
        /// Model is invalid
        /// </response>
        /// <response code="500">
        /// An error occured
        /// </response>
        [HttpPost("applicants")]
        [ProducesResponseType(typeof(ApplicantWDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Insert([FromBody] ApplicantWDTO model)
        {
            try
            {
                var id = service.InsertApplicant(model);
                var url = Url.Link("GetApplicant", new { id = id });
                return Created(url, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. Please, check the log file for details.");
            }
        }

        /// <summary>
        ///    Updates an existing applicant.
        /// </summary>
        /// <param name="id">The applicant id to update</param>
        /// <returns><see cref="ActionResult"/></returns>
        /// <response code="200">
        /// Applicant successfully updated
        /// </response>
        /// <response code="404">
        /// Applicant cannot be found
        /// </response>
        /// <response code="500">
        /// An error occured
        /// </response>
        [HttpPut("applicants/{id}")]
        [ProducesResponseType(typeof(ApplicantWDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] ApplicantWDTO model)
        {
            try
            {
                var res = service.UpdateApplicant(id, model);

                return res != null? Ok(model) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. Please, check the log file for details.");
            }
        }

        /// <summary>
        ///    Deletes an existing applicant.
        /// </summary>
        /// <param name="id">The applicant id to delete</param>
        /// <returns><see cref="ActionResult"/></returns>
        /// <response code="204">
        /// Applicant successfully removed
        /// </response>
        /// <response code="404">
        /// Applicant cannot be found
        /// </response>
        /// <response code="500">
        /// An error occured
        /// </response>
        [HttpDelete("applicants/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                var res = service.DeleteApplicant(id);

                return res ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. Please, check the log file for details.");
            }
        }
    }
}
