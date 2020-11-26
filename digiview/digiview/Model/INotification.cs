using System;
using System.Collections.Generic;
using System.Text;

namespace digiview.Model {
    interface INotification {
        void ShowNotification(Int32 id, String title, String msg);
    }
}
