var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.CloudLearn_Api>("apiservice");

builder.AddProject<Projects.CloudLearn>("web-frontend")
    .WithReference(apiservice);



builder.Build().Run();
