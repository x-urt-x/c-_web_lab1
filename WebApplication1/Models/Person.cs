using Dapper;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Person
    {
        private static string connectoinString = "Server=localhost;Port=3306;Database=lab1;Uid=root;Pwd=password";
        public string Date { get; set; }
        [Required(ErrorMessage = "no name")]
        [StringLength(45, ErrorMessage = "too long name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "no email")]
        [EmailAddress(ErrorMessage = "bad email")]
        public string Email { get ; set; }
        [StringLength(15, ErrorMessage = "too long phone")]
        public string? Phone { get; set; }
        public string? Review { get; set; }
		[Required(ErrorMessage = "no mark")]
		[Range(1,5, ErrorMessage = "bad mark range")]
        public int Mark { get; set; }
        public Person(){ Date = DateTime.Now.Date.ToLongDateString(); }
        public Person(string name, string emale, string? phone, string? review, int mark) 
            => (Name, Email, Phone, Review, Mark) = 
            (name, emale, phone, review, mark);

        public static async Task CreateAsync(Person person) => await CreateAsync(person.Name, person.Email, person.Phone, person.Review, person.Mark, person.Date);
        public static async Task CreateAsync(string name, string email, string? phone, string? review, int mark, string date)
        {
            using (var connection = new MySqlConnection(connectoinString))
            {
                connection.Open();

                var sql = @"
INSERT INTO `lab1`.`students` (`Name`, `Email`, `Phone`, `Review`, `Mark`, `Date`) 
VALUES (@Name, @Email, @Phone, @Review, @Mark, @Date);
";

                await connection.ExecuteAsync(sql, new { Name= name , Email = email, Phone = phone, Review = review, Mark = mark, Date = date});
            }
        }
        public static async Task<List<Person>> GetAllAsync()
        {
            using (var connection = new MySqlConnection(connectoinString))
            {
                connection.Open();

                var sql = @"SELECT Date, Name, Mark from students";

                List<Person> persons = (await connection.QueryAsync<Person>(sql)).ToList();

                return persons;
            }
        }
    }
}