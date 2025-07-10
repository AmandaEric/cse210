using System;

class Program
{
    public class Video
    {
        public string _title;
        public string _author;
        public int _length;
        public List<Comment> _comments;

        public Video(string title, string author, int length)
        {
            _title = title;
            _author = author;
            _length = length;
            _comments = new List<Comment>();
        }
        public int GetCommentCount()
        {
            return _comments.Count;
        }
    }

    public class Comment
    {
        public string _commentText;
        public string _commenterName;
        public Comment(string commentername, string commentText)
        {
            _commenterName = commentername;
            _commentText = commentText;
        }


    }
static void Main(string[] args)
{
    List<Video> videos = new List<Video>();

    // First video
    Video video1 = new Video("Cool Video", "Amanda", 120);
    video1._comments.Add(new Comment("John", "Great video!"));
    video1._comments.Add(new Comment("Sara", "Very helpful."));
    video1._comments.Add(new Comment("Mike", "Loved it!"));
    videos.Add(video1);

    // Second video
    Video video2 = new Video("Super Pets", "Jamie", 690);
    video2._comments.Add(new Comment("Lilly", "Great video!"));
    video2._comments.Add(new Comment("Mary", "Very helpful."));
    video2._comments.Add(new Comment("Molly", "Loved it!"));
    videos.Add(video2);

    // Third video
    Video video3 = new Video("German Ballet of Swan Lake", "John Johnson", 1090);
    video3._comments.Add(new Comment("Sarah", "Thank you <3!"));
    video3._comments.Add(new Comment("Joseph", "Pretty."));
    video3._comments.Add(new Comment("Maizy", "Loved it!"));
    video3._comments.Add(new Comment("Daren", "Wow great dancing"));
    videos.Add(video3);

    // Print all video info
    foreach (Video video in videos)
    {
        Console.WriteLine($"\n'{video._title}' by {video._author}, is {video._length} seconds long and has {video.GetCommentCount()} comments:");
        foreach (Comment comment in video._comments)
        {
            Console.WriteLine($"{comment._commenterName} says \"{comment._commentText}\"");
        }
    }
}
 \


}