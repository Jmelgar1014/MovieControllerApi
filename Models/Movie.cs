using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using MovieControllerApi.Models;

namespace MovieControllerApi;

public class Movie
{
    public int Id {get; set;}
    public required string Title {get; set;}

    public List<FormatType> Format {get; set;} = new();

    public required string Genre {get; set;}

    public required DateTime CreatedAt {get; set;}

    public bool WatchedMovie {get; set;}

    public bool Favorites {get; set;}

    [NotMapped]
    public string? PosterPath {get; set;}

}
