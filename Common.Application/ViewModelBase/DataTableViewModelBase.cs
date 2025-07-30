using Microsoft.AspNetCore.Html;
using System.Collections.Generic;

namespace Common.Application.ViewModelBase
{

//{
//  "?draw":"2",
//  "columns":{
//    "0":{
//      "data":"",
//      "name":"",
//      "searchable":"true",
//      "orderable":"true",
//      "search":{"value":"","regex":"false"}
//    },
//    "1":{
//      "data":"title",
//      "name":"",
//      "searchable":"true",
//      "orderable":"true",
//      "search":{"value":"","regex":"false"}
//    },
//    "2":{
//      "data":"currentPrice",
//      "name":"",
//      "searchable":"true",
//      "orderable":"true",
//      "search":{"value":"","regex":"false"}
//    },
//    .......
//  },
//  "order":[{"column":"0","dir":"asc"}],
//  "start":"0",
//  "length":"10",
//  "search":{"value":"p","regex":"false"},
//  "_":"1598692400852"
//}
    public abstract class DataTableViewModelBase
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public int Draw { get; set; }
        public List<Column> Columns { get; set; }
        public Search Search { get; set; }
        public List<Order> Order { get; set; }
    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}
