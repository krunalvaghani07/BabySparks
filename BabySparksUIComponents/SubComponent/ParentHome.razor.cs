using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BabySparksUIComponents.SubComponent
{
    public partial class ParentHome : ComponentBase
    {
        TabPosition tabPosition = TabPosition.Top;
        void OnChange(int index)
        {
            Console.WriteLine($"Tab with index {index} was selected.");
        }
    }
}
