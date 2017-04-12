# MetaVertex.DataModel.Db2

Create strongly-typed data models from data returned in `DbDataReader` instances.

## Intent

This package is most useful for:

* Databases which don't support Entity Framework.
* Calling existing Stored Procedures.

Additional specific functionality coming soon for [`IBM.Data.DB2.iSeries.iDB2DataReader`](https://www.nuget.org/packages/IBM.Data.DB2.iSeries/).

## Usage

A simple example of using this to return `MyDataModel` instances:

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
      <PackageReference Include="MetaVertex.DataModel" Version="0.2.0-a008" />
    </ItemGroup>
