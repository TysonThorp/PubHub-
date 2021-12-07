﻿using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Models;
using Capstone.Security;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerDao beerDao;

        public BeersController(IBeerDao _beerDao)
        {
            beerDao = _beerDao;
        }

        [HttpGet]
        public IActionResult GetAllBeers()
        {
            List<Beer> listOfBeers = beerDao.GetAllBeers();
            if (listOfBeers != null)
            {
                return Ok(listOfBeers);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{beerId}")]
        public IActionResult GetBeerById(int beerId)
        {
            Beer beer = beerDao.GetBeerById(beerId);
            if (beer != null)
            {
                return Ok(beer);
            }
            else
            {
                return NotFound();
            }
        }
    }
}