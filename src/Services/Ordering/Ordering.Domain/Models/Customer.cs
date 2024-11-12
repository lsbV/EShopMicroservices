using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class Customer : Entity<CustomerId>
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    private Customer(string name, string email) : base(CustomerId.Of(Guid.NewGuid()))
    {
        Name = name;
        Email = email;
    }
    public static Customer Create(CustomerId id, string name, string email)
    {
        //TODO: Add validation
        var customer = new Customer(name, email)
        {
            Id = id
        };
        return customer;
    }

}