using lab2;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//Species Meerkat = new Species("Meerkat", null);
//Meerkat.favouriteFoods.Add(Meerkat);

//Species Kakapo = new Species("Kakapo", null);

var species_names = new List<string>() { "Meerkat", "Kakapo", "Bengal Tiger", "Panda", "Python", "Dungeness Crab", "Gopher", "Cats", "Penguin" };
var dict = Species.dict;
foreach (var name in species_names)
{
    dict[name] = new Species(name, null);
}

dict["Meerkat"].favouriteFoods.Add(dict["Meerkat"]);

dict["Bengal Tiger"].favouriteFoods.Add(dict["Panda"]);
dict["Bengal Tiger"].favouriteFoods.Add(dict["Gopher"]);
dict["Bengal Tiger"].favouriteFoods.Add(dict["Cats"]);

dict["Python"].favouriteFoods.Add(dict["Panda"]);
dict["Python"].favouriteFoods.Add(dict["Bengal Tiger"]);

dict["Dungeness Crab"].favouriteFoods.Add(dict["Python"]);

dict["Cats"].favouriteFoods.Add(dict["Gopher"]);
dict["Penguin"].favouriteFoods.Add(dict["Bengal Tiger"]);



List<Animal> animals = new List<Animal>();

string s = @"1. Harold, 2, Meerkat
2. Ryan, 1, Meerkat
3. Jenkins, 15, Kakapo
4. Kaka, 10, Kakapo
5. Ada, 13, Bengal Tiger
6. Jett, 2, Panda
7. Conda, 4, Python
8. Samuel, 2, Python
9. Claire, 2, Dungeness Crab
10. Andy, 3, Gopher
11. Arrow, 5, Cats
12. Arch, 1, Penguin
13. Ubuntu, 1, Penguin
14. Fedora, 1, Penguin";

foreach(string line in s.Split('\n'))
{
    var fields = line.Split(", ");
    animals.Add(new Animal(fields[0].Split()[^1], int.Parse(fields[1]), Species.dict[fields[2]]));
}

animals.Add(new Animal("Harold", 2, Species.dict["Meerkat"]));

var _ = new Employee("Ricardo", "Stallmano", 73, null);
_ = new Employee("Steve", "Irvin", 43, null);

new Enclosure("311", null, Employee.dict["Ricardo Stallmano"]);
new Enclosure("Break", null, Employee.dict["Steve Irvin"]);
new Enclosure("Jurasic Park", null, Employee.dict["Steve Irvin"]);


species_names = new List<string>() { "Penguin", "Python", "Panda" };
foreach (var species_name in species_names)
{
    Enclosure.dict["311"].animals.AddRange(Species.dict[species_name].animal_list);
}


species_names = new List<string>() { "Cats", "Gopher", "Meerkat"};
foreach (var species_name in species_names)
{
    Enclosure.dict["Break"].animals.AddRange(Species.dict[species_name].animal_list);
}

species_names = new List<string>() { "Kakapo", "Bengal Tiger", "Dungeness Crab" };
foreach (var species_name in species_names)
{
    Enclosure.dict["Jurasic Park"].animals.AddRange(Species.dict[species_name].animal_list);
}


//FieldHashMap map = new FieldHashMap();

//EnclosureHashmapAdapter enclosure_hashmap = new EnclosureHashmapAdapter(Enclosure.dict["311"]);
//Console.WriteLine(enclosure_hashmap);
//Console.WriteLine(enclosure_hashmap.animals_.Count);
//Console.WriteLine(enclosure_hashmap.animals.Count);

IRep8TupleStack<Enclosure> e = new Rep8TupleStackAdapter<Enclosure>(Enclosure.dict["311"]);
var (id, stack) = e.content;
Console.WriteLine(id);
Console.WriteLine(stack);
int i = 0;
foreach(string ss in stack)
{
    
    Console.WriteLine($"{i}, {ss}");
    ++i;
}

Solver solver = new Solver();

List<IRep8TupleStack<Enclosure>> l = new List<IRep8TupleStack<Enclosure>>();

foreach (var enc in ObjectID<Enclosure>.list)
{
    l.Add(new Rep8TupleStackAdapter<Enclosure>(enc));
}
Console.WriteLine($"From Main: Call solver for Rep 8");
solver.solve(l);


//solver.solve(new List<IRep8TupleStack<Enclosure>>() { e });
//Console.WriteLine("Begin");
Console.WriteLine($"From Main: Call solver for Rep base");
solver.solve(ObjectID<Enclosure>.list);


