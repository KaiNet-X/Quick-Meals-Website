using QuickMeals.Models;
using System;
using System.IO;
using Xunit;

namespace Quickmeals_test
{
    public class RecipeTest
    {
        [Fact]
        public void ReturnsCorrectImageDirectory()
        {
            string PredictedDirectory = Directory.GetCurrentDirectory() + "/wwwroot/RecipeImages/";

            string RealDirectory = Recipe.Base;

            Assert.Equal(PredictedDirectory, RealDirectory);
        }
    }
}
