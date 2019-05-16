﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechTest.Repositories;
using TechTest.Repositories.Models;

namespace TechTest.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        public PeopleController(IPersonRepository personRepository)
        {
            this.PersonRepository = personRepository;
        }

        private IPersonRepository PersonRepository { get; }

        [HttpGet]
        public IActionResult GetAll()
        {

            // TODO: Step 1
            //
            // Implement a JSON endpoint that returns the full list
            // of people from the PeopleRepository. If there are zero
            // people returned from PeopleRepository then an empty
            // JSON array should be returned.

            IEnumerable<Person> AllPeople = PersonRepository.GetAll();
            if (AllPeople != null && AllPeople.Any())
            {
                return new JsonResult(AllPeople);
            }
            else
            {
                return new JsonResult(new Array[0]);
            }


        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // TODO: Step 2
            //
            // Implement a JSON endpoint that returns a single person
            // from the PeopleRepository based on the id parameter.
            // If null is returned from the PeopleRepository with
            // the supplied id then a NotFound should be returned.

            Person PersonById = PersonRepository.Get(id);
            if (PersonById != null)
            {
                return new JsonResult(PersonById);
            }
            else
            {
                return new NotFoundResult();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PersonUpdate personUpdate)
        {
            // TODO: Step 3
            //
            // Implement an endpoint that receives a JSON object to
            // update a person using the PeopleRepository based on
            // the id parameter. Once the person has been successfully
            // updated, the person should be returned from the endpoint.
            // If null is returned from the PeopleRepository then a
            // NotFound should be returned.

            Person personByID = PersonRepository.Get(id);
            if (personByID != null)
            {
                personByID.Authorised = personUpdate.Authorised;
                personByID.Enabled = personUpdate.Enabled;
                personByID.Colours = personUpdate.Colours;

                return new JsonResult(PersonRepository.Update(personByID));
            }
            else
            {
                return new NotFoundResult();
            }


        }
    }
}