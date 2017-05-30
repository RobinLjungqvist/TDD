using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StringCalculatorApp
{
    [Serializable]
    public class NegativeNumberNotAllowedException : Exception
    {
        public List<int> NegativeNumbers { get; set; }
        public NegativeNumberNotAllowedException()
        {
        }

        public NegativeNumberNotAllowedException(string message) : base(message)
        {
        }
        public NegativeNumberNotAllowedException(string message, List<int> negativeNumbers) : base(message)
        {
            this.NegativeNumbers = negativeNumbers;
        }

        public NegativeNumberNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NegativeNumberNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}