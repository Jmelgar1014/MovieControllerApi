using System;

namespace MovieControllerApi.Models;

public class TmdbSearchResults
{
    public List<TmdbMovie> Results {get;set;} = new();

}
