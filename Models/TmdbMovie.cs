using System;

namespace MovieControllerApi.Models;

public class TmdbMovie
{
    public int Id {get;set;}
    public string Title {get;set;} = "";

    public string PosterPath {get; set;} = "";
}
