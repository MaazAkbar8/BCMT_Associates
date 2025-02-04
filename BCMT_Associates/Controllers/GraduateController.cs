﻿using BCMT_Associates.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BCMT_Associates.Controllers
{
    public class GraduateController : Controller
    {
        private readonly ILogger<GraduateController> _logger;
        private static List<Graduate> _graduates = new List<Graduate>();

        public GraduateController(ILogger<GraduateController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_graduates);
        }

        [HttpPost]
        public IActionResult AddGraduate([FromBody] Graduate graduate)
        {
            if (graduate == null)
            {
                return BadRequest("Invalid graduate data.");
            }

            try
            {
                graduate.Id = _graduates.Any() ? _graduates.Max(g => g.Id) + 1 : 1;
                _graduates.Add(graduate);

                _logger.LogInformation($"Graduate added: {graduate.Id} - {graduate.Name}");
                return Ok(graduate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding graduate: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public IActionResult EditGraduate([FromBody] Graduate graduate)
        {
            var existingGraduate = _graduates.FirstOrDefault(g => g.Id == graduate.Id);

            if (existingGraduate == null)
            {
                return NotFound("Graduate not found.");
            }

            try
            {
                existingGraduate.Image = graduate.Image;
                existingGraduate.Name = graduate.Name;
                existingGraduate.EnrollmentDate = graduate.EnrollmentDate;
                existingGraduate.Course = graduate.Course;
                existingGraduate.Location = graduate.Location;
                existingGraduate.Phone = graduate.Phone;
                existingGraduate.Email = graduate.Email;
                existingGraduate.CertificationNumber = graduate.CertificationNumber;
                existingGraduate.CNIC = graduate.CNIC;
                existingGraduate.Address = graduate.Address;
                existingGraduate.EmergencyContact = graduate.EmergencyContact;

                _logger.LogInformation($"Graduate edited: {graduate.Id}");
                return Json(existingGraduate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error editing graduate: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public IActionResult DeleteGraduate(int id)
        {
            var graduate = _graduates.FirstOrDefault(g => g.Id == id);

            if (graduate == null)
            {
                return Json(new { success = false, message = "Graduate not found." });
            }

            try
            {
                _graduates.Remove(graduate);
                _logger.LogInformation($"Graduate deleted: {id}");
                return Json(new { success = true, message = "Graduate deleted successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting graduate: {ex.Message}");
                return Json(new { success = false, message = "An error occurred while deleting the graduate." });
            }
        }

        public IActionResult GetGraduate(int id)
        {
            var graduate = _graduates.FirstOrDefault(g => g.Id == id);

            if (graduate == null)
            {
                return NotFound("Graduate not found.");
            }

            return Json(graduate);
        }
    }
}
