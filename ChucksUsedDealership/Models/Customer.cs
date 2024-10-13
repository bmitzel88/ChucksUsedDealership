namespace ChucksUsedDealership.Models
{
    public class Customer
    {
        /// <summary>
        /// CustomerId is the primary key for the Customer table
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// FirstName is the first name of the customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName is the last name of the customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Address is the street address of the customer
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email is the email address of the customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// PhoneNumber is the phone number of the customer
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
