using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleApp1;

public class FileManager
{
    
    public void SaveToFile(string filename, Board b)
    {
        var shapes = b.GetShapes();
        var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true};
        string jsonString = JsonSerializer.Serialize(shapes, options);
        
        File.WriteAllText(filename, jsonString);
        Console.WriteLine($"Saved successfully! Path {Path.GetFullPath(filename)}");
    }

    public void LoadFromFile(string filename, Board b)
    {
        if (File.Exists(filename))
        {
            try
            {
                string jsonString = File.ReadAllText(filename);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var shapes = JsonSerializer.Deserialize<List<Shape>>(jsonString) ?? new List<Shape>();
                b.SetShape(shapes);
                Console.WriteLine("Loaded successfully!");
            }
            catch (Exception)
            {
                Console.WriteLine("error! invalid file!");
            }
            
        }
        else
        {
            Console.WriteLine("File doesnt exist!");
        }
    }

    public void SaveCmd(string[] parts, Board b)
    {
        string filename = parts.Length > 1 ? parts[1] : "shapes.json";
        FileManager fileManager = new FileManager();
        fileManager.SaveToFile(filename, b);
    }

    public void LoadCmd(string[] parts, Board b)
    {
        string filename = parts.Length > 1 ? parts[1] : "shapes.json";
        FileManager fileManager = new FileManager();
        fileManager.LoadFromFile(filename, b);
        b.ClearSelectedId();
    }
}