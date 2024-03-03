# Develix.Essentials

This package provides a F#-like Result class.

## Result usage example

```cs
public void UseServiceData()
{
    var dataResult = GetWebServiceData();
    if(dataResult.Valid)
    {
        var data = dataResult.Value;
        // Do something with the data
    }
    else
    {
        var errorMessage = dataResult.Message;
        // Do something with the error message
    }
}

private Result<string> GetWebServiceData()
{
    try
    {
        var data = serviceClient.GetStuff();
        return Result.Ok(data);
    }
    catch(Exception ex)
    {
        var message = $"""
            Something went horribly wrong, see Exception for details:
            {ex}
            """;
        return Result.Fail<string>(message);
    }
}
```

## Changelog

### 1.1.2

* Add readme to nuget file

### 1.1.1

* Update to dotnet 8