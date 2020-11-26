using System;
using System.Collections.Generic;
using System.Text;

namespace digiview.Models.Sync {
    public class Push {

        public Push() {
        }

        public Int32 id {
            get; set;
        }
        public String title {
            get; set;
        }
        public String description {
            get; set;
        }
        public String user_id {
            get; set;
        }
        public String date_create {
            get; set;
        }
        public String send {
            get; set;
        }
        public String date_send {
            get; set;
        }

    }
}
