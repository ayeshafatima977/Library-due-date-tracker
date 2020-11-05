using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDay3.Models.Exceptions
{
    public class ValidationException:Exception
    {
        // A list of exceptions shown as one Exception
        public List<Exception> newExceptions { get; set; } = new List<Exception>();

        // Override our message with a summary.
        public override string Message => $"There are {newExceptions.Count} exceptions.";

        // When we construct this exception without a messsage, we get an empty sub-list which we can populate.
        public ValidationException() : base()
        { }

        public ValidationException(string message) : base("Please view ValidationExceptions for details.")
        {
            // When we construct this exception with a message, it gets added to the newexceptions list.
            newExceptions.Add(new Exception(message));
        }
    }
}