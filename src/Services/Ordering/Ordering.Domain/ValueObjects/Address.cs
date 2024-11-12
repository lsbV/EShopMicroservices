namespace Ordering.Domain.ValueObjects;

public class Address
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }

    protected Address()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Street = string.Empty;
        City = string.Empty;
        State = string.Empty;
        Country = string.Empty;
        ZipCode = string.Empty;
    }

    private Address(string firstName, string lastName, string street, string city, string state, string country, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName, string lastName, string street, string city, string state, string country, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(street);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        ArgumentException.ThrowIfNullOrWhiteSpace(state);
        ArgumentException.ThrowIfNullOrWhiteSpace(country);
        ArgumentException.ThrowIfNullOrWhiteSpace(zipCode);

        return new Address(firstName, lastName, street, city, state, country, zipCode);
    }
}