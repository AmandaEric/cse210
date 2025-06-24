using System;
class Program
{
    static void Main(string[] args)
    {

    }
}
class Assignment
{
    // Create a constructor for this class that 
    // receives a student name and topic and sets the member variables
    private string _studentName = "";
    private string _topic = "";
    public string GetStudent()
    {
        return _studentName;
    }
    public void SetStudent(string student)
    {
        _studentName = student;
    }
    public string GetTopic()
    {
        return _topic;
    }
    public void SetTopic(string topic)
    {
        _topic = topic;
    }

    public string GetAssignmentInfo()
    {
        return $"{_studentName} in {_topic}";
    }
}
class MathAssignment
{
    string _textbookSection;
    string _problems;

    public string GetSection()
    {
        return _textbookSection;
    }
    public void SetSection(string textbookSection)
    {
        _textbookSection = textbookSection;
    }
    public string GetProblems()
    {
        return _problems;
    }
    public void SetProblems(string problems)
    {
        _problems = problems;
    }

    public string GetAssignmentInfo()
    {
        return $"{_studentName} in {_topic}";
    }


}
class WritingAssignment
{
    string _title;
}