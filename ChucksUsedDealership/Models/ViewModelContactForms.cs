namespace ChucksUsedDealership.Models
{
    public class ViewModelContactForms
    {
        /// <summary>
        /// List of cars to display on the page
        /// </summary>
        public List<ContactForm> ContactForms { get; set; }

        /// <summary>
        /// Stores the current page number
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Stores the total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Stores the number of cars to display per page
        /// </summary>
        public int PageSize { get; set; }
    }
}
