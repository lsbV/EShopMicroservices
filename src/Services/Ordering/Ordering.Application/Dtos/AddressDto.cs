namespace Ordering.Application.Dtos;

public record AddressDto(
    string FirstName,
    string LastName,
    string Street,
    string City,
    string State,
    string Country,
    string ZipCode);