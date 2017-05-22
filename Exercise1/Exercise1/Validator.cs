using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using ValidationEngine.Exceptions;

namespace ValidationEngine
{
    public class Validator
    {
        public Validator()
        {
        }

        public bool ValidateEmailAdress(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            else
            {
                throw new InvalidEmailException();
            }
        
        }

        public bool ValidateWithMailClass(string email)
        {
            try
            {
                var mailAdress = new MailAddress(email);

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidEmailException("Invalid Email", ex);
            }
        }
    }
}