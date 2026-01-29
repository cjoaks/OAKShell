using System.CommandLine;

namespace OAKShell.Interfaces;

public interface IOAKShellCommand
{
    string Verb { get; }
    string Description { get; }
    Command Build();
    Task Handler(ParseResult input); 
}
