using OAKShell.Interfaces;
using System.CommandLine;

namespace OAKShell.Commands;

public sealed class SAMDeployCommand(IProcessService processService) : IOAKShellCommand
{
    private readonly IProcessService _processService = processService;

    public string Verb => "deploy-sam";

    public string Description => "Package and deploy an AWS SAM app."; 

    /// <summary>
    /// Build the deploy-sam command.
    /// </summary>
    /// <returns></returns>
    public Command Build()
    {
        var deploySam = new Command(Verb, Description)
        {
            new Option<string>("--path", "-p")
            {
                Description = "Path to the directory of the SAM template."
            }, 
            new Option<string>("--template-file", "-tf")
            {
                Description = "SAM template file name."
            }, 
            new Option<string>("--config-file", "-cf")
            {
                Description = "Name of SAM configuration file."
            }, 
            new Option<string>("--deploy-environment", "-de")
            {
                Description = "Name of the deployment environment you want to use."
            }
        };
        deploySam.SetAction(Handler);
        return deploySam;
    }


    public async Task Handler(ParseResult input)
    {
        var templatePath = input.GetValue<string>("--path");
        var templateFile = input.GetValue<string>("--template-file") ?? "serverless.template";
        var configFile = input.GetValue<string>("--config-file") ?? "samconfig.toml";
        var deployEnvironment = input.GetValue<string>("--deploy-environment") ?? "default";

        string[] commandSequence = [
            $"build --template-file {templateFile}",
            $"deploy --config-file {configFile} --config-env {deployEnvironment}"
           ]; 
        foreach (var command in commandSequence)
        {
            var result = await _processService.ExecuteProcessAsync("sam", command, templatePath);
            if (result.ExitCode == 0)
                Console.WriteLine(result.StandardOutput); 
            else
            {
                Console.Error.WriteLine($"Failed to execute command {command}: {result.StandardError}");
                return;
            }
        }
        Console.WriteLine($"Successfully deployed SAM application."); 
    }
}
