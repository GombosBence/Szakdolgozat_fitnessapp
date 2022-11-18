namespace azuretest
{
    public class MyMealsModel
    {
        

      
        public MyMealsModel(int _id ,string _foodName, int calories2, int protein2, int carbohydrate2, int fat2, double quantityInGrams, DateTime date)
        {
            id = _id;
            foodName = _foodName;
            calories = calories2;
            protein = protein2;
            carbohydrate = carbohydrate2;
            fat = fat2;
            quantity = quantityInGrams;
            mealDate = date;
        }

        public int id { get; set; }
        public string foodName { get; set; }
        public int calories { get; set; }
        public int protein { get; set; }
        public int fat { get; set; }
        public int carbohydrate { get; set; }

        public double quantity { get; set; }

        public DateTime mealDate { get; set; }
    }
}
