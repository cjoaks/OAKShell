using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine; 
using OAKShell.Interfaces;
using OAKShell.Services;
using OAKShell.Commands;
using OAKShell.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IFileSystemOperationService, FileSystemOperationService>();
builder.Services.AddSingleton<RemoveFilesCommand>(); 
builder.Logging.AddConsole(); 
using var host = builder.Build();

var root = new RootCommand();
root.AddOAKShellCommand<RemoveFilesCommand>(host); 
