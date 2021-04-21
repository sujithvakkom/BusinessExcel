using System;
using System.Collections.Generic;

namespace DataProvider.Entities
{
    public class Filter
    {
        public string brand { get; set; }
        public string name { get; set; }
        public bool view { get; set; }
    }

    public class Root
    {
        public string role { get; set; }
        public List<Filter> filters { get; set; }
    }

    public class RootObject
    {
        public Root root { get; set; }
    }
}