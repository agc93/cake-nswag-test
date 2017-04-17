#addin nuget:?package=Cake.NSwag&prerelease

using NSwag.CodeGeneration.TypeScript;
///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
	// Executed BEFORE the first task.
	Information("Running tasks...");
});

Teardown(ctx =>
{
	// Executed AFTER the last task.
	Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Sample")
.Does(() => {
    CreateDirectory("./dist/sample");
    #break
    NSwag.FromSwaggerSpecification("./swagger.json")
        .ToCSharpClient("./client.cs", "Swagger.Client")
        .ToTypeScriptClient("./client.ts", s => s.WithClassName("Client").WithModuleName("Swagger"));
});

Task("Full-Settings")
.Does(() => {
    NSwag.FromSwaggerSpecification("./samples/swagger.json")
    .ToTypeScriptClient("./client.ts", s =>
        s.WithClassName("ApiClient")
            .WithModuleName("SwaggerApi")
            .WithSettings(new SwaggerToTypeScriptClientGeneratorSettings
            {
                PromiseType = PromiseType.Promise
            }));
});

RunTarget("Sample");