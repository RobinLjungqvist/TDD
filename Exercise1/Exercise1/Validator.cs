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
            Regex regex = new Regex(@"^([^.\d\-]+)@([^.\d\-]+)((\.(\D){2,3})+)$");
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

        public bool ValidateWithMailAdressClass(string email)
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