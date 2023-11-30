using System;

public class Assignment
{
    private string _studentName;
    private string _topic;

    public Assignment(string studentName, string topic)
    {
        _studentName = studentName;
        _topic = topic;
    }

    public string GetStudentName()
    {
        return _studentName;
    }

    public string GetTopic()
    {
        return _topic;
    }

    public string GetSummary()
    {
        return $"{GetStudentName()} - {GetTopic()}";
    }
}

public class MathAssignment : Assignment
{
    private string _homeworkList;

    public MathAssignment(string studentName, string topic, string homeworkList) : base(studentName, topic)
    {
        _homeworkList = homeworkList;
    }

    public string GetHomeworkList()
    {
        return _homeworkList;
    }
}

public class WritingAssignment : Assignment
{
    private string _writingInformation;

    public WritingAssignment(string studentName, string topic, string writingInformation) : base(studentName, topic)
    {
        _writingInformation = writingInformation;
    }

    public string GetWritingInformation()
    {
        return _writingInformation;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Test base class
        Assignment assignment = new Assignment("Pablo Toloza", "Multiplication");
        Console.WriteLine(assignment.GetSummary());

        // Test MathAssignment class
        MathAssignment mathAssignment = new MathAssignment("Lionel Messi", "Fractions", "Section 7.3 Problems 8-19");
        Console.WriteLine(mathAssignment.GetSummary());
        Console.WriteLine(mathAssignment.GetHomeworkList());

        // Test WritingAssignment class
        WritingAssignment writingAssignment = new WritingAssignment("Ginés Gonzalez García", "World History", "The Causes of Covid-19");
        Console.WriteLine(writingAssignment.GetSummary());
        Console.WriteLine(writingAssignment.GetWritingInformation());
    }
}
