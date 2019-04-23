using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transfer8Pro.Entity
{
    [Serializable]
    public class ResponseModel
    {
        public ResponseModel()
        {
            Status = 1;
        }

        public ResponseModel(string msg)
        {
            Status = -1;
            Msg = msg;
        }

        public ResponseModel(object data)
        {
            Status = 1;
            Data = data;
        }

        public ResponseModel(int status, string msg)
        {
            Status = status;
            Msg = msg;
        }

        public int Status { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }
    }
}
