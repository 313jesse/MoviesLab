using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Movies.Models;
using System.Collections.Generic;

namespace Movies.Controllers
{
    public class ValuesController : ApiController
    {
        //action to get info about all customers
        public List<MovieList> GetAllMovies()
        {
            //create the ORM
            MovieListEntities ORM = new MovieListEntities();

            // get all customers form ORM
            List<MovieList> MoviesList = ORM.MovieLists.ToList();

            //Return the list of customers
            return MoviesList;
        }

        //#2 - get a list of movies in a specific category
        [HttpGet]
        public List<MovieList> GetMoviesByCategory(string category)
        {
            MovieListEntities ORM = new MovieListEntities();

            return ORM.MovieLists.Where(x => x.Category.ToLower() == category.ToLower()).ToList();
        }

        //#3 get a rando movie.
        [HttpGet]
        public MovieList GetRandomMovie()
        {
            MovieListEntities ORM = new MovieListEntities();
            List<MovieList> randoList = ORM.MovieLists.ToList();
            Random r = new Random();
            int randomMovie = r.Next(0, randoList.Count);

            return randoList[randomMovie];
        }

        private MovieList _Randomizer(List<MovieList> ListOfMovies)
        {
            Random r = new Random();
            int randomMovie = r.Next(0, ListOfMovies.Count);

            return ListOfMovies[randomMovie];
        }

        //#4 get a rando movie pic from specific category

        public MovieList GetRandomTitleByCat(string category)
        {
            MovieListEntities ORM = new MovieListEntities();

            Random r = new Random();
            int newrnd = r.Next(ORM.MovieLists.Where(x => x.Category == category).Count());

            List<MovieList> movie = ORM.MovieLists.Where(x => x.Category == category).ToList();
            return movie[newrnd].Title;

        }

        //#5 get a list of rando titles where user specifies the num of movies
        public List<MovieList> GetRandomMovies(int num)
        {
            MovieListEntities ORM = new MovieListEntities();
            Random r = new Random();
            List<MovieList> movies = ORM.MovieLists.ToList();
            List<MovieList> movielist = new List<MovieList>();

            if (num > movies.Count())
                for (int i = 0; i < num; i++)
                {
                    int nrand = r.Next(movies.Count());
                    movielist.Add(movies[nrand]);
                    movies.Remove(movies[nrand]);
                }
            return movielist;
        }

        //#6 get a list of all movie categories
        public List<string> GetMovieCategories()
        {
            MovieListEntities ORM = new MovieListEntities();
            return ORM.MovieLists.Where(y => y.Category != null).Select(x => x.Category).Distinct().ToList();
        }

        //#7 get info about a specific movie - user specifies title as a query param
        public string GetMovieDescription(string movietitle)
        {
            MovieListEntities ORM = new MovieListEntities();
            List<MovieList> movie = ORM.MovieLists.Where(x => x.Title.ToLower() == movietitle.ToLower()).ToList();
            return movie[0].Description;
        }
        //#8 get a list of movies which have a keyword in their title - user specifies title as a query param. (use where)

        public List<MovieList> GetMoviesByKeyword(string search)
        {
            MovieListEntities ORM = new MovieListEntities();
            return ORM.MovieLists.Where(x => x.Title.ToString().Contains(search)).ToList();
        }

    }
}
