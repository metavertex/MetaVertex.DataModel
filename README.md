# MetaVertex.DataModel

This library assists with using strongly-typed POCO objects against "traditional" ADO.NET objects
such as `DbDataReader`, `DbParameter`, `DbCommand`, etc. It is a work in progress but the basic
functionality is in place.

It makes extensive use of C# attributes to mark up POCO types and properties to create and read
strongly-typed models.

## Use Cases

This package is most useful in these scenarios:

* Using a database provider which doesn't support Entity Framework.
* Working with an existing database or with lots of stored procedures.
* Working with a database that doesn't make good use of strong types (e.g. it uses strings for date
  input parameters) and therefore needs custom casting / parsing etc.

The main `MetaVertex.DataModel` library contains provider-agnostic code. It targets **.NET Standard 1.4**.

The `MetaVertex.DataModel.Db2` library contains logic specific to [`IBM.Data.DB2.iSeries`](https://www.nuget.org/packages/IBM.Data.DB2.iSeries/).
It is written in **.NET 4.6** since the IBM library does not support .NET Standard.

## Usage

Better docs coming soon. For now, check out the unit tests and code comments. There are two general
types of POCO models supported:

1. "Result Models" which allow converting a `DbDataReader` based instance into a collection of POCO models.
2. "Query Models" which allow converting a POCO model into a collection of `DbParameter` based parameters.
   Support for converting output parameters back into a POCO model is planned soon.

A simple example of returning `MyDataModel` result models from a `DbCommand.ExecuteReader()` call:

```csharp
    public class MyDataModel
    {
        [DataField("NAME")]
        public string Name { get; set; }

        [DataField("START_DATE")]
        public DateTime StartDate { get; set; }

        [DataField("ITEM_COUNT")]
        public int ItemCount { get; set; }

        [DataField("ITEM_COST")]
        public decimal Cost { get; set; }

        public string NonMappedValue { get; set; }
    }
```

Then in your data access layer:

```csharp
    public IEnumerable<MyDataModel> GetDataModels(DbCommand cmd)
    {
        using (var reader = cmd.ExecuteReader())
        {
            var info = reader.GetModelInfo<MyDataModel>();
            while (reader.Read())
            {
                yield return info.GetModel();
            }
        }
    }
```

## Installation

Install from [the NuGet package](https://www.nuget.org/packages/MetaVertex.DataModel) using Visual Studio or PowerShell:

    Install-Package MetaVertex.DataModel -Pre

Or, add as a `PackageReference` to your .NET Core `csproj` file:

    <ItemGroup>
      <PackageReference Include="MetaVertex.DataModel" Version="0.2.0-a022" />
    </ItemGroup>
