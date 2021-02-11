# VulnerableApi

## Description

Intentionally Vulnerable API provided using

- SoapCore for NET5 SOAP service
- .NET 4.8 ASP.NET Web service providing matching soap in the previous generation
- REST using NET5 ASP.NET Core 
- GraphQL 

Initial approach will not use authentication but that will come over time

## Vulnerabilitiies

- http protocol - no https provided, obvious but simple vulnerability
- SQL injection, several Get...ByName APIs are purposing building the SQL query from provided data allowing for simple vulnerability
