using lib1;
using System.Collections.Generic;
// See https://aka.ms/new-console-template for more information
var garden = lib1.gardenReader.ReadGarden("..\\..\\..\\garden.xml");

if (garden != null)
{
    Dictionary <string, Library.color> color_dict = new Dictionary<string, Library.color>();
    foreach(var item in garden.Items)
    {
        if(item.GetType() == typeof(Library.color))
        {
            var x = (Library.color)item;
            color_dict[x.name] = x;
        }
    }

    foreach (var item in garden.Items)
    {
        if (item.GetType() == typeof(Library.insectType))
        {
            var x = (Library.insectType)item;
            var c = color_dict[x.color];
            Console.WriteLine(string.Format($"Insect: {x.latinName} of color RGB: {c.red}/{c.green}/{c.blue}"));
        }
    }


    //foreach(var book in lib_sample.book)
    //{
    //    foreach(var author_ref in book.author)
    //    {
    //        Console.WriteLine(author_dict[author_ref.@ref].surname.ToString());
    //    }
    //}

    //foreach(var a in lib_sample.get_authors())
    //{
    //    Console.WriteLine(a.surname.ToString());
    //}
}
