using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions;

public class ValidationAppException(Dictionary<string, string[]> errors)
    : Exception("One or more validation failures have occurred.")
{
    public Dictionary<string, string[]> Errors { get; } = errors;

    public static void ThrowIfError(Dictionary<string, string[]> errors)
    {
        if (errors.Count > 0)
        {
            throw new ValidationAppException(errors);
        }
    }

}