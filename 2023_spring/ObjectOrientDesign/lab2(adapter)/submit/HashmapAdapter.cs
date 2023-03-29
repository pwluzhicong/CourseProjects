using System;
namespace lab2
{

    public abstract class HashmapAdapter<T>
    {
        protected readonly T imp_;
        public HashmapAdapter(T imp)
        {
            imp_ = imp;
        }

        public override string ToString()
        {
            Type t = GetType();

            string result = "";
            result += "{";
            foreach (var p in t.GetProperties())
            {
                var oType = p.PropertyType;
                string pname = p.Name;
                bool isList = oType.Equals(typeof(List<long>));
                Console.WriteLine($"isList: {isList}");
                if (isList)
                {
                    var lst = p.GetValue(this);
                    if(lst != null)
                    {
                        result += $"{pname}:";
                        foreach (var v in (List<long>)lst)
                        {
                            result += $"{v};";
                        }
                    }
                    result += ",";

                }
                else
                {
                    result += $"{pname}:{p.GetValue(this)},";
                }
               
            }
            result += "}";
            return result;
        }
    }

    public class EnclosureHashmapAdapter: HashmapAdapter<Enclosure>, IEnclosureHashmap
    {
        //private readonly Enclosure e_;
        //private readonly FieldHashMap map_;

        public List<long>? animals_;
        public long name
        {
            get { return FieldHashMap.getKey(imp_.name); }
        }
        public List<long> animals
        {
            get
            {
                if (animals_ == null)
                {
                    animals_ = new List<long>();
                    foreach (Animal a in imp_.animals)
                    {
                        animals_.Add(FieldHashMap.getKey(a.name));
                    }
                }
                return animals_;
            }
        }
        public long employee
        {
            get
            {
                if(imp_.employee != null)
                {
                    return FieldHashMap.getKey(imp_.employee.name);
                }
                else
                {
                    return -1;
                }
                
            }
        }

        public EnclosureHashmapAdapter(Enclosure e) : base(e) { }

        //public override string ToString()
        //{
        //    return $"name:{name}, animals:{animals}, employee:{employee}";
        //}
    }
    public class SpeciesHashMapAdapter: HashmapAdapter<Species>, ISpeciesHashmap
    {
        //private Species s_;
        //private FieldHashMap map_;
        private List<long>? favouriteFoods_;
        public long name { get { return FieldHashMap.getKey(imp_.name); } }

        public List<long> favouriteFoods
        {
            get
            {
                if (favouriteFoods_ == null)
                {
                    favouriteFoods_ = new List<long>();
                    foreach (var food in imp_.favouriteFoods)
                    {
                        favouriteFoods_.Add(FieldHashMap.getKey(food.name));
                        //favouriteFoods_.Add(new SpeciesHashMapAdapter(food, map_).name);
                    }
                }
                return favouriteFoods_;
            }
        }
        public SpeciesHashMapAdapter(Species s) : base(s) { }

    }
    public class EmployeeHashmapAdapter: HashmapAdapter<Employee>, IEmployeeHashmap
    {
        List<long>? enclosures_;
        public long name { get { return FieldHashMap.getKey(imp_.name); } }
        public long surname { get { return FieldHashMap.getKey(imp_.surname); } }
        public long age { get { return FieldHashMap.getKey(imp_.age.ToString()); } }
        public List<long> enclosures
        {
            get
            {
                if (enclosures_ == null)
                {
                    enclosures_ = new List<long>();
                    foreach (Enclosure e in imp_.enclosures)
                    {

                        enclosures_.Add(FieldHashMap.getKey(e.name));
                    }
                }

                return enclosures_;
            }
        }

        public EmployeeHashmapAdapter(Employee e) : base(e) { }

    }
    public class VisitorHashMapAdapter: HashmapAdapter<Visitor>, IVisitorHashmap
    {
        //FieldHashMap map_;
        List<long>? visitedEnclosures_;


        public long name { get { return FieldHashMap.getKey(imp_.name); } }
        public long surname { get { return FieldHashMap.getKey(imp_.surname); } }
        public List<long> visitedEnclosures
        {
            get
            {
                if (visitedEnclosures_ == null)
                {
                    visitedEnclosures_ = new List<long>();
                    foreach (var e in imp_.visitedEnclosures)
                    {
                        visitedEnclosures_.Add(FieldHashMap.getKey(e.name));
                    }
                }
                return visitedEnclosures_;
            }
        }

        public VisitorHashMapAdapter(Visitor v) : base(v) { }

    }

}

