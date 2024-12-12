using System;

namespace Tools {
    public abstract class Gui {
        public static void Alert(string m = "") {
            Dialog.Instance.ShowDialog(m);
        }

        public static void Alert(Action a, string m = "") {
            Dialog.Instance.ShowDialog(m, a);
        }

        // TODO constant with Action to update text on screen every t time
    }
}