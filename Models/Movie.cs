using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MovieControllerApi;

public class Movie
{
    public int Id {get; set;}
    public required string Title {get; set;}

    public required string Format {get; set;}

    public required string Genre {get; set;}

    public required DateTime CreatedAt {get; set;}

    [NotMapped]
    public string? PosterPath {get; set;}

}
