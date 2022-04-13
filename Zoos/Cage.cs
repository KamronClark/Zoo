using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using CagedItems;

namespace Zoos
{
    /// <summary>
    /// The class which represents a cage in the zoo.
    /// </summary>
    [Serializable]
    public class Cage
    {
        /// <summary>
        /// List of thing which can be caged.
        /// </summary>
        private List<ICageable> cagedItems;

        /// <summary>
        /// Type of animal the cage contains.
        /// </summary>
        private Type animalType;

        private Action<ICageable> onImageUpdate;

        /// <summary>
        /// Initializes a new instance of the Cage class.
        /// </summary>
        /// <param name="height">Height of the cage.</param>
        /// <param name="width">Width of the cage.</param>
        /// <param name="animalType">The type of animal the cage contains.</param>
        public Cage(int height, int width, Type animalType)
        {
            this.cagedItems = new List<ICageable>();
            this.animalType = animalType;
            this.Height = height;
            this.Width = width;
        }

        /// <summary>
        /// Gets the type of animal the cage contains.
        /// </summary>
        public Type AnimalType
        {
            get
            {
                // For some reason when I used the auto property it was always null.
                return this.animalType;
            }

            private set
            {
                this.animalType = value;
            }
        }

        public Action<ICageable> OnImageUpdate
        {
            get
            {
                return this.onImageUpdate;
            }

            set
            {
                this.onImageUpdate = value;
            }
        }

        /// <summary>
        /// Gets the height of the cage.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the width of the cage.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the list of caged items.
        /// </summary>
        public IEnumerable<ICageable> CagedItems
        {
            get
            {
                return this.cagedItems;
            }
        }

        /// <summary>
        /// Adds an item to the cage.
        /// </summary>
        /// <param name="cagedItem">Item to be added.</param>
        public void Add(ICageable cagedItem)
        {
            this.cagedItems.Add(cagedItem);
            cagedItem.OnImageUpdate += this.HandleImageUpdate;

            if (this.OnImageUpdate != null)
            {
                this.OnImageUpdate(cagedItem);
            }
        }

        /// <summary>
        /// Removes an item to the cage.
        /// </summary>
        /// <param name="cagedItem">Item to be removed.</param>
        public void Remove(ICageable cagedItem)
        {
            this.cagedItems.Remove(cagedItem);
            cagedItem.OnImageUpdate -= this.HandleImageUpdate;

            if (this.OnImageUpdate != null)
            {
                this.OnImageUpdate(cagedItem);
            }
        }

        public override string ToString()
        {
            string result = this.cagedItems[0].GetType() + "cage " + "(" + this.Width + " x " + this.Height + ")";

            foreach (ICageable i in this.cagedItems)
            {
                result += "\n" + i.ToString() + "(" + i.XPosition + " x " + i.YPosition + ")";
            }

            return result;
        }

        private void HandleImageUpdate(ICageable item)
        {
            if (this.OnImageUpdate != null)
            {
                this.OnImageUpdate(item);
            }
        }
    }
}
