using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiVisualisation
{
    public class GridObject
    {
        // Can the robot pass through this object?
        public bool IsInteractable { get; set; }
        // The char to display the object
        public string Name { get; set; }
        public GridObject(string name, bool isInteractable) 
        {
            this.IsInteractable = isInteractable; 
            this.Name = name;
        }
    }
}
