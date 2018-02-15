using System.Collections.Generic;

namespace CodingExercise.Model
{
    public class FactsModel
    {
        public string title { get; set; }
        public List<Row> rows { get; set; }
    }
    
    public class Row
    {
        public string title { get; set; }
        public string description { get; set; }
        public string imageHref { get; set; }
    }
}
