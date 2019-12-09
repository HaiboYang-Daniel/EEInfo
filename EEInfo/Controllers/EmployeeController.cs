using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EEInfo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EEInfo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string _baseUrl = "http://localhost:6699/";
        private readonly string _apiUrl = "api/employees/";
        // GET: Employee
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(_apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var employees = await response.Content.ReadAsAsync<IEnumerable<Employee>>();

                    return View(employees);
                }
            }
            return View();
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(_apiUrl + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var employee = await response.Content.ReadAsAsync<Employee>();

                    HttpResponseMessage responseTask = await client.GetAsync(_apiUrl + id.ToString() + "/EmployeeTasks");
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var employeeTasks = await responseTask.Content.ReadAsAsync<ICollection<EmployeeTask>>();
                        employee.EmployeeTasks = employeeTasks;
                    }

                    return View(employee);
                }
            }
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new Employee
            {
                FirstName = string.Empty,
                LastName = string.Empty,
                HiredDate = DateTime.Now
            });
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP POST
                HttpResponseMessage response = await client.PostAsJsonAsync<Employee>(_apiUrl, employee);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(_apiUrl + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var employee = await response.Content.ReadAsAsync<Employee>();

                    HttpResponseMessage responseTask = await client.GetAsync(_apiUrl + id.ToString() + "/EmployeeTasks");
                    if (responseTask.IsSuccessStatusCode)
                    {
                        var employeeTasks = await responseTask.Content.ReadAsAsync<ICollection<EmployeeTask>>();
                        employee.EmployeeTasks = employeeTasks;
                    }

                    return View(employee);
                }
            }
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP PUT
                HttpResponseMessage response = await client.PutAsJsonAsync<Employee>(_apiUrl + id.ToString(), employee);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(_apiUrl + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var employee = await response.Content.ReadAsAsync<Employee>();

                    return View(employee);
                }
            }
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP DELETE
                HttpResponseMessage response = await client.DeleteAsync(_apiUrl + id.ToString());
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        // GET: Employee/CreateEmployeeTask
        public ActionResult CreateEmployeeTask(Guid id)
        {
            return RedirectToAction("Create", "EmployeeTask", new { employeeId = id });
        }
        // GET: Employee/EditEmployeeTask
        public ActionResult EditEmployeeTask(Guid id)
        {
            return RedirectToAction("Edit", "EmployeeTask", new { id = id });
        }
        // GET: Employee/DetailsEmployeeTask
        public ActionResult DetailsEmployeeTask(Guid id)
        {
            return RedirectToAction("Details", "EmployeeTask", new { id = id });
        }
        // GET: Employee/DeleteEmployeeTask
        public ActionResult DeleteEmployeeTask(Guid id)
        {
            return RedirectToAction("Delete", "EmployeeTask", new { id = id });
        }
    }
}