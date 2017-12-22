using System;
using System.Collections.Generic;
using System.Text;

namespace Website.Models
{
    public class MsgInfo
    {
        public int msgid { get; set; }

        public String mobile { get; set; }

        public String msgcontent { get; set; }

        public String msgip { get; set; }

        public short sendstatus { get; set; }

        public String sendtime { get; set; }

        public int sourceid { get; set; }

        public String createtime { get; set; }

        public short isactive { get; set; }
    }
}
