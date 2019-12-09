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
    public class EmployeeTaskController : Controller
    {
        private readonly string _baseUrl = "http://localhost:6699/";
        private readonly string _apiUrl = "api/EmployeeTasks/";
        // GET: EmployeeTask
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
                    var EmployeeTasks = await response.Content.ReadAsAsync<IEnumerable<EmployeeTask>>();

                    return View(EmployeeTasks);
                }
            }
            return View();
        }

        // GET: EmployeeTask/Details/5
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
                    var employeeTask = await response.Content.ReadAsAsync<EmployeeTask>();

                    HttpResponseMessage responseEE = await client.GetAsync("api/Employees/" + employeeTask.EmployeeId.ToString());
                    if (responseEE.IsSuccessStatusCode)
                    {
                        var employee = await responseEE.Content.ReadAsAsync<Employee>();
                        employeeTask.Employee = employee;
                    }

                    return View(employeeTask);
                }
            }
            return View();
        }

        // GET: EmployeeTask/Create
        public ActionResult Create(Guid employeeId)
        {
            return View(new EmployeeTask
            {
                EmployeeId = employeeId,
                TaskName = string.Empty,
                StartTime = DateTime.Now,
                Deadline = DateTime.Now.AddDays(7)
            });
        }

        // POST: EmployeeTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeTask employeeTask)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP POST
                HttpResponseMessage response = await client.PostAsJsonAsync<EmployeeTask>(_apiUrl, employeeTask);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Edit", "Employee", new { id = employeeTask.EmployeeId });
                }
            }
            return View();
        }

        // GET: EmployeeTask/Edit/5
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
                    var employeeTask = await response.Content.ReadAsAsync<EmployeeTask>();

                    HttpResponseMessage responseEE = await client.GetAsync("api/Employees/" + employeeTask.EmployeeId.ToString());
                    if (responseEE.IsSuccessStatusCode)
                    {
                        var employee = await responseEE.Content.ReadAsAsync<Employee>();
                        employeeTask.Employee = employee;
                    }

                    return View(employeeTask);
                }
            }
            return View();
        }

        // POST: EmployeeTask/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EmployeeTask employeeTask)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP PUT
                HttpResponseMessage response = await client.PutAsJsonAsync<EmployeeTask>(_apiUrl + id.ToString(), employeeTask);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Edit", "Employee", new { id = employeeTask.EmployeeId });
                }
            }
            return View();
        }

        // GET: EmployeeTask/Delete/5
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
                    var employeeTask = await response.Content.ReadAsAsync<EmployeeTask>();

                    HttpResponseMessage responseEE = await client.GetAsync("api/Employees/" + employeeTask.EmployeeId.ToString());
                    if (responseEE.IsSuccessStatusCode)
                    {
                        var employee = await responseEE.Content.ReadAsAsync<Employee>();
                        employeeTask.Employee = employee;
                    }

                    return View(employeeTask);
                }
            }
            return View();
        }

        // POST: EmployeeTask/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, EmployeeTask employeeTask)
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
                    var eeTask = await response.Content.ReadAsAsync<EmployeeTask>();
                    return RedirectToAction("Edit", "Employee", new { id = eeTask.EmployeeId });
                }
            }
            return View();
        }
    }
}