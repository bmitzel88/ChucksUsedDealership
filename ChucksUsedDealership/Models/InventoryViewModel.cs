namespace ChucksUsedDealership.Models
{
    public class InventoryViewModel
    {

        public InventoryViewModel()
        {
            Cars = new List<CarInventory>();
        }

        public InventoryViewModel(List<CarInventory> cars, int lastPage, int currPage) 
        {
            Cars = cars;
            LastPage = lastPage;
            CurrentPage = currPage;
        }

        /// <summary>
        /// The full list of cars in stock
        /// </summary>
        public List<CarInventory> Cars { get; private set; }
        
        /// <summary>
        /// The last page of the Inventory of cars
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// The current page the user is viewing
        /// </summary>
        public int CurrentPage { get; private set; }
    }
}
