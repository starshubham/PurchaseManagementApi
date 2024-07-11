using Common.Dto;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PurchaseManagementApi.DAL;
using System;

namespace PurchaseManagementApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            var persons = await _context.Persons.ToListAsync();

            var userList = (from user in users
                            join person in persons
                            on user.personId equals person.id
                            select new UserDto
                            {
                                UserId = user.id,
                                Email = user.email,
                                Password = user.password,
                                ConfirmPassword = user.password,
                                PersonId = person.id,

                                FirstName = person.firstName,
                                MiddleName = person.middleName,
                                LastName = person.lastName,
                                FullName = string.Join(" ", new[] { person.firstName, person.middleName, person.lastName }
                                            .Where(name => !string.IsNullOrWhiteSpace(name))),
                                DOB = person.dateOfBirth,
                                Gender = person.gender,
                                Salutation = person.salutation,
                                MobNo = person.mobNo,
                                Address = person.address
                            }).ToList();

            return Ok(userList);
        }

        [HttpGet("getUser/{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.id == id);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.personId == id);

            if ( person != null && user != null)
            {
                string fullName = string.Join(" ", new[] { person.firstName, person.middleName, person.lastName }
                                        .Where(name => !string.IsNullOrWhiteSpace(name)));

                var getUser = new UserDto
                {
                    UserId = user.id,
                    Email = user.email,
                    Password = user.password,
                    ConfirmPassword = user.password,
                    PersonId = user.personId,

                    FirstName = person.firstName,
                    MiddleName = person.middleName,
                    LastName = person.lastName,
                    FullName = fullName,
                    DOB = person.dateOfBirth,
                    Gender = person.gender,
                    Salutation = person.salutation,
                    MobNo = person.mobNo,
                    Address = person.address
                };
                return Ok(getUser);
            }

            return NotFound(); // Return a 404 Not Found response if either person or user is null
        }

        [HttpPost("createUser")]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var personData = new Person
            {
                firstName = user.FirstName,
                middleName = user.MiddleName,
                lastName = user.LastName,
                dateOfBirth = user.DOB,
                gender = user.Gender,
                salutation = user.Salutation,
                mobNo = user.MobNo,
                address = user.Address
            };

            _context.Add(personData);
            await _context.SaveChangesAsync(); // Ensure that personData is saved before using its ID
            var personResponse = CreatedAtAction("GetPerson", new { id = personData.id }, personData);

            var userData = new User
            {
                email = user.Email,
                password = user.Password,
                personId = personData.id, // Use the newly created person's ID
            };

            _context.Add(userData);
            await _context.SaveChangesAsync();

            // Return the UserDto with the newly created user's details
            var createdUser = new UserDto
            {
                UserId = userData.id,
                Email = userData.email,
                Password = userData.password,
                ConfirmPassword = userData.password,
                PersonId = userData.personId,
                FirstName = personData.firstName,
                MiddleName = personData.middleName,
                LastName = personData.lastName,
                FullName = string.Join(" ", new[] {personData.firstName, personData.middleName, personData.lastName}
                                        .Where(name => !string.IsNullOrWhiteSpace(name))),
                DOB = personData.dateOfBirth,
                Gender = personData.gender,
                Salutation = personData.salutation,
                MobNo = personData.mobNo,
                Address = personData.address
            };

            return CreatedAtAction("GetUser", new {id = createdUser.PersonId}, createdUser);
        }

        [HttpPut("updateUser/{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserDto user)
        {
            if (id != user.PersonId)
            {
                return BadRequest();
            }

            if(PersonExists(id))
            {
                var person = await _context.Persons.FirstOrDefaultAsync(p => p.id == id);
                if (person == null)
                {
                    return NotFound();
                }

                person.firstName = user.FirstName;
                person.middleName = user.MiddleName;
                person.lastName = user.LastName;
                person.dateOfBirth = user.DOB;
                person.gender = user.Gender;
                person.salutation = user.Salutation;
                person.mobNo = user.MobNo;
                person.address = user.Address;

                _context.Entry(person).State = EntityState.Modified;

                var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.personId == id);
                if (userEntity == null)
                {
                    return NotFound();
                }

                userEntity.email = user.Email;
                userEntity.password = user.Password;

                _context.Entry(userEntity).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!PersonExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var person = _context.Persons.Where(x => x.id == id).FirstOrDefault();
            var user = _context.Users.Where(x => x.personId == id).FirstOrDefault();

            if (user == null || person == null)
            {
                return NotFound();
            }
            else
            {
                if(user != null)
                {
                    _context.Remove(user);
                    await _context.SaveChangesAsync();
                }

                if (person != null)
                {
                    _context.Remove(person);
                    await _context.SaveChangesAsync();
                }

                return NoContent();
            }
            
        }

        private bool PersonExists(int personId)
        {
            return _context.Persons.Any(x => x.id == personId);
        }
    }
}
