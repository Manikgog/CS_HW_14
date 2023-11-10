using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_HW_14
{
    internal class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public float Rating { get; set; }

        public Movie(string title, string genre, int year, float rating)
        {
            Title=title;
            Genre=genre;
            Year=year;
            Rating=rating;
        }

        public override string ToString()
        {
            return "Название фильма: " + Title + ".\n" + "Жанр: " + Genre + ".\n"  + "Год выхода: " + Year + ".\n"  + "Рейтинг: " + Rating + ".\n";
        }
    }
}
