using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Transfer8Pro.WebApp.Models
{
    public class JsTreeEntity
    {
        public JsTreeEntity()
        {
            children = new List<JsTreeEntity>();
        }

        public string id { get; set; }

        public string text { get; set; }

        public string pid { get; set; }

        public List<JsTreeEntity> children { get; set; }

        private State _state;
        public State state
        {
            get
            {
                if (_state == null)
                {
                    _state = new State();
                }
                return _state;
            }
            set { _state = value; }
        }
    }

    public class State
    {
        private bool _opened = false;
        public bool opened
        {
            get { return _opened; }
            set { _opened = value; }
        }

        private bool _selected = false;
        public bool selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public bool disabled { get; set; }
    }
}