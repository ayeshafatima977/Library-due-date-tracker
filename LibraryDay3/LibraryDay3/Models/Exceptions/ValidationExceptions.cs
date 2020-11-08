using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryDay3.Models.Exceptions
{
    public class ValidationExceptions:Exception
    {
        // A list of exceptions shown as one Exception
        public List<Exception> newExceptions { get; set; } = new List<Exception>();

        // Override our current message To get a List of Error Messages togather.
        public override string Message => $"There are {newExceptions.Count} exceptions.";

        //Default Exception just for case of override
        public ValidationExceptions() : base()
        { }

        public ValidationExceptions(string message) : base("Please view ValidationExceptions for details.")
        {
            // When we construct this exception with a message, it gets added to the newexceptions list.
            newExceptions.Add(new Exception(message));
        }
    }
}