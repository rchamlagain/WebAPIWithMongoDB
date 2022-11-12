namespace dotnetwithmongo.Models
{
    public class PostFilter
    {
        public int Limit { get; set; }= 10;
        public int Offset { get; set; } = 0;
        public string SearchKeyword { get; set; }
        public List<string> Tags { get; set; }
        public string CategoryName { get; set; }
    }
    
}
