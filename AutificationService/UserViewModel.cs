using System.Net.Mail;

namespace AutificationService
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public bool FromRussia { get; set; }

        public UserViewModel(User user)
        {
            FullName = String.Concat(user.FirstName, " " , user.LastName);
        }

        public string GetFullName(string firstName , string lastName)
        {
           return String.Concat(firstName, " ", lastName);
        }

        public bool GetFromRussiaValue(string email)
        {
            MailAddress mailAddress = new MailAddress(email);
            if (mailAddress.Host.Contains(".ru"))
                return true;
            return false;
        }
    }
}
