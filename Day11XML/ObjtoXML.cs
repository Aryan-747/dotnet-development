using System;
using System.IO;
using System.Xml.Serialization;
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public List<int> mathmarks {get; set;}
    public int[] chemmarks {get; set;}
}

class ObjtoXML
{
    static void Main()
    {
        // Step 1: Create object
        Student student = new Student
        {
            Id = 101,
            Name = "Aryan",
            Age = 21,
            mathmarks = [51,39,49,59],
            chemmarks = [34,54,57,55],
        };

        // Step 2: Create XmlSerializer
        XmlSerializer serializer = new XmlSerializer(typeof(Student));

        // Step 3: Convert to XML
        using(FileStream fs = new FileStream("student.xml", FileMode.Create))
        {
            serializer.Serialize(fs, student);
        }
    }
}
