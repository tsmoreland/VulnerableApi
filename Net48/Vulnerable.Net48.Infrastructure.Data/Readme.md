# Database migrations

In order to add another migration (explicit to allow versioning) the following steps are required

1. set automatic migrations to false 
2. run Update-Database (may need to drop existing first) 
3. only after the database is added with non-automatic migrations can we start modifying the DbContext settings
4. make necessary changes
5. run Add-Migration (name) having first set startup project to API (or whatever project has the IoC setup and 
   connection string details) set Default proejct in package manager console to Data


## Touble Shooting

Notes as they are disvoered...

- need to check swapping between autoamtic and non-automatic migrations, automatic is handy for this app because
we don't want long term data, but it'd be good to see how this is properly done anyway which is why I'm trying to
maintain actual migration snapshots