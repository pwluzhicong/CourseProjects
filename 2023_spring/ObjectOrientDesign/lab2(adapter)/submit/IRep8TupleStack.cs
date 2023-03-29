using System;
using System.Linq;
using System.Xml.Linq;

namespace lab2
{
	
	public interface IRep8TupleStack<T>
    {
		public (int, Stack<string>) content { get; }
	}

	public class Solver
	{

		public void solve(List<Enclosure> enclosures)
		{
            Console.WriteLine("Solve Task 2.6 for Rep base");
            List<IRep8TupleStack<Enclosure>> adapters = new List<IRep8TupleStack<Enclosure>>();
			foreach(var e in enclosures)
			{
				adapters.Add(new Rep8TupleStackAdapter<Enclosure>(e));
			}
            Console.WriteLine("From Solver-base: Call Rep 8 from Rep Base");
            solve(adapters);
		}


		public void solve(List<IRep8TupleStack<Enclosure>> enclosures)
		{
			Console.WriteLine("Solve Task 2.6 for Rep 8");
			foreach(var enclosure in enclosures)
			{
                var (id, s) = enclosure.content;

				bool readAnimals = false;
				List<int> ages = new List<int>();
				while (true)
				{
					string element = s.Pop();
					
                    if (element == "animals")
					{
						break;
						
					}
                }
				int count = int.Parse(s.Pop());
				List<Animal> animal_list = new List<Animal>();
				for(int i =0; i < count; ++i)
				{
					string element = s.Pop();
					var a = ObjectID<Animal>.list[int.Parse(element)];
					animal_list.Add(a);
                    ages.Add(a.age);

                }
				if(ages.Count>0 && ages.Average() < 3)
				{
					Console.Write($"Enclosure: {id}, Name:{ObjectID<Enclosure>.list[id].name}, Average_Age: {ages.Average()}, Animals:");
					foreach(Animal a in animal_list)
					{
						Console.Write($"{a.id},");
					}
                    Console.Write($"\n");

				}
				else
				{
                    //Console.WriteLine($"Enclosure: {id}, Name:{ObjectID<Enclosure>.list[id].name}, Average_Age: {ages.Average()} > 3, Invalid");
                }
     //           foreach (string element in s)
     //           {
     //               if (element == "animals")
					//{
					//	readAnimals = true;
     //               }

					//if (readAnimals)
					//{
					//	ages.Add(ObjectID<Animal>.list[int.Parse(element)].age);
					//}

					//if(readAnimals && )
     //       }
            }

		}
	}


	public class Rep8TupleStackAdapter<T>: IRep8TupleStack<T> where T: notnull, IWithID
    {
		public (int, Stack<string>) content { get { return (id_, s_); } }
        T imp_;
		//int id_ { get { return imp_.GetHashCode(); } }
		int id_ { get { return imp_.id; } }
		Stack<string> s_=new Stack<string>();

		public Rep8TupleStackAdapter(T imp)
		{
			imp_ = imp;
			Type t = imp.GetType();
            foreach (var p in t.GetProperties())
			{
				if(p == null)
				{
					throw new Exception("null Property");
				}
                var oType = p.PropertyType;
                string pname = p.Name;
				//bool isList = oType.Equals(typeof(List<long>));
				bool isList = oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>));

				//Console.WriteLine($"!isList:{isList}");
				if(isList)
				{
                    var lst = p.GetValue(imp);
					//Type elementType = oType.GetGenericArguments().Single();
					int count = 0;
                    if (lst != null)
					{
						foreach (var v in (IEnumerable<IWithID>)lst)
						{
							s_.Push(v.id.ToString());
							++count;
						}
					}
					s_.Push(count.ToString());
                }
				else
				{
					var v = p.GetValue(imp);

					//Console.WriteLine($"!IsValueType:{oType.IsValueType}");
					if (oType.GetInterfaces().Contains(typeof(IWithID))){
                        s_.Push(((IWithID)v).id.ToString());
					}
					else
					{
                        s_.Push(v.ToString());
                    }
     //               if (oType.IsValueType)
					//{
     //                   s_.Push(v.ToString());
					//}
					//else
					//{
     //                   s_.Push(((IWithID)v).id.ToString());
     //               }
					s_.Push("1");
				}
				s_.Push(p.Name);
            }

        }
    }
}

