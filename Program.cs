using System.Globalization;
using System.Linq;
using ConsoleApp1;

Board b = new Board();
FileManager fileManager = new FileManager();

while (true)
{
    Console.WriteLine("write a command:\ndraw list shapes add select remove edit paint move clear save load");
    string output = Console.ReadLine();
    
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
            b.AddShapeCmd(output);
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
                b.RemoveCmd(parts);
                break;
        case "select":
            b.SelectCmd(parts);
            break;
        case "move":
            b.MoveCmd(parts);
            break;
        case "paint":
            b.PaintCms(parts);
            break;
        case "edit":
            b.EditCmd(parts);
            break;
    default:
            Console.WriteLine("uncorrect command!");
            break;
    }
}


