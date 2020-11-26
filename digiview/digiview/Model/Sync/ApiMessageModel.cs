using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digiview.Models.Sync {
    public class ApiMessageModel<T> {

        public String language {
            get;
            set;
        }

        public String api_key {
            get;
            set;
        }

        public ApiMessageResultModel result{
            get; set;
        } 

        public T data {
            get;set;
        }
        
        public ApiMessageParamModel @params {
            get; set;
        }
    }
}
