using System.Collections.Generic;
namespace chartjs
{
    
    public partial class padding
    {
        public int left { get; set; }
        public int right { get; set; }
        public int top { get; set; }
        public int bottom { get; set; }

    }
    public partial class tooltips
    {
        /*
        point
        nearest
        dataset
        index
        x
        y
         */
        public string mode { get; set; }
        /*
        x
        y
         */
        public string axis { get; set; }
    }

    public partial class scales
    {
        public Axes yAxes { get; set; }
        public Axes xAxes { get; set; }
    }

    public partial class Axes
    {
        public ticks ticks { get; set; }
    }
    public partial class ticks
    {
        public bool beginAtZero { get; set; }
    }

}