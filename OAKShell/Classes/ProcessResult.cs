using System;
using System.Collections.Generic;
using System.Text;

namespace OAKShell.Classes;

public class ProcessResult
{
    public int ExitCode { get; init; }
    public string? StandardOutput { get; init; }
    public string? StandardError { get; init; }
}
