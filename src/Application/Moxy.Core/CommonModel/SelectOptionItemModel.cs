using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Core
{
    public class SelectOptionItemModel
    {
        public SelectOptionItemModel() { }
        public SelectOptionItemModel(string text, object value)
        {
            Text = text;
            Value = value.ToString();

        }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
