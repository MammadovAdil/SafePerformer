# SafePerformer

Library to help to perform any operations *safely*. 
Generally, this library is inteded to be used by Windows or other similar bakround running programs. The main purpose here is to not cancel all operations when first exception occurs. Instead, we log the prblem, try to connect to source (database, other service etc.) by the help of `IConnectionTester` and then try the operation again.

You can find **NuGet package** for this project [here](https://www.nuget.org/packages/Ma.SafePerformer/).
