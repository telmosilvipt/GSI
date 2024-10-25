using Azure;
using GSI.WebApi.AD;
using GSI.WebApi.DB;
using GSI.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GSI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GSIController : ControllerBase
    {
        public GSIController()
        {
        }

        [HttpGet(Name = nameof(GetEmployeesFromAd))]
        public GetEmployeesResponse GetEmployeesFromAd()
        {
            var adUsers = AdHelper.GetUsers();

            var response = new GetEmployeesResponse();

            foreach (var employee in adUsers)
            {
                response.Items.Add(new GetEmployeesResponse.GetEmployeesResponseItem
                {
                    Name = employee.Name,
                });
            }

            return response;
        }

        [HttpGet(Name = nameof(GetEmployeesFromBd))]
        public GetEmployeesResponse GetEmployeesFromBd()
        {
            using (var db = new MainDbContext())
            {
                var employees = db.Employees;

                var response = new GetEmployeesResponse();

                foreach (var employee in employees)
                {
                    response.Items.Add(new GetEmployeesResponse.GetEmployeesResponseItem
                    {
                        Name = employee.Name,
                    });
                }

                return response;
            }
        }

        [HttpPost(Name = nameof(SyncDbFromAd))]
        public void SyncDbFromAd()
        {
            using (var db = new MainDbContext())
            {
                var users = AdHelper.GetUsers();

                foreach (var user in users)
                {
                    var employee = db.Employees.FirstOrDefault(x => x.Id == user.Id);
                    if (employee == null)
                    {
                        employee = new Employee();
                        employee.Id = user.Id;
                        db.Employees.Add(employee);
                    }
                    employee.Name = user.Name;
                    employee.Department = user.Department;
                    employee.AccountExpiresDate = user.AccountExpires;
                    employee.LastLogonDate = user.LastLogon;
                }

                db.SaveChanges();
            }
        }
    }
}
