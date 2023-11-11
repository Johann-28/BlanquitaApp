@echo off
dotnet ef dbcontext scaffold "Server=localhost;Database=Tacos_Blanquita;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --context-dir .\Data --output-dir .\Data\BlanquitaModels --force --no-onconfiguring --no-pluralize
pause