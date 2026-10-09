using System.Globalization;
using System.Linq;
using ConsoleApp1;

Board b = new Board();
FileManager fileManager = new FileManager();
CommandHandler commandHandler = new CommandHandler(b);

while (true)
{
    Console.WriteLine("write a command:\ndraw list shapes add select remove edit paint move clear save load");
    string output = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(output)) {continue;}
    
    string[] parts = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (parts.Length == 0){continue;} 
    
    string cmd = parts[0].ToLower();
    
    switch (cmd)
    {
        case "draw":
            b.Draw();
            break;
        case "list":
            b.ListShapes();
            break;
        case "shapes":
            b.PrintAvailableShapes();
             break;
        case "add":
            commandHandler.AddShapeCmd(output);
            break;
        case "save":
            fileManager.SaveCmd(parts, b);
            break;
        case "load":
            fileManager.LoadCmd(parts, b);
            b.ClearSelectedId();
            break;
        case "clear":
            b.Clear();
            b.ClearSelectedId();
            break;
        case "remove": 
            commandHandler.RemoveCmd(parts);
                break;
        case "select":
            commandHandler.SelectCmd(parts);
            break;
        case "move":
            commandHandler.MoveCmd(parts);
            break;
        case "paint":
            commandHandler.PaintCms(parts);
            break;
        case "edit":
            commandHandler.EditCmd(parts);
            break;
    default:
            Console.WriteLine("uncorrect command!");
            break;
    }
}


