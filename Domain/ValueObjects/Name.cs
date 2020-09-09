using Domain.Infrastucture;

namespace Domain.ValueObjects
{

    /// <summary>
    /// Value object to store name of a person
    /// </summary>
    public class Name
    {
        private string firstName;
        private string lastName;

        /// <summary>
        /// First name of the person
        /// </summary>
        public string FirstName
        {
            get { return firstName.ToInitialCapital(); }
            set { firstName = value; }
        }

        /// <summary>
        /// Last name / surname of the person
        /// </summary>
        public string LastName
        {
            get { return lastName.ToInitialCapital(); }
            set { lastName = value; }
        }
        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
