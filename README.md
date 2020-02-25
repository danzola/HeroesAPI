# Heroes API

Run the following commands to verify that EF Core CLI tools are correctly installed:
* dotnet restore
* dotnet ef

dotnet ef dbcontext scaffold "Server=xxx.database.windows.net;Database=xxx;user=xxx;pwd=xxx" Microsoft.EntityFrameworkCore.SqlServer -o Models
