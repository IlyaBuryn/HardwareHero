
## References service
```yml
  ports:
    - "5016:443?"
    - "5017:80?"
```

The service is used to process 'directory' data - that is, data that will be changed very rarely and is mainly needed for information retrieval.

There are reference books like this:

#### Regions
```csharp
  public class Region
  {
    public Guid Id { get; set; }
    public string? Country { get; set; }
    public string? Code { get; set; }
    public string? City { get; set; }
  }
```

#### Currencies
```csharp
  public class Currency
  {
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Symbol { get; set; }
  }
```

For each individual directory, there is a controller to modify the data or retrieve specific data: `RegionController`, `CurrencyController`.

```http
  POST /api/<currency|region>
  PUT /api/<currency|region>
  GET /api/<currency|region>/every
```

But there is also a controller whose purpose is to output all directories somehow related to each other: `ReferencesController`.

```http
  GET /api/references/geo
```
It is needed for the client not to send several requests, but to get the necessary data at once. For example, the client has a window for changing user data, and we can transfer all data related to the user at once.

_The service is available via Api Gateway: replace `api/` with `gateway/`._