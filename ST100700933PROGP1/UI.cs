using Microsoft.VisualBasic;
using ST100700933PROGP1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST100700933PROGP1
{
    class UI
    {
        private RecipeManager recipeManager;

        public UI()
        {
            recipeManager = new RecipeManager();
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to Recipe Manager!");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Add a recipe");
                Console.WriteLine("2. Remove a recipe");
                Console.WriteLine("3. Display recipe list");
                Console.WriteLine("4. Display recipe details");
                Console.WriteLine("5. Scale recipe");
                Console.WriteLine("6. Reset recipe quantities");
                Console.WriteLine("7. Clear all data");
                Console.WriteLine("8. Exit");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice! Please enter a number."); //If the incorrect input is made this message will display
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddRecipe();
                        break;
                    case 2:
                        RemoveRecipe();
                        break;
                    case 3:
                        DisplayRecipeList();
                        break;
                    case 4:
                        DisplayRecipeDetails();
                        break;
                    case 5:
                        ScaleRecipe();
                        break;
                    case 6:
                        ResetRecipeQuantities();
                        break;
                    case 7:
                        ClearAllData();
                        break;
                    case 8:
                        exit = true;
                        Console.WriteLine("Exiting Recipe Manager. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please select a valid option (1-8).");
                        break;
                }

                Console.WriteLine(); // Adds a new line 
            }
        }

        private void AddRecipe() //this code allows users  to enter the recipe name, number of ingredients, and number of steps. 
        {                        //It then prompts the user for information on each ingredient and step and adds them to the recipe.
            Console.WriteLine("Enter recipe name:");
            string name = Console.ReadLine();

            List<Ingredient> ingredients = new List<Ingredient>();
            Console.WriteLine("Enter number of ingredients:");
            int numIngredients;
            if (!int.TryParse(Console.ReadLine(), out numIngredients))
            {
                Console.WriteLine("Invalid input for number of ingredients. Please enter a valid number.");
                return;
            }

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter ingredient {i + 1} name:");
                string ingredientName = Console.ReadLine();

                Console.WriteLine($"Enter ingredient {i + 1} quantity:");
                double quantity;
                if (!double.TryParse(Console.ReadLine(), out quantity))
                {
                    Console.WriteLine("Invalid input for ingredient quantity. Please enter a valid number.");
                    return;
                }

                Console.WriteLine($"Enter ingredient {i + 1} unit of measurement:");
                string unitOfMeasurement = Console.ReadLine();

                Console.WriteLine($"Enter ingredient {i + 1} calories:");
                int calories;
                if (!int.TryParse(Console.ReadLine(), out calories))
                {
                    Console.WriteLine("Invalid input for ingredient calories. Please enter a valid number.");
                    return;
                }

                Console.WriteLine($"Enter ingredient {i + 1} food group:");
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient
                {
                    Name = ingredientName,
                    Quantity = quantity,
                    Unit = unitOfMeasurement,
                    Calories = calories,
                    FoodGroup = foodGroup
                };

                ingredients.Add(ingredient);
            }

            List<Step> steps = new List<Step>();
            Console.WriteLine("Enter number of steps:");
            int numSteps;
            if (!int.TryParse(Console.ReadLine(), out numSteps))
            {
                Console.WriteLine("Invalid input for number of steps. Please enter a valid number.");
                return;
            }

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step {i + 1} description:");
                string description = Console.ReadLine();

                Step step = new Step
                {
                    Description = description
                };

                steps.Add(step);
            }

            Recipe recipe = new Recipe(name, ingredients, steps);
            recipeManager.AddRecipe(recipe);

            Console.WriteLine($"Recipe '{name}' added successfully!");//message displayed if recipe is added
        }

        private void RemoveRecipe()// this method provides a way for users to  remove a recipe from a recipe manager by specifying the name of the recipe they wish to remove
        {                          
            Console.WriteLine("Enter recipe name to remove:");
            string name = Console.ReadLine();

            Recipe recipeToRemove = recipeManager.GetRecipeByName(name);
            if (recipeToRemove != null)
            {
                recipeManager.RemoveRecipe(recipeToRemove);
                Console.WriteLine($"Recipe '{name}' removed successfully!");//message displayed if recipe is removed 
            }
            else
            {
                Console.WriteLine($"Recipe '{name}' not found!");
            }
        }

        private void DisplayRecipeList() // this method is used to display a list of the recipe names
        {
            Console.WriteLine("Recipe list:");
            List<Recipe> recipes = recipeManager.GetRecipeList();
            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
            }
        }

        private void DisplayRecipeDetails()//this method is to control the viewing of detailed information about a specific recipe 
        {                                   //Used chatgpt to help correct the original code i made 
            Console.WriteLine("Enter recipe name to display details:");
            string name = Console.ReadLine();

            Recipe recipe = recipeManager.GetRecipeByName(name);

            if (recipe != null)
            {
                Console.WriteLine($"Recipe '{name}' details:");
                Console.WriteLine("Ingredients:");
                foreach (Ingredient ingredient in recipe.Ingredients)
                {
                    Console.WriteLine($"- {ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}, {ingredient.Calories} calories, {ingredient.FoodGroup} food group");
                }

                Console.WriteLine("Steps:");
                foreach (Step step in recipe.Steps)
                {
                    Console.WriteLine($"- {step.Description}");
                }

                Console.WriteLine($"Total calories: {recipe.TotalCalories}"); //display totoal calories 

                if (recipe.TotalCalories > 300)//display message if total calories of recipe is greater than 300 calories 
                {
                    Console.WriteLine("WARNING: Recipe exceeds 300 calories!");
                }
            }
            else
            {
                Console.WriteLine($"Recipe '{name}' not found!");
            }
        }

        private void ScaleRecipe()//this is a method designed to control the scaling of the ingredient quantities for a specified recipe
        {
            Console.WriteLine("Enter recipe name to scale:");
            string name = Console.ReadLine();

            Recipe recipe = recipeManager.GetRecipeByName(name);

            if (recipe != null)
            {
                Console.WriteLine($"Recipe '{name}' current scale factor: {recipe.ScaleFactor}");
                Console.WriteLine("Enter new scale factor (0.5, 2, or 3):");

                double newScaleFactor;
                if (!double.TryParse(Console.ReadLine(), out newScaleFactor) || (newScaleFactor != 0.5 && newScaleFactor != 2 && newScaleFactor != 3))
                {
                    Console.WriteLine("Invalid scale factor. Please enter 0.5, 2, or 3.");
                    return;
                }

                recipe.Scale(newScaleFactor);
                Console.WriteLine($"Recipe '{name}' scaled successfully!");
                DisplayRecipeDetails();
            }
            else
            {
                Console.WriteLine($"Recipe '{name}' not found!");
            }
        }

        private void ResetRecipeQuantities()//this is a method is used for resetting the ingredient quantities for a specified recipe
        {                                   //Code corrected by chat gpt 
            Console.WriteLine("Enter recipe name to reset quantities:");
            string recipeName = Console.ReadLine();
            Recipe recipe = recipeManager.GetRecipeByName(recipeName);

            if (recipe != null)
            {
                recipe.ResetQuantities();
                Console.WriteLine("Recipe quantities reset successfully!");
            }
            else
            {
                Console.WriteLine("Recipe not found!");
            }
        }

        private void ClearAllData() //this is a method that interacts with the user to confirm and then clear all recipe data
        {
            Console.WriteLine("Are you sure you want to clear all data? (Y/N)");
            string answer = Console.ReadLine().ToUpper();

            if (answer == "Y")
            {
                recipeManager.ClearRecipes();
                Console.WriteLine("All data has been cleared.");
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }
        }
    }
}