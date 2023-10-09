using api_cinema_challenge.Models.Customer;
using human.repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using static System.Net.WebRequestMethods;

namespace cinema.endpoints
{
    public static class CustomerApi
    {
      

        public static void ConfigureCustomer(this WebApplication app)
        {
            app.MapPost("/customers", Insert);
            app.MapGet("/customers", Get);
            app.MapPut("/customers/{id}", Update);
            app.MapDelete("/customers/{id}", Delete);
       
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> Delete(int id, IDatabaseRepository<Customer> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (!service.Table.Any(x => x.Id == id)) return Results.NotFound();
                    var customer = service.GetById(id);
                    
                    service.Delete(id);
                    service.Save();
                    return Results.Ok(customer);
                });

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> Update(int id, CustomerPost customerPost, IDatabaseRepository<Customer> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (customerPost == null) return Results.NotFound();
                    if (!service.Table.Any(x => x.Id == id)) return Results.NotFound();
                    
                    var customer = service.GetById(id);
                    customer.email = customerPost.email;
                    customer.name = customerPost.name;
                    customer.phone = customerPost.phone;
                    customer.updatedAt = DateTime.UtcNow;
                    
                    service.Update(customer);
                    service.Save();
                    return Results.Created($"https://localhost:7195/customer/{customer.Id}", customer);
                });

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> Get(IDatabaseRepository<Customer> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return Results.Ok(service.GetAll());
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> Insert(CustomerPost customerDetails, IDatabaseRepository<Customer> service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (customerDetails == null) return Results.NotFound();
                    Customer newCustomer = new Customer();
                    newCustomer.email = customerDetails.email;
                    newCustomer.name = customerDetails.name;
                    newCustomer.phone = customerDetails.phone;  
                    newCustomer.createdAt = DateTime.UtcNow;
                    newCustomer.updatedAt = DateTime.UtcNow;
                    service.Insert(newCustomer);
                    service.Save();
                    return Results.Created($"https://localhost:7195/customer/{newCustomer.Id}", newCustomer);
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }         
    }
}
