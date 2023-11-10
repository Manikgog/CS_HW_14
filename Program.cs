using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace CS_HW_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XDocument moviesDoc = XDocument.Load("C:\\Users\\Maksim\\source\\repos\\CS_HW_14\\movies.xml");

            XElement Movies = moviesDoc.Element("movies");

            List<Movie> movieList = new List<Movie>();

            if(Movies != null)
            {
                foreach(XElement element in Movies.Elements("movie")) 
                {
                    string title = element.Element("title")?.Value;
                    string genre = element.Element("genre")?.Value;
                    int year = Convert.ToInt32(element.Element("year")?.Value);
                    float rating = float.Parse(element.Element("rating")?.Value, CultureInfo.InvariantCulture.NumberFormat);

                    movieList.Add(new Movie(title, genre, year, rating));

                }
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
            string sortByTitleString = "+++++++++++++++| Сортировка фильмов по названию |+++++++++++++++";
            Console.WriteLine(sortByTitleString);
            var sortByTitle  = movieList.OrderBy(movie => movie.Title);

            foreach(var m in sortByTitle)
            {
                Console.WriteLine(m.ToString());
            }

            List<string> jsonStrings = new List<string>();
            jsonStrings.Add(sortByTitleString);
            
            foreach (var movie in sortByTitle)
            {
                string outputJsonSortingByTitle = JsonConvert.SerializeObject(movie);
                jsonStrings.Add(outputJsonSortingByTitle);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////  
            string uniqueGenreString = "+++++++++++++++| Список уникальных жанров фильмов |+++++++++++++++";
            Console.WriteLine(uniqueGenreString);
            var uniqGenre = movieList.GroupBy(movie => movie.Genre).Where(genre => genre.Count() == 1).Select(genre => genre.Key);

            foreach (var m in uniqGenre)
            {
                Console.WriteLine(m.ToString());
            }
            jsonStrings.Add(uniqueGenreString);
            foreach (var movie in uniqGenre)
            {
                string outputJsonSortingByTitle = JsonConvert.SerializeObject(movie);
                jsonStrings.Add(outputJsonSortingByTitle);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string amountOfmoviesInFileString = ("+++++++++++++++| Количество фильмов в файле |+++++++++++++++");
            Console.WriteLine(amountOfmoviesInFileString);
            int countOfMovies = movieList.Count();
            Console.WriteLine(countOfMovies.ToString());

            jsonStrings.Add(amountOfmoviesInFileString);
            jsonStrings.Add(countOfMovies.ToString());

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string moviesWithMaxRating = "+++++++++++++++| Фильм с самыи высоким рейтингом |+++++++++++++++";
            Console.WriteLine(moviesWithMaxRating);
            float maxRatingMovie = movieList.Max(movie => movie.Rating);
            var listOfMaxRatingMovie = movieList.Where(movie => movie.Rating == maxRatingMovie).ToList();
            jsonStrings.Add(moviesWithMaxRating);
            foreach (var m in listOfMaxRatingMovie)
            {
                Console.WriteLine(m.Title + " " + m.Rating);
                jsonStrings.Add(m.Title + " " + m.Rating);
            }




            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string descendingSortMovies = "+++++++++++++++| Список фильмов, отсортированных по году выпуска, начиная с самых новых |+++++++++++++++";
            Console.WriteLine(descendingSortMovies);
            var listMoviesByYearOfRelease = movieList.OrderByDescending(movie => movie.Title).ToList();
            jsonStrings.Add(descendingSortMovies);
            foreach (var m in listMoviesByYearOfRelease)
            {
                Console.WriteLine(m.Title + " " + m.Year);
                jsonStrings.Add(m.Title + " " + m.Year);
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string last5YearsMoviesString = "+++++++++++++++| Фильмы, выпущенные в последние 5 лет, и выведите их названия и год выпуска |+++++++++++++++";
            Console.WriteLine(last5YearsMoviesString);
            int borderYear = DateTime.Now.Year - 5;

            //var last5YearsMovies = from movie in movieList where movie.Year > borderYear select movie;

            var last5YearsMovies = movieList.Where(movie => movie.Year > borderYear).Select(movie => movie).ToList();
            jsonStrings.Add(last5YearsMoviesString);
            foreach (var m in last5YearsMovies)
            {
                Console.WriteLine(m.Title + " " + m.Year);
                jsonStrings.Add(m.Title + " " + m.Year);
            }


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string averageRatingString = "+++++++++++++++| Cредний рейтинг всех фильмов |+++++++++++++++";
            Console.WriteLine(averageRatingString);
            float avgRating = movieList.Average(movie => movie.Rating);
            Console.WriteLine("Средний рейтинг всех фильмов -> " + avgRating);
            jsonStrings.Add(averageRatingString);
            jsonStrings.Add("Средний рейтинг всех фильмов -> " + avgRating);


            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string after2010Movies = "+++++++++++++++| Фильмы, которые были выпущены после 2010 года и имеют рейтинг выше 8 |+++++++++++++++";
            Console.WriteLine(after2010Movies);
            var after2010moviesWithRatingMore8 = movieList.Where(movie => movie.Year > 2010 && movie.Rating > 8).Select(movie => movie).ToList();
            jsonStrings.Add(after2010Movies);
            foreach (var m in after2010moviesWithRatingMore8)
            {
                Console.WriteLine(m.Title + " год выхода - " + m.Year + ", рейтинг - " + m.Rating);
                jsonStrings.Add(m.Title + " год выхода - " + m.Year + ", рейтинг - " + m.Rating);
            }

            System.IO.File.AppendAllLines("C:\\Users\\Maksim\\source\\repos\\CS_HW_14\\output.json", jsonStrings);

        }

        public static void PrintMovies(List<Movie> movieList)
        {
            foreach (Movie m in movieList)
            {
                Console.WriteLine(m);
            }
        }
    }
}
