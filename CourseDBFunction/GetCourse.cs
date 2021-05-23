using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CourseDBFunction
{
    public static class GetCourses
    {
        [FunctionName("GetCourses")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<Course> _lst = new List<Course>();
            string _connection_string = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");
            string _statement = "select CourseID, CourseName, Rating from Course";
            SqlConnection _connection = new SqlConnection(_connection_string);
            _connection.Open();
            SqlCommand _sqlCommand = new SqlCommand(_statement, _connection);

            using (SqlDataReader _reader = _sqlCommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Course _course = new Course()
                    {
                        CourseID = _reader.GetInt32(0),
                        CourseName = _reader.GetString(1),
                        Rating = _reader.GetDecimal(2)
                    };
                    _lst.Add(_course);
                }
            }
            _connection.Close();
            return new OkObjectResult(_lst);
        }
    }
}
