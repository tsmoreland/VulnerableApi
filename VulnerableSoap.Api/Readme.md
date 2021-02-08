## Database Setup

Run the following commands


```dotnet ef database update```

## Initial Setup

this adds the first migration, could be done in package manager as well with Add-Migration

```dotnet ef migrations add Initial --project VulnerableSoap.Data --startup-project VulnerableSoap.Api```


## Environment Variable

Use the following to set the protocol, address and port as seen externally from docker

```SoapSettings__Namespace```
