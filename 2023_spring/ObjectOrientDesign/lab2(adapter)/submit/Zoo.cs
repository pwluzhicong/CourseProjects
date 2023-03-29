using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab2
{
    public static class ObjectID<T>
    {
        public static List<T> list = new List<T>();
    }

    public interface IWithID
    {
        int id { get; set; }
    }

    public interface IEnclosureHashmap
    {
        public long name { get; }
        public List<long> animals { get; }
        public long employee { get; }
    }

    public interface IAnimalHashmap
    {
        public long name { get; }
        public long age { get; }
        public long species { get; }
    }

    public interface ISpeciesHashmap
    {
        public long name { get; }
        public List<long> favouriteFoods { get; }
    }

    public interface IEmployeeHashmap
    {
        public long name { get; }
        public long surname { get; }
        public long age { get; }
        public List<long> enclosures { get; }
    }

    public interface IVisitorHashmap
    {
        public long name { get; }
        public long surname { get; }
        public List<long> visitedEnclosures { get; }
    }

    //public interface IEnclosureBase
    //{
    //    public string name { get; }
    //    public List<IAnimalBase> animals { get; }
    //    public IEmployeeBase employee { get; }
    //}

    //public interface ISpeciesBase
    //{
    //    public string name { get; }
    //    public List<ISpeciesBase> favouriteFoods { get; }
    //}

    //public interface IAnimalBase
    //{
    //    public string name { get; }
    //    public int age { get; }
    //    public ISpeciesBase species { get; }
    //}

    //public interface IEmployeeBase
    //{

    //    public string name { get; }
    //    public string surname { get; }
    //    public int age { get; }
    //    public List<IEnclosureBase> enclosures { get; }
    //}

    //public interface IVisitorBase
    //{
    //    public string name { get; }
    //    public string surname { get; }
    //    public List<IEnclosureBase> visitedEnclosures { get; }
    //}

    public class Enclosure: IWithID
    {
        public static Dictionary<string, Enclosure> dict = new Dictionary<string, Enclosure>();
        public int id { get; set; }
        //string name_;
        public string name { get; set; }
        public List<Animal> animals { get; set; }
        //public List<IAnimalBase> animals { get { return new List<IAnimalBase>(animals_.Cast<IAnimalBase>()); } }
        public Employee? employee { get; set; }
        //public IEmployeeBase employee
        //{
        //    get {
        //        if (employee_ == null) throw new Exception("Empty Employee");
        //        return employee_;
        //    }
        //}

        public Enclosure(string n, List<Animal>? a, Employee? e)
		{
            name = n;
            animals = new List<Animal>();
            if(a != null)
            {
                foreach (var animal in a)
                {
                    animals.Add(animal);
                }
            }

            employee = e;

            dict[name] = this;

            if(e is not null)
            {
                Employee.dict[e.name + " " + e.surname].enclosures.Add(this);
            }

            ObjectID<Enclosure>.list.Add(this);
            id = ObjectID<Enclosure>.list.Count - 1;
        }


        //public T toHashMap<T>() where T:HashmapAdapter<Enclosure> { return new T(this); }
        //public EnclosureHashmapAdapter toHashMap() { return new EnclosureHashmapAdapter(this); }
    }


    public class Animal : IWithID
    {
        public string name { get; set; }
        public int age { get; set; }
        public int id { get; set; }
        public Species species { get; set; }
        public Animal(string name, int age, Species species)
        {
            this.name = name;
            this.age = age;
            this.species = species;

            species.animal_list.Add(this);

            ObjectID<Animal>.list.Add(this);
            id = ObjectID<Animal>.list.Count - 1;
        }
    }



    public class Species : IWithID
    {
        public static Dictionary<string, Species> dict = new Dictionary<string, Species>();
        public string name { set; get; }
        public List<Species> favouriteFoods;
        public int id { get; set; }
        public List<Animal> animal_list;
        

        static Species()
        {
            dict = new Dictionary<string, Species>();


        }

        public Species(string name, List<Species>? favouriteFoods)
        {
            this.name = name;
            this.favouriteFoods = new List<Species>();
            if(favouriteFoods != null)
            {
                foreach(var food in favouriteFoods)
                {
                    this.favouriteFoods.Add(food);
                }
            }
            dict[name] = this;
            animal_list = new List<Animal>();

            ObjectID<Species>.list.Add(this);
            id = ObjectID<Species>.list.Count - 1;
        }
    }



    public class Employee : IWithID
    {
        public static Dictionary<string, Employee> dict = new Dictionary<string, Employee>();
        public int id { get; set; }
        static Employee()
        {
            //new Employee("Ricardo", "Stallmano", 73, null);
            //new Employee("Steve", "Irvin", 43, null);
        }


        public string name { set; get; }
        public string surname { get; set; }
        public int age { get; set; }
        public List<Enclosure> enclosures { get; set; }

        //public List<IEnclosureBase> enclosures { get { return new List<IEnclosureBase>(enclosures_.ToList<IEnclosureBase>()); } }

        public Employee(string name, string surname, int age, List<Enclosure>? enclosures)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.enclosures = new List<Enclosure>();
            if(enclosures != null)
            {
                foreach(var e in enclosures)
                {
                    this.enclosures.Add(e);
                }
                
            }
            dict[name + " " + surname] = this;
            //Console.WriteLine("Add Empoyee: name" + " " + surname);

            ObjectID<Employee>.list.Add(this);
            id = ObjectID<Employee>.list.Count - 1;
        }
    }


    public class Visitor : IWithID
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public List<Enclosure> visitedEnclosures;
        //public List<IEnclosureBase> visitedEnclosures
        //{
        //    get
        //    {
        //        return new List<IEnclosureBase>(visitedEnclosures_.ToList<IEnclosureBase>());
        //    }
        //}

        public Visitor(string name, string surname, List<Enclosure>? visitedEnclosures)
        {
            this.name = name;
            this.surname = surname;
            this.visitedEnclosures = new List<Enclosure>();
            if(visitedEnclosures != null)
            {
                foreach(var v in visitedEnclosures)
                {
                    this.visitedEnclosures.Add(v);
                }
            }

            ObjectID<Visitor>.list.Add(this);
            id = ObjectID<Visitor>.list.Count - 1;
        }

    }

    
}

