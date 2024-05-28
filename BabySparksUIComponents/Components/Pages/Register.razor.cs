using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabySparksUIComponents.Components.Pages
{
    public partial class Register : ComponentBase
    {
        private string selectedValue;

        private void OnNextClick()
        {
            if (!string.IsNullOrEmpty(selectedValue))
            {
                // Handle the selected value (e.g., navigate to another page or display a message)
                Console.WriteLine($"Selected value: {selectedValue}");
            }
            else
            {
                Console.WriteLine("No option selected.");
            }
        }
        private void HandleSelection(ChangeEventArgs e)
        {
            selectedValue = e.Value.ToString();
        }
    }
}
