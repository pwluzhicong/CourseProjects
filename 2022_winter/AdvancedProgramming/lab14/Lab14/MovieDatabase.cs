using System.Globalization;

namespace p3a
{
	public class MovieDatabase
	{
		List<Movie> movies;

		List<(DateTime, string)> holidays = new List<(DateTime, string)>() {(DateTime.Parse("2012-01-02"),"New Year Day"),
																			(DateTime.Parse("2012-01-16"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2012-02-20"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2012-05-28"),"Memorial Day"),
																			(DateTime.Parse("2012-07-04"),"Independence Day"),
																			(DateTime.Parse("2012-09-03"),"Labor Day"),
																			(DateTime.Parse("2012-10-08"),"Columbus Day"),
																			(DateTime.Parse("2012-11-12"),"Veterans Day"),
																			(DateTime.Parse("2012-11-22"),"Thanksgiving Day"),
																			(DateTime.Parse("2012-12-25"),"Christmas Day"),
																			(DateTime.Parse("2013-01-01"),"New Year Day"),
																			(DateTime.Parse("2013-01-21"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2013-02-18"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2013-05-27"),"Memorial Day"),
																			(DateTime.Parse("2013-07-04"),"Independence Day"),
																			(DateTime.Parse("2013-09-02"),"Labor Day"),
																			(DateTime.Parse("2013-10-14"),"Columbus Day"),
																			(DateTime.Parse("2013-11-11"),"Veterans Day"),
																			(DateTime.Parse("2013-11-28"),"Thanksgiving Day"),
																			(DateTime.Parse("2013-12-25"),"Christmas Day"),
																			(DateTime.Parse("2014-01-01"),"New Year Day"),
																			(DateTime.Parse("2014-01-20"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2014-02-17"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2014-05-26"),"Memorial Day"),
																			(DateTime.Parse("2014-07-04"),"Independence Day"),
																			(DateTime.Parse("2014-09-01"),"Labor Day"),
																			(DateTime.Parse("2014-10-13"),"Columbus Day"),
																			(DateTime.Parse("2014-11-11"),"Veterans Day"),
																			(DateTime.Parse("2014-11-27"),"Thanksgiving Day"),
																			(DateTime.Parse("2014-12-25"),"Christmas Day"),
																			(DateTime.Parse("2015-01-01"),"New Year Day"),
																			(DateTime.Parse("2015-01-19"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2015-02-16"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2015-05-25"),"Memorial Day"),
																			(DateTime.Parse("2015-07-03"),"Independence Day"),
																			(DateTime.Parse("2015-09-07"),"Labor Day"),
																			(DateTime.Parse("2015-10-12"),"Columbus Day"),
																			(DateTime.Parse("2015-11-11"),"Veterans Day"),
																			(DateTime.Parse("2015-11-26"),"Thanksgiving Day"),
																			(DateTime.Parse("2015-12-25"),"Christmas Day"),
																			(DateTime.Parse("2016-01-01"),"New Year Day"),
																			(DateTime.Parse("2016-01-18"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2016-02-15"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2016-05-30"),"Memorial Day"),
																			(DateTime.Parse("2016-07-04"),"Independence Day"),
																			(DateTime.Parse("2016-09-05"),"Labor Day"),
																			(DateTime.Parse("2016-10-10"),"Columbus Day"),
																			(DateTime.Parse("2016-11-11"),"Veterans Day"),
																			(DateTime.Parse("2016-11-24"),"Thanksgiving Day"),
																			(DateTime.Parse("2016-12-25"),"Christmas Day"),
																			(DateTime.Parse("2017-01-02"),"New Year Day"),
																			(DateTime.Parse("2017-01-16"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2017-02-20"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2017-05-29"),"Memorial Day"),
																			(DateTime.Parse("2017-07-04"),"Independence Day"),
																			(DateTime.Parse("2017-09-04"),"Labor Day"),
																			(DateTime.Parse("2017-10-09"),"Columbus Day"),
																			(DateTime.Parse("2017-11-10"),"Veterans Day"),
																			(DateTime.Parse("2017-11-23"),"Thanksgiving Day"),
																			(DateTime.Parse("2017-12-25"),"Christmas Day"),
																			(DateTime.Parse("2018-01-01"),"New Year Day"),
																			(DateTime.Parse("2018-01-15"),"Martin Luther King Jr. Day"),
																			(DateTime.Parse("2018-02-19"),"Presidents Day (Washingtons Birthday)"),
																			(DateTime.Parse("2018-05-28"),"Memorial Day"),
																			(DateTime.Parse("2018-07-04"),"Independence Day"),
																			(DateTime.Parse("2018-09-03"),"Labor Day"),
																			(DateTime.Parse("2018-10-08"),"Columbus Day"),
																			(DateTime.Parse("2018-11-12"),"Veterans Day"),
																			(DateTime.Parse("2018-11-22"),"Thanksgiving Day"),
																			(DateTime.Parse("2018-12-25"),"Christmas Day"),
																			(DateTime.Parse("2019-01-01"),"New Year Day"),
																			(DateTime.Parse("2019-01-21"),"Martin Luther King Jr. Day") };

		public MovieDatabase()
		{
			LoadMovies();
		}

		private void LoadMovies()
		{
			string filePath = @"../../../movies.csv";

			if (!File.Exists(filePath))
			{
				Console.WriteLine("File movies.csv doesn't exist in project directory.");
				return;
			}

			movies = new List<Movie>();

			StreamReader reader = new StreamReader(filePath);
			string line;
			line = reader.ReadLine(); //skip header line
			while ((line = reader.ReadLine()) != null)
			{
				string[] splitLine = line.Split(';');
				Movie newMovie = new Movie();
				newMovie.title = splitLine[0];
				newMovie.budget = double.Parse(splitLine[1], CultureInfo.InvariantCulture);
				newMovie.genres = splitLine[2];
				newMovie.origin_language = splitLine[3];
				newMovie.release_date = DateTime.Parse(splitLine[4]);
				newMovie.revenue = double.Parse(splitLine[5], CultureInfo.InvariantCulture);
				newMovie.runtime = int.Parse(splitLine[6]);
				newMovie.vote_average = double.Parse(splitLine[7], CultureInfo.InvariantCulture);
				newMovie.vote_count = int.Parse(splitLine[8]);

				movies.Add(newMovie);
			}

			reader.Close();
		}

        //solutions for this stage MUST be written with Query Expressions
        public void StageA() // 1.0 Point
		{
			//find and print titles of all movies released in 2006 (release_date),
			//with budget greater than 100 millions
			Console.WriteLine("A1: ");

			var res1 = from movie in movies
					   where movie.release_date.Year == 2006 && movie.budget > 100000000
					   select movie.title;
			Console.Write(String.Join(", ", res1)+"\n");

            //find a movie (print its title) released in the previous century with the greatest revenue
            Console.WriteLine("A2: ");
            var res2 = (from movie in movies
                       where movie.release_date.Year < 2000 && movie.release_date.Year >= 1900
					   orderby movie.revenue descending
                       select movie).First();
            Console.Write(res2.title+ "\n");

            //find 5 comedies (print their titles) with the greatest average rate (vote_average)
            //which number of votes (votes_count) is greater than 5000
            Console.WriteLine("A3: ");
			var res3 = (from movie in movies
					   where movie.genres.Split(" | ").Contains("Comedy") && movie.vote_count > 5000
					   orderby movie.vote_average descending
					   select movie.title).Take(5);
            Console.Write(String.Join(", ", res3)+ "\n");
        }

		public void StageB() // 1.0 Point
		{
			//print average number of words in non-English movies' titles (origin_language != "en")
			Console.WriteLine("B1: ");
			var res4 = (from movie in movies
					   where movie.origin_language != "en"
					   select movie.title.Split().Count()).Average();
            Console.Write(res4 + "\n");
            //print average number of votes for movies with release date in 2016
            //but don't count 10 with the greatest and 10 with the lowest number of votes
            Console.WriteLine("B2: ");
			var res5 = (from movie in movies
						where movie.release_date.Year == 2016
						orderby movie.vote_count
						select movie.vote_count).Skip(10).SkipLast(10).Average();

            Console.Write(res5 + "\n");

        }

		public void StageC() // 1.0 Point
		{
			//find the day (date) with the most number of movies released
			//print which day it was and how many movies was released
			Console.WriteLine("C1: ");
			var res6 = (from movie in movies
						group movie by movie.release_date into g
						select new { D = g.Key, C = g.Count() } into g1
						orderby g1.C descending
						select g1).First();
			Console.Write($"{res6.D.Year}-{res6.D.Month}-{res6.D.Day} number of movies: {res6.C}\n");
					   
			//for which language (origin_language) excluding english (en) sum of movies budgets was the greatest
			//print this language and sum
			Console.WriteLine("C2: ");
			var res7 = (from movie in movies
						where movie.origin_language != "en"
						group movie.budget by movie.origin_language into g
						select new { Language = g.Key, Budget = g.Sum() } into g2
						orderby g2.Budget descending
						select g2).First();
			Console.Write($"{res7.Language}:{res7.Budget}\n");

		}

        //solutions for this stage MUST be written with Query Expressions
        public void StageD() // 1.0 Point
		{
			//using list of American holidays created at the beginning of this class find all movies
			//which release date was holiday day and title of the movie has at least one common word with holiday name (case insensitive)
			//the results print as pairs: movie title, holiday name
			Console.WriteLine("D1: ");
			var res8 = from movie in movies
					   from holiday in holidays
					   where movie.release_date == holiday.Item1 && movie.title.ToLower().Split(" ").Intersect(holiday.Item2.ToLower().Split(" ")).Count()>0
					   select $"({movie.title}, {holiday.Item2})";
			Console.Write(String.Join(", ", res8) + "\n");
						

			//find and print titles of all movies pairs which were released in the same day, have the same average vote,
			//and number of votes both of them are greater than 1000
			//all returned pairs have to be unique - print only (a,b), not (b,a)
			Console.WriteLine("D2: ");
			var res9 = from movie in movies
					   join movie2 in movies
					   on new { movie.release_date , movie.vote_average } equals new { movie2.release_date, movie2.vote_average }
					   where movie.title.CompareTo(movie2.title) < 0 && movie.vote_count>1000 && movie2.vote_count > 1000
                       select $"({movie.title}, {movie2.title})";
            Console.Write(String.Join(", ", res9) + "\n");


			//Console.Write("t\n");
        }


		public void StageE()
		{
			//find 5 the most frequent words (case insensitive) which has at least 4 letters in titles of movies
			//which were released after 2010
			//print those words and their frequency (number of titles)
			//Console.WriteLine("E1: ");
			//var res10 = from movie in movies
			//			where movie.release_date.Year >= 2010
			//			select movie.title.Split(" ") into t1
			//			select t1.ToList()


			var f = (int number) =>
			{
				if (number <= 1) return false;
				if (number == 2) return true;
				if (number % 2 == 0) return false;

				var boundary = (int)Math.Floor(Math.Sqrt(number));

				for (int i = 3; i <= boundary; i += 2)
					if (number % i == 0)
						return false;

				return true;
			};

            //find and print all prime numbers less than 100
            Console.WriteLine("E2: ");
			var res11 = from n in Enumerable.Range(1, 100)
						where f(n) is true
						select n;

			Console.Write(String.Join(", ", res11) + "\n");

        }

	}
}