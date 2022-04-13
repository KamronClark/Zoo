using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using CagedItems;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    [Serializable]
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        /// <summary>
        /// Determines random sequences within the animal class.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// Gender of the animal.
        /// </summary>
        private Gender gender;

        private List<Animal> children;

        [NonSerialized]
        private Action<Animal> onTextChange;

        [NonSerialized]
        private Action<IReproducer> onPregnant;

        /// <summary>
        /// Timer which determines how often the animal moves.
        /// </summary>
        [NonSerialized]
        private Timer moveTimer;

        [NonSerialized]
        private Timer hungerTimer;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">Gender of the animal.</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.name = name;
            this.weight = weight;
            this.gender = gender;
            
            this.XPositionMax = 800;
            this.YPositionMax = 400;
            this.MoveDistance = random.Next(5, 16);
            this.XPosition = random.Next(1, this.XPositionMax += 1);
            this.YPosition = random.Next(1, this.YPositionMax += 1);            
            this.XDirection = random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;

            this.CreateTimers();

            this.children = new List<Animal>();
        }

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">Name of the animal.</param>
        /// <param name="weight">Weight of the animal.</param>
        /// <param name="gender">Gender of the animal.</param>
        public Animal(string name, double weight, Gender gender)
            : this(name, 0, weight, gender)
        {
        }

        /// <summary>
        /// Gets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }
        }

        public IEnumerable<Animal> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    this.name = value;

                    if (this.OnTextChange != null)
                    {
                        this.OnTextChange(this);
                    }
                }
                else
                {
                    throw new FormatException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the animal's weight (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value >= 0 && value <= 1000)
                {
                    this.weight = value;

                    if (this.OnTextChange != null)
                    {
                        this.OnTextChange(this);
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the age of the animal.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value >= 0 && value <= 100)
                {
                    this.age = value;
                    
                    if (this.OnTextChange != null)
                    {
                        this.OnTextChange(this);
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }                           
            }
        }

        /// <summary>
        /// Gets or sets the gender of the animal.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        public Action<Animal> OnTextChange
        {
            get
            {
                return this.onTextChange;
            }

            set
            {
                this.onTextChange = value;
            }
        }

        public Action<IReproducer> OnPregnant
        {
            get
            {
                return this.onPregnant;
            }

            set
            {
                this.onPregnant = value;
            }
        }

        /// <summary>
        /// Gets or sets the distance the animal will move.
        /// </summary>
        public int MoveDistance { get; set; }

        /// <summary>
        /// Gets or sets the x direction the animal will move.
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets the x position of the animal.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets the maximum x position of the animal.
        /// </summary>
        public int XPositionMax { get; set; }

        /// <summary>
        /// Gets or sets the y direction the animal will move.
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Gets or sets the y position of the animal.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets or sets the maximum y position of the animal.
        /// </summary>
        public int YPositionMax { get; set; }

        public IMoveBehavior MoveBehavior { get; set; }

        public IEatBehavior EatBehavior { get; set; }

        public IBirthBehavior BirthBehavior { get; set; }

        /// <summary>
        /// Gets the display size of the animal.
        /// </summary>
        public virtual double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 0.5 : 1.0;
            }
        }

        /// <summary>
        /// Gets the animal resource key.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return this.GetType().Name + (this.Age == 0 ? "Baby" : "Adult");
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public abstract double WeightGainPercentage
        {
            get;
        }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        public double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            set
            {
                this.babyWeightPercentage = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.moveTimer.Enabled;
            }

            set
            {
                this.moveTimer.Enabled = value;
            }
        }

        public HungerState HungerState { get; set; }

        public Action OnHunger { get; set; }

        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// Converts the animal type enum into a generic Type.
        /// </summary>
        /// <param name="animalType">The animal type.</param>
        /// <returns>Returns the animal's converted type.</returns>
        public static Type ConvertAnimalTypetoType(AnimalType animalType)
        {
            Type type = null;

            switch (animalType)
            {
                case AnimalType.Chimpanzee:
                    type = typeof(Chimpanzee);
                    break;

                case AnimalType.Dingo:
                    type = typeof(Dingo);
                    break;

                case AnimalType.Eagle:
                    type = typeof(Eagle);
                    break;

                case AnimalType.Hummingbird:
                    type = typeof(Hummingbird);
                    break;

                case AnimalType.Kangaroo:
                    type = typeof(Kangaroo);
                    break;

                case AnimalType.Ostrich:
                    type = typeof(Ostrich);
                    break;

                case AnimalType.Platypus:
                    type = typeof(Platypus);
                    break;

                case AnimalType.Shark:
                    type = typeof(Shark);
                    break;

                case AnimalType.Squirrel:
                    type = typeof(Squirrel);
                    break;
            }

            return type;
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public virtual void Eat(Food food)
        {
            this.EatBehavior.Eat(this, food);
            this.HungerState = HungerState.Satisfied;
            this.hungerTimer.Stop();
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            this.isPregnant = true;

            if (this.OnTextChange != null)
            {
                this.OnTextChange(this);
            }

            if (this.OnPregnant != null)
            {
                this.OnPregnant(this);
            }

            this.MoveBehavior = new NoMoveBehavior();
        }

        /// <summary>
        /// Moves about.
        /// </summary>
        public void Move()
        {
            this.MoveBehavior.Move(this);

            if (this.OnImageUpdate != null)
            {
                this.OnImageUpdate(this);
            }
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer Reproduce()
        {
            // Make mother animal to be no longer pregnant.
            this.isPregnant = false;

            // [11 (E)] In the animal's Reproduce method, pass in a gender in the CreateInstance method as the last parameter. Use a ternary operator and the animal's random to set the gender randomly to either male or female.
            // [11 (C)] Reproduce behavior implementation > Move the code to GiveBirthBehavior.Reproduce (except for setting the code above), invoke the Reproduce on the reproduce behavior
            return this.BirthBehavior.Reproduce(this);
        }

        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            string animalString;

            animalString = this.Name + ": " + this.GetType().Name + " (" + this.Age + ", " + this.Weight;

            // this.isPregnant == true ? animalString += ", P)" : animalString += ")";
            // Again, not sure what the problem is with this. As far as i know the ternary above says the same thing as the if/else below, yet it doesn't work.
            if (this.IsPregnant)
            {
                animalString += ", P)";
            }
            else
            {
                animalString += ")";
            }

            return animalString;
        }
        
        public void AddChild(Animal animal)
        {
            this.children.Add(animal);
        }

        /// <summary>
        /// Handles the movement of the animal.
        /// </summary>
        /// <param name="sender">Object which sent the request.</param>
        /// <param name="e">Time elapsed event.</param>
        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
#if DEBUG            
            //this.moveTimer.Stop();
#endif
            this.Move();
#if DEBUG   
            //this.moveTimer.Start();
#endif
        }
        
        private void CreateTimers()
        {
            this.moveTimer = new Timer(1000);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();

            this.hungerTimer = new Timer(random.Next(10000, 20001));
            this.hungerTimer.Elapsed += this.HandleHungerStateChange;
            this.hungerTimer.Start();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        private void HandleHungerStateChange(object sender, ElapsedEventArgs e)
        {
            switch(this.HungerState)
            {
                case HungerState.Satisfied:
                    this.HungerState = HungerState.Hungry;
                    break;

                case HungerState.Hungry:
                    this.HungerState = HungerState.Starving;
                    break;

                case HungerState.Starving:
                    this.HungerState = HungerState.Unconscious;
                    break;

                case HungerState.Unconscious:
                    if (this.OnHunger != null)
                    {
                        this.OnHunger();
                    }
                    
                    break;
            }
        }
    }
}